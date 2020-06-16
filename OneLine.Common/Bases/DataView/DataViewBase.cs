using FluentValidation;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public abstract partial class DataViewBase<T, TIdentifier, TId, THttpService> :
        IDataView<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public virtual async Task Load()
        {
            if (Identifier.IsNotNull() && Identifier.Model.IsNotNull())
            {
                Response = await HttpService.GetOne<T>(Identifier, new EmptyValidator());
                ResponseChanged?.Invoke(Response);
                if (Response.IsNotNull() && Response.HttpResponseMessage.IsSuccessStatusCode && 
                    Response.Succeed && Response.Response.Status.Succeeded())
                {
                    Record = Response.Response.Data;
                    RecordChanged?.Invoke(Record);
                }
            }
            else if (Identifiers.IsNotNull() && Identifiers.Any())
            {
                ResponseCollection = await HttpService.GetRange<T>(Identifiers, new EmptyValidator());
                ResponseCollectionChanged?.Invoke(ResponseCollection);
                if (ResponseCollection.IsNotNull() && ResponseCollection.HttpResponseMessage.IsSuccessStatusCode && 
                    ResponseCollection.Succeed && ResponseCollection.Response.Status.Succeeded())
                {
                    if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Replace)
                    {
                        Records.ReplaceRange(ResponseCollection.Response.Data);
                        RecordsFilteredSorted.ReplaceRange(Records);
                    }
                    else if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Add)
                    {
                        Records.AddRange(ResponseCollection.Response.Data);
                        RecordsFilteredSorted.AddRange(Records);
                    }
                    RecordsChanged?.Invoke(Records);
                    RecordsFilteredSortedChanged?.Invoke(RecordsFilteredSorted);
                }
            }
        }
        public virtual async Task Search()
        {
            ResponsePaged = await HttpService.Search<T>(SearchPaging, SearchExtraParams);
            ResponsePagedChanged?.Invoke(ResponsePaged);
            if (ResponsePaged.IsNotNull() && ResponsePaged.HttpResponseMessage.IsSuccessStatusCode && 
                ResponsePaged.Succeed && ResponsePaged.Response.Status.Succeeded())
            {
                if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Replace)
                {
                    Records.ReplaceRange(ResponsePaged.Response.Data.Data);
                    RecordsFilteredSorted.ReplaceRange(Records);
                }
                else if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Add)
                {
                    Records.AddRange(ResponsePaged.Response.Data.Data);
                    RecordsFilteredSorted.AddRange(Records);
                }
                RecordsChanged?.Invoke(Records);
                RecordsFilteredSortedChanged?.Invoke(RecordsFilteredSorted);
            }
            OnAfterSearch?.Invoke();
        }
        public virtual Task SelectRecord(T selectedRecord)
        {
            if (RecordsSelectionMode.IsSingle())
            {
                SelectedRecord = selectedRecord;
                SelectedRecordChanged?.Invoke(SelectedRecord);
            }
            else if (RecordsSelectionMode.IsMultiple())
            {
                if (SelectedRecords.Contains(selectedRecord))
                {
                    SelectedRecords.Remove(selectedRecord);
                }
                else if (MaximumRecordsSelections <= 0 || (MaximumRecordsSelections > 0 && SelectedRecords.Count < MaximumRecordsSelections))
                {
                    SelectedRecords.Add(selectedRecord);
                }
                MinimunRecordsSelectionsReached = SelectedRecords.Count >= MinimunRecordsSelections;
                MinimunRecordsSelectionsReachedChanged?.Invoke(MinimunRecordsSelectionsReached);
                MaximumRecordsSelectionsReached = SelectedRecords.Count >= MaximumRecordsSelections;
                MaximumRecordsSelectionsReachedChanged?.Invoke(MaximumRecordsSelectionsReached);
                SelectedRecordsChanged?.Invoke(SelectedRecords, MinimunRecordsSelectionsReached, MaximumRecordsSelectionsReached);
            }
            AfterSelectedRecord?.Invoke();
            return Task.CompletedTask;
        }
        public virtual Task FilterAndSort(string sortBy, bool descending)
        {
            FilterSortBy = sortBy;
            FilterSortByChanged?.Invoke(FilterSortBy);
            FilterDescending = descending;
            FilterDescendingChanged?.Invoke(FilterDescending);
            return FilterAndSort();
        }
        public virtual Task FilterAndSort(string sortBy, bool descending, Func<T, bool> filterPredicate)
        {
            FilterSortBy = sortBy;
            FilterSortByChanged?.Invoke(FilterSortBy);
            FilterDescending = descending;
            FilterDescendingChanged?.Invoke(FilterDescending);
            FilterPredicate = filterPredicate;
            FilterPredicateChanged?.Invoke(FilterPredicate);
            return FilterAndSort();
        }
        public virtual Task FilterAndSort()
        {
            if (Records.IsNotNull() && Records.Any())
            {
                IEnumerable<T> recordsFilteredSorted;
                if (FilterPredicate.IsNotNull())
                {
                    recordsFilteredSorted = Records.Where(FilterPredicate);
                }
                else
                {
                    recordsFilteredSorted = Records;
                }
                if (FilterSortBy.IsNotNull())
                {
                    if (FilterDescending)
                    {
                        recordsFilteredSorted = recordsFilteredSorted.OrderByPropertyDescending(FilterSortBy);
                    }
                    else
                    {
                        recordsFilteredSorted = recordsFilteredSorted.OrderByProperty(FilterSortBy);
                    }
                }
                //Creates a deep copy prevent deleting the original collection.
                RecordsFilteredSorted.ReplaceRange(recordsFilteredSorted.AutoMap<T, T>());
                RecordsFilteredSortedChanged?.Invoke(RecordsFilteredSorted);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoPreviousPage()
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoPreviousPage(int pageSize)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.PageSize = pageSize;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoPreviousPage(string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoPreviousPage(int pageSize, string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoNextPage()
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoNextPage(int pageSize)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.PageSize = pageSize;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoNextPage(string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoNextPage(int pageSize, string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoToPage(int pageIndex, int pageSize)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.PageSize = pageSize;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoToPage(int pageIndex, string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task GoToPage(int pageIndex, int pageSize, string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        public virtual Task Sort()
        {
            SearchPaging.Descending = !SearchPaging.Descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        public virtual Task Sort(bool descending)
        {
            SearchPaging.Descending = descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        public virtual Task SortAscending()
        {
            SearchPaging.Descending = false;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        public virtual Task SortDescending()
        {
            SearchPaging.Descending = true;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        public virtual Task SortBy(string sortBy)
        {
            if(SearchPaging.SortBy.Equals(sortBy))
            {
                SearchPaging.Descending = !SearchPaging.Descending;
            }
            else
            {
                SearchPaging.SortBy = sortBy;
            }
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        public virtual Task SortBy(string sortBy, bool descending)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        public virtual Task SortByAscending(string sortBy)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = false;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        public virtual Task SortByDescending(string sortBy)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = true;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
    }
}
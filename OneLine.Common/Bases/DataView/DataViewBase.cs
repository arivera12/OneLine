using FluentValidation;
using Microsoft.Extensions.Configuration;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public abstract class DataViewBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public virtual TIdentifier Identifier { get; set; }
        public virtual IEnumerable<TIdentifier> Identifiers { get; set; }
        public virtual T Record { get; set; }
        public virtual ObservableRangeCollection<T> Records { get; set; }
        public virtual object SearchExtraParams { get; set; }
        public virtual Func<T, bool> FilterPredicate { get; set; }
        public virtual string FilterSortBy { get; set; }
        public virtual bool FilterDescending { get; set; }
        public virtual ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        public virtual IResponseResult<IApiResponse<T>> Response { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnResponse { get; set; }
        public virtual IResponseResult<IApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollection { get; set; }
        public virtual IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>> ResponsePaged { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePaged { get; set; }
        public virtual THttpService HttpService { get; set; }
        public virtual IConfiguration Configuration { get; set; }
        public virtual ISearchPaging SearchPaging { get; set; }
        public virtual RecordsSelectionMode RecordsSelectionMode { get; set; }
        public virtual ObservableRangeCollection<T> SelectedRecords { get; set; }
        public virtual long MinimunRecordSelections { get; set; }
        public virtual long MaximumRecordSelections { get; set; }
        public virtual bool MinimunRecordSelectionsReached { get; set; }
        public virtual bool MaximumRecordSelectionsReached { get; set; }
        public virtual Action<Action> OnBeforeSearch { get; set; }
        public virtual Action OnAfterSearch { get; set; }
        public virtual Action<T> OnSelectedRecord { get; set; }
        public virtual Action<IEnumerable<T>, bool, bool> OnSelectedRecords { get; set; }
        public virtual Action<bool> OnMinimunRecordSelectionsReached { get; set; }
        public virtual Action<bool> OnMaximumRecordSelectionsReached { get; set; }
        public virtual CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        public DataViewBase()
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
        }
        public DataViewBase(object searchExtraParams)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            SearchExtraParams = searchExtraParams;
        }
        public DataViewBase(SearchPaging searchPaging)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = searchPaging;
        }
        public DataViewBase(SearchPaging searchPaging, object searchExtraParams)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = searchPaging;
            SearchExtraParams = searchExtraParams;
        }
        public DataViewBase(TIdentifier identifier)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            _ = new Action(async () => await Load());
        }
        public DataViewBase(IEnumerable<TIdentifier> identifiers)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
            _ = new Action(async () => await Load());
        }
        public DataViewBase(T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
        }
        public DataViewBase(IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.AddRange(records);
            RecordsFilteredSorted = new ObservableRangeCollection<T>();
            RecordsFilteredSorted.AddRange(records);
            HttpService = new THttpService();
            SearchPaging = new SearchPaging();
        }
        public virtual async Task Load()
        {
            if (Identifier != null && Identifier.Model != null)
            {
                Response = await HttpService.GetOne<T>(Identifier, new EmptyValidator());
                OnResponse?.Invoke(Response);
                if (Response.Succeed && Response.Response.Status.Succeeded())
                {
                    Record = Response.Response.Data;
                }
            }
            else if (Identifiers != null && Identifiers.Any())
            {
                ResponseCollection = await HttpService.GetRange<T>(Identifiers, new EmptyValidator());
                OnResponseCollection?.Invoke(ResponseCollection);
                if (ResponseCollection.Succeed && ResponseCollection.Response.Status.Succeeded())
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
                }
            }
        }
        public virtual async Task Search()
        {
            if(OnBeforeSearch == null)
            {
                await InternalSearch();
            }
            else
            {
                OnBeforeSearch?.Invoke(async () => await InternalSearch());
            }
        }
        private async Task InternalSearch()
        {
            ResponsePaged = await HttpService.Search<T>(SearchPaging, SearchExtraParams);
            OnResponsePaged?.Invoke(ResponsePaged);
            if (ResponsePaged.Succeed && ResponsePaged.Response.Status.Succeeded())
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
            }
            OnAfterSearch?.Invoke();
        }
        public virtual async Task PagingFilterChange(IPaging paging)
        {
            SearchPaging.AutoMap(paging);
            await Search();
        }
        public virtual Task SelectRecord(T selectedRecord)
        {
            if (RecordsSelectionMode.IsSingle())
            {
                Record = selectedRecord;
                OnSelectedRecord?.Invoke(selectedRecord);
            }
            else if (RecordsSelectionMode.IsMultiple())
            {
                if (SelectedRecords.Contains(selectedRecord))
                {
                    SelectedRecords.Remove(selectedRecord);
                }
                else if (MaximumRecordSelections <= 0 || (MaximumRecordSelections > 0 && SelectedRecords.Count < MaximumRecordSelections))
                {
                    SelectedRecords.Add(selectedRecord);
                }
                MinimunRecordSelectionsReached = SelectedRecords.Count >= MinimunRecordSelections;
                OnMinimunRecordSelectionsReached?.Invoke(MinimunRecordSelectionsReached);
                MaximumRecordSelectionsReached = SelectedRecords.Count >= MaximumRecordSelections;
                OnMaximumRecordSelectionsReached?.Invoke(MaximumRecordSelectionsReached);
                OnSelectedRecords?.Invoke(SelectedRecords, MinimunRecordSelectionsReached, MaximumRecordSelectionsReached);
            }
            return Task.CompletedTask;
        }
        public virtual Task FilterAndSort(string sortBy, bool descending)
        {
            FilterSortBy = sortBy;
            FilterDescending = descending;
            return FilterAndSort();
        }
        public virtual Task FilterAndSort(string sortBy, bool descending, Func<T, bool> filterPredicate)
        {
            FilterSortBy = sortBy;
            FilterDescending = descending;
            FilterPredicate = filterPredicate;
            return FilterAndSort();
        }
        public virtual Task FilterAndSort()
        {
            if (Records != null && Records.Any())
            {
                IEnumerable<T> recordsFilteredSorted;
                if (FilterPredicate != null)
                {
                    recordsFilteredSorted = Records.Where(FilterPredicate);
                }
                else
                {
                    recordsFilteredSorted = Records;
                }
                if (FilterSortBy != null)
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
            }
            return Task.CompletedTask;
        }
        public virtual async Task GoPreviousPage()
        {
            if(ResponsePaged != null  && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                await Search();
            }
        }
        public virtual async Task GoPreviousPage(int pageSize)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.PageSize = pageSize;
                await Search();
            }
        }
        public virtual async Task GoPreviousPage(string sortBy)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.SortBy = sortBy;
                await Search();
            }
        }
        public virtual async Task GoPreviousPage(int pageSize, string sortBy)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                await Search();
            }
        }
        public virtual async Task GoNextPage()
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                await Search();
            }
        }
        public virtual async Task GoNextPage(int pageSize)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.PageSize = pageSize;
                await Search();
            }
        }
        public virtual async Task GoNextPage(string sortBy)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.SortBy = sortBy;
                await Search();
            }
        }
        public virtual async Task GoNextPage(int pageSize, string sortBy)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                await Search();
            }
        }
        public virtual async Task GoToPage(int pageIndex, int pageSize)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.PageSize = pageSize;
                await Search();
            }
        }
        public virtual async Task GoToPage(int pageIndex, string sortBy)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.SortBy = sortBy;
                await Search();
            }
        }
        public virtual async Task GoToPage(int pageIndex, int pageSize, string sortBy)
        {
            if (ResponsePaged != null && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                await Search();
            }
        }
    }
}
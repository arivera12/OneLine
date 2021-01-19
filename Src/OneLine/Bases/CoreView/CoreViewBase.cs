using FluentValidation.Results;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Services;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    /// <summary>
    /// Base core view implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="THttpService"></typeparam>
    public partial class CoreViewBase<T, TIdentifier, TId, THttpService> :
        ICoreView<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public CoreViewBase()
        { }
        public CoreViewBase(IApplicationConfiguration applicationConfiguration,
            IResourceManagerLocalizer resourceManagerLocalizer,
            IApplicationState applicationState,
            IDevice device,
            ISaveFile saveFile)
        {
            ApplicationConfiguration = applicationConfiguration;
            ResourceManagerLocalizer = resourceManagerLocalizer;
            ApplicationState = applicationState;
            Device = device;
            SaveFile = saveFile; 
        }
        /// <inheritdoc/>
        public virtual async Task Load()
        {
            if (Identifier.IsNotNull() && Identifier.Model.IsNotNull())
            {
                Response = await HttpService.GetOneAsync<T>(Identifier, new EmptyValidator());
                ResponseChanged?.Invoke(Response);
                if (Response.IsNotNull() &&
                    Response.Succeed &&
                    Response.Response.IsNotNull() &&
                    Response.Response.Status.Succeeded() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.IsSuccessStatusCode)
                {
                    Record = Response.Response.Data;
                    RecordChanged?.Invoke(Record);
                    await SelectRecord(Record);
                    FormStateChanged?.Invoke(FormState);
                }
                OnAfterLoad?.Invoke();
            }
            else if (Identifiers.IsNotNull() && Identifiers.Any())
            {
                ResponseCollection = await HttpService.GetRangeAsync<T>(Identifiers, new EmptyValidator());
                ResponseCollectionChanged?.Invoke(ResponseCollection);
                if (ResponseCollection.IsNotNull() &&
                    ResponseCollection.Succeed &&
                    ResponseCollection.Response.IsNotNull() &&
                    ResponseCollection.Response.Status.Succeeded() &&
                    ResponseCollection.HttpResponseMessage.IsNotNull() &&
                    ResponseCollection.HttpResponseMessage.IsSuccessStatusCode)
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
                    await SelectRecords(Records);
                    FormStateChanged?.Invoke(FormState);
                }
                OnAfterLoad?.Invoke();
            }
        }
        /// <inheritdoc/>
        public virtual async Task Search()
        {
            ResponsePaged = await HttpService.SearchAsync<T>(SearchPaging, SearchExtraParams);
            ResponsePagedChanged?.Invoke(ResponsePaged);
            if (ResponsePaged.IsNotNull() &&
                ResponsePaged.Succeed &&
                ResponsePaged.Response.IsNotNull() &&
                ResponsePaged.Response.Status.Succeeded() &&
                ResponsePaged.HttpResponseMessage.IsNotNull() &&
                ResponsePaged.HttpResponseMessage.IsSuccessStatusCode)
            {
                if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Replace)
                {
                    if (Records.IsNull() || RecordsFilteredSorted.IsNull())
                    {
                        Records = new ObservableRangeCollection<T>(ResponsePaged.Response.Data.Data);
                        RecordsFilteredSorted = new ObservableRangeCollection<T>(ResponsePaged.Response.Data.Data);
                    }
                    else
                    {
                        Records.ReplaceRange(ResponsePaged.Response.Data.Data);
                        RecordsFilteredSorted.ReplaceRange(Records);
                    }

                }
                else if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Add)
                {
                    if (Records.IsNull() || RecordsFilteredSorted.IsNull())
                    {
                        Records = new ObservableRangeCollection<T>(ResponsePaged.Response.Data.Data);
                        RecordsFilteredSorted = new ObservableRangeCollection<T>(ResponsePaged.Response.Data.Data);
                    }
                    else
                    {
                        Records.AddRange(ResponsePaged.Response.Data.Data);
                        RecordsFilteredSorted.AddRange(Records);
                    }
                }
                RecordsChanged?.Invoke(Records);
                RecordsFilteredSortedChanged?.Invoke(RecordsFilteredSorted);
            }
            OnAfterSearch?.Invoke();
        }
        /// <inheritdoc/>
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
                MinimumRecordsSelectionsReached = SelectedRecords.Count >= MinimumRecordsSelections;
                MinimumRecordsSelectionsReachedChanged?.Invoke(MinimumRecordsSelectionsReached);
                MaximumRecordsSelectionsReached = SelectedRecords.Count >= MaximumRecordsSelections;
                MaximumRecordsSelectionsReachedChanged?.Invoke(MaximumRecordsSelectionsReached);
                SelectedRecordsChanged?.Invoke(SelectedRecords);
            }
            AfterSelectedRecord?.Invoke();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SelectRecords(IEnumerable<T> selectedRecords)
        {
            if (MaximumRecordsSelections <= 0 || (MaximumRecordsSelections > 0 && selectedRecords.Count() < MaximumRecordsSelections))
            {
                SelectedRecords = new ObservableRangeCollection<T>(selectedRecords);
            }
            MinimumRecordsSelectionsReached = SelectedRecords.Count >= MinimumRecordsSelections;
            MinimumRecordsSelectionsReachedChanged?.Invoke(MinimumRecordsSelectionsReached);
            MaximumRecordsSelectionsReached = SelectedRecords.Count >= MaximumRecordsSelections;
            MaximumRecordsSelectionsReachedChanged?.Invoke(MaximumRecordsSelectionsReached);
            SelectedRecordsChanged?.Invoke(SelectedRecords);
            AfterSelectedRecord?.Invoke();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual async Task Validate()
        {
            if (FormMode.IsSingle())
            {
                ValidationResult = await Validator.ValidateAsync(Record);
            }
            else
            {
                foreach (var record in Records)
                {
                    var result = await Validator.ValidateAsync(record);
                    foreach (var validationFailure in result.Errors)
                    {
                        ValidationResult.Errors.Add(validationFailure);
                    }
                }
            }
            ValidationResultChanged?.Invoke(ValidationResult);
            IsValidModelState = ValidationResult.IsValid;
            IsValidModelStateChanged?.Invoke(IsValidModelState);
            OnAfterValidate?.Invoke();
        }
        /// <inheritdoc/>
        public virtual async Task Save()
        {
            if (FormState.IsCreate() || FormState.IsEdit() || FormState.IsCopy())
            {
                if (FormMode.IsSingle())
                {
                    if (FormState.IsCopy() || FormState.IsCreate())
                    {
                        Response = await HttpService.AddAsync<T>(Record);
                    }
                    else if (FormState.IsEdit())
                    {
                        Response = await HttpService.UpdateAsync<T>(Record);
                    }
                    ResponseChanged?.Invoke(Response);
                    if (Response.IsNotNull() &&
                        Response.Succeed &&
                        Response.Response.IsNotNull() &&
                        Response.Response.Status.Succeeded() &&
                        Response.HttpResponseMessage.IsNotNull() &&
                        Response.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        Record = Response.Response.Data;
                        RecordChanged?.Invoke(Record);
                        FormState = FormState.Edit;
                        FormStateChanged?.Invoke(FormState);
                        ClearMutableBlobDatasWithRules();
                    }
                }
                else if (FormMode.IsMultiple())
                {
                    if (FormState.IsCopy() || FormState.IsCreate())
                    {
                        ResponseCollection = await HttpService.AddRangeAsync<IEnumerable<T>>(Records);
                    }
                    else if (FormState.IsEdit())
                    {
                        ResponseCollection = await HttpService.UpdateRangeAsync<IEnumerable<T>>(Records);
                    }
                    ResponseCollectionChanged?.Invoke(ResponseCollection);
                    if (ResponseCollection.IsNotNull() &&
                        ResponseCollection.Succeed &&
                        ResponseCollection.Response.IsNotNull() &&
                        ResponseCollection.Response.Status.Succeeded() &&
                        ResponseCollection.HttpResponseMessage.IsNotNull() &&
                        ResponseCollection.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        Records.ReplaceRange(ResponseCollection.Response.Data);
                        RecordsChanged?.Invoke(Records);
                        FormState = FormState.Edit;
                        FormStateChanged?.Invoke(FormState);
                        ClearMutableBlobDatasWithRules();
                    }
                }
                OnAfterSave?.Invoke();
            }
        }
        /// <inheritdoc/>
        public virtual async Task Delete()
        {
            if (FormState.IsDelete())
            {
                if (FormMode.IsSingle())
                {
                    Response = await HttpService.DeleteAsync<T>(Identifier);
                    ResponseChanged?.Invoke(Response);
                    if (Response.IsNotNull() &&
                        Response.Succeed &&
                        Response.Response.IsNotNull() &&
                        Response.Response.Status.Succeeded() &&
                        Response.HttpResponseMessage.IsNotNull() &&
                        Response.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        Record = Response.Response.Data;
                        RecordChanged?.Invoke(Record);
                        FormState = FormState.Deleted;
                        FormStateChanged?.Invoke(FormState);
                    }
                }
                else if (FormMode.IsMultiple())
                {
                    ResponseCollection = await HttpService.DeleteRangeAsync<T>(Identifiers);
                    ResponseCollectionChanged?.Invoke(ResponseCollection);
                    if (ResponseCollection.IsNotNull() &&
                        ResponseCollection.Succeed &&
                        ResponseCollection.Response.IsNotNull() &&
                        ResponseCollection.Response.Status.Succeeded() &&
                        ResponseCollection.HttpResponseMessage.IsNotNull() &&
                        ResponseCollection.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        Records.ReplaceRange(ResponseCollection.Response.Data);
                        RecordsChanged?.Invoke(Records);
                        FormState = FormState.Deleted;
                        FormStateChanged?.Invoke(FormState);
                    }
                }
                OnAfterDelete?.Invoke();
            }
        }
        /// <inheritdoc/>
        public virtual IEnumerable<PropertyInfo> GetMutableBlobDatasWithRulesProperties()
        {
            if (FormMode.IsSingle())
            {
                return Record.GetType().GetProperties().Where(w => w.PropertyType == typeof(Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>));
            }
            else if (FormMode.IsMultiple())
            {
                var propertiesInfos = new List<PropertyInfo>();
                foreach (var record in Records)
                {
                    foreach (var property in record.GetType().GetProperties().Where(w => w.PropertyType == typeof(Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)))
                    {
                        propertiesInfos.Add(property);
                    }
                }
                return propertiesInfos;
            }
            else
            {
                return Enumerable.Empty<PropertyInfo>();
            }
        }
        /// <inheritdoc/>
        public virtual void ClearMutableBlobDatasWithRules()
        {
            foreach (var blobDataProperty in GetMutableBlobDatasWithRulesProperties())
            {
                if (FormMode.IsSingle())
                {
                    var blobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(Record);
                    blobDataProperty.SetValue(Record, new Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>(blobDatas.Item1, Enumerable.Empty<BlobData>(), blobDatas.Item3));
                }
                else
                {
                    foreach (var record in Records)
                    {
                        var blobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(record);
                        blobDataProperty.SetValue(record, new Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>(blobDatas.Item1, Enumerable.Empty<BlobData>(), blobDatas.Item3));
                    }
                }
            }
        }
        /// <inheritdoc/>
        public virtual async Task ValidateMutableBlobDatas()
        {
            foreach (var blobDataProperty in GetMutableBlobDatasWithRulesProperties())
            {
                if (FormMode.IsSingle())
                {
                    var mutableBlobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(Record);
                    var formFileRules = mutableBlobDatas.Item1;
                    var blobDatas = mutableBlobDatas.Item2;
                    var userBlobs = mutableBlobDatas.Item3;
                    ValidationResult = new ValidationResult();
                    var currentBlobsCount = 0;
                    if (formFileRules.ForceUpload)
                    {
                        if (blobDatas.IsNull() || !blobDatas.Any())
                        {
                            ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                        }
                        else
                        {
                            await InternalValidateBlobDatas(blobDatas, formFileRules);
                        }
                    }
                    else if (!formFileRules.IsRequired && blobDatas.IsNull() || !blobDatas.Any())
                    {
                        continue;
                    }
                    else if (!formFileRules.IsRequired && blobDatas.IsNotNull() && blobDatas.Any())
                    {
                        await InternalValidateBlobDatas(blobDatas, formFileRules);
                    }
                    else if (formFileRules.IsRequired)
                    {
                        if ((blobDatas.IsNull() || !blobDatas.Any()) && (userBlobs.IsNull() || !userBlobs.Any()))
                        {
                            ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                            continue;
                        }
                        if (userBlobs.IsNotNull() && userBlobs.Any())
                        {
                            currentBlobsCount += userBlobs.Count();
                        }
                        if (blobDatas.IsNotNull() && blobDatas.Any())
                        {
                            currentBlobsCount += blobDatas.Count();
                        }
                        if (currentBlobsCount >= formFileRules.AllowedMinimunFiles)
                        {
                            if (blobDatas.IsNotNull() && blobDatas.Any())
                            {
                                await InternalValidateBlobDatas(blobDatas, formFileRules);
                            }
                        }
                        else
                        {
                            ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                        }
                    }
                }
                else
                {
                    ValidationResult = new ValidationResult();
                    foreach (var record in Records)
                    {
                        var mutableBlobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(record);
                        var formFileRules = mutableBlobDatas.Item1;
                        var blobDatas = mutableBlobDatas.Item2;
                        var userBlobs = mutableBlobDatas.Item3;
                        var currentBlobsCount = 0;
                        if (formFileRules.ForceUpload)
                        {
                            if (blobDatas.IsNull() || !blobDatas.Any())
                            {
                                ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                            }
                            else
                            {
                                await InternalValidateBlobDatas(blobDatas, formFileRules);
                            }
                        }
                        else if (!formFileRules.IsRequired && blobDatas.IsNull() || !blobDatas.Any())
                        {
                            continue;
                        }
                        else if (!formFileRules.IsRequired && blobDatas.IsNotNull() && blobDatas.Any())
                        {
                            await InternalValidateBlobDatas(blobDatas, formFileRules);
                        }
                        else if (formFileRules.IsRequired)
                        {
                            if ((blobDatas.IsNull() || !blobDatas.Any()) && (userBlobs.IsNull() || !userBlobs.Any()))
                            {
                                ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                                continue;
                            }
                            if (userBlobs.IsNotNull() && userBlobs.Any())
                            {
                                currentBlobsCount += userBlobs.Count();
                            }
                            if (blobDatas.IsNotNull() && blobDatas.Any())
                            {
                                currentBlobsCount += blobDatas.Count();
                            }
                            if (currentBlobsCount >= formFileRules.AllowedMinimunFiles)
                            {
                                if (blobDatas.IsNotNull() && blobDatas.Any())
                                {
                                    await InternalValidateBlobDatas(blobDatas, formFileRules);
                                }
                            }
                            else
                            {
                                ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                            }
                        }
                    }
                }
            }
            IsValidModelState = ValidationResult.IsValid;
        }
        private async Task InternalValidateBlobDatas(IEnumerable<BlobData> blobDatas, FormFileRules formFileRules)
        {
            var validator = new BlobDataCollectionValidator();
            var result = await validator.ValidateFormFileRulesAsync(blobDatas, formFileRules);
            if (!result.IsValid)
            {
                foreach (var validationFailure in result.Errors)
                {
                    ValidationResult.Errors.Add(validationFailure);
                }
            }
        }
        /// <inheritdoc/>
        public virtual Task Reset()
        {
            FormState = FormState.Create;
            FormStateChanged?.Invoke(FormState);
            Record = new T();
            RecordChanged?.Invoke(Record);
            Records.Clear();
            RecordsChanged?.Invoke(Records);
            Identifier = new TIdentifier();
            IdentifierChanged?.Invoke(Identifier);
            Identifiers = new List<TIdentifier>();
            IdentifiersChanged?.Invoke(Identifiers);
            var mutableBlobDatasWithRulesProperties = GetMutableBlobDatasWithRulesProperties();
            if (mutableBlobDatasWithRulesProperties.IsNotNull() && mutableBlobDatasWithRulesProperties.Any())
            {
                ClearMutableBlobDatasWithRules();
            }
            OnAfterReset?.Invoke();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task Cancel()
        {
            OnAfterCancel?.Invoke();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task FilterAndSort(string sortBy, bool descending)
        {
            FilterSortBy = sortBy;
            FilterSortByChanged?.Invoke(FilterSortBy);
            FilterDescending = descending;
            FilterDescendingChanged?.Invoke(FilterDescending);
            return FilterAndSort();
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
                RecordsFilteredSorted.ReplaceRange(recordsFilteredSorted.AutoMap<T>());
                RecordsFilteredSortedChanged?.Invoke(RecordsFilteredSorted);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoPreviousPage()
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public virtual Task GoNextPage()
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public virtual Task Sort()
        {
            SearchPaging.Descending = !SearchPaging.Descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task Sort(bool descending)
        {
            SearchPaging.Descending = descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortAscending()
        {
            SearchPaging.Descending = false;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortDescending()
        {
            SearchPaging.Descending = true;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortBy(string sortBy)
        {
            if (SearchPaging.SortBy.Equals(sortBy))
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
        /// <inheritdoc/>
        public virtual Task SortBy(string sortBy, bool descending)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortByAscending(string sortBy)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = false;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortByDescending(string sortBy)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = true;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
    }
}

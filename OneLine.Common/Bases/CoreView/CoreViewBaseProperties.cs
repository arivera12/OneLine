using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneLine.Bases
{
    public abstract partial class CoreViewBase<T, TIdentifier, TId, THttpService> :
        ICoreView<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public virtual TIdentifier Identifier { get; set; }
        public virtual IEnumerable<TIdentifier> Identifiers { get; set; }
        public virtual T Record { get; set; }
        public virtual ObservableRangeCollection<T> Records { get; set; }
        public virtual IValidator Validator { get; set; }
        public virtual ValidationResult ValidationResult { get; set; }
        public virtual bool IsValidModelState { get; set; }
        public virtual FormState FormState { get; set; }
        public virtual FormMode FormMode { get; set; }
        public virtual bool AutoLoad { get; set; }
        public virtual bool AllowDuplicates { get; set; }
        public virtual bool InitialAutoSearch { get; set; }
        public virtual object SearchExtraParams { get; set; }
        public virtual Func<T, bool> FilterPredicate { get; set; }
        public virtual string FilterSortBy { get; set; }
        public virtual bool FilterDescending { get; set; }
        public virtual ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        public virtual IResponseResult<ApiResponse<T>> Response { get; set; }
        public virtual IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        public virtual IResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
        public virtual THttpService HttpService { get; set; }
        public virtual IConfiguration Configuration { get; set; }
        public virtual IPaging Paging { get; set; }
        public virtual ISearchPaging SearchPaging { get; set; }
        public virtual RecordsSelectionMode RecordsSelectionMode { get; set; }
        public virtual CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        public virtual T SelectedRecord { get; set; }
        public virtual ObservableRangeCollection<T> SelectedRecords { get; set; }
        public virtual long MinimumRecordsSelections { get; set; }
        public virtual long MaximumRecordsSelections { get; set; }
        public virtual bool MinimumRecordsSelectionsReached { get; set; }
        public virtual bool MaximumRecordsSelectionsReached { get; set; }
        public virtual Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        public virtual Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        public virtual Action<IResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
        public virtual Action OnBeforeSearch { get; set; }
        public virtual Action OnAfterSearch { get; set; }
        public virtual Action<TIdentifier> IdentifierChanged { get; set; }
        public virtual Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        public virtual Action<T> RecordChanged { get; set; }
        public virtual Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        public virtual Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
        public virtual Action<T> SelectedRecordChanged { get; set; }
        public virtual Action BeforeSelectedRecord { get; set; }
        public virtual Action AfterSelectedRecord { get; set; }
        public virtual Action<ObservableRangeCollection<T>> SelectedRecordsChanged { get; set; }
        public virtual Action<bool> MinimumRecordsSelectionsReachedChanged { get; set; }
        public virtual Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        public virtual Action<IPaging> PagingChanged { get; set; }
        public virtual Action<ISearchPaging> SearchPagingChanged { get; set; }
        public virtual Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        public virtual Action<string> FilterSortByChanged { get; set; }
        public virtual Action<bool> FilterDescendingChanged { get; set; }
        public virtual Action<ValidationResult> ValidationResultChanged { get; set; }
        public virtual Action<bool> IsValidModelStateChanged { get; set; }
        public virtual Action<FormState> FormStateChanged { get; set; }
        public virtual Action<FormMode> FormModeChanged { get; set; }
        public virtual Action OnBeforeLoad { get; set; }
        public virtual Action OnAfterLoad { get; set; }
        public virtual Action OnBeforeReset { get; set; }
        public virtual Action OnAfterReset { get; set; }
        public virtual Action OnBeforeCancel { get; set; }
        public virtual Action OnAfterCancel { get; set; }
        public virtual Action OnBeforeSave { get; set; }
        public virtual Action OnAfterSave { get; set; }
        public virtual Action OnBeforeDelete { get; set; }
        public virtual Action OnAfterDelete { get; set; }
        public virtual Action OnBeforeValidate { get; set; }
        public virtual Action OnAfterValidate { get; set; }
    }
}

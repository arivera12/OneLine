using FluentValidation;
using FluentValidation.Results;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Models;
using OneLine.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        /// <inheritdoc/>
        public ISaveFile SaveFile { get; set; }
        /// <inheritdoc/>
        public IDevice Device { get; set; }
        /// <inheritdoc/>
        public IDeviceStorage DeviceStorage { get; set; }
        /// <inheritdoc/>
        public IResourceManagerLocalizer ResourceManagerLocalizer { get; set; }
        /// <inheritdoc/>
        public IApplicationState ApplicationState { get; set; }
        /// <inheritdoc/>
        public IApplicationConfiguration ApplicationConfiguration { get; set; }
        /// <inheritdoc/>
        public TIdentifier Identifier { get; set; }
        /// <inheritdoc/>
        public IEnumerable<TIdentifier> Identifiers { get; set; }
        /// <inheritdoc/>
        public T Record { get; set; }
        /// <inheritdoc/>
        public ObservableRangeCollection<T> Records { get; set; }
        /// <inheritdoc/>
        public IValidator Validator { get; set; }
        /// <inheritdoc/>
        public ValidationResult ValidationResult { get; set; }
        /// <inheritdoc/>
        public bool IsValidModelState { get; set; }
        /// <inheritdoc/>
        public FormState FormState { get; set; }
        /// <inheritdoc/>
        public FormMode FormMode { get; set; }
        /// <inheritdoc/>
        public bool AutoLoad { get; set; }
        /// <inheritdoc/>
        public bool AllowDuplicates { get; set; }
        /// <inheritdoc/>
        public bool InitialAutoSearch { get; set; }
        /// <inheritdoc/>
        public object SearchExtraParams { get; set; }
        /// <inheritdoc/>
        public Func<T, bool> FilterPredicate { get; set; }
        /// <inheritdoc/>
        public string FilterSortBy { get; set; }
        /// <inheritdoc/>
        public bool FilterDescending { get; set; }
        /// <inheritdoc/>
        public ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        /// <inheritdoc/>
        public IResponseResult<ApiResponse<T>> Response { get; set; }
        /// <inheritdoc/>
        public IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        /// <inheritdoc/>
        public IResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
        /// <inheritdoc/>
        public THttpService HttpService { get; set; }
        /// <inheritdoc/>
        public IPaging Paging { get; set; }
        /// <inheritdoc/>
        public ISearchPaging SearchPaging { get; set; }
        /// <inheritdoc/>
        public RecordsSelectionMode RecordsSelectionMode { get; set; }
        /// <inheritdoc/>
        public CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        /// <inheritdoc/>
        public T SelectedRecord { get; set; }
        /// <inheritdoc/>
        public ObservableRangeCollection<T> SelectedRecords { get; set; }
        /// <inheritdoc/>
        public long MinimumRecordsSelections { get; set; }
        /// <inheritdoc/>
        public long MaximumRecordsSelections { get; set; }
        /// <inheritdoc/>
        public bool MinimumRecordsSelectionsReached { get; set; }
        /// <inheritdoc/>
        public bool MaximumRecordsSelectionsReached { get; set; }
        /// <inheritdoc/>
        public Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        /// <inheritdoc/>
        public Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        /// <inheritdoc/>
        public Action<IResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
        /// <inheritdoc/>
        public Action OnBeforeSearch { get; set; }
        /// <inheritdoc/>
        public Action OnAfterSearch { get; set; }
        /// <inheritdoc/>
        public Action<TIdentifier> IdentifierChanged { get; set; }
        /// <inheritdoc/>
        public Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        /// <inheritdoc/>
        public Action<T> RecordChanged { get; set; }
        /// <inheritdoc/>
        public Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        /// <inheritdoc/>
        public Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
        /// <inheritdoc/>
        public Action<T> SelectedRecordChanged { get; set; }
        /// <inheritdoc/>
        public Action BeforeSelectedRecord { get; set; }
        /// <inheritdoc/>
        public Action AfterSelectedRecord { get; set; }
        /// <inheritdoc/>
        public Action<ObservableRangeCollection<T>> SelectedRecordsChanged { get; set; }
        /// <inheritdoc/>
        public Action<bool> MinimumRecordsSelectionsReachedChanged { get; set; }
        /// <inheritdoc/>
        public Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        /// <inheritdoc/>
        public Action<IPaging> PagingChanged { get; set; }
        /// <inheritdoc/>
        public Action<ISearchPaging> SearchPagingChanged { get; set; }
        /// <inheritdoc/>
        public Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        /// <inheritdoc/>
        public Action<string> FilterSortByChanged { get; set; }
        /// <inheritdoc/>
        public Action<bool> FilterDescendingChanged { get; set; }
        /// <inheritdoc/>
        public Action<ValidationResult> ValidationResultChanged { get; set; }
        /// <inheritdoc/>
        public Action<bool> IsValidModelStateChanged { get; set; }
        /// <inheritdoc/>
        public Action<FormState> FormStateChanged { get; set; }
        /// <inheritdoc/>
        public Action<FormMode> FormModeChanged { get; set; }
        /// <inheritdoc/>
        public Action OnBeforeLoad { get; set; }
        /// <inheritdoc/>
        public Action OnAfterLoad { get; set; }
        /// <inheritdoc/>
        public Action OnBeforeReset { get; set; }
        /// <inheritdoc/>
        public Action OnAfterReset { get; set; }
        /// <inheritdoc/>
        public Action OnBeforeCancel { get; set; }
        /// <inheritdoc/>
        public Action OnAfterCancel { get; set; }
        /// <inheritdoc/>
        public Action OnBeforeSave { get; set; }
        /// <inheritdoc/>
        public Action OnAfterSave { get; set; }
        /// <inheritdoc/>
        public Action OnBeforeDelete { get; set; }
        /// <inheritdoc/>
        public Action OnAfterDelete { get; set; }
        /// <inheritdoc/>
        public Action OnBeforeValidate { get; set; }
        /// <inheritdoc/>
        public Action OnAfterValidate { get; set; }
    }
}

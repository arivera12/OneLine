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
    /// <summary>
    /// Base core view implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="THttpService"></typeparam>
    public abstract partial class CoreViewBase<T, TIdentifier, TId, THttpService> :
        ICoreView<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        /// <inheritdoc/>
        public virtual TIdentifier Identifier { get; set; }
        /// <inheritdoc/>
        public virtual IEnumerable<TIdentifier> Identifiers { get; set; }
        /// <inheritdoc/>
        public virtual T Record { get; set; }
        /// <inheritdoc/>
        public virtual ObservableRangeCollection<T> Records { get; set; }
        /// <inheritdoc/>
        public virtual IValidator Validator { get; set; }
        /// <inheritdoc/>
        public virtual ValidationResult ValidationResult { get; set; }
        /// <inheritdoc/>
        public virtual bool IsValidModelState { get; set; }
        /// <inheritdoc/>
        public virtual FormState FormState { get; set; }
        /// <inheritdoc/>
        public virtual FormMode FormMode { get; set; }
        /// <inheritdoc/>
        public virtual bool AutoLoad { get; set; }
        /// <inheritdoc/>
        public virtual bool AllowDuplicates { get; set; }
        /// <inheritdoc/>
        public virtual bool InitialAutoSearch { get; set; }
        /// <inheritdoc/>
        public virtual object SearchExtraParams { get; set; }
        /// <inheritdoc/>
        public virtual Func<T, bool> FilterPredicate { get; set; }
        /// <inheritdoc/>
        public virtual string FilterSortBy { get; set; }
        /// <inheritdoc/>
        public virtual bool FilterDescending { get; set; }
        /// <inheritdoc/>
        public virtual ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        /// <inheritdoc/>
        public virtual IResponseResult<ApiResponse<T>> Response { get; set; }
        /// <inheritdoc/>
        public virtual IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        /// <inheritdoc/>
        public virtual IResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
        /// <inheritdoc/>
        public virtual THttpService HttpService { get; set; }
        /// <inheritdoc/>
        public virtual IConfiguration Configuration { get; set; }
        /// <inheritdoc/>
        public virtual IPaging Paging { get; set; }
        /// <inheritdoc/>
        public virtual ISearchPaging SearchPaging { get; set; }
        /// <inheritdoc/>
        public virtual RecordsSelectionMode RecordsSelectionMode { get; set; }
        /// <inheritdoc/>
        public virtual CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        /// <inheritdoc/>
        public virtual T SelectedRecord { get; set; }
        /// <inheritdoc/>
        public virtual ObservableRangeCollection<T> SelectedRecords { get; set; }
        /// <inheritdoc/>
        public virtual long MinimumRecordsSelections { get; set; }
        /// <inheritdoc/>
        public virtual long MaximumRecordsSelections { get; set; }
        /// <inheritdoc/>
        public virtual bool MinimumRecordsSelectionsReached { get; set; }
        /// <inheritdoc/>
        public virtual bool MaximumRecordsSelectionsReached { get; set; }
        /// <inheritdoc/>
        public virtual Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<IResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action OnBeforeSearch { get; set; }
        /// <inheritdoc/>
        public virtual Action OnAfterSearch { get; set; }
        /// <inheritdoc/>
        public virtual Action<TIdentifier> IdentifierChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<T> RecordChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<T> SelectedRecordChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action BeforeSelectedRecord { get; set; }
        /// <inheritdoc/>
        public virtual Action AfterSelectedRecord { get; set; }
        public virtual Action<ObservableRangeCollection<T>> SelectedRecordsChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<bool> MinimumRecordsSelectionsReachedChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<IPaging> PagingChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<ISearchPaging> SearchPagingChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<string> FilterSortByChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<bool> FilterDescendingChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<ValidationResult> ValidationResultChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<bool> IsValidModelStateChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<FormState> FormStateChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<FormMode> FormModeChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action OnBeforeLoad { get; set; }
        /// <inheritdoc/>
        public virtual Action OnAfterLoad { get; set; }
        /// <inheritdoc/>
        public virtual Action OnBeforeReset { get; set; }
        /// <inheritdoc/>
        public virtual Action OnAfterReset { get; set; }
        /// <inheritdoc/>
        public virtual Action OnBeforeCancel { get; set; }
        /// <inheritdoc/>
        public virtual Action OnAfterCancel { get; set; }
        /// <inheritdoc/>
        public virtual Action OnBeforeSave { get; set; }
        /// <inheritdoc/>
        public virtual Action OnAfterSave { get; set; }
        /// <inheritdoc/>
        public virtual Action OnBeforeDelete { get; set; }
        /// <inheritdoc/>
        public virtual Action OnAfterDelete { get; set; }
        /// <inheritdoc/>
        public virtual Action OnBeforeValidate { get; set; }
        /// <inheritdoc/>
        public virtual Action OnAfterValidate { get; set; }
    }
}

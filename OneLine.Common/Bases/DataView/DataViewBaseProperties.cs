using FluentValidation;
using Microsoft.Extensions.Configuration;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OneLine.Bases
{
    public abstract partial class DataViewBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IDataView<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public virtual TIdentifier Identifier { get; set; }
        public virtual IEnumerable<TIdentifier> Identifiers { get; set; }
        public virtual T Record { get; set; }
        public virtual ObservableRangeCollection<T> Records { get; set; }
        public virtual object[] SearchExtraParams { get; set; }
        public virtual Func<T, bool> FilterPredicate { get; set; }
        public virtual string FilterSortBy { get; set; }
        public virtual bool FilterDescending { get; set; }
        public virtual ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        public virtual ResponseResult<ApiResponse<T>> Response { get; set; }
        public virtual ResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        public virtual ResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
        public virtual THttpService HttpService { get; set; }
        public virtual IConfiguration Configuration { get; set; }
        public virtual IPaging Paging { get; set; }
        public virtual ISearchPaging SearchPaging { get; set; }
        public virtual RecordsSelectionMode RecordsSelectionMode { get; set; }
        public virtual CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        public virtual T SelectedRecord { get; set; }
        public virtual ObservableRangeCollection<T> SelectedRecords { get; set; }
        public virtual long MinimunRecordsSelections { get; set; }
        public virtual long MaximumRecordsSelections { get; set; }
        public virtual bool MinimunRecordsSelectionsReached { get; set; }
        public virtual bool MaximumRecordsSelectionsReached { get; set; }
        public virtual Action<ResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        public virtual Action<ResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        public virtual Action<ResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
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
        public virtual Action<IEnumerable<T>, bool, bool> SelectedRecordsChanged { get; set; }
        public virtual Action<bool> MinimunRecordsSelectionsReachedChanged { get; set; }
        public virtual Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        public virtual Action<IPaging> PagingChanged { get; set; }
        public virtual Action<ISearchPaging> SearchPagingChanged { get; set; }
        public virtual Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        public virtual Action<string> FilterSortByChanged { get; set; }
        public virtual Action<bool> FilterDescendingChanged { get; set; }
    }
}
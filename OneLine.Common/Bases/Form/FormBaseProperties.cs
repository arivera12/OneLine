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
    public abstract partial class FormBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public virtual T Record { get; set; }
        public virtual ObservableRangeCollection<T> Records { get; set; }
        public virtual TIdentifier Identifier { get; set; }
        public virtual IEnumerable<TIdentifier> Identifiers { get; set; }
        public virtual CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        public virtual IValidator Validator { get; set; }
        public virtual ValidationResult ValidationResult { get; set; }
        public virtual bool IsValidModelState { get; set; }
        public virtual THttpService HttpService { get; set; }
        public virtual IList<TBlobData> BlobDatas { get; set; }
        public virtual IConfiguration Configuration { get; set; }
        public virtual FormState FormState { get; set; }
        public virtual FormMode FormMode { get; set; }
        public virtual ResponseResult<ApiResponse<T>> Response { get; set; }
        public virtual ResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        public virtual ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> ResponseAddWithBlobs { get; set; }
        public virtual ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> ResponseAddCollectionWithBlobs { get; set; }
        public virtual ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateWithBlobs { get; set; }
        public virtual ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateCollectionWithBlobs { get; set; }
        public virtual Action<TIdentifier> IdentifierChanged { get; set; }
        public virtual Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        public virtual Action<T> RecordChanged { get; set; }
        public virtual Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        public virtual Action<IList<TBlobData>> BlobDatasChanged { get; set; }
        public virtual Action<ValidationResult> ValidationResultChanged { get; set; }
        public virtual Action<bool> IsValidModelStateChanged { get; set; }
        public virtual Action<FormState> FormStateChanged { get; set; }
        public virtual Action<FormMode> FormModeChanged { get; set; }
        public virtual Action<ResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        public virtual Action<ResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        public virtual Action<ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> ResponseAddWithBlobsChanged { get; set; }
        public virtual Action<ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> ResponseAddCollectionWithBlobsChanged { get; set; }
        public virtual Action<ResponseResult<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> ResponseUpdateWithBlobsChanged { get; set; }
        public virtual Action<ResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> ResponseUpdateCollectionWithBlobsChanged { get; set; }
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

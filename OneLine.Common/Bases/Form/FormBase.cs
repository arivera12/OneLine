using FluentValidation;
using Microsoft.Extensions.Configuration;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public abstract class FormBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>, new()
        where TId : class
        where THttpService : HttpBaseCrudExtendedService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public virtual T Record { get; set; }
        public virtual ObservableRangeCollection<T> Records { get; set; }
        public virtual TIdentifier Identifier { get; set; }
        public virtual IEnumerable<TIdentifier> Identifiers { get; set; }
        public virtual THttpService HttpService { get; set; }
        public virtual IList<TBlobData> BlobDatas { get; set; }
        public virtual IConfiguration Configuration { get; set; }
        public virtual FormState FormState { get; set; }
        public virtual FormMode FormMode { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnLoad { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnLoadSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnLoadException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnLoadFailed { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollection { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollectionSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollectionException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollectionFailed { get; set; }
        public virtual IResponseResult<IApiResponse<T>> Response { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnResponse { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnResponseSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnResponseException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<T>>> OnResponseFailed { get; set; }
        public virtual IResponseResult<IApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollection { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionFailed { get; set; }
        public virtual IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> ResponseAddWithBlobs { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobs { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsFailed { get; set; }
        public virtual IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> ResponseAddCollectionWithBlobs { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobs { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobsSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobsException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobsFailed { get; set; }
        public virtual IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateWithBlobs { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobs { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsFailed { get; set; }
        public virtual IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateCollectionWithBlobs { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobs { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobsSucceeded { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobsException { get; set; }
        public virtual Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobsFailed { get; set; }
        public virtual Action<Action> OnBeforeReset { get; set; }
        public virtual Action OnAfterReset { get; set; }
        public virtual Action<Action> OnBeforeCancel { get; set; }
        public virtual Action OnAfterCancel { get; set; }
        public virtual Action<Action> OnBeforeSave { get; set; }
        public virtual Action OnAfterSave { get; set; }
        public virtual Action<Action> OnBeforeDelete { get; set; }
        public virtual Action<T> OnAfterDelete { get; set; }
        public virtual Action OnFailedSave { get; set; }
        public virtual Action OnFailedValidation { get; set; }
        public virtual CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        public FormBase()
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
        }
        public FormBase(FormState formState)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
        }
        public FormBase(FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>(); ;
            FormMode = formMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(TIdentifier identifier)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
        }
        public FormBase(IEnumerable<TIdentifier> identifiers)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
        }
        public FormBase(T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
        }
        public FormBase(IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.AddRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
        }
        public FormBase(FormState formState, FormMode formMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
        }
        public FormBase(FormState formState, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, TIdentifier identifier)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
        }
        public FormBase(FormState formState, IEnumerable<TIdentifier> identifiers)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
        }
        public FormBase(FormState formState, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
        }
        public FormBase(FormState formState, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
        }
        public FormBase(FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormMode formMode, TIdentifier identifier)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
        }
        public FormBase(FormMode formMode, IEnumerable<TIdentifier> identifiers)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
        }
        public FormBase(FormMode formMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
        }
        public FormBase(FormMode formMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormMode = formMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<TIdentifier> identifiers)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, TIdentifier identifier)
        {
            Identifier = identifier;
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<TIdentifier> identifiers)
        {
            Identifier = new TIdentifier();
            Identifiers = identifiers;
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, T record)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = record;
            Records = new ObservableRangeCollection<T>();
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public FormBase(FormState formState, FormMode formMode, CollectionAppendReplaceMode collectionAppendReplaceMode, IEnumerable<T> records)
        {
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            Record = new T();
            Records = new ObservableRangeCollection<T>();
            Records.ReplaceRange(records);
            HttpService = new THttpService();
            BlobDatas = new List<TBlobData>();
            FormState = formState;
            FormMode = formMode;
            CollectionAppendReplaceMode = collectionAppendReplaceMode;
        }
        public virtual async Task Load()
        {
            if (FormMode == FormMode.Single)
            {
                if (Identifier != null && Identifier.Model != null)
                {
                    Response = await HttpService.GetOne<T>(Identifier, new EmptyValidator());
                    if (Response.Succeed && Response.Response.Status.Succeeded())
                    {
                        Record = Response.Response.Data;
                        OnLoadSucceeded?.Invoke(Response);
                    }
                    else if (Response.HasException)
                    {
                        OnLoadException?.Invoke(Response);
                    }
                    else
                    {
                        OnLoadFailed?.Invoke(Response);
                    }
                }
                OnLoad?.Invoke(Response);
            }
            else if (FormMode == FormMode.Multiple)
            {
                if (Identifiers != null && Identifiers.Any())
                {
                    ResponseCollection = await HttpService.GetRange<T>(Identifiers, new EmptyValidator());
                    if (ResponseCollection.Succeed && ResponseCollection.Response.Status.Succeeded())
                    {
                        if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Replace)
                        {
                            Records.ReplaceRange(ResponseCollection.Response.Data);
                        }
                        else if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Add)
                        {
                            Records.AddRange(ResponseCollection.Response.Data);
                        }
                        OnLoadCollectionSucceeded?.Invoke(ResponseCollection);
                    }
                    else if (Response.HasException)
                    {
                        OnLoadCollectionException?.Invoke(ResponseCollection);
                    }
                    else
                    {
                        OnLoadCollectionFailed?.Invoke(ResponseCollection);
                    }
                }
                OnLoadCollection?.Invoke(ResponseCollection);
            }
        }
        public virtual async Task Save(IValidator validator)
        {
            if (FormState == FormState.Copy || FormState == FormState.Create || FormState == FormState.Edit)
            {
                if (FormState == FormState.Edit)
                {
                    if (OnAfterSave == null)
                    {
                        await InternalUpdate(validator);
                    }
                    else
                    {
                        OnBeforeSave?.Invoke(async () => await InternalUpdate(validator));
                    }
                }
                else
                {
                    if (OnAfterSave == null)
                    {
                        await InternalCreate(validator);
                    }
                    else
                    {
                        OnBeforeSave?.Invoke(async () => await InternalCreate(validator));
                    }
                }
            }
        }
        private async Task InternalCreate(IValidator validator)
        {
            if (BlobDatas.Any())
            {
                await InternalCreateWitBlobData(validator);
            }
            else
            {
                if (FormMode == FormMode.Single)
                {
                    Response = await HttpService.Add<T>(Record, validator);
                }
                else if (FormMode == FormMode.Multiple)
                {
                    ResponseCollection = await HttpService.AddRange<IEnumerable<T>>(Records, validator);
                }
                InternalResponse(FormState.Edit);
            }
        }
        private async Task InternalCreateWitBlobData(IValidator validator)
        {
            if (FormMode == FormMode.Single)
            {
                ResponseAddWithBlobs = await HttpService.Add(Record, validator, BlobDatas);
                OnResponseAddWithBlobs?.Invoke(ResponseAddWithBlobs);
                if (ResponseAddWithBlobs.Succeed && ResponseAddWithBlobs.Response.Status.Succeeded())
                {
                    Record = ResponseAddWithBlobs.Response.Data.Item1;
                    FormState = FormState.Edit;
                    OnResponseAddWithBlobsSucceeded?.Invoke(ResponseAddWithBlobs);
                    OnAfterSave?.Invoke();
                }
                else if (ResponseAddWithBlobs.HasException)
                {
                    OnFailedSave?.Invoke();
                    OnResponseAddWithBlobsException?.Invoke(ResponseAddWithBlobs);
                }
                else if (ResponseAddWithBlobs.Succeed && ResponseAddWithBlobs.Response.Status.Failed() && ResponseAddWithBlobs.Response.Message == "ValidationFailed")
                {
                    OnFailedSave?.Invoke();
                    OnResponseAddWithBlobsFailed?.Invoke(ResponseAddWithBlobs);
                }
                else
                {
                    OnFailedSave?.Invoke();
                    OnResponseAddWithBlobsFailed?.Invoke(ResponseAddWithBlobs);
                }
            }
            else if (FormMode == FormMode.Multiple)
            {
                ResponseAddCollectionWithBlobs = await HttpService.AddRange(Records, validator, BlobDatas);
                OnResponseAddCollectionWithBlobs?.Invoke(ResponseAddCollectionWithBlobs);
                if (ResponseAddCollectionWithBlobs.Succeed && ResponseAddCollectionWithBlobs.Response.Status.Succeeded())
                {
                    Records.ReplaceRange(ResponseAddCollectionWithBlobs.Response.Data.Item1);
                    FormState = FormState.Edit;
                    OnResponseAddCollectionWithBlobsSucceeded?.Invoke(ResponseAddCollectionWithBlobs);
                    OnAfterSave?.Invoke();
                }
                else if (ResponseAddCollectionWithBlobs.HasException)
                {
                    OnFailedSave?.Invoke();
                    OnResponseAddCollectionWithBlobsException?.Invoke(ResponseAddCollectionWithBlobs);
                }
                else if (ResponseAddCollectionWithBlobs.Succeed && ResponseAddCollectionWithBlobs.Response.Status.Failed() &&
                    ResponseAddCollectionWithBlobs.Response.Message == "ValidationFailed")
                {
                    OnFailedSave?.Invoke();
                    OnResponseAddCollectionWithBlobsFailed?.Invoke(ResponseAddCollectionWithBlobs);
                }
                else
                {
                    OnFailedSave?.Invoke();
                    OnResponseAddCollectionWithBlobsFailed?.Invoke(ResponseAddCollectionWithBlobs);
                }
            }
        }
        private async Task InternalUpdate(IValidator validator)
        {
            if (FormMode == FormMode.Single)
            {
                if (BlobDatas.Any())
                {
                    await InternalUpdateWitBlobData(validator);
                }
                else
                {
                    if (FormMode == FormMode.Single)
                    {
                        Response = await HttpService.Update<T>(Record, validator);
                    }
                    else if (FormMode == FormMode.Multiple)
                    {
                        ResponseCollection = await HttpService.UpdateRange<IEnumerable<T>>(Records, validator);
                    }
                    InternalResponse(FormState.Edit);
                }
            }
        }
        private async Task InternalUpdateWitBlobData(IValidator validator)
        {
            if (FormMode == FormMode.Single)
            {
                ResponseUpdateWithBlobs = await HttpService.Update(Record, validator, BlobDatas);
                OnResponseUpdateWithBlobs?.Invoke(ResponseUpdateWithBlobs);
                if (ResponseUpdateWithBlobs.Succeed && Response.Response.Status.Succeeded())
                {
                    Record = ResponseUpdateWithBlobs.Response.Data.Item1;
                    FormState = FormState.Edit;
                    OnResponseUpdateWithBlobsSucceeded?.Invoke(ResponseUpdateWithBlobs);
                    OnAfterSave?.Invoke();
                }
                else if (ResponseUpdateWithBlobs.HasException)
                {
                    OnFailedSave?.Invoke();
                    OnResponseUpdateWithBlobsException?.Invoke(ResponseUpdateWithBlobs);
                }
                else if (ResponseUpdateWithBlobs.Succeed && ResponseUpdateWithBlobs.Response.Status.Failed() &&
                    ResponseUpdateWithBlobs.Response.Message == "ValidationFailed")
                {
                    OnFailedSave?.Invoke();
                    OnResponseUpdateWithBlobsFailed?.Invoke(ResponseUpdateWithBlobs);
                }
                else
                {
                    OnFailedSave?.Invoke();
                    OnResponseUpdateWithBlobsFailed?.Invoke(ResponseUpdateWithBlobs);
                }
            }
            else if (FormMode == FormMode.Multiple)
            {
                ResponseUpdateCollectionWithBlobs = await HttpService.UpdateRange(Records, validator, BlobDatas);
                OnResponseUpdateCollectionWithBlobs?.Invoke(ResponseUpdateCollectionWithBlobs);
                if (ResponseUpdateCollectionWithBlobs.Succeed && Response.Response.Status.Succeeded())
                {
                    Records.ReplaceRange(ResponseUpdateCollectionWithBlobs.Response.Data.Item1);
                    FormState = FormState.Edit;
                    OnResponseUpdateCollectionWithBlobsSucceeded?.Invoke(ResponseUpdateCollectionWithBlobs);
                    OnAfterSave?.Invoke();
                }
                else if (ResponseUpdateCollectionWithBlobs.HasException)
                {
                    OnFailedSave?.Invoke();
                    OnResponseUpdateCollectionWithBlobsException?.Invoke(ResponseUpdateCollectionWithBlobs);
                }
                else if (ResponseUpdateCollectionWithBlobs.Succeed && ResponseUpdateCollectionWithBlobs.Response.Status.Failed() &&
                    ResponseUpdateCollectionWithBlobs.Response.Message == "ValidationFailed")
                {
                    OnFailedSave?.Invoke();
                    OnResponseUpdateCollectionWithBlobsFailed?.Invoke(ResponseUpdateCollectionWithBlobs);
                }
                else
                {
                    OnFailedSave?.Invoke();
                    OnResponseUpdateCollectionWithBlobsFailed?.Invoke(ResponseUpdateCollectionWithBlobs);
                }
            }
        }
        private void InternalResponse(FormState formState)
        {
            if (FormMode == FormMode.Single)
            {
                OnResponse?.Invoke(Response);
                if (Response.Succeed && Response.Response.Status.Succeeded())
                {
                    Record = Response.Response.Data;
                    FormState = formState;
                    OnResponseSucceeded?.Invoke(Response);
                    OnAfterSave?.Invoke();
                }
                else if (Response.HasException)
                {
                    OnResponseException?.Invoke(Response);
                }
                else if (Response.Succeed && Response.Response.Status.Failed() && Response.Response.Message == "ValidationFailed")
                {
                    OnFailedValidation?.Invoke();
                    OnResponseFailed?.Invoke(Response);
                }
                else
                {
                    OnFailedSave?.Invoke();
                    OnResponseFailed?.Invoke(Response);
                }
            }
            else if (FormMode == FormMode.Multiple)
            {
                OnResponseCollection?.Invoke(ResponseCollection);
                if (ResponseCollection.Succeed && ResponseCollection.Response.Status.Succeeded())
                {
                    Records.ReplaceRange(ResponseCollection.Response.Data);
                    FormState = formState;
                    OnResponseCollectionSucceeded?.Invoke(ResponseCollection);
                    OnAfterSave?.Invoke();
                }
                else if (ResponseCollection.HasException)
                {
                    OnFailedSave?.Invoke();
                    OnResponseCollectionException?.Invoke(ResponseCollection);
                }
                else if (ResponseCollection.Succeed && ResponseCollection.Response.Status.Failed() &&
                    ResponseCollection.Response.Message == "ValidationFailed")
                {
                    OnFailedValidation?.Invoke();
                    OnResponseFailed?.Invoke(Response);
                }
                else
                {
                    OnFailedSave?.Invoke();
                    OnResponseCollectionFailed?.Invoke(ResponseCollection);
                }
            }
        }
        public virtual async Task Delete(IValidator validator)
        {
            if (FormState == FormState.Delete)
            {
                if (OnBeforeDelete == null)
                {
                    if (FormMode == FormMode.Single)
                    {
                        Response = await HttpService.Delete<T>(Identifier, validator);
                    }
                    else if (FormMode == FormMode.Multiple)
                    {
                        ResponseCollection = await HttpService.DeleteRange<T>(Identifiers, validator);
                    }
                    InternalResponse(FormState.Deleted);
                }
                else
                {
                    OnBeforeDelete?.Invoke(async () =>
                    {
                        if (FormMode == FormMode.Single)
                        {
                            Response = await HttpService.Delete<T>(Identifier, validator);
                        }
                        else if (FormMode == FormMode.Multiple)
                        {
                            ResponseCollection = await HttpService.DeleteRange<T>(Identifiers, validator);
                        }
                        InternalResponse(FormState.Deleted);
                    });
                }
                OnAfterDelete?.Invoke(Response.Response.Data);
            }
        }
        public virtual Task Reset()
        {
            if (OnBeforeReset == null)
            {
                return InternalReset();
            }
            OnBeforeReset?.Invoke(() => InternalReset());
            return Task.CompletedTask;
        }
        private Task InternalReset()
        {
            Record = new T();
            Records.Clear();
            Identifier = new TIdentifier();
            Identifiers = new List<TIdentifier>();
            BlobDatas.Clear();
            OnAfterReset?.Invoke();
            return Task.CompletedTask;
        }
        public virtual Task Cancel()
        {
            if (OnBeforeCancel == null)
            {
                return InternalCancel();
            }
            OnBeforeCancel?.Invoke(() => InternalCancel());
            return Task.CompletedTask;
        }
        private Task InternalCancel()
        {
            OnAfterCancel?.Invoke();
            return Task.CompletedTask;
        }
    }
}
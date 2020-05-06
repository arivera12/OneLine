using FluentValidation;
using FluentValidation.Results;
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
    public abstract partial class FormBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public virtual async Task Load()
        {
            if (FormMode.IsSingle())
            {
                if (Identifier != null && Identifier.Model != null)
                {
                    Response = await HttpService.GetOne<T>(Identifier);
                    ResponseChanged?.Invoke(Response);
                    if (Response.Succeed && Response.Response.Status.Succeeded())
                    {
                        Record = Response.Response.Data;
                        RecordChanged?.Invoke(Record);
                    }
                }
            }
            else if (FormMode.IsMultiple())
            {
                if (Identifiers != null && Identifiers.Any())
                {
                    ResponseCollection = await HttpService.GetRange<T>(Identifiers);
                    ResponseCollectionChanged?.Invoke(ResponseCollection);
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
                        RecordsChanged?.Invoke(Records);
                    }
                }
            }
        }
        public virtual async Task Validate()
        {
            ValidationResult = await Validator.ValidateAsync(Record);
            ValidationResultChanged?.Invoke(ValidationResult);
            IsValidModelState = ValidationResult.IsValid;
            IsValidModelStateChanged?.Invoke(IsValidModelState);
            if(IsValidModelState)
            {
                if (BlobDatas == null || BlobDatas.Any())
                {
                    OnValidationSucceeded?.Invoke();
                }
                else
                {
                    var BlobValidator = new BlobDataCollectionValidator();
                    ValidationResult = await BlobValidator.ValidateAsync(BlobDatas);
                    ValidationResultChanged?.Invoke(ValidationResult);
                    IsValidModelState = ValidationResult.IsValid;
                    IsValidModelStateChanged?.Invoke(IsValidModelState);
                    if (IsValidModelState)
                    {
                        OnValidationSucceeded?.Invoke();
                    }
                    else
                    {
                        OnValidationFailed?.Invoke();
                    }
                }
            }
            else
            {
                OnValidationFailed?.Invoke();
            }
        }
        public virtual async Task Save()
        {
            if (FormState.IsCopy() || FormState.IsCreate() || FormState.IsEdit())
            {
                if (FormState.IsEdit())
                {
                    if (OnBeforeSave == null)
                    {
                        await InternalUpdate();
                    }
                    else
                    {
                        OnBeforeSave?.Invoke(async () => await InternalUpdate());
                    }
                }
                else
                {
                    if (OnBeforeSave == null)
                    {
                        await InternalCreate();
                    }
                    else
                    {
                        OnBeforeSave?.Invoke(async () => await InternalCreate());
                    }
                }
            }
        }
        private async Task InternalCreate()
        {
            if (BlobDatas.Any())
            {
                await InternalCreateWitBlobData();
            }
            else
            {
                if (FormMode.IsSingle())
                {
                    Response = await HttpService.Add<T>(Record);
                    ResponseChanged?.Invoke(Response);
                }
                else if (FormMode.IsMultiple())
                {
                    ResponseCollection = await HttpService.AddRange<IEnumerable<T>>(Records);
                    ResponseCollectionChanged?.Invoke(ResponseCollection);
                }
                InternalResponse(FormState.Edit);
            }
        }
        private async Task InternalCreateWitBlobData()
        {
            if (FormMode.IsSingle())
            {
                ResponseAddWithBlobs = await HttpService.Add(Record, BlobDatas);
                ResponseAddWithBlobsChanged?.Invoke(ResponseAddWithBlobs);
                if (ResponseAddWithBlobs.Succeed && ResponseAddWithBlobs.Response.Status.Succeeded())
                {
                    Record = ResponseAddWithBlobs.Response.Data.Item1;
                    RecordChanged?.Invoke(Record);
                    FormState = FormState.Edit;
                    FormStateChanged?.Invoke(FormState);
                    OnAfterSave?.Invoke();
                }
                else
                {
                    OnSaveFailed?.Invoke();
                }
            }
            else if (FormMode.IsMultiple())
            {
                ResponseAddCollectionWithBlobs = await HttpService.AddRange(Records, BlobDatas);
                ResponseAddCollectionWithBlobsChanged?.Invoke(ResponseAddCollectionWithBlobs);
                if (ResponseAddCollectionWithBlobs.Succeed && ResponseAddCollectionWithBlobs.Response.Status.Succeeded())
                {
                    Records.ReplaceRange(ResponseAddCollectionWithBlobs.Response.Data.Item1);
                    RecordsChanged?.Invoke(Records);
                    FormState = FormState.Edit;
                    FormStateChanged?.Invoke(FormState);
                    OnAfterSave?.Invoke();
                }
                else
                {
                    OnSaveFailed?.Invoke();
                }
            }
        }
        private async Task InternalUpdate()
        {
            if (FormMode.IsSingle())
            {
                if (BlobDatas.Any())
                {
                    await InternalUpdateWitBlobData();
                }
                else
                {
                    if (FormMode.IsSingle())
                    {
                        Response = await HttpService.Update<T>(Record);
                    }
                    else if (FormMode.IsMultiple())
                    {
                        ResponseCollection = await HttpService.UpdateRange<IEnumerable<T>>(Records);
                    }
                    InternalResponse(FormState.Edit);
                }
            }
        }
        private async Task InternalUpdateWitBlobData()
        {
            if (FormMode.IsSingle())
            {
                ResponseUpdateWithBlobs = await HttpService.Update(Record, BlobDatas);
                ResponseUpdateWithBlobsChanged?.Invoke(ResponseUpdateWithBlobs);
                if (ResponseUpdateWithBlobs.Succeed && Response.Response.Status.Succeeded())
                {
                    Record = ResponseUpdateWithBlobs.Response.Data.Item1;
                    RecordChanged?.Invoke(Record);
                    FormState = FormState.Edit;
                    FormStateChanged?.Invoke(FormState);
                    OnAfterSave?.Invoke();
                }
                else
                {
                    OnSaveFailed?.Invoke();
                }
            }
            else if (FormMode.IsMultiple())
            {
                ResponseUpdateCollectionWithBlobs = await HttpService.UpdateRange(Records, BlobDatas);
                ResponseUpdateCollectionWithBlobsChanged?.Invoke(ResponseUpdateCollectionWithBlobs);
                if (ResponseUpdateCollectionWithBlobs.Succeed && Response.Response.Status.Succeeded())
                {
                    Records.ReplaceRange(ResponseUpdateCollectionWithBlobs.Response.Data.Item1);
                    RecordsChanged?.Invoke(Records);
                    FormState = FormState.Edit;
                    FormStateChanged?.Invoke(FormState);
                    OnAfterSave?.Invoke();
                }
                else
                {
                    OnSaveFailed?.Invoke();
                }
            }
        }
        public virtual async Task Delete()
        {
            if (FormState.IsDelete())
            {
                if (OnBeforeDelete == null)
                {
                    await InternalDelete();
                }
                else
                {
                    OnBeforeDelete?.Invoke(async () => await InternalDelete());
                }
            }
        }
        private async Task InternalDelete()
        {
            if (FormMode.IsSingle())
            {
                Response = await HttpService.Delete<T>(Identifier);
                ResponseChanged?.Invoke(Response);
            }
            else if (FormMode.IsMultiple())
            {
                ResponseCollection = await HttpService.DeleteRange<T>(Identifiers);
                ResponseCollectionChanged?.Invoke(ResponseCollection);
            }
            InternalResponse(FormState.Deleted);
        }
        private void InternalResponse(FormState formState)
        {
            if (FormMode.IsSingle())
            {
                if (Response.Succeed && Response.Response.Status.Succeeded())
                {
                    Record = Response.Response.Data;
                    RecordChanged?.Invoke(Record);
                    FormState = formState;
                    FormStateChanged?.Invoke(FormState);
                    if(FormState.IsDeleted())
                    {
                        OnAfterDelete?.Invoke();
                    }
                    else
                    {
                        OnAfterSave?.Invoke();
                    }
                }
                else
                {
                    if (FormState.IsDelete())
                    {
                        OnDeleteFailed?.Invoke();
                    }
                    else
                    {
                        OnSaveFailed?.Invoke();
                    }   
                }
            }
            else if (FormMode.IsMultiple())
            {
                if (ResponseCollection.Succeed && ResponseCollection.Response.Status.Succeeded())
                {
                    Records.ReplaceRange(ResponseCollection.Response.Data);
                    RecordsChanged?.Invoke(Records);
                    FormState = formState;
                    FormStateChanged?.Invoke(FormState);
                    if (FormState.IsDeleted())
                    {
                        OnAfterDelete?.Invoke();
                    }
                    else
                    {
                        OnAfterSave?.Invoke();
                    }
                }
                else
                {
                    if (FormState.IsDelete())
                    {
                        OnDeleteFailed?.Invoke();
                    }
                    else
                    {
                        OnSaveFailed?.Invoke();
                    }
                }
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
            RecordChanged?.Invoke(Record);
            Records.Clear();
            RecordsChanged?.Invoke(Records);
            Identifier = new TIdentifier();
            IdentifierChanged?.Invoke(Identifier);
            Identifiers = new List<TIdentifier>();
            IdentifiersChanged?.Invoke(Identifiers);
            BlobDatas.Clear();
            BlobDatasChanged?.Invoke(BlobDatas);
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
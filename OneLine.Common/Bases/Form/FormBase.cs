using FluentValidation;
using FluentValidation.Results;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
        public virtual async Task Load()
        {
            if (FormMode.IsSingle())
            {
                if (Identifier.IsNotNull() && Identifier.Model.IsNotNull())
                {
                    Response = await HttpService.GetOne<T>(Identifier);
                    ResponseChanged?.Invoke(Response);
                    if (Response.IsNotNull() && Response.HttpResponseMessage.IsSuccessStatusCode &&
                        Response.Succeed && Response.Response.Status.Succeeded())
                    {
                        Record = Response.Response.Data;
                        RecordChanged?.Invoke(Record);
                    }
                }
            }
            else if (FormMode.IsMultiple())
            {
                if (Identifiers.IsNotNull() && Identifiers.Any())
                {
                    ResponseCollection = await HttpService.GetRange<T>(Identifiers);
                    ResponseCollectionChanged?.Invoke(ResponseCollection);
                    if (ResponseCollection.IsNotNull() && ResponseCollection.HttpResponseMessage.IsSuccessStatusCode &&
                        ResponseCollection.Succeed && ResponseCollection.Response.Status.Succeeded())
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
            //May be removed soon after some testings
            if (IsValidModelState)
            {
                if (BlobDatas.IsNotNullAndNotEmpty())
                {
                    var BlobValidator = new BlobDataCollectionValidator();
                    ValidationResult = await BlobValidator.ValidateAsync(BlobDatas);
                    ValidationResultChanged?.Invoke(ValidationResult);
                    IsValidModelState = ValidationResult.IsValid;
                    IsValidModelStateChanged?.Invoke(IsValidModelState);
                }
            }
            OnAfterValidate?.Invoke();
        }
        public virtual async Task Save()
        {
            if(FormState.IsCreate() || FormState.IsEdit() || FormState.IsCopy())
            {
                if (FormMode.IsSingle())
                {
                    if (FormState.IsCopy() || FormState.IsCreate())
                    {
                        Response = await HttpService.Add<T>(Record);
                    }
                    else if (FormState.IsEdit())
                    {
                        Response = await HttpService.Update<T>(Record);
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
                    }
                }
                else if (FormMode.IsMultiple())
                {
                    if (FormState.IsCopy() || FormState.IsCreate())
                    {
                        ResponseCollection = await HttpService.AddRange<IEnumerable<T>>(Records);
                    }
                    else if (FormState.IsEdit())
                    {
                        ResponseCollection = await HttpService.UpdateRange<IEnumerable<T>>(Records);
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
                    }
                }
            }
        }
        public virtual async Task Delete()
        {
            if (FormState.IsDelete())
            {
                if (FormMode.IsSingle())
                {
                    Response = await HttpService.Delete<T>(Identifier);
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
                    ResponseCollection = await HttpService.DeleteRange<T>(Identifiers);
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
            }
        }
        public virtual IEnumerable<PropertyInfo> GetBlobDatasWithRulesProperties()
        {
            return GetType().GetProperties()
                .Where(w => w.PropertyType == typeof(IMutable<IEnumerable<TBlobData>, FormFileRules>) ||
                        w.PropertyType == typeof(Mutable<IEnumerable<TBlobData>, FormFileRules>));
        }
        public virtual bool HasBlobDatasWithRules()
        {
            return GetBlobDatasWithRulesProperties().Any();
        }
        public virtual void ClearBlobDatasWithRules()
        {
            foreach (var blobDataProperty in GetBlobDatasWithRulesProperties())
            {
                var blobDatas = (IMutable<IEnumerable<TBlobData>, FormFileRules>)blobDataProperty.GetValue(this);
                blobDataProperty.SetValue(this, new Mutable<IEnumerable<TBlobData>, FormFileRules>(Enumerable.Empty<TBlobData>(), blobDatas.Item2));
            }
        }
        public async virtual Task ValidateBlobDatas()
        {
            foreach (var blobDataProperty in GetBlobDatasWithRulesProperties())
            {
                var blobDatas = (IMutable<IEnumerable<TBlobData>, FormFileRules>)blobDataProperty.GetValue(this);
                var validator = new BlobDataCollectionValidator();
                ValidationResult = await validator.ValidateFormFileRulesAsync(blobDatas.Item1, blobDatas.Item2);
                IsValidModelState = ValidationResult.IsValid;
                if (!IsValidModelState)
                {
                    break;
                }
            }
        }
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
            BlobDatas?.Clear();
            BlobDatasChanged?.Invoke(BlobDatas);
            if (HasBlobDatasWithRules())
            {
                ClearBlobDatasWithRules();
            }
            OnAfterReset?.Invoke();
            return Task.CompletedTask;
        }
        public virtual Task Cancel()
        {
            OnAfterCancel?.Invoke();
            return Task.CompletedTask;
        }
    }
}
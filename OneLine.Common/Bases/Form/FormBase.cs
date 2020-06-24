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
    public abstract partial class FormBase<T, TIdentifier, TId, THttpService> :
        IForm<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : IHttpCrudExtendedService<T, TIdentifier>, new()
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
            if(FormMode.IsSingle())
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
                        ClearMutableBlobDatasWithRules();
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
                        ClearMutableBlobDatasWithRules();
                    }
                }
                OnAfterSave?.Invoke();
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
                OnAfterDelete?.Invoke();
            }
        }
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
                    foreach(var property in record.GetType().GetProperties().Where(w => w.PropertyType == typeof(Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)))
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
        public virtual void ClearMutableBlobDatasWithRules()
        {
            foreach (var blobDataProperty in GetMutableBlobDatasWithRulesProperties())
            {
                if(FormMode.IsSingle())
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
        public virtual async Task ValidateMutableBlobDatas()
        {
            foreach (var blobDataProperty in GetMutableBlobDatasWithRulesProperties())
            {
                if(FormMode.IsSingle())
                {
                    var mutableBlobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(Record);
                    var formFileRules = mutableBlobDatas.Item1;
                    var blobDatas = mutableBlobDatas.Item2;
                    var userBlobs = mutableBlobDatas.Item3;
                    ValidationResult = new ValidationResult();
                    var currentBlobsCount = 0;
                    if (formFileRules.ForceUpload)
                    {
                        if (blobDatas.IsNullOrEmpty())
                        {
                            ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                        }
                        else
                        {
                            await InternalValidateBlobDatas(blobDatas, formFileRules);
                        }
                    }
                    else if (!formFileRules.IsRequired && blobDatas.IsNullOrEmpty())
                    {
                        continue;
                    }
                    else if (!formFileRules.IsRequired && blobDatas.IsNotNullAndNotEmpty())
                    {
                        await InternalValidateBlobDatas(blobDatas, formFileRules);
                    }
                    else if (formFileRules.IsRequired)
                    {
                        if (blobDatas.IsNullOrEmpty() && userBlobs.IsNullOrEmpty())
                        {
                            ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                            continue;
                        }
                        if (userBlobs.IsNotNullAndNotEmpty())
                        {
                            currentBlobsCount += userBlobs.Count();
                        }
                        if (blobDatas.IsNotNullAndNotEmpty())
                        {
                            currentBlobsCount += blobDatas.Count();
                        }
                        if (currentBlobsCount >= formFileRules.AllowedMinimunFiles)
                        {
                            if(blobDatas.IsNotNullAndNotEmpty())
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
                            if (blobDatas.IsNullOrEmpty())
                            {
                                ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                            }
                            else
                            {
                                await InternalValidateBlobDatas(blobDatas, formFileRules);
                            }
                        }
                        else if (!formFileRules.IsRequired && blobDatas.IsNullOrEmpty())
                        {
                            continue;
                        }
                        else if (!formFileRules.IsRequired && blobDatas.IsNotNullAndNotEmpty())
                        {
                            await InternalValidateBlobDatas(blobDatas, formFileRules);
                        }
                        else if (formFileRules.IsRequired)
                        {
                            if (blobDatas.IsNullOrEmpty() && userBlobs.IsNullOrEmpty())
                            {
                                ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                                continue;
                            }
                            if (userBlobs.IsNotNullAndNotEmpty())
                            {
                                currentBlobsCount += userBlobs.Count();
                            }
                            if (blobDatas.IsNotNullAndNotEmpty())
                            {
                                currentBlobsCount += blobDatas.Count();
                            }
                            if (currentBlobsCount >= formFileRules.AllowedMinimunFiles)
                            {
                                if (blobDatas.IsNotNullAndNotEmpty())
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
            if (GetMutableBlobDatasWithRulesProperties().IsNotNullAndNotEmpty())
            {
                ClearMutableBlobDatasWithRules();
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
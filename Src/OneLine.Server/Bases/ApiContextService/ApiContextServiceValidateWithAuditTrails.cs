using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Messaging;
using OneLine.Models;
using OneLine.Validations;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub> :
        IApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub>
        where TDbContext : DbContext
        where T : class, new()
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorage, new()
        where TSmtp : class, ISmtp, new()
        where TMessageHub : class, ISendMessageHub, new()
    {
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> ValidateAsync(T record, IValidator validator)
        {
            if (record.IsNull())
            {
                //await CreateAuditrailsAsync(record, $"Record was null on validation operation on method {MethodBase.GetCurrentMethod().Name}");
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "ErrorSavingRecord");
            }
            if (validator.IsNull())
            {
                //await CreateAuditrailsAsync(record, $"Record validator was null on validation operation on method {MethodBase.GetCurrentMethod().Name}");
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "ModelStateInvalid");
            }
            var validationResult = await validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                //await CreateAuditrailsAsync(record, $"Record validation failed on validation operation on method {MethodBase.GetCurrentMethod().Name}");
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, record);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> ValidateRangeAsync(IEnumerable<T> records, IValidator validator)
        {
            if (records.IsNull() || !records.Any())
            {
                //await CreateRangeAuditrailsAsync(records, $"Records collection was null or empty on validation operation on method {MethodBase.GetCurrentMethod().Name}");
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "InvalidModelState");
            }
            if (validator.IsNull())
            {
                //await CreateRangeAuditrailsAsync(records, $"Records validator was null on validation operation on method {MethodBase.GetCurrentMethod().Name}");
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "ModelStateInvalid");
            }
            var validationResult = await validator.ValidateAsync(records);
            if (!validationResult.IsValid)
            {
                //await CreateRangeAuditrailsAsync(records, $"A record/s validation failed in the collection on validation operation on method {MethodBase.GetCurrentMethod().Name}");
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Succeeded, records);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> ValidatedWithBlobsAsync(T record, IValidator validator, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas)
        {
            var apiResponse = await ValidateAsync(record, validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            //Lets check if our files uploaded comply with our rules first
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var formFileUploadedApiResponse = await IsValidBlobDataAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules);
                //Lets check if something went wrong
                if (formFileUploadedApiResponse.Status.Failed() || !formFileUploadedApiResponse.Data)
                {
                    //Does this file upload was required and we are creating it?
                    //Do we forced file upload on update operation?
                    if ((uploadBlobData.FormFileRules.IsRequired && saveOperation.IsAdd()) ||
                        (uploadBlobData.ForceUpload && saveOperation.IsUpdate()))
                    {
                        //await CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} on a field was required on create and the file was null or empty");
                        return record.ToApiResponseFailed("FileUploadIsRequired");
                    }
                    //Does the file is required but we are updating?
                    else if (uploadBlobData.FormFileRules.IsRequired && saveOperation.IsUpdate())
                    {
                        var propertyName = uploadBlobData.FormFileRules.PropertyName;
                        //Lets check that the required file was not deleted on update
                        var propertyValue = record.GetType().GetProperty(propertyName).GetValue(record);
                        if (propertyValue == null)
                        {
                            //await CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required on update and the file was null or empty");
                            return record.ToApiResponseFailed("FileUploadIsRequired");
                        }
                        //Lets validate that userblobs were not overrided with random values 
                        //and that the value parses to user blobs and is valid userblobs
                        try
                        {
                            var userBlobs = JsonConvert.DeserializeObject<IEnumerable<UserBlobs>>(Encoding.UTF8.GetString((byte[])propertyValue));
                            var userBlobsCollectionValidator = new UserBlobsCollectionValidator();
                            var validationResult = await userBlobsCollectionValidator.ValidateAsync(userBlobs);
                            if (!validationResult.IsValid)
                            {
                                //await CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required and the file was null or empty");
                                return new T().ToApiResponseFailed("FileUploadIsRequired");
                            }

                        }
                        catch (Exception)
                        {
                            //await CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required and the file was null or empty");
                            return new T().ToApiResponseFailed("FileUploadIsRequired");
                        }
                    }
                }
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, record);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> ValidatedRangeWithBlobsAsync(IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas)
        {
            var apiResponse = await ValidateRangeAsync(records, validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            foreach (var record in records)
            {
                //Lets check if our files uploaded comply with our rules first
                foreach (var uploadBlobData in uploadBlobDatas)
                {
                    var formFileUploadedApiResponse = await IsValidBlobDataAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules);
                    //Lets check if something went wrong
                    if (formFileUploadedApiResponse.Status.Failed() || !formFileUploadedApiResponse.Data)
                    {
                        //Does this file upload was required and we are creating it?
                        //Do we forced file upload on update operation?
                        if ((uploadBlobData.FormFileRules.IsRequired && saveOperation.IsAdd()) ||
                            (uploadBlobData.ForceUpload && saveOperation.IsUpdate()))
                        {
                            //await CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} on a field was required on create and the file was null or empty");
                            return records.ToApiResponseFailed("FileUploadIsRequired");
                        }
                        //Does the file is required but we are updating?
                        else if (uploadBlobData.FormFileRules.IsRequired && saveOperation.IsUpdate())
                        {
                            var propertyName = uploadBlobData.FormFileRules.PropertyName;
                            //Lets check that the required file was not deleted on update
                            var propertyValue = record.GetType().GetProperty(propertyName).GetValue(record);
                            if (propertyValue == null)
                            {
                                //await CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required on update and the file was null or empty");
                                return records.ToApiResponseFailed("FileUploadIsRequired");
                            }
                            //Lets validate that userblobs were not overrided with random values 
                            //and that the value parses to user blobs and is valid userblobs
                            try
                            {
                                var userBlobs = JsonConvert.DeserializeObject<IEnumerable<UserBlobs>>(Encoding.UTF8.GetString((byte[])propertyValue));
                                var userBlobsCollectionValidator = new UserBlobsCollectionValidator();
                                var validationResult = await userBlobsCollectionValidator.ValidateAsync(userBlobs);
                                if (!validationResult.IsValid)
                                {
                                    //await CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required and the file was null or empty");
                                    return records.ToApiResponseFailed("FileUploadIsRequired");
                                }
                            }
                            catch (Exception)
                            {
                                //await CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required and the file was null or empty or manipulated on the client side");
                                return records.ToApiResponseFailed("FileUploadIsRequired");
                            }
                        }
                    }
                }
            }
            return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Succeeded, records);
        }
    }
}

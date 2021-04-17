using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, TAuditTrails, TUserBlobs, TBlobStorage>
        where TDbContext : DbContext
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
    {
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> ValidateUploadBlobsAsync<T>(T record, IEnumerable<IUploadBlobData> uploadBlobDatas, SaveOperation saveOperation)
        {
            //Lets check if our files uploaded comply with our rules first
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var formFileUploadedApiResponse = await IsValidBlobDataAsync<T>(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules);
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
                            return record.ToApiResponseFailed("FileUploadIsRequired");
                        }
                        //Lets validate that userblobs were not overrided with random values 
                        //and that the value parses to user blobs and is valid userblobs
                        try
                        {
                            var userBlobs = JsonConvert.DeserializeObject<IEnumerable<TUserBlobs>>(Encoding.UTF8.GetString((byte[])propertyValue));
                            var userBlobsCollectionValidator = new UserBlobsCollectionValidator();
                            var validationResult = await userBlobsCollectionValidator.ValidateAsync(userBlobs);
                            if (!validationResult.IsValid)
                            {
                                return Activator.CreateInstance<T>().ToApiResponseFailed("FileUploadIsRequired");
                            }

                        }
                        catch (Exception)
                        {
                            return Activator.CreateInstance<T>().ToApiResponseFailed("FileUploadIsRequired");
                        }
                    }
                }
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, record);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> ValidateRangeUploadBlobsAsync<T>(IEnumerable<T> records, IEnumerable<IUploadBlobData> uploadBlobDatas, SaveOperation saveOperation)
        {
            foreach (var record in records)
            {
                //Lets check if our files uploaded comply with our rules first
                foreach (var uploadBlobData in uploadBlobDatas)
                {
                    var formFileUploadedApiResponse = await IsValidBlobDataAsync<T>(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules);
                    //Lets check if something went wrong
                    if (formFileUploadedApiResponse.Status.Failed() || !formFileUploadedApiResponse.Data)
                    {
                        //Does this file upload was required and we are creating it?
                        //Do we forced file upload on update operation?
                        if ((uploadBlobData.FormFileRules.IsRequired && saveOperation.IsAdd()) ||
                            (uploadBlobData.ForceUpload && saveOperation.IsUpdate()))
                        {
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
                                return records.ToApiResponseFailed("FileUploadIsRequired");
                            }
                            //Lets validate that userblobs were not overrided with random values 
                            //and that the value parses to user blobs and is valid userblobs
                            try
                            {
                                var userBlobs = JsonConvert.DeserializeObject<IEnumerable<TUserBlobs>>(Encoding.UTF8.GetString((byte[])propertyValue));
                                var userBlobsCollectionValidator = new UserBlobsCollectionValidator();
                                var validationResult = await userBlobsCollectionValidator.ValidateAsync(userBlobs);
                                if (!validationResult.IsValid)
                                {
                                    return records.ToApiResponseFailed("FileUploadIsRequired");
                                }
                            }
                            catch (Exception)
                            {
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

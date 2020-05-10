using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
        /// <summary>
        /// Adds a file from the http request
        /// </summary>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> CreateUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IFormFileCollection files, Func<IFormFile, bool> predicate, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, string tableName, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var any = predicate == null ? files.Any() : files.Any(predicate);
            if (!any)
            {
                await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file uploaded", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<UserBlobs>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            if (any && formFileRules != null)
            {
                var isValidFormFileApiResponse = files.IsValidFormFileApiResponse(predicate, formFileRules);
                if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
                {
                    return new ApiResponse<UserBlobs>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message);
                }
            }
            IFormFile file = predicate == null ? files.FirstOrDefault() : files.FirstOrDefault(predicate);
            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueFileName = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
            var filename = $"{uniqueFileName}{fileExtension}";
            await blobStorage.WriteAsync(filename, file.OpenReadStream());
            var userBlob = new UserBlobs().AutoMap(file);
            userBlob.FilePath = filename;
            userBlob.CreatedBy = userId;
            userBlob.CreatedOn = DateTime.Now;
            userBlob.TableName = tableName;
            await dbContext.AddAuditedAsync(userBlob, "File was uploaded", userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlob);
        }
        /// <summary>
        /// Adds file/s from the http request
        /// </summary>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> CreateRangeUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IFormFileCollection files, Func<IFormFile, bool> predicate, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, string tableName, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var any = predicate == null ? files.Any() : files.Any(predicate);
            if (any)
            {
                await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file/s uploaded", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<UserBlobs>>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            if (any && formFileRules != null)
            {
                var isValidFormFileApiResponse = files.IsValidFormFileApiResponse(predicate, formFileRules);
                if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
                {
                    return new ApiResponse<IEnumerable<UserBlobs>>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message);
                }
            }
            var createdOn = DateTime.Now;
            IEnumerable<IFormFile> formFiles = predicate == null ? files.ToList() : files.Where(predicate);
            IList<UserBlobs> uploadedUserBlobs = new List<UserBlobs>();
            foreach (var file in formFiles)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                var uniqueFileName = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
                var filename = $"{uniqueFileName}{fileExtension}";
                await blobStorage.WriteAsync(filename, file.OpenReadStream());
                var userBlob = new UserBlobs().AutoMap(file);
                userBlob.FilePath = filename;
                userBlob.CreatedBy = userId;
                userBlob.CreatedOn = createdOn;
                userBlob.TableName = tableName;
                uploadedUserBlobs.Add(userBlob);
                await dbContext.AddAuditedAsync(userBlob, "File was uploaded", userId, controllerName, actionName, remoteIpAddress);
            }
            var result = await dbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(uploadedUserBlobs.AsEnumerable());
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file and bind the userblob to the model property name
        /// </summary>
        /// <param name="record">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> CreateAndBindUserBlobsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, string propertyName, IFormFileCollection files, Func<IFormFile, bool> predicate, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, string tableName, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            var isFormFileUploadedApiResponse = await dbContext.IsFormFileUploadedAsync(files, predicate, formFileRules, userId, controllerName, actionName, remoteIpAddress);
            if (isFormFileUploadedApiResponse.Status.Failed() || !isFormFileUploadedApiResponse.Data)
            {
                return new ApiResponse<UserBlobs>(isFormFileUploadedApiResponse.Status, isFormFileUploadedApiResponse.Message);
            }
            var blob = await dbContext.CreateUserBlobsAsync(files, predicate, formFileRules, blobStorage, userId, tableName, controllerName, actionName, remoteIpAddress);
            if (blob.Status.Failed())
            {
                return blob;
            }
            record.GetType().GetProperty(propertyName).SetValue(record, JsonConvert.SerializeObject(blob.Data));
            return blob;
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file/s and bind the userblob to the model property name
        /// </summary>
        /// <param name="record">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> CreateRangeAndBindUserBlobsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, string propertyName, IFormFileCollection files, Func<IFormFile, bool> predicate, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, string tableName, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            var isFormFileUploadedApiResponse = await dbContext.IsFormFileUploadedAsync(files, predicate, formFileRules, userId, controllerName, actionName, remoteIpAddress);
            if (isFormFileUploadedApiResponse.Status.Failed() || !isFormFileUploadedApiResponse.Data)
            {
                return new ApiResponse<IEnumerable<UserBlobs>>(isFormFileUploadedApiResponse.Status, isFormFileUploadedApiResponse.Message);
            }
            var blobs = await dbContext.CreateRangeUserBlobsAsync(files, predicate, formFileRules, blobStorage, userId, tableName, controllerName, actionName, remoteIpAddress);
            if (blobs.Status.Failed())
            {
                return blobs;
            }
            record.GetType().GetProperty(propertyName).SetValue(record, JsonConvert.SerializeObject(blobs.Data));
            return blobs;
        }
    }
}

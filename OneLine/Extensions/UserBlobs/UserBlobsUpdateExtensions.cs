using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
        /// <summary>
        /// Updates a blob from the storage. Delete the provide userBlob and upload the new file from the http request.
        /// </summary>
        /// <param name="UserBlobs">The user blob to delete</param>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<IApiResponse<Tuple<UserBlobs, UserBlobs>>> UpdateUserBlobsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IFormFileCollection files, Func<IFormFile, bool> predicate, IUploadFormFile uploadFormFile, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            var addHttpBlobApiResponse = await dbContext.CreateUserBlobsAsync(files, predicate, uploadFormFile.FormFileRules, blobStorage, userId, typeof(T).Name, controllerName, actionName, remoteIpAddress);
            if (addHttpBlobApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<UserBlobs, UserBlobs>>(ApiResponseStatus.Failed, Tuple.Create(addHttpBlobApiResponse.Data, new UserBlobs()), addHttpBlobApiResponse.Message);
            }
            var userBlobs = JsonConvert.DeserializeObject<UserBlobs>(typeof(T).GetProperty(uploadFormFile.FileInputName).GetValue(record).ToString());
            var deleteBlobApiResponse = await dbContext.DeleteUserBlobsAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (addHttpBlobApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<UserBlobs, UserBlobs>>(ApiResponseStatus.Failed, Tuple.Create(addHttpBlobApiResponse.Data, deleteBlobApiResponse.Data), deleteBlobApiResponse.Message);
            }
            return new ApiResponse<Tuple<UserBlobs, UserBlobs>>(ApiResponseStatus.Succeeded, Tuple.Create(addHttpBlobApiResponse.Data, deleteBlobApiResponse.Data));
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file and bind the userblob to the model property name
        /// </summary>
        /// <param name="model">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<IApiResponse<Tuple<UserBlobs, UserBlobs>>> UpdateAndBindUserBlobsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, string propertyName, IFormFileCollection files, Func<IFormFile, bool> predicate, IUploadFormFile uploadFormFile, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            var blob = await dbContext.UpdateUserBlobsAsync(record, files, predicate, uploadFormFile, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            record.GetType().GetProperty(propertyName).SetValue(record, JsonConvert.SerializeObject(blob.Data.Item1));
            return blob;
        }
        /// <summary>
        /// Updates a blob/s from the storage. Delete the provide userBlobs and upload the new files from the http request.
        /// </summary>
        /// <param name="UserBlobs">The user blob/s to delete</param>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>> UpdateRangeUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> UserBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var tableName = UserBlobs?.FirstOrDefault()?.TableName;
            var addMultipleApiResponse = await dbContext.CreateRangeUserBlobsAsync(files, predicate, formFileRules, blobStorage, userId, tableName, controllerName, actionName, remoteIpAddress);
            if (addMultipleApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(addMultipleApiResponse.Data, new List<UserBlobs>().AsEnumerable()), addMultipleApiResponse.Message);
            }
            var deleteMultipleApiResponse = await dbContext.DeleteRangeUserBlobsAsync(UserBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (deleteMultipleApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(addMultipleApiResponse.Data, deleteMultipleApiResponse.Data), deleteMultipleApiResponse.Message);
            }
            return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(addMultipleApiResponse.Data, deleteMultipleApiResponse.Data));
        }
        /// <summary>
        /// Updates a blob/s from the storage. Delete the provide userBlobs and upload the new files from the http request.
        /// </summary>
        /// <param name="UserBlobs">The user blob/s to delete</param>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>> UpdateRangeUserBlobsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, T originalRecord, string propertyName, IFormFileCollection files, Func<IFormFile, bool> predicate, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var addMultipleApiResponse = await dbContext.CreateRangeUserBlobsAsync(files, predicate, formFileRules, blobStorage, userId, typeof(T).Name, controllerName, actionName, remoteIpAddress);
            if (addMultipleApiResponse.Status.Failed())
            {
                return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(addMultipleApiResponse.Data, new List<UserBlobs>().AsEnumerable()), addMultipleApiResponse.Message);
            }
            var userBlobsOriginal = JsonConvert.DeserializeObject<IEnumerable<UserBlobs>>(typeof(T).GetProperty(propertyName).GetValue(originalRecord).ToString());
            var userBlobsOld = JsonConvert.DeserializeObject<IEnumerable<UserBlobs>>(typeof(T).GetProperty(propertyName).GetValue(record).ToString());
            var userBlobsToDelete = userBlobsOriginal.Where(w => !userBlobsOld.Contains(w));
            var deleteMultipleApiResponse = await dbContext.DeleteRangeUserBlobsAsync(userBlobsToDelete, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (deleteMultipleApiResponse.Status.Failed())
            {
                return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(addMultipleApiResponse.Data, deleteMultipleApiResponse.Data), deleteMultipleApiResponse.Message);
            }
            return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(addMultipleApiResponse.Data, deleteMultipleApiResponse.Data));
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file/s and bind the userblob to the model property name
        /// </summary>
        /// <param name="model">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>> UpdateRangeAndBindUserBlobsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, T originalRecord, string propertyName, IFormFileCollection files, Func<IFormFile, bool> predicate, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            var blobs = await dbContext.UpdateRangeUserBlobsAsync(record, originalRecord, propertyName, files, predicate, formFileRules, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            record.GetType().GetProperty(propertyName).SetValue(record, JsonConvert.SerializeObject(blobs.Data.Item1));
            return blobs;
        }
    }
}

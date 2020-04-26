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
        public static async Task<IApiResponse<Tuple<UserBlobs, UserBlobs>>> UpdateUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var addHttpBlobApiResponse = await dbContext.CreateUserBlobsAsync(files, predicate, formFileRules, blobStorage, userId, userBlobs.TableName, controllerName, actionName, remoteIpAddress);
            if (addHttpBlobApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<UserBlobs, UserBlobs>>() { Data = Tuple.Create(addHttpBlobApiResponse.Data, new UserBlobs()), Message = addHttpBlobApiResponse.Message, Status = ApiResponseStatus.Failed };
            }
            var deleteBlobApiResponse = await dbContext.DeleteUserBlobsAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (addHttpBlobApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<UserBlobs, UserBlobs>>() { Data = Tuple.Create(addHttpBlobApiResponse.Data, deleteBlobApiResponse.Data), Message = deleteBlobApiResponse.Message, Status = ApiResponseStatus.Failed };
            }
            return new ApiResponse<Tuple<UserBlobs, UserBlobs>> { Data = Tuple.Create(addHttpBlobApiResponse.Data, deleteBlobApiResponse.Data), Status = ApiResponseStatus.Succeeded };
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file and bind the userblob to the model property name
        /// </summary>
        /// <param name="model">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<IApiResponse<Tuple<UserBlobs, UserBlobs>>> UpdateRangeAndBindUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, object model, string propertyName, UserBlobs userBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var blob = await dbContext.UpdateUserBlobsAsync(userBlobs, files, predicate, formFileRules, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            model.GetType().GetProperty(propertyName).SetValue(model, JsonConvert.SerializeObject(blob.Data.Item1));
            return blob;
        }
        /// <summary>
        /// Updates a blob/s from the storage. Delete the provide userBlobs and upload the new files from the http request.
        /// </summary>
        /// <param name="UserBlobs">The user blob/s to delete</param>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>> UpdateRangeUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> UserBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var tableName = UserBlobs?.FirstOrDefault()?.TableName;
            var addMultipleApiResponse = await dbContext.CreateRangeUserBlobsAsync(files, predicate, formFileRules, blobStorage, userId, tableName, controllerName, actionName, remoteIpAddress);
            if (addMultipleApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>() { Data = Tuple.Create(addMultipleApiResponse.Data, new List<UserBlobs>().AsEnumerable()), Message = addMultipleApiResponse.Message, Status = ApiResponseStatus.Failed };
            }
            var deleteMultipleApiResponse = await dbContext.DeleteRangeUserBlobsAsync(UserBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (deleteMultipleApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>() { Data = Tuple.Create(addMultipleApiResponse.Data, deleteMultipleApiResponse.Data), Message = deleteMultipleApiResponse.Message, Status = ApiResponseStatus.Failed };
            }
            return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>() { Data = Tuple.Create(addMultipleApiResponse.Data, deleteMultipleApiResponse.Data), Status = ApiResponseStatus.Failed };
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file/s and bind the userblob to the model property name
        /// </summary>
        /// <param name="model">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>> UpdateRangeAndBindUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, object model, string propertyName, IEnumerable<UserBlobs> UserBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var blobs = await dbContext.UpdateRangeUserBlobsAsync(UserBlobs, files, predicate, formFileRules, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            model.GetType().GetProperty(propertyName).SetValue(model, JsonConvert.SerializeObject(blobs.Data.Item1));
            return blobs;
        }
    }
}

using Newtonsoft.Json;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
        ///// <summary>
        ///// Updates a blob from the storage. Delete the provide userBlob and upload the new file from the http request.
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to delete</param>
        ///// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        ///// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        ///// <returns></returns>
        //public static async Task<IApiResponse<Tuple<UserBlobs, UserBlobs>>> UpdateUserBlobsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IUploadBlobData uploadBlobData, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        //    where T : class
        //{
        //    var addHttpBlobApiResponse = await dbContext.CreateUserBlobsAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules, blobStorage, userId, typeof(T).Name, controllerName, actionName, remoteIpAddress);
        //    if (addHttpBlobApiResponse.Status == ApiResponseStatus.Failed)
        //    {
        //        return new ApiResponse<Tuple<UserBlobs, UserBlobs>>(ApiResponseStatus.Failed, Tuple.Create(addHttpBlobApiResponse.Data, new UserBlobs()), addHttpBlobApiResponse.Message);
        //    }
        //    var propertyName = uploadBlobData.BlobDatas.FirstOrDefault().InputName;
        //    var userBlobs = JsonConvert.DeserializeObject<UserBlobs>(typeof(T).GetProperty(propertyName).GetValue(record).ToString());
        //    var deleteBlobApiResponse = await dbContext.DeleteUserBlobsAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
        //    if (addHttpBlobApiResponse.Status == ApiResponseStatus.Failed)
        //    {
        //        return new ApiResponse<Tuple<UserBlobs, UserBlobs>>(ApiResponseStatus.Failed, Tuple.Create(addHttpBlobApiResponse.Data, deleteBlobApiResponse.Data), deleteBlobApiResponse.Message);
        //    }
        //    return new ApiResponse<Tuple<UserBlobs, UserBlobs>>(ApiResponseStatus.Succeeded, Tuple.Create(addHttpBlobApiResponse.Data, deleteBlobApiResponse.Data));
        //}
        ///// <summary>
        ///// This is a helper method which tryies to upload a file and bind the userblob to the model property name
        ///// </summary>
        ///// <param name="model">The model to bind the result</param>
        ///// <param name="propertyName">The property name of the model to bind</param>
        ///// <param name="predicate">The form file field name</param>
        ///// <param name="formFileRules">The rules for the files</param>
        ///// <returns></returns>
        //public static async Task<IApiResponse<Tuple<UserBlobs, UserBlobs>>> UpdateAndBindUserBlobsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, string propertyName, IUploadBlobData uploadBlobData, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        //    where T : class
        //{
        //    var blob = await dbContext.UpdateUserBlobsAsync(record, uploadBlobData, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
        //    record.GetType().GetProperty(propertyName).SetValue(record, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(blob.Data.Item1)));
        //    return blob;
        //}
        ///// <summary>
        ///// Updates a blob/s from the storage. Delete the provide userBlobs and upload the new files from the http request.
        ///// </summary>
        ///// <param name="UserBlobs">The user blob/s to delete</param>
        ///// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        ///// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        ///// <returns></returns>
        //public static async Task<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>> UpdateRangeUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> UserBlobs, IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        //{
        //    var tableName = UserBlobs?.FirstOrDefault()?.TableName;
        //    var addMultipleApiResponse = await dbContext.CreateRangeUserBlobsAsync(blobDatas, formFileRules, blobStorage, userId, tableName, controllerName, actionName, remoteIpAddress);
        //    if (addMultipleApiResponse.Status == ApiResponseStatus.Failed)
        //    {
        //        return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(addMultipleApiResponse.Data, new List<UserBlobs>().AsEnumerable()), addMultipleApiResponse.Message);
        //    }
        //    var deleteMultipleApiResponse = await dbContext.DeleteRangeUserBlobsAsync(UserBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
        //    if (deleteMultipleApiResponse.Status == ApiResponseStatus.Failed)
        //    {
        //        return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(addMultipleApiResponse.Data, deleteMultipleApiResponse.Data), deleteMultipleApiResponse.Message);
        //    }
        //    return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(addMultipleApiResponse.Data, deleteMultipleApiResponse.Data));
        //}
        /// <summary>
        /// Updates a blob/s from the storage. Delete the provide userBlobs and upload the new files from the http request.
        /// </summary>
        /// <param name="UserBlobs">The user blob/s to delete</param>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IList<IEnumerable<UserBlobs>>>> UpdateUserBlobsRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, T originalRecord, IEnumerable<IUploadBlobData> uploadBlobDatas, IBlobStorage blobStorage, string userId, string tableName, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            var userBlobsList = new List<IEnumerable<UserBlobs>>();
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                IEnumerable<UserBlobs> userBlobsOriginal;
                byte[] userBlobByteArrayOriginal;
                var userBlobOriginal = typeof(T).GetProperty(uploadBlobData.PropertyName).GetValue(originalRecord);
                if (userBlobOriginal.IsNull())
                {
                    continue;
                }
                else
                {
                    userBlobByteArrayOriginal = (byte[])userBlobOriginal;
                    userBlobsOriginal = JsonConvert.DeserializeObject<IEnumerable<UserBlobs>>(Encoding.UTF8.GetString(userBlobByteArrayOriginal));
                }
                IEnumerable<UserBlobs> userBlobsNew = Enumerable.Empty<UserBlobs>();
                byte[] userBlobByteArrayNew;
                var userBlobNew = typeof(T).GetProperty(uploadBlobData.PropertyName).GetValue(record);
                if (userBlobNew.IsNotNull())
                {
                    userBlobByteArrayNew = (byte[])userBlobNew;
                    userBlobsNew = JsonConvert.DeserializeObject<IEnumerable<UserBlobs>>(Encoding.UTF8.GetString(userBlobByteArrayNew));
                }
                var userBlobsToDelete = userBlobsOriginal.Where(w => !userBlobsNew.Contains(w));
                var deleteMultipleApiResponse = await dbContext.DeleteRangeUserBlobsAsync(userBlobsToDelete, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                userBlobsList.Add(deleteMultipleApiResponse.Data);
                if (deleteMultipleApiResponse.Status.Failed() && !string.IsNullOrWhiteSpace(deleteMultipleApiResponse.Message))
                {
                    return new ApiResponse<IList<IEnumerable<UserBlobs>>>(deleteMultipleApiResponse.Status, userBlobsList, deleteMultipleApiResponse.Message);
                }
            }
            return await dbContext.CreateAndBindUserBlobsRangeAsync(record, uploadBlobDatas, blobStorage, userId, tableName, controllerName, actionName, remoteIpAddress);
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file/s and bind the userblob to the model property name
        /// </summary>
        /// <param name="model">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IList<IEnumerable<UserBlobs>>>> UpdateAndBindUserBlobsRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, T originalRecord, IEnumerable<IUploadBlobData> uploadBlobDatas, IBlobStorage blobStorage, string userId, string tableName, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var isFormFileUploadedApiResponse = await dbContext.IsValidBlobDataAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules, userId, controllerName, actionName, remoteIpAddress);
                if (isFormFileUploadedApiResponse.Status.Failed() || !isFormFileUploadedApiResponse.Data)
                {
                    return new ApiResponse<IList<IEnumerable<UserBlobs>>>(isFormFileUploadedApiResponse.Status, isFormFileUploadedApiResponse.Message);
                }
            }
            return await dbContext.UpdateUserBlobsRangeAsync(record, originalRecord, uploadBlobDatas, blobStorage, userId, tableName, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
        }
    }
}

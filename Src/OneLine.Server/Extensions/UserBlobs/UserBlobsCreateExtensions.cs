using Newtonsoft.Json;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
        ///// <summary>
        ///// Adds a file from the http request
        ///// </summary>
        ///// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        ///// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        ///// <returns></returns>
        //public static async Task<IApiResponse<UserBlobs>> CreateUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules, IBlobStorage blobStorage, string userId, string tableName, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        //{
        //    var any = blobDatas.IsNotNullAndNotEmpty();
        //    if (!any)
        //    {
        //        await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file uploaded", userId, controllerName, actionName, remoteIpAddress);
        //        return new ApiResponse<UserBlobs>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
        //    }
        //    if (any && formFileRules.IsNotNull())
        //    {
        //        var isValidFormFileApiResponse = blobDatas.IsValidBlobDataApiResponse(formFileRules);
        //        if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
        //        {
        //            return new ApiResponse<UserBlobs>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message);
        //        }
        //    }
        //    var file = blobDatas.FirstOrDefault();
        //    var fileExtension = Path.GetExtension(file.Name);
        //    var uniqueFileName = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
        //    var filename = $"{uniqueFileName}{fileExtension}";
        //    await blobStorage.WriteAsync(filename, file.Data);
        //    var userBlob = new UserBlobs().AutoMap(file);
        //    userBlob.ContentType = file.Type;
        //    userBlob.FilePath = filename;
        //    userBlob.CreatedBy = userId;
        //    userBlob.CreatedOn = DateTime.Now;
        //    userBlob.TableName = tableName;
        //    await dbContext.AddAuditedAsync(userBlob, "File was uploaded", userId, controllerName, actionName, remoteIpAddress);
        //    var result = await dbContext.SaveChangesAsync();
        //    return result.TransactionResultApiResponse(userBlob);
        //}
        /// <summary>
        /// Checks that blobdatas aren't empty or null.
        /// Check wether the form rules are satified by the blobdatas.
        /// Adds blob datas to the blob storage. 
        /// Converts the blob datas to user blobs.
        /// UserBlobs are attached to the dbcontext . 
        /// No save of db context is made by this method.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="blobDatas"></param>
        /// <param name="formFileRules"></param>
        /// <param name="blobStorage"></param>
        /// <param name="userId"></param>
        /// <param name="tableName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<UserBlobs>> AddUserBlobsRangeAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<BlobData> blobDatas, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, string tableName, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var any = blobDatas.IsNotNull() && blobDatas.Any();
            if (!any)
            {
                await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file/s uploaded", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<UserBlobs>();      
            }
            if (any && formFileRules != null)
            {
                var isValidFormFileApiResponse = blobDatas.IsValidBlobDataApiResponse(formFileRules);
                if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
                {
                    return Enumerable.Empty<UserBlobs>();
                }
            }
            var createdOn = DateTime.Now;
            IList<UserBlobs> uploadedUserBlobs = new List<UserBlobs>();
            foreach (var file in blobDatas)
            {
                var fileExtension = Path.GetExtension(file.Name);
                var uniqueFileName = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
                var filename = $"{uniqueFileName}{fileExtension}";
                await blobStorage.WriteAsync(filename, file.Data);
                var userBlob = new UserBlobs().AutoMap(file);
                userBlob.UserBlobId = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
                userBlob.FileName = file.Name;
                userBlob.FilePath = filename;
                userBlob.Length = file.Size;
                userBlob.UserIdentifier = userId;
                userBlob.CreatedBy = userId;
                userBlob.CreatedOn = createdOn;
                userBlob.TableName = tableName;
                uploadedUserBlobs.Add(userBlob);
                await dbContext.AddAuditedAsync(userBlob, "File was uploaded", userId, controllerName, actionName, remoteIpAddress);
            }
            return uploadedUserBlobs;
        }
        /// <summary>
        /// Checks that blobdatas aren't empty or null.
        /// Check wether the form rules are satified by the blobdatas.
        /// Adds blob datas to the blob storage. 
        /// Converts the blob datas to user blobs.
        /// UserBlobs are attached to the dbcontext . 
        /// The dbcontext saves the userblobs.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="blobDatas"></param>
        /// <param name="formFileRules"></param>
        /// <param name="blobStorage"></param>
        /// <param name="userId"></param>
        /// <param name="tableName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> CreateUserBlobsRangeAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<BlobData> blobDatas, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, string tableName, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var any = blobDatas.IsNotNull() && blobDatas.Any();
            if (!any)
            {
                await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file/s uploaded", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<UserBlobs>>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            if (any && formFileRules != null)
            {
                var isValidFormFileApiResponse = blobDatas.IsValidBlobDataApiResponse(formFileRules);
                if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
                {
                    return new ApiResponse<IEnumerable<UserBlobs>>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message);
                }
            }
            var createdOn = DateTime.Now;
            IList<UserBlobs> uploadedUserBlobs = new List<UserBlobs>();
            foreach (var file in blobDatas)
            {
                var fileExtension = Path.GetExtension(file.Name);
                var uniqueFileName = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
                var filename = $"{uniqueFileName}{fileExtension}";
                await blobStorage.WriteAsync(filename, file.Data);
                var userBlob = new UserBlobs().AutoMap(file);
                userBlob.UserBlobId = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
                userBlob.FileName = file.Name;
                userBlob.FilePath = filename;
                userBlob.Length = file.Size;
                userBlob.UserIdentifier = userId;
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
        /// Checks that blobdatas aren't empty or null.
        /// Check wether the form rules are satified by the blobdatas.
        /// Adds blob datas to the blob storage. 
        /// Converts the blob datas to user blobs.
        /// UserBlobs are attached to the dbcontext. 
        /// The dbcontext saves the userblobs.
        /// The UserBlobs collection reference value is serialized to json string.
        /// Then converted to a byte array.
        /// Then binded to the reference PropertyName.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="record"></param>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="blobStorage"></param>
        /// <param name="userId"></param>
        /// <param name="tableName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<IList<IEnumerable<UserBlobs>>>> CreateAndBindUserBlobsRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IEnumerable<IUploadBlobData> uploadBlobDatas, IBlobStorage blobStorage, string userId, string tableName, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            var userBlobsList = new List<IEnumerable<UserBlobs>>();
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var isFormFileUploadedApiResponse = await dbContext.IsValidBlobDataAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules, userId, controllerName, actionName, remoteIpAddress);
                if (isFormFileUploadedApiResponse.Status.Failed() || !isFormFileUploadedApiResponse.Data)
                {
                    return new ApiResponse<IList<IEnumerable<UserBlobs>>>(isFormFileUploadedApiResponse.Status, isFormFileUploadedApiResponse.Message);
                }
            }
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var userBlobs = await dbContext.AddUserBlobsRangeAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules, blobStorage, userId, tableName, controllerName, actionName, remoteIpAddress);
                userBlobsList.Add(userBlobs);
                record.GetType().GetProperty(uploadBlobData.PropertyName).SetValue(record, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userBlobs)));
            }
            return new ApiResponse<IList<IEnumerable<UserBlobs>>>(ApiResponseStatus.Succeeded, userBlobsList);
        }        
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OneLine.Bases;
using OneLine.Constants;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static class UserBlobsExtensions
    {
        #region Helper Methods

        /// <summary>
        /// Checks is the current user is the blob owner
        /// </summary>
        /// <param name="UserBlobs"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<bool>> IsBlobOwnerAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null)
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblob is null on method IsBlobOwner", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>() { Status = ApiResponseStatus.Failed, Message = "FileNotFound" };
            }
            var result = dbContext.GetOneAsync(new Identifier<string> { Model = userBlobs.UserBlobId }, userId) != null;
            if (!result)
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "User is not the blob owner", userId, controllerName, actionName, remoteIpAddress);
            }
            return new ApiResponse<bool>() { Status = ApiResponseStatus.Succeeded, Data = result };
        }
        /// <summary>
        /// Checks if the current user is the blob owner and the file exists
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<bool>> IsBlobOwnerAndFileExistsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null)
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblob is null on method IsBlobOwnerAndFileExistsAsync", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>() { Status = ApiResponseStatus.Failed, Message = "FileNotFound" };
            }
            if (!ignoreBlobOwner)
            {
                var isBlobOwner = await dbContext.IsBlobOwnerAsync(userBlobs, userId, controllerName, actionName, remoteIpAddress);
                if (isBlobOwner.Status == ApiResponseStatus.Failed || !isBlobOwner.Data)
                {
                    return new ApiResponse<bool>() { Status = ApiResponseStatus.Failed, Message = isBlobOwner.Message };
                }
            }
            var fileExist = await blobStorage.ExistsAsync(userBlobs.FilePath);
            if (!fileExist)
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "File Not found on method IsBlobOwnerAndFileExists", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>() { Status = ApiResponseStatus.Failed, Data = fileExist, Message = "FileNotFound" };
            }
            await dbContext.CreateAuditrailsAsync(userBlobs, "File was found", userId, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<bool>() { Status = ApiResponseStatus.Succeeded, Data = true };
        }
        /// <summary>
        /// Check if a expected file is uploaded with specified file rules if any
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="formFileRules"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<bool>> IsFormFileUploadedAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var any = predicate == null ? files.Any() : files.Any(predicate);
            if (!any)
            {
                await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file uploaded", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>() { Status = ApiResponseStatus.Failed, Message = "FileIsNullOrEmpty", Data = false };
            }
            if (any && formFileRules != null)
            {
                var isValidFormFileApiResponse = files.IsValidFormFileApiResponse(predicate, formFileRules);
                if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
                {
                    return new ApiResponse<bool>() { Status = ApiResponseStatus.Failed, Message = isValidFormFileApiResponse.Message, Data = false };
                }
            }
            return new ApiResponse<bool>() { Status = ApiResponseStatus.Succeeded, Data = true };
        }

        #endregion

        #region Create Methods

        /// <summary>
        /// Adds a file from the http request
        /// </summary>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> CreateAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var any = predicate == null ? files.Any() : files.Any(predicate);
            if (!any)
            {
                await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file uploaded", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<UserBlobs>() { Status = ApiResponseStatus.Failed, Message = "FileIsNullOrEmpty" };
            }
            if (any && formFileRules != null)
            {
                var isValidFormFileApiResponse = files.IsValidFormFileApiResponse(predicate, formFileRules);
                if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
                {
                    return new ApiResponse<UserBlobs>() { Status = ApiResponseStatus.Failed, Message = isValidFormFileApiResponse.Message };
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
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> CreateRangeAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var any = predicate == null ? files.Any() : files.Any(predicate);
            if (any)
            {
                await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file/s uploaded", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<UserBlobs>>() { Status = ApiResponseStatus.Failed, Message = "FileIsNullOrEmpty" };
            }
            if (any && formFileRules != null)
            {
                var isValidFormFileApiResponse = files.IsValidFormFileApiResponse(predicate, formFileRules);
                if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
                {
                    return new ApiResponse<IEnumerable<UserBlobs>>() { Status = ApiResponseStatus.Failed, Message = isValidFormFileApiResponse.Message };
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
                uploadedUserBlobs.Add(userBlob);
                await dbContext.AddAuditedAsync(userBlob, "File was uploaded", userId, controllerName, actionName, remoteIpAddress);
            }
            var result = await dbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(uploadedUserBlobs.AsEnumerable());
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file and bind the userblob to the model property name
        /// </summary>
        /// <param name="model">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> CreateAndBindAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, object model, string propertyName, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isFormFileUploadedApiResponse = await dbContext.IsFormFileUploadedAsync(files, predicate, formFileRules, userId, controllerName, actionName, remoteIpAddress);
            if (isFormFileUploadedApiResponse.Status == ApiResponseStatus.Failed || !isFormFileUploadedApiResponse.Data)
            {
                return new ApiResponse<UserBlobs>() { Status = isFormFileUploadedApiResponse.Status, Message = isFormFileUploadedApiResponse.Message };
            }
            var blob = await dbContext.CreateAsync(files, predicate, formFileRules, blobStorage, userId, controllerName, actionName, remoteIpAddress);
            if (blob.Status == ApiResponseStatus.Failed)
            {
                return blob;
            }
            model.GetType().GetProperty(propertyName).SetValue(model, JsonConvert.SerializeObject(blob.Data));
            return blob;
        }
        /// <summary>
        /// This is a helper method which tryies to upload a file/s and bind the userblob to the model property name
        /// </summary>
        /// <param name="model">The model to bind the result</param>
        /// <param name="propertyName">The property name of the model to bind</param>
        /// <param name="predicate">The form file field name</param>
        /// <param name="formFileRules">The rules for the files</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> CreateRangeAndBindAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, object model, string propertyName, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isFormFileUploadedApiResponse = await dbContext.IsFormFileUploadedAsync(files, predicate, formFileRules, userId, controllerName, actionName, remoteIpAddress);
            if (isFormFileUploadedApiResponse.Status == ApiResponseStatus.Failed || !isFormFileUploadedApiResponse.Data)
            {
                return new ApiResponse<IEnumerable<UserBlobs>>() { Status = isFormFileUploadedApiResponse.Status, Message = isFormFileUploadedApiResponse.Message };
            }
            var blobs = await dbContext.CreateRangeAsync(files, predicate, formFileRules, blobStorage, userId, controllerName, actionName, remoteIpAddress);
            if (blobs.Status == ApiResponseStatus.Failed)
            {
                return blobs;
            }
            model.GetType().GetProperty(propertyName).SetValue(model, JsonConvert.SerializeObject(blobs.Data));
            return blobs;
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// Deletes a blob from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to delete</param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> DeleteAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool IgnoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlobs, blobStorage, userId, IgnoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<UserBlobs>() { Status = ApiResponseStatus.Failed, Message = isBlobOwnerAndFileExists.Message };
            }
            await blobStorage.DeleteAsync(userBlobs.FilePath);
            await dbContext.RemoveAuditedAsync(userBlobs, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlobs);
        }
        /// <summary>
        /// Deletes a blob from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to delete</param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> DeleteForcedAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            await blobStorage.DeleteAsync(userBlobs.FilePath);
            await dbContext.RemoveAuditedAsync(userBlobs, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlobs);
        }
        /// <summary>
        /// Deletes blob/s from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blobs to delete</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> DeleteRangeAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "UserBlobs is null or empty on method DeleteRange", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<UserBlobs>>() { Status = ApiResponseStatus.Failed, Message = "FileNotFound" };
            }
            foreach (var userBlob in userBlobs)
            {
                var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                {
                    return new ApiResponse<IEnumerable<UserBlobs>>() { Status = ApiResponseStatus.Failed, Message = isBlobOwnerAndFileExists.Message };
                }
                await blobStorage.DeleteAsync(userBlob.FilePath);
                await dbContext.RemoveAuditedAsync(userBlob, userId, controllerName, actionName, remoteIpAddress);
            }
            var result = await dbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlobs);
        }
        /// <summary>
        /// Deletes blob/s from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blobs to delete</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> DeleteRangeForcedAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "UserBlobs is null or empty on method DeleteRange", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<UserBlobs>>() { Status = ApiResponseStatus.Failed, Message = "FileNotFound" };
            }
            foreach (var userBlob in userBlobs)
            {
                await blobStorage.DeleteAsync(userBlob.FilePath);
                await dbContext.RemoveAuditedAsync(userBlob, userId, controllerName, actionName, remoteIpAddress);
            }
            var result = await dbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlobs);
        }
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="blobStorage"></param>
        /// <param name="userId"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<IApiResponse<IEnumerable<UserBlobs>>>>> DeleteUserBlobsFromObjectAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, object model, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            bool anyError = false;
            var apiResponse = new ApiResponse<IEnumerable<IApiResponse<IEnumerable<UserBlobs>>>>();
            var deletedUserBlobs = new List<IApiResponse<IEnumerable<UserBlobs>>>();
            foreach (var property in model
                                .GetType()
                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .Where(w => w.PropertyType.IsAssignableFrom(typeof(IUserBlobs)) ||
                                            w.PropertyType.IsAssignableFrom(typeof(IEnumerable<IUserBlobs>)) ||
                                            w.PropertyType.IsAssignableFrom(typeof(UserBlobs)) ||
                                            w.PropertyType.IsAssignableFrom(typeof(IEnumerable<UserBlobs>))
                                      )
                    )
            {
                if (property.PropertyType.IsAssignableFrom(typeof(UserBlobs)))
                {
                    var UserBlobs = (UserBlobs)property.GetValue(model);
                    var deletedBlobs = await dbContext.DeleteForcedAsync(UserBlobs, blobStorage, userId, controllerName, actionName, remoteIpAddress);
                    if (!anyError)
                    {
                        if (deletedBlobs.Status == ApiResponseStatus.Failed)
                        {
                            anyError = true;
                        }
                    }
                    deletedUserBlobs.Add(new ApiResponse<IEnumerable<UserBlobs>>() { Data = new List<UserBlobs>() { deletedBlobs.Data }, Message = deletedBlobs.Message, Status = deletedBlobs.Status });
                }
                else
                {
                    var UserBlobs = (IEnumerable<UserBlobs>)property.GetValue(model);
                    var deletedBlobs = await dbContext.DeleteRangeForcedAsync(UserBlobs, blobStorage, userId, controllerName, actionName, remoteIpAddress);
                    if (!anyError)
                    {
                        if (deletedBlobs.Status == ApiResponseStatus.Failed)
                        {
                            anyError = true;
                        }
                    }
                    deletedUserBlobs.Add(deletedBlobs);
                }
            }
            var apiResponseStatus = anyError ? ApiResponseStatus.Failed : ApiResponseStatus.Succeeded;
            apiResponse.Data = deletedUserBlobs;
            apiResponse.Status = apiResponseStatus;
            return apiResponse;
        }
        /// <summary>
        ///  This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="blobsStorage"></param>
        /// <param name="userId"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<TEntity, IEnumerable<UserBlobs>>>> DeleteUserBlobsFromEntityAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, TEntity entity, IBlobStorage blobsStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            if (entity== null)
            {
                return new ApiResponse<Tuple<TEntity, IEnumerable<UserBlobs>>> () { Status = ApiResponseStatus.Failed, Data = Tuple.Create <TEntity, IEnumerable <UserBlobs>> (entity, null), Message = "ErrorDeletingRecord" };  
            }
            //Helper method that deletes all files in a object
            var DeleteBlobsApiResponse = await dbContext.DeleteUserBlobsFromObjectAsync(entity, blobsStorage, userId, controllerName, actionName, remoteIpAddress);
            var deletedUserBlobs = DeleteBlobsApiResponse.Data.SelectMany(s => s.Data.Select(x => x));
            if (DeleteBlobsApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<TEntity, IEnumerable <UserBlobs>>> () { Status = ApiResponseStatus.Failed, Data = Tuple.Create(entity, deletedUserBlobs), Message = DeleteBlobsApiResponse.Message };
            }
            await dbContext.RemoveAuditedAsync(entity, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            var message = result.IsSuccesSave() ? "RecordSavedSuccessfully" : "ErrorSavingRecord";
            return new ApiResponse<Tuple<TEntity, IEnumerable <UserBlobs>>> () { Status = ApiResponseStatus.Succeeded, Data = Tuple.Create(entity, deletedUserBlobs), Message = message };
        }
       
        #endregion

        #region Update Methods

        /// <summary>
        /// Updates a blob from the storage. Delete the provide userBlob and upload the new file from the http request.
        /// </summary>
        /// <param name="UserBlobs">The user blob to delete</param>
        /// <param name="predicate">Set this param if you want to read a file from a specific form field name.</param>
        /// <param name="formFileRules">The rules to apply to the file uploaded.</param>
        /// <returns></returns>
        public static async Task<IApiResponse<Tuple<UserBlobs, UserBlobs>>> UpdateAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var addHttpBlobApiResponse = await dbContext.CreateAsync(files, predicate, formFileRules, blobStorage, userId, controllerName, actionName, remoteIpAddress);
            if (addHttpBlobApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<UserBlobs, UserBlobs>>() { Data = Tuple.Create(addHttpBlobApiResponse.Data, new UserBlobs()), Message = addHttpBlobApiResponse.Message, Status = ApiResponseStatus.Failed };
            }
            var deleteBlobApiResponse = await dbContext.DeleteAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
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
        public static async Task<IApiResponse<Tuple<UserBlobs, UserBlobs>>> UpdateAndBindAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, object model, string propertyName, UserBlobs userBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var blob = await dbContext.UpdateAsync(userBlobs, files, predicate, formFileRules, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
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
        public static async Task<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>> UpdateAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> UserBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var addMultipleApiResponse = await dbContext.CreateRangeAsync(files, predicate, formFileRules, blobStorage, userId, controllerName, actionName, remoteIpAddress);
            if (addMultipleApiResponse.Status == ApiResponseStatus.Failed)
            {
                return new ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>() { Data = Tuple.Create(addMultipleApiResponse.Data, new List<UserBlobs>().AsEnumerable()), Message = addMultipleApiResponse.Message, Status = ApiResponseStatus.Failed };
            }
            var deleteMultipleApiResponse = await dbContext.DeleteRangeAsync(UserBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
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
        public static async Task<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>> UpdateAndBindAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, object model, string propertyName, IEnumerable<UserBlobs> UserBlobs, IFormFileCollection files, Func<IFormFile, bool> predicate, FormFileRules formFileRules, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var blobs = await dbContext.UpdateAsync(UserBlobs, files, predicate, formFileRules, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            model.GetType().GetProperty(propertyName).SetValue(model, JsonConvert.SerializeObject(blobs.Data.Item1));
            return blobs;
        }

        #endregion

        #region Search User Blobs

        /// <summary>
        /// Search user blobs
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="SearchTerm"></param>
        /// <param name="UserBlobId"></param>
        /// <param name="LangCode"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortBy"></param>
        /// <param name="Descending"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<UserBlobs>>> SearchPaged(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, string SearchTerm, IList<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        {
            var query = dbContext.UserBlobs.AsQueryable();
            query = query.Where(w => UserBlobId.Any() ?
                            UserBlobId.Contains(w.UserBlobId) :
                            !string.IsNullOrWhiteSpace(SearchTerm) ?
                            w.ContentDisposition.Contains(SearchTerm) ||
                            w.ContentType.Contains(SearchTerm) ||
                            w.CreatedBy.Contains(SearchTerm) ||
                            w.FileName.Contains(SearchTerm) ||
                            w.FilePath.Contains(SearchTerm) ||
                            w.Name.Contains(SearchTerm) ||
                            w.UserBlobId.Contains(SearchTerm) :
                            true);
            if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
            {
                if (Descending.Value)
                {
                    query = query.OrderByPropertyDescending(SortBy);
                }
                else
                {
                    query = query.OrderByProperty(SortBy);
                }
            }
            else
            {
                query.OrderByDescending(o => o.CreatedBy);
            }
            return query.ToApiResponsePaged(Page, PageSize, out Count);
        }
        /// <summary>
        /// Gets a list of user blobs
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="SearchTerm"></param>
        /// <param name="UserBlobId"></param>
        /// <param name="LangCode"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortBy"></param>
        /// <param name="Descending"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<UserBlobs>>> ListPaged(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, string SearchTerm, IList<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        {
            var query = dbContext.UserBlobs.AsQueryable();
            query = query.Where(w => UserBlobId.Any() ?
                            UserBlobId.Contains(w.UserBlobId) :
                            !string.IsNullOrWhiteSpace(SearchTerm) ?
                            w.ContentDisposition.Contains(SearchTerm) ||
                            w.ContentType.Contains(SearchTerm) ||
                            w.CreatedBy.Contains(SearchTerm) ||
                            w.FileName.Contains(SearchTerm) ||
                            w.FilePath.Contains(SearchTerm) ||
                            w.Name.Contains(SearchTerm) ||
                            w.UserBlobId.Contains(SearchTerm) :
                            true);
            if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
            {
                if (Descending.Value)
                {
                    query = query.OrderByPropertyDescending(SortBy);
                }
                else
                {
                    query = query.OrderByProperty(SortBy);
                }
            }
            else
            {
                query.OrderByDescending(o => o.CreatedBy);
            }
            return query.ToApiResponsePaged(Page, PageSize, out Count);
        }
        /// <summary>
        /// Gets a user blob
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> GetOneAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier)
        {
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                return new UserBlobs().ToApiResponseFailed("RecordNotFound");
            }
            var record = await dbContext.UserBlobs.FindAsync(identifier.Model);
            return record == null ? record.ToApiResponseFailed("RecordNotFound") : record.ToApiResponse();
        }
        /// <summary>
        /// Gets a user blob
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> GetOneAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, string userId)
        {
            var record = await dbContext.UserBlobs.FirstOrDefaultAsync(x => x.UserBlobId == identifier.Model && x.CreatedBy == userId);
            return record == null ? record.ToApiResponseFailed("RecordNotFound") : record.ToApiResponse();
        }

        #endregion

        #region Read Blobs As Stream From Storage

        /// <summary>
        /// Read a blob as stream from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<Stream> ReadBlobAsStreamAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return null;
            }
            await dbContext.CreateAuditrailsAsync(userBlobs, "File was found and read as stream", userId, controllerName, actionName, remoteIpAddress);
            return await blobStorage.OpenReadAsync(userBlobs.FilePath);
        }
        /// <summary>
        /// Read a blobs as streams from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blobs to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Stream>> ReadBlobAsStreamAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblobs is null or empty on method ReadBlobAsStream", userId, controllerName, actionName, remoteIpAddress);
                return null;
            }
            var streams = new List<Stream>();
            foreach (var userBlob in userBlobs)
            {
                streams.Add(await dbContext.ReadBlobAsStreamAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return streams;
        }
        /// <summary>
        /// Read a blob as a stream api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IApiResponse<Stream>> ReadBlobAsStreamApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<Stream>() { Status = ApiResponseStatus.Failed, Message = isBlobOwnerAndFileExists.Message };
            }
            await dbContext.CreateAuditrailsAsync(userBlobs, "UserBlob was readed as api response", userId, controllerName, actionName, remoteIpAddress);
            var stream = await blobStorage.OpenReadAsync(userBlobs.FilePath);
            return new ApiResponse<Stream>() { Data = stream, Status = ApiResponseStatus.Succeeded };
        }
        /// <summary>
        /// Read a blob as a stream api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<IApiResponse<Stream>>> ReadBlobAsStreamApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblobs is null or empty on method ReadBlobAsStreamApiResponse", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<IApiResponse<Stream>>();
            }
            var streams = new List<IApiResponse<Stream>>();
            foreach (var userBlob in userBlobs)
            {
                streams.Add(await dbContext.ReadBlobAsStreamApiResponseAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return streams;
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a file stream result from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <param name="zipFilename">The zip file name. (Remember include *.zip extension)</param>
        /// <returns></returns>
        public static async Task<Stream> ReadBlobsIntoZipFolderStreamAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    foreach (var userBlob in userBlobs)
                    {
                        var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                        if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                        {
                            return null;
                        }
                        var stream = await blobStorage.OpenReadAsync(userBlob.FilePath);
                        var entry = zip.CreateEntry(userBlob.FileName);
                        using (var entryStream = entry.Open())
                        {
                            await stream.CopyToAsync(entryStream);
                        }
                    }
                }
                zipStream.Position = 0;
                await dbContext.CreateAuditrailsAsync(userBlobs, "A list of UserBlobs were readed into a compressed zip folder as stream result", userId, controllerName, actionName, remoteIpAddress);
                return zipStream;
            }
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a file stream result from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <param name="zipFilename">The zip file name. (Remember include *.zip extension)</param>
        /// <returns></returns>
        public static async Task<IApiResponse<Stream>> ReadBlobsIntoZipFolderStreamApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblobs is null or empty on method ReadBlobsIntoZipFolderStreamApiResponseAsync", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<Stream>() { Message = "FileIsNullOrEmpty" };
            }
            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    foreach (var userBlob in userBlobs)
                    {
                        var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                        if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                        {
                            return new ApiResponse<Stream>() { Message = isBlobOwnerAndFileExists.Message, Status = ApiResponseStatus.Failed };
                        }
                        var stream = await blobStorage.OpenReadAsync(userBlob.FilePath);
                        var entry = zip.CreateEntry(userBlob.FileName);
                        using (var entryStream = entry.Open())
                        {
                            await stream.CopyToAsync(entryStream);
                        }
                    }
                }
                zipStream.Position = 0;
                await dbContext.CreateAuditrailsAsync(userBlobs, "A list of UserBlobs were readed into a compressed zip folder as stream result", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<Stream>() { Data = zipStream, Status = ApiResponseStatus.Succeeded };
            }
        }

        #endregion

        #region Read Blobs as Byte Array From Storage

        /// <summary>
        /// Read a blob as a byte array from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<byte[]> ReadBlobAsByteArrayAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return (await dbContext.ReadBlobAsStreamAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress)).ToByteArray();
        }
        /// <summary>
        /// Read a blob as a byte array from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<byte[]>> ReadBlobAsByteArrayAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblobs is null or empty on method ReadBlobAsByteArrayAsync", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<byte[]>();
            }
            var BytesArray = new List<byte[]>();
            foreach (var userBlob in userBlobs)
            {
                BytesArray.Add((await dbContext.ReadBlobAsStreamAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress)).ToByteArray());
            }
            return BytesArray;
        }
        /// <summary>
        /// Read a blob as a byte array api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IApiResponse<byte[]>> ReadBlobAsByteArrayApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<byte[]>() { Status = ApiResponseStatus.Failed, Message = isBlobOwnerAndFileExists.Message };
            }
            await dbContext.CreateAuditrailsAsync(userBlobs, "File was found and readed as api response byte array", userId, controllerName, actionName, remoteIpAddress);
            var bytes = await blobStorage.ReadBytesAsync(userBlobs.FilePath);
            return new ApiResponse<byte[]>() { Data = bytes, Status = ApiResponseStatus.Succeeded };
        }
        /// <summary>
        /// Read a blob as a byte array api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<IApiResponse<byte[]>>> ReadBlobAsByteArrayApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblobs is null or empty on method ReadBlobAsByteArrayApiResponseAsync", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<IApiResponse<byte[]>>();
            }
            var bytesArray = new List<IApiResponse<byte[]>>();
            foreach (var userBlob in userBlobs)
            {
                bytesArray.Add(await dbContext.ReadBlobAsByteArrayApiResponseAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return bytesArray;
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a byte array from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <returns></returns>
        public static async Task<byte[]> ReadBlobsIntoZipFolderByteArrayAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return (await dbContext.ReadBlobsIntoZipFolderStreamAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress)).ToByteArray();
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a byte array from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <returns></returns>
        public static async Task<ApiResponse<byte[]>> ReadBlobsIntoZipFolderByteArrayApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var streamApiResponse = await dbContext.ReadBlobsIntoZipFolderStreamApiResponseAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<byte[]>() { Data = streamApiResponse.Data?.ToByteArray(), Message = streamApiResponse.Message, Status = streamApiResponse.Status };
        }

        #endregion

        #region Read Blobs as Base 64 String From Storage

        /// <summary>
        /// Read a blob as a base 64 string from the storage.
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<string> ReadBlobAsBase64Async(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return Convert.ToBase64String(await dbContext.ReadBlobAsByteArrayAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
        }
        /// <summary>
        /// Read a blob as a base 64 string from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> ReadBlobAsBase64Async(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblobs is null or empty on method ReadBlobAsBase64Async", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<string>();
            }
            var BytesArray = new List<string>();
            foreach (var userBlob in userBlobs)
            {
                BytesArray.Add(await dbContext.ReadBlobAsBase64Async(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return BytesArray;
        }
        /// <summary>
        /// Read a blob as a base 64 string api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IApiResponse<string>> ReadBlobAsBase64ApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = isBlobOwnerAndFileExists.Message };
            }
            await dbContext.CreateAuditrailsAsync(userBlobs, "File was found and readed as api response base 64 string", userId, controllerName, actionName, remoteIpAddress);
            var base64 = Convert.ToBase64String(await blobStorage.ReadBytesAsync(userBlobs.FilePath));
            return new ApiResponse<string>() { Data = base64, Status = ApiResponseStatus.Succeeded };
        }
        /// <summary>
        /// Read a blob as a base 64 string api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<IApiResponse<string>>> ReadBlobAsBase64ApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs == null || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "Userblobs is null or empty on method ReadBlobAsBase64ApiResponseAsync", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<IApiResponse<string>>();
            }
            var base64Array = new List<IApiResponse<string>>();
            foreach (var userBlob in userBlobs)
            {
                base64Array.Add(await dbContext.ReadBlobAsBase64ApiResponseAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return base64Array;
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <returns></returns>
        public static async Task<string> ReadBlobsIntoZipFolderBase64Async(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return Convert.ToBase64String(await dbContext.ReadBlobsIntoZipFolderByteArrayAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IApiResponse<string>> ReadBlobsIntoZipFolderBase64ApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var streamApiResponse = await dbContext.ReadBlobsIntoZipFolderByteArrayApiResponseAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<string>() { Data = Convert.ToBase64String(streamApiResponse.Data), Message = streamApiResponse.Message, Status = streamApiResponse.Status };
        }

        #endregion

        #region Read Blobs as File Stream Result From Storage

        /// <summary>
        /// Read a blob as a file stream result from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IActionResult> ReadBlobAsFileStreamResult(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return isBlobOwnerAndFileExists.ToJson();
            }
            var stream = await blobStorage.OpenReadAsync(userBlobs.FilePath);
            await dbContext.CreateAuditrailsAsync(userBlobs, "UserBlob was readed as file stream result", userId, controllerName, actionName, remoteIpAddress);
            var contentType = !string.IsNullOrWhiteSpace(userBlobs.ContentType) ? userBlobs.ContentType : MimeTypes.Application.OctetStream;
            return new FileStreamResult(stream, contentType)
            {
                FileDownloadName = Path.GetFileName(userBlobs.FileName)
            };
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a file stream result from the storage.
        /// If no zipFilename is provided then the zip file name will be auto generated.
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <param name="zipFilename">The zip file name. (Remember include *.zip extension)</param>
        /// <returns></returns>
        public static async Task<IActionResult> ReadBlobsIntoZipFolderAsFileStreamResult(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, string zipFilename = null, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    foreach (var userBlob in userBlobs)
                    {
                        var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                        if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                        {
                            return isBlobOwnerAndFileExists.ToJson();
                        }
                        var stream = await blobStorage.OpenReadAsync(userBlob.FilePath);
                        var entry = zip.CreateEntry(userBlob.FileName);
                        using (var entryStream = entry.Open())
                        {
                            await stream.CopyToAsync(entryStream);
                        }
                    }
                }
                zipStream.Position = 0;
                await dbContext.CreateAuditrailsAsync(userBlobs, "A list of UserBlobs were readed as stream into a compressed zip folder and downloaded as a file stream result", userId, controllerName, actionName, remoteIpAddress);
                zipFilename = string.IsNullOrWhiteSpace(zipFilename) ? $"{".zip".GenerateUniqueFileName()}" : zipFilename;
                return new FileStreamResult(zipStream, MimeTypes.Application.Zip)
                {
                    FileDownloadName = zipFilename
                };
            }
        }

        #endregion
    }
}
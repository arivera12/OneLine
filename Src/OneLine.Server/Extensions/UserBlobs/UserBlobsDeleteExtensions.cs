using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
        /// <summary>
        /// Deletes a blob from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to delete</param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> DeleteUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, bool IgnoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlobs, blobStorage, userId, IgnoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<UserBlobs>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
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
        public static async Task<IApiResponse<UserBlobs>> DeleteForcedUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, UserBlobs userBlobs, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> DeleteRangeUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "UserBlobs is null or empty on method DeleteRange", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<UserBlobs>>(ApiResponseStatus.Succeeded, Enumerable.Empty<UserBlobs>());
            }
            foreach (var userBlob in userBlobs)
            {
                var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                if (isBlobOwnerAndFileExists.Status.Failed() && isBlobOwnerAndFileExists.Message == "FileNotFound")
                {
                    continue;
                }
                else if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
                {
                    return new ApiResponse<IEnumerable<UserBlobs>>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
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
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> DeleteRangeForcedUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "UserBlobs is null or empty on method DeleteRange", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<UserBlobs>>(ApiResponseStatus.Failed, "FileNotFound");
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
                                .Where(w => w.PropertyType.IsAssignableFrom(typeof(IEnumerable<UserBlobs>))
                                      )
                    )
            {
                if (property.PropertyType.IsAssignableFrom(typeof(UserBlobs)))
                {
                    var UserBlobs = (UserBlobs)property.GetValue(model);
                    var deletedBlobs = await dbContext.DeleteForcedUserBlobsAsync(UserBlobs, blobStorage, userId, controllerName, actionName, remoteIpAddress);
                    if (!anyError)
                    {
                        if (deletedBlobs.Status.Failed())
                        {
                            anyError = true;
                        }
                    }
                    deletedUserBlobs.Add(new ApiResponse<IEnumerable<UserBlobs>>(deletedBlobs.Status, new List<UserBlobs>() { deletedBlobs.Data }, deletedBlobs.Message));
                }
                else
                {
                    var UserBlobs = (IEnumerable<UserBlobs>)property.GetValue(model);
                    var deletedBlobs = await dbContext.DeleteRangeForcedUserBlobsAsync(UserBlobs, blobStorage, userId, controllerName, actionName, remoteIpAddress);
                    if (!anyError)
                    {
                        if (deletedBlobs.Status.Failed())
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
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="blobsStorage"></param>
        /// <param name="userId"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<T, IEnumerable<UserBlobs>>>> DeleteUserBlobsFromEntityAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, IBlobStorage blobsStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            if (entity == null)
            {
                return new ApiResponse<Tuple<T, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<T, IEnumerable<UserBlobs>>(entity, null), "ErrorDeletingRecord");
            }
            //Helper method that deletes all files in a object
            var DeleteBlobsApiResponse = await dbContext.DeleteUserBlobsFromObjectAsync(entity, blobsStorage, userId, controllerName, actionName, remoteIpAddress);
            var deletedUserBlobs = DeleteBlobsApiResponse.Data.SelectMany(s => s.Data.Select(x => x));
            if (DeleteBlobsApiResponse.Status.Failed())
            {
                return new ApiResponse<Tuple<T, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(entity, deletedUserBlobs), DeleteBlobsApiResponse.Message);
            }
            await dbContext.RemoveAuditedAsync(entity, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            var message = result.Succeeded() ? "RecordSavedSuccessfully" : "ErrorSavingRecord";
            return new ApiResponse<Tuple<T, IEnumerable<UserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(entity, deletedUserBlobs), message);
        }
        /// <summary>
        ///  This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="blobsStorage"></param>
        /// <param name="userId"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<T, IEnumerable<UserBlobs>>>> DeleteUserBlobsFromEntityAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, IBlobStorage blobsStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (predicate == null)
            {
                return new ApiResponse<Tuple<T, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<T, IEnumerable<UserBlobs>>(new T(), null), "ErrorDeletingRecord");
            }
            var record = dbContext.Set<T>().Where(predicate).FirstOrDefault();
            return await dbContext.DeleteUserBlobsFromEntityAsync(record, blobsStorage, userId, controllerName, actionName, remoteIpAddress);
        }
        /// <summary>
        ///  This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="blobsStorage"></param>
        /// <param name="userId"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<UserBlobs>>>> DeleteUserBlobsFromEntitiesAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, IBlobStorage blobsStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            if (entities == null || !entities.Any())
            {
                return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<IEnumerable<T>, IEnumerable<UserBlobs>>(entities, null), "ErrorDeletingRecord");
            }
            var deletedUserBlobs = new List<UserBlobs>();
            foreach (var entity in entities)
            {
                //Helper method that deletes all files in a object
                var DeleteBlobsApiResponse = await dbContext.DeleteUserBlobsFromObjectAsync(entity, blobsStorage, userId, controllerName, actionName, remoteIpAddress);
                deletedUserBlobs.AddRange(DeleteBlobsApiResponse.Data.SelectMany(s => s.Data.Select(x => x)));
                if (DeleteBlobsApiResponse.Status.Failed())
                {
                    return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(entities, deletedUserBlobs.AsEnumerable()), DeleteBlobsApiResponse.Message);
                }
                await dbContext.RemoveAuditedAsync(entity, userId, controllerName, actionName, remoteIpAddress);
            }
            var result = await dbContext.SaveChangesAsync();
            var message = result.Succeeded() ? "RecordSavedSuccessfully" : "ErrorSavingRecord";
            return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(entities, deletedUserBlobs.AsEnumerable()), message);
        }
        /// <summary>
        ///  This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="blobsStorage"></param>
        /// <param name="userId"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        public static async Task<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<UserBlobs>>>> DeleteUserBlobsFromEntitiesAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, IBlobStorage blobsStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            if (predicate == null )
            {
                return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<UserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<IEnumerable<T>, IEnumerable<UserBlobs>>(Enumerable.Empty<T>(), null), "ErrorDeletingRecord");
            }
            var records = dbContext.Set<T>().Where(predicate);
            return await dbContext.DeleteUserBlobsFromEntitiesAsync(records, blobsStorage, userId, controllerName, actionName, remoteIpAddress);
        }
    }
}

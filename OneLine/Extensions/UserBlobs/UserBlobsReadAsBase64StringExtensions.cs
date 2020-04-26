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
        public static async Task<IEnumerable<string>> ReadBlobRangeAsBase64Async(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
        public static async Task<IEnumerable<IApiResponse<string>>> ReadBlobRangeAsBase64ApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
        public static async Task<string> ReadBlobRangeIntoZipFolderBase64Async(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return Convert.ToBase64String(await dbContext.ReadBlobRangeIntoZipFolderByteArrayAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IApiResponse<string>> ReadBlobRangeIntoZipFolderBase64ApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var streamApiResponse = await dbContext.ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<string>() { Data = Convert.ToBase64String(streamApiResponse.Data), Message = streamApiResponse.Message, Status = streamApiResponse.Status };
        }
    }
}

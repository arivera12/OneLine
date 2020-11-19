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
        public static async Task<string> ReadBlobAsBase64Async(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return Convert.ToBase64String(await dbContext.ReadBlobAsByteArrayAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
        }
        /// <summary>
        /// Read a blob as a base 64 string from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> ReadBlobRangeAsBase64Async(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Identifiers is null or empty on method ReadBlobAsBase64Async", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<string>();
            }
            var BytesArray = new List<string>();
            foreach (var identifier in identifiers)
            {
                BytesArray.Add(await dbContext.ReadBlobAsBase64Async(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return BytesArray;
        }
        /// <summary>
        /// Read a blob as a base 64 string api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IApiResponse<string>> ReadBlobAsBase64ApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<string>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            var userBlobsApiResponse = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
            await dbContext.CreateAuditrailsAsync(userBlobsApiResponse.Data, "File was found and readed as api response base 64 string", userId, controllerName, actionName, remoteIpAddress);
            var base64 = Convert.ToBase64String(await blobStorage.ReadBytesAsync(userBlobsApiResponse.Data.FilePath));
            return new ApiResponse<string>(ApiResponseStatus.Succeeded, data:base64);
        }
        /// <summary>
        /// Read a blob as a base 64 string api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<IApiResponse<string>>> ReadBlobRangeAsBase64ApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Identifiers is null or empty on method ReadBlobAsBase64ApiResponseAsync", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<IApiResponse<string>>();
            }
            var base64Array = new List<IApiResponse<string>>();
            foreach (var identifier in identifiers)
            {
                base64Array.Add(await dbContext.ReadBlobAsBase64ApiResponseAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return base64Array;
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <returns></returns>
        public static async Task<string> ReadBlobRangeIntoZipFolderBase64Async(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return Convert.ToBase64String(await dbContext.ReadBlobRangeIntoZipFolderByteArrayAsync(identifiers, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IApiResponse<string>> ReadBlobRangeIntoZipFolderBase64ApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var streamApiResponse = await dbContext.ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(identifiers, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<string>(streamApiResponse.Status, data: Convert.ToBase64String(streamApiResponse.Data), message: streamApiResponse.Message);
        }
    }
}

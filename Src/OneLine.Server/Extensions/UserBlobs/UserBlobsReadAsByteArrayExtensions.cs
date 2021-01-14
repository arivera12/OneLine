using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
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
        public static async Task<IEnumerable<byte[]>> ReadBlobRangeAsByteArrayAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
                return new ApiResponse<byte[]>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            await dbContext.CreateAuditrailsAsync(userBlobs, "File was found and readed as api response byte array", userId, controllerName, actionName, remoteIpAddress);
            var bytes = await blobStorage.ReadBytesAsync(userBlobs.FilePath);
            return new ApiResponse<byte[]>(ApiResponseStatus.Succeeded, bytes);
        }
        /// <summary>
        /// Read a blob as a byte array api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<IApiResponse<byte[]>>> ReadBlobRangeAsByteArrayApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
        public static async Task<byte[]> ReadBlobRangeIntoZipFolderByteArrayAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return (await dbContext.ReadBlobRangeIntoZipFolderStreamAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress)).ToByteArray();
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a byte array from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <returns></returns>
        public static async Task<ApiResponse<byte[]>> ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var streamApiResponse = await dbContext.ReadBlobRangeIntoZipFolderStreamApiResponseAsync(userBlobs, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<byte[]>(streamApiResponse.Status, streamApiResponse.Data?.ToByteArray(), streamApiResponse.Message);
        }
    }
}

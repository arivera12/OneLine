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
        /// <returns></returns>
        public static async Task<byte[]> ReadBlobAsByteArrayAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return (await dbContext.ReadBlobAsStreamAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress)).ToByteArray();
        }
        /// <summary>
        /// Read a blob as a byte array from the storage
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<byte[]>> ReadBlobRangeAsByteArrayAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Identifiers is null or empty on method ReadBlobAsByteArrayAsync", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<byte[]>();
            }
            var BytesArray = new List<byte[]>();
            foreach (var identifier in identifiers)
            {
                BytesArray.Add((await dbContext.ReadBlobAsStreamAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress)).ToByteArray());
            }
            return BytesArray;
        }
        /// <summary>
        /// Read a blob as a byte array api response from the storage
        /// </summary>
        /// <returns></returns>
        public static async Task<IApiResponse<byte[]>> ReadBlobAsByteArrayApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<byte[]>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            var userBlobsApiResponse = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
            await dbContext.CreateAuditrailsAsync(userBlobsApiResponse.Data, "File was found and readed as api response byte array", userId, controllerName, actionName, remoteIpAddress);
            var bytes = await blobStorage.ReadBytesAsync(userBlobsApiResponse.Data.FilePath);
            return new ApiResponse<byte[]>(ApiResponseStatus.Succeeded, bytes);
        }
        /// <summary>
        /// Read a blob as a byte array api response from the storage
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<IApiResponse<byte[]>>> ReadBlobRangeAsByteArrayApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Identifier is null or empty on method ReadBlobAsByteArrayApiResponseAsync", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<IApiResponse<byte[]>>();
            }
            var bytesArray = new List<IApiResponse<byte[]>>();
            foreach (var identifier in identifiers)
            {
                bytesArray.Add(await dbContext.ReadBlobAsByteArrayApiResponseAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return bytesArray;
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a byte array from the storage
        /// </summary>
        /// <returns></returns>
        public static async Task<byte[]> ReadBlobRangeIntoZipFolderByteArrayAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return (await dbContext.ReadBlobRangeIntoZipFolderStreamAsync(identifiers, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress)).ToByteArray();
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a byte array from the storage
        /// </summary>
        /// <returns></returns>
        public static async Task<ApiResponse<byte[]>> ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var streamApiResponse = await dbContext.ReadBlobRangeIntoZipFolderStreamApiResponseAsync(identifiers, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<byte[]>(streamApiResponse.Status, streamApiResponse.Data?.ToByteArray(), streamApiResponse.Message);
        }
    }
}

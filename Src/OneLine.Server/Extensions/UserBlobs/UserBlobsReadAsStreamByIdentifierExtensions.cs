using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
        /// <summary>
        /// Read a blob as stream from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<Stream> ReadBlobAsStreamAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return null;
            }
            var userBlobsApiResponse = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
            await dbContext.CreateAuditrailsAsync(userBlobsApiResponse.Data, "File was found and read as stream", userId, controllerName, actionName, remoteIpAddress);
            return await blobStorage.OpenReadAsync(userBlobsApiResponse.Data.FilePath);
        }
        /// <summary>
        /// Read a blobs as streams from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blobs to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Stream>> ReadBlobRangeAsStreamAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
        public static async Task<IApiResponse<Stream>> ReadBlobAsStreamApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<Stream>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            var userBlobsApiResponse = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
            await dbContext.CreateAuditrailsAsync(userBlobsApiResponse.Data, "UserBlob was readed as api response", userId, controllerName, actionName, remoteIpAddress);
            var stream = await blobStorage.OpenReadAsync(userBlobsApiResponse.Data.FilePath);
            return new ApiResponse<Stream>(ApiResponseStatus.Succeeded, stream);
        }
        /// <summary>
        /// Read a blob as a stream api response from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to search</param>
        /// <returns></returns>
        public static async Task<IEnumerable<IApiResponse<Stream>>> ReadBlobRangeAsStreamApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Identifier is null or empty on method ReadBlobAsStreamApiResponse", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<IApiResponse<Stream>>();
            }
            var streams = new List<IApiResponse<Stream>>();
            foreach (var identifier in identifiers)
            {
                streams.Add(await dbContext.ReadBlobAsStreamApiResponseAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress));
            }
            return streams;
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a file stream result from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <param name="zipFilename">The zip file name. (Remember include *.zip extension)</param>
        /// <returns></returns>
        public static async Task<Stream> ReadBlobRangeIntoZipFolderStreamAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Identifier is null or empty on method ReadBlobRangeIntoZipFolderStreamAsync", userId, controllerName, actionName, remoteIpAddress);
                return null;
            }
            var userBlobs = new List<UserBlobs>();
            using MemoryStream zipStream = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var identifier in identifiers)
                {
                    var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                    {
                        return null;
                    }
                    var userBlobsApiResponse = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
                    userBlobs.Add(userBlobsApiResponse.Data);
                    var stream = await blobStorage.OpenReadAsync(userBlobsApiResponse.Data.FilePath);
                    var entry = zip.CreateEntry(userBlobsApiResponse.Data.FileName);
                    using var entryStream = entry.Open();
                    await stream.CopyToAsync(entryStream);
                }
            }
            zipStream.Position = 0;
            await dbContext.CreateAuditrailsAsync(userBlobs, "A list of UserBlobs were readed into a compressed zip folder as stream result", userId, controllerName, actionName, remoteIpAddress);
            return zipStream;
        }
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a file stream result from the storage
        /// </summary>
        /// <param name="UserBlobsViewModels">The user blob to search</param>
        /// <param name="zipFilename">The zip file name. (Remember include *.zip extension)</param>
        /// <returns></returns>
        public static async Task<IApiResponse<Stream>> ReadBlobRangeIntoZipFolderStreamApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Identifier is null or empty on method ReadBlobsIntoZipFolderStreamApiResponseAsync", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<Stream>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            var userBlobs = new List<UserBlobs>();
            using MemoryStream zipStream = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var identifier in identifiers)
                {
                    var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(identifier, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                    {
                        return new ApiResponse<Stream>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
                    }
                    var userBlobsApiResponse = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
                    userBlobs.Add(userBlobsApiResponse.Data);
                    var stream = await blobStorage.OpenReadAsync(userBlobsApiResponse.Data.FilePath);
                    var entry = zip.CreateEntry(userBlobsApiResponse.Data.FileName);
                    using var entryStream = entry.Open();
                    await stream.CopyToAsync(entryStream);
                }
            }
            zipStream.Position = 0;
            await dbContext.CreateAuditrailsAsync(userBlobs, "A list of UserBlobs were readed into a compressed zip folder as stream result", userId, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<Stream>(ApiResponseStatus.Succeeded, zipStream);
        }
    }
}

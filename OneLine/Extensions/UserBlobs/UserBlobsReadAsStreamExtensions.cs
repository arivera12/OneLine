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
        public static async Task<IEnumerable<Stream>> ReadBlobRangeAsStreamAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
        public static async Task<IEnumerable<IApiResponse<Stream>>> ReadBlobRangeAsStreamApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
        public static async Task<Stream> ReadBlobRangeIntoZipFolderStreamAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
        public static async Task<IApiResponse<Stream>> ReadBlobRangeIntoZipFolderStreamApiResponseAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
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
    }
}

using Microsoft.AspNetCore.Mvc;
using OneLine.Bases;
using OneLine.Constants;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
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
                return isBlobOwnerAndFileExists.ToJsonActionResult();
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
        public static async Task<IActionResult> ReadBlobRangeIntoZipFolderAsFileStreamResult(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<UserBlobs> userBlobs, IBlobStorage blobStorage, string userId, string zipFilename = null, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            using MemoryStream zipStream = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var userBlob in userBlobs)
                {
                    var isBlobOwnerAndFileExists = await dbContext.IsBlobOwnerAndFileExistsAsync(userBlob, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
                    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                    {
                        return isBlobOwnerAndFileExists.ToJsonActionResult();
                    }
                    var stream = await blobStorage.OpenReadAsync(userBlob.FilePath);
                    var entry = zip.CreateEntry(userBlob.FileName);
                    using var entryStream = entry.Open();
                    await stream.CopyToAsync(entryStream);
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
}

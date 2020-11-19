using Microsoft.EntityFrameworkCore;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using Storage.Net.Blobs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
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
                return new ApiResponse<bool>(ApiResponseStatus.Failed, "FileNotFound");
            }
            var result = (await dbContext.UserBlobs.FirstOrDefaultAsync(x => x.UserBlobId == userBlobs.UserBlobId && x.CreatedBy == userId)) != null;
            if (result)
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "User is the blob owner", userId, controllerName, actionName, remoteIpAddress);
            }
            else
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "User is not the blob owner", userId, controllerName, actionName, remoteIpAddress);
            }
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, result);
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
                return new ApiResponse<bool>(ApiResponseStatus.Failed, "FileNotFound");
            }
            if (!ignoreBlobOwner)
            {
                var isBlobOwner = await dbContext.IsBlobOwnerAsync(userBlobs, userId, controllerName, actionName, remoteIpAddress);
                if (isBlobOwner.Status == ApiResponseStatus.Failed || !isBlobOwner.Data)
                {
                    return new ApiResponse<bool>(ApiResponseStatus.Failed, isBlobOwner.Message);
                }
            }
            var fileExist = await blobStorage.ExistsAsync(userBlobs.FilePath);
            if (!fileExist)
            {
                await dbContext.CreateAuditrailsAsync(userBlobs, "File Not found on method IsBlobOwnerAndFileExists", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>(ApiResponseStatus.Failed, fileExist, "FileNotFound");
            }
            await dbContext.CreateAuditrailsAsync(userBlobs, "File was found", userId, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, true);
        }
        /// <summary>
        /// Check if a expected file is uploaded with specified file rules if any
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="formFileRules"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<bool>> IsValidBlobDataAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<BlobData> blobDatas, FormFileRules formFileRules, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var isValidFormFileApiResponse = blobDatas.IsValidBlobDataApiResponse(formFileRules);
            if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
            {
                await dbContext.CreateAuditrailsAsync(blobDatas, isValidFormFileApiResponse.Message, userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message, true);
            }
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, true);
        }
    }
}

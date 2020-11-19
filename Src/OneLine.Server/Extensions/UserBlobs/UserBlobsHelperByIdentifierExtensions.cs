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
        /// <returns></returns>
        public static async Task<IApiResponse<bool>> IsBlobOwnerAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifier.IsNull() || identifier.Model.IsNull())
            {
                await dbContext.CreateAuditrailsAsync(identifier, "Identifier is null on method IsBlobOwner", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>(ApiResponseStatus.Failed, "FileNotFound");
            }
            var result = (await dbContext.UserBlobs.AsNoTracking().FirstOrDefaultAsync(x => x.UserBlobId == identifier.Model && x.CreatedBy == userId)) != null;
            if (result)
            {
                await dbContext.CreateAuditrailsAsync(identifier, "User is the blob owner", userId, controllerName, actionName, remoteIpAddress);
            }
            else
            {
                await dbContext.CreateAuditrailsAsync(identifier, "User is not the blob owner", userId, controllerName, actionName, remoteIpAddress);
            }
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, result);
        }
        /// <summary>
        /// Checks if the current user is the blob owner and the file exists
        /// </summary>
        /// <returns></returns>
        public static async Task<IApiResponse<bool>> IsBlobOwnerAndFileExistsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifier.IsNull() || identifier.Model.IsNull())
            {
                await dbContext.CreateAuditrailsAsync(identifier, "Identifier is null on method IsBlobOwnerAndFileExistsAsync", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>(ApiResponseStatus.Failed, "FileNotFound");
            }
            if (!ignoreBlobOwner)
            {
                var isBlobOwner = await dbContext.IsBlobOwnerAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
                if (isBlobOwner.Status == ApiResponseStatus.Failed || !isBlobOwner.Data)
                {
                    return new ApiResponse<bool>(ApiResponseStatus.Failed, isBlobOwner.Message);
                }
            }
            var userBlobsApiResponse = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
            if(userBlobsApiResponse.Status.Failed())
            {
                return new ApiResponse<bool>(userBlobsApiResponse.Status, false, userBlobsApiResponse.Message);
            }
            var fileExist = await blobStorage.ExistsAsync(userBlobsApiResponse.Data.FilePath);
            if (!fileExist)
            {
                await dbContext.CreateAuditrailsAsync(userBlobsApiResponse.Data, "File Not found on method IsBlobOwnerAndFileExists", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<bool>(ApiResponseStatus.Failed, fileExist, "FileNotFound");
            }
            await dbContext.CreateAuditrailsAsync(userBlobsApiResponse.Data, "File was found", userId, controllerName, actionName, remoteIpAddress);
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, true);
        }
    }
}

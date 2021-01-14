using OneLine.Bases;
using OneLine.Models;
using Storage.Net.Blobs;
using System.Collections.Generic;
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
        public static async Task<IApiResponse<UserBlobs>> DeleteUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, bool IgnoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var record = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
            if (record.Status.Failed())
            {
                return new ApiResponse<UserBlobs>(record.Status, record.Data, record.Message, record.ErrorMessages);
            }
            return await dbContext.DeleteUserBlobsAsync(record.Data, blobStorage, userId, IgnoreBlobOwner, controllerName, actionName, remoteIpAddress);
        }

        /// <summary>
        /// Deletes a blob from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blob to delete</param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> DeleteForcedUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var record = await dbContext.GetOneUserBlobsAsync(identifier, userId, controllerName, actionName, remoteIpAddress);
            if (record.Status.Failed())
            {
                return new ApiResponse<UserBlobs>(record.Status, record.Data, record.Message, record.ErrorMessages);
            }
            return await dbContext.DeleteForcedUserBlobsAsync(record.Data, blobStorage, userId, controllerName, actionName, remoteIpAddress);
        }

        /// <summary>
        /// Deletes blob/s from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blobs to delete</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> DeleteRangeUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, bool ignoreBlobOwner = false, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var records = await dbContext.GetRangeUserBlobsAsync(identifiers, userId, controllerName, actionName, remoteIpAddress);
            if (records.Status.Failed())
            {
                return new ApiResponse<IEnumerable<UserBlobs>>(records.Status, records.Data, records.Message, records.ErrorMessages);
            }
            return await dbContext.DeleteRangeUserBlobsAsync(records.Data, blobStorage, userId, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
        }
        /// <summary>
        /// Deletes blob/s from the storage
        /// </summary>
        /// <param name="UserBlobs">The user blobs to delete</param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> DeleteRangeForcedUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobStorage, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var records = await dbContext.GetRangeUserBlobsAsync(identifiers, userId, controllerName, actionName, remoteIpAddress);
            if (records.Status.Failed())
            {
                return new ApiResponse<IEnumerable<UserBlobs>>(records.Status, records.Data, records.Message, records.ErrorMessages);
            }
            return await dbContext.DeleteRangeForcedUserBlobsAsync(records.Data, blobStorage, userId, controllerName, actionName, remoteIpAddress);
        }
    }
}

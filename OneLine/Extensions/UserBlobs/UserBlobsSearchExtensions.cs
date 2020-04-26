using Microsoft.EntityFrameworkCore;
using OneLine.Bases;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static partial class UserBlobsExtensions
    {
        /// <summary>
        /// Search user blobs
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="SearchTerm"></param>
        /// <param name="UserBlobId"></param>
        /// <param name="LangCode"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortBy"></param>
        /// <param name="Descending"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<UserBlobs>>> SearchUserBlobsPaged(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, string SearchTerm, IEnumerable<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        {
            var query = dbContext.UserBlobs.AsQueryable();
            query = query.Where(w => UserBlobId.Any() ?
                            UserBlobId.Contains(w.UserBlobId) :
                            !string.IsNullOrWhiteSpace(SearchTerm) ?
                            w.ContentDisposition.Contains(SearchTerm) ||
                            w.ContentType.Contains(SearchTerm) ||
                            w.CreatedBy.Contains(SearchTerm) ||
                            w.FileName.Contains(SearchTerm) ||
                            w.FilePath.Contains(SearchTerm) ||
                            w.Name.Contains(SearchTerm) ||
                            w.UserBlobId.Contains(SearchTerm) :
                            true);
            if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
            {
                if (Descending.Value)
                {
                    query = query.OrderByPropertyDescending(SortBy);
                }
                else
                {
                    query = query.OrderByProperty(SortBy);
                }
            }
            else
            {
                query.OrderByDescending(o => o.CreatedBy);
            }
            return query.ToApiResponsePaged(Page, PageSize, out Count);
        }
        /// <summary>
        /// Search user blobs
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="SearchTerm"></param>
        /// <param name="UserBlobId"></param>
        /// <param name="LangCode"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortBy"></param>
        /// <param name="Descending"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<UserBlobs>>> SearchOwnsUserBlobsPaged(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, string userId, string SearchTerm, IEnumerable<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        {
            var query = dbContext.UserBlobs.AsQueryable();
            query = query.Where(w => UserBlobId.Any() ?
                            UserBlobId.Contains(w.UserBlobId) &&
                            w.CreatedBy == userId :
                            !string.IsNullOrWhiteSpace(SearchTerm) ?
                            w.ContentDisposition.Contains(SearchTerm) ||
                            w.ContentType.Contains(SearchTerm) ||
                            w.FileName.Contains(SearchTerm) ||
                            w.FilePath.Contains(SearchTerm) ||
                            w.Name.Contains(SearchTerm) ||
                            w.UserBlobId.Contains(SearchTerm) &&
                            w.CreatedBy == userId :
                            w.CreatedBy == userId);
            if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
            {
                if (Descending.Value)
                {
                    query = query.OrderByPropertyDescending(SortBy);
                }
                else
                {
                    query = query.OrderByProperty(SortBy);
                }
            }
            else
            {
                query.OrderByDescending(o => o.CreatedBy);
            }
            return query.ToApiResponsePaged(Page, PageSize, out Count);
        }
        /// <summary>
        /// Gets a list of user blobs
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="SearchTerm"></param>
        /// <param name="UserBlobId"></param>
        /// <param name="LangCode"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortBy"></param>
        /// <param name="Descending"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<UserBlobs>>> ListUserBlobsPaged(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, string SearchTerm, IEnumerable<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        {
            var query = dbContext.UserBlobs.AsQueryable();
            query = query.Where(w => UserBlobId.Any() ?
                            UserBlobId.Contains(w.UserBlobId) :
                            !string.IsNullOrWhiteSpace(SearchTerm) ?
                            w.ContentDisposition.Contains(SearchTerm) ||
                            w.ContentType.Contains(SearchTerm) ||
                            w.CreatedBy.Contains(SearchTerm) ||
                            w.FileName.Contains(SearchTerm) ||
                            w.FilePath.Contains(SearchTerm) ||
                            w.Name.Contains(SearchTerm) ||
                            w.UserBlobId.Contains(SearchTerm) :
                            true);
            if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
            {
                if (Descending.Value)
                {
                    query = query.OrderByPropertyDescending(SortBy);
                }
                else
                {
                    query = query.OrderByProperty(SortBy);
                }
            }
            else
            {
                query.OrderByDescending(o => o.CreatedBy);
            }
            return query.ToApiResponsePaged(Page, PageSize, out Count);
        }
        /// <summary>
        /// Gets a list of user blobs
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="SearchTerm"></param>
        /// <param name="UserBlobId"></param>
        /// <param name="LangCode"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortBy"></param>
        /// <param name="Descending"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static IApiResponse<IPaged<IEnumerable<UserBlobs>>> ListOwnsUserBlobsPaged(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, string userId, string SearchTerm, IEnumerable<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        {
            var query = dbContext.UserBlobs.AsQueryable();
            query = query.Where(w => UserBlobId.Any() ?
                            UserBlobId.Contains(w.UserBlobId) &&
                            w.CreatedBy == userId :
                            !string.IsNullOrWhiteSpace(SearchTerm) ?
                            w.ContentDisposition.Contains(SearchTerm) ||
                            w.ContentType.Contains(SearchTerm) ||
                            w.CreatedBy.Contains(SearchTerm) ||
                            w.FileName.Contains(SearchTerm) ||
                            w.FilePath.Contains(SearchTerm) ||
                            w.Name.Contains(SearchTerm) ||
                            w.UserBlobId.Contains(SearchTerm) &&
                            w.CreatedBy == userId :
                            w.CreatedBy == userId);
            if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
            {
                if (Descending.Value)
                {
                    query = query.OrderByPropertyDescending(SortBy);
                }
                else
                {
                    query = query.OrderByProperty(SortBy);
                }
            }
            else
            {
                query.OrderByDescending(o => o.CreatedBy);
            }
            return query.ToApiResponsePaged(Page, PageSize, out Count);
        }
        /// <summary>
        /// Gets a user blob
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> GetOneUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                await dbContext.CreateAuditrailsAsync(identifier, "Record not found on GetOneUserBlobsAsync", userId, controllerName, actionName, remoteIpAddress);
                return new UserBlobs().ToApiResponseFailed("RecordNotFound");
            }
            var record = await dbContext.UserBlobs.FindAsync(identifier.Model);
            return record == null ? record.ToApiResponseFailed("RecordNotFound") : record.ToApiResponse();
        }
        /// <summary>
        /// Gets a user blob that user owns
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<UserBlobs>> GetOneOwnsUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                await dbContext.CreateAuditrailsAsync(identifier, "Record not found on GetOneUserBlobsAsync", userId, controllerName, actionName, remoteIpAddress);
                return new UserBlobs().ToApiResponseFailed("RecordNotFound");
            }
            var record = await dbContext.UserBlobs.FirstOrDefaultAsync(x => x.UserBlobId == identifier.Model && x.CreatedBy == userId);
            return record == null ? record.ToApiResponseFailed("RecordNotFound") : record.ToApiResponse();
        }
        /// <summary>
        /// Gets a user blob
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> GetRangeUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers == null || !identifiers.Any())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Record identifiers was null or empty on delete operation", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<UserBlobs>().ToApiResponseFailed("RecordNotFound");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    await dbContext.CreateAuditrailsAsync(identifier, "Record identifier was null or empty on delete operation", userId, controllerName, actionName, remoteIpAddress);
                    return Enumerable.Empty<UserBlobs>().ToApiResponseFailed("RecordNotFound");
                }
            }
            var ids = identifiers.Select(s => s.Model);
            var records = await dbContext.UserBlobs.Where(w => ids.Contains(w.UserBlobId)).ToListAsync();
            if (records == null || !records.Any())
            {
                await dbContext.CreateAuditrailsAsync(records, "Records not found on select in operation", userId, controllerName, actionName, remoteIpAddress);
                return records.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            return records.AsEnumerable().ToApiResponse();
        }
        /// <summary>
        /// Gets a user blob that user owns
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        public static async Task<IApiResponse<IEnumerable<UserBlobs>>> GetRangeOwnsUserBlobsAsync(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            if (identifiers == null || !identifiers.Any())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Record identifiers was null or empty on delete operation", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<UserBlobs>().ToApiResponseFailed("RecordNotFound");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    await dbContext.CreateAuditrailsAsync(identifier, "Record identifier was null or empty on delete operation", userId, controllerName, actionName, remoteIpAddress);
                    return Enumerable.Empty<UserBlobs>().ToApiResponseFailed("RecordNotFound");
                }
            }
            var ids = identifiers.Select(s => s.Model);
            var records = await dbContext.UserBlobs.Where(w => ids.Contains(w.UserBlobId)).ToListAsync();
            if (records == null || !records.Any())
            {
                await dbContext.CreateAuditrailsAsync(records, "Records not found on select in operation", userId, controllerName, actionName, remoteIpAddress);
                return records.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            return records.AsEnumerable().ToApiResponse();
        }
    }
}

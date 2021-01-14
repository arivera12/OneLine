using OneLine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IApiContextUserBlobsService<T, TUserBlobs>
    {
        #region Add

        /// <summary>
        /// Adds a range of <paramref name="uploadBlobDatas"/> to the specified storage path 
        /// </summary>
        /// <param name="uploadBlobDatas"></param>      
        /// <param name="path"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> AddUserBlobsRangeAsync(IEnumerable<IUploadBlobData> uploadBlobDatas, string path = "");
        /// <summary>
        /// Adds and bind a range of <paramref name="uploadBlobDatas"/> to the specified storage path
        /// </summary>
        /// <param name="record"></param>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> AddAndBindUserBlobsRangeAsync(T record, IEnumerable<IUploadBlobData> uploadBlobDatas, string path = "");

        #endregion

        #region Update

        /// <summary>
        /// Updates a range of <paramref name="uploadBlobDatas"/> to the specified storage path
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="path"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> UpdateUserBlobsRangeAsync(IEnumerable<TUserBlobs> userBlobs, IEnumerable<IUploadBlobData> uploadBlobDatas, string path = "", bool ignoreBlobOwner = false);
        /// <summary>
        /// Updates and bind a range of <paramref name="uploadBlobDatas"/> to the specified storage path
        /// </summary>
        /// <param name="record"></param>
        /// <param name="originalRecord"></param>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> UpdateAndBindUserBlobsRangeAsync(T record, T originalRecord, IEnumerable<IUploadBlobData> uploadBlobDatas, bool ignoreBlobOwner = false);

        #endregion

        #region Remove By UserBlob

        /// <summary>
        /// Removes an user blob from the storage and database reference
        /// </summary>
        /// <param name="userBlob"></param>
        /// <param name="IgnoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> RemoveUserBlobAsync(TUserBlobs userBlob, bool IgnoreBlobOwner = false);
        /// <summary>
        /// Removes an user blob from the storage and database reference and audits the transaction.
        /// </summary>
        /// <param name="userBlob"></param>
        /// <param name="IgnoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> RemoveUserBlobAuditedAsync(TUserBlobs userBlob, bool IgnoreBlobOwner = false);
        /// <summary>
        /// Removes an user blob from the storage and database reference forced.
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> RemoveForcedUserBlobAsync(TUserBlobs userBlob);
        /// <summary>
        /// Removes an user blob from the storage and database reference forced and audits the transaction.
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> RemoveForcedUserBlobAuditedAsync(TUserBlobs userBlob);
        /// <summary>
        /// Removes a range of user blobs from the storage and database reference
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeUserBlobsAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false);
        /// <summary>
        /// Removes a range of user blobs from the storage and database reference and audits the transaction.
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeUserBlobsAuditedAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false);
        /// <summary>
        /// Removes a range of user blobs from the storage and database reference forced.
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeForcedUserBlobsAsync(IEnumerable<TUserBlobs> userBlobs);
        /// <summary>
        /// Removes a range of user blobs from the storage and database reference forced and audits the transaction.
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeForcedUserBlobsAuditedAsync(IEnumerable<TUserBlobs> userBlobs);
        #endregion

        #region Remove by Identifier

        /// <summary>
        /// Removes an user blob from the storage and database reference
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="IgnoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> RemoveUserBlobAsync(IIdentifier<string> identifier, bool IgnoreBlobOwner = false);
        /// <summary>
        /// Removes an user blob from the storage and database reference
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> RemoveForcedUserBlobAsync(IIdentifier<string> identifier);
        /// <summary>
        /// Removes a range of user blobs from the storage and database reference
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false);
        /// <summary>
        /// Removes a range of user blobs from the storage and database reference
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeForcedUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers);

        #endregion

        #region Remove helpers

        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s within the entity object.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> RemoveUserBlobsAuditedFromEntityAsync(T entity);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s within the entity object.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<IApiResponse<IEnumerable<TUserBlobs>>>>> RemoveUserBlobsFromObjectAsync(object model);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s within the entity object.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> RemoveUserBlobAuditedFromEntityAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s within the entity object.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> RemoveUserBlobsAuditedFromEntitiesAsync(IEnumerable<T> entities);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s within the entity object.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> RemoveUserBlobAuditedFromEntitiesAsync(Expression<Func<T, bool>> predicate);


        #endregion

        #region Delete

        /// <summary>
        /// Deletes a blob from the storage and database reference
        /// </summary>
        /// <param name="userBlob"></param>
        /// <param name="IgnoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> DeleteUserBlobsAsync(TUserBlobs userBlob, bool IgnoreBlobOwner = false);
        /// <summary>
        /// Deletes a blob from the storage and database reference
        /// </summary>
        /// <param name="userBlob"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> DeleteForcedUserBlobsAsync(TUserBlobs userBlob);
        /// <summary>
        /// Deletes blob/s from the storage and database reference
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> DeleteRangeUserBlobsAsync(IEnumerable<TUserBlobs> userBlobs);
        /// <summary>
        /// Deletes blob/s from the storage and database reference
        /// </summary>
        /// <param name="userBlobs">The user blobs to delete</param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> DeleteRangeForcedUserBlobsAsync(IEnumerable<TUserBlobs> userBlobs);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<IApiResponse<IEnumerable<TUserBlobs>>>>> DeleteUserBlobsFromObjectAsync(object model);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> DeleteUserBlobsFromEntityAsync(T entity);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> DeleteUserBlobsFromEntityAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> DeleteUserBlobsFromEntitiesAsync(IEnumerable<T> entities);
        /// <summary>
        /// This is a helper method which simplifies the delete process of file/s.
        /// It validates every file deleted to check if there was any error deleting any of them.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> DeleteUserBlobsFromEntitiesAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Deletes a blob from the storage and database reference
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> DeleteUserBlobsAsync(IIdentifier<string> identifier);
        /// <summary>
        /// Deletes a blob from the storage and database reference
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> DeleteForcedUserBlobsAsync(IIdentifier<string> identifier);
        /// <summary>
        /// Deletes blob/s from the storage and database reference
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> DeleteRangeUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers);
        /// <summary>
        /// Deletes blob/s from the storage and database reference
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> DeleteRangeForcedUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers);

        #endregion

        //public IApiResponse<IPaged<IEnumerable<TUserBlobs>>> SearchUserBlobsPaged<TUserBlobs>(string SearchTerm, IEnumerable<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        //    where TUserBlobs : class, IUserBlobs, new()
        //{
        //    var query = dbContext.Set<TUserBlobs>().AsQueryable();
        //    query = query.Where(w => UserBlobId.Any() ?
        //                    UserBlobId.Contains(w.UserBlobId) :
        //                    !string.IsNullOrWhiteSpace(SearchTerm) ?
        //                    w.ContentDisposition.Contains(SearchTerm) ||
        //                    w.ContentType.Contains(SearchTerm) ||
        //                    w.CreatedBy.Contains(SearchTerm) ||
        //                    w.FileName.Contains(SearchTerm) ||
        //                    w.FilePath.Contains(SearchTerm) ||
        //                    w.Name.Contains(SearchTerm) ||
        //                    w.UserBlobId.Contains(SearchTerm) :
        //                    true);
        //    if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
        //    {
        //        if (Descending.Value)
        //        {
        //            query = query.OrderByPropertyDescending(SortBy);
        //        }
        //        else
        //        {
        //            query = query.OrderByProperty(SortBy);
        //        }
        //    }
        //    else
        //    {
        //        query.OrderByDescending(o => o.CreatedBy);
        //    }
        //    return query.ToPagedApiResponse(Page, PageSize, out Count);
        //}
        //public IApiResponse<IPaged<IEnumerable<TUserBlobs>>> SearchOwnsUserBlobsPaged<TUserBlobs>(string userId, string SearchTerm, IEnumerable<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        //    where TUserBlobs : class, IUserBlobs, new()
        //{
        //    var query = dbContext.Set<TUserBlobs>().AsQueryable();
        //    query = query.Where(w => UserBlobId.Any() ?
        //                    UserBlobId.Contains(w.UserBlobId) &&
        //                    w.CreatedBy == userId :
        //                    !string.IsNullOrWhiteSpace(SearchTerm) ?
        //                    w.ContentDisposition.Contains(SearchTerm) ||
        //                    w.ContentType.Contains(SearchTerm) ||
        //                    w.FileName.Contains(SearchTerm) ||
        //                    w.FilePath.Contains(SearchTerm) ||
        //                    w.Name.Contains(SearchTerm) ||
        //                    w.UserBlobId.Contains(SearchTerm) &&
        //                    w.CreatedBy == userId :
        //                    w.CreatedBy == userId);
        //    if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
        //    {
        //        if (Descending.Value)
        //        {
        //            query = query.OrderByPropertyDescending(SortBy);
        //        }
        //        else
        //        {
        //            query = query.OrderByProperty(SortBy);
        //        }
        //    }
        //    else
        //    {
        //        query.OrderByDescending(o => o.CreatedBy);
        //    }
        //    return query.ToPagedApiResponse(Page, PageSize, out Count);
        //}
        //public IApiResponse<IPaged<IEnumerable<TUserBlobs>>> ListUserBlobsPaged<TUserBlobs>(string SearchTerm, IEnumerable<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        //    where TUserBlobs : class, IUserBlobs, new()
        //{
        //    var query = dbContext.Set<TUserBlobs>().AsQueryable();
        //    query = query.Where(w => UserBlobId.Any() ?
        //                    UserBlobId.Contains(w.UserBlobId) :
        //                    !string.IsNullOrWhiteSpace(SearchTerm) ?
        //                    w.ContentDisposition.Contains(SearchTerm) ||
        //                    w.ContentType.Contains(SearchTerm) ||
        //                    w.CreatedBy.Contains(SearchTerm) ||
        //                    w.FileName.Contains(SearchTerm) ||
        //                    w.FilePath.Contains(SearchTerm) ||
        //                    w.Name.Contains(SearchTerm) ||
        //                    w.UserBlobId.Contains(SearchTerm) :
        //                    true);
        //    if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
        //    {
        //        if (Descending.Value)
        //        {
        //            query = query.OrderByPropertyDescending(SortBy);
        //        }
        //        else
        //        {
        //            query = query.OrderByProperty(SortBy);
        //        }
        //    }
        //    else
        //    {
        //        query.OrderByDescending(o => o.CreatedBy);
        //    }
        //    return query.ToPagedApiResponse(Page, PageSize, out Count);
        //}
        //public IApiResponse<IPaged<IEnumerable<TUserBlobs>>> ListOwnsUserBlobsPaged<TUserBlobs>(string userId, string SearchTerm, IEnumerable<string> UserBlobId, string LangCode, int? Page, int? PageSize, string SortBy, bool? Descending, out int Count)
        //    where TUserBlobs : class, IUserBlobs, new()
        //{
        //    var query = dbContext.Set<TUserBlobs>().AsQueryable();
        //    query = query.Where(w => UserBlobId.Any() ?
        //                    UserBlobId.Contains(w.UserBlobId) &&
        //                    w.CreatedBy == userId :
        //                    !string.IsNullOrWhiteSpace(SearchTerm) ?
        //                    w.ContentDisposition.Contains(SearchTerm) ||
        //                    w.ContentType.Contains(SearchTerm) ||
        //                    w.CreatedBy.Contains(SearchTerm) ||
        //                    w.FileName.Contains(SearchTerm) ||
        //                    w.FilePath.Contains(SearchTerm) ||
        //                    w.Name.Contains(SearchTerm) ||
        //                    w.UserBlobId.Contains(SearchTerm) &&
        //                    w.CreatedBy == userId :
        //                    w.CreatedBy == userId);
        //    if (Descending.HasValue && !string.IsNullOrWhiteSpace(SortBy))
        //    {
        //        if (Descending.Value)
        //        {
        //            query = query.OrderByPropertyDescending(SortBy);
        //        }
        //        else
        //        {
        //            query = query.OrderByProperty(SortBy);
        //        }
        //    }
        //    else
        //    {
        //        query.OrderByDescending(o => o.CreatedBy);
        //    }
        //    return query.ToPagedApiResponse(Page, PageSize, out Count);
        //}

        #region Get one and get range

        /// <summary>
        /// Get one user blob by it's id
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> GetOneUserBlobsAsync(IIdentifier<string> identifier);
        /// <summary>
        /// Get one user blob by it's id and by the current user id
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<IApiResponse<TUserBlobs>> GetOneOwnsUserBlobsAsync(IIdentifier<string> identifier);
        /// <summary>
        /// Gets a range of user blobs by it's id's
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> GetRangeUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers);
        /// <summary>
        /// Gets a range of user blobs by it's id's and by the current user id
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<TUserBlobs>>> GetRangeOwnsUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers);

        #endregion

        //#region Read as base 64 string

        //Task<string> ReadBlobAsBase64Async(TUserBlobs userBlob, bool ignoreBlobOwner = false)
        //{
        //    return Convert.ToBase64String(await ReadBlobAsByteArrayAsync(userBlob, ignoreBlobOwner));
        //}
        //Task<IEnumerable<string>> ReadBlobRangeAsBase64Async(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    if (userBlobs.IsNull() || !userBlobs.Any())
        //    {
        //        //await CreateAuditrailsAsync(new T(), "Userblobs is null or empty on method ReadBlobAsBase64Async");
        //        return Enumerable.Empty<string>();
        //    }
        //    var BytesArray = new List<string>();
        //    foreach (var userBlob in userBlobs)
        //    {
        //        BytesArray.Add(await ReadBlobAsBase64Async(userBlob, ignoreBlobOwner));
        //    }
        //    return BytesArray;
        //}
        //Task<IApiResponse<string>> ReadBlobAsBase64ApiResponseAsync(TUserBlobs userBlob, bool ignoreBlobOwner = false)
        //{
        //    var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, ignoreBlobOwner);
        //    if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
        //    {
        //        return new ApiResponse<string>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
        //    }
        //    //await DbContext.AddAsync(userBlob.CreateAuditTrails<TUserBlobs, TAuditTrails>("File was found and readed as api response base 64 string", HttpContextAccessor));
        //    var base64 = Convert.ToBase64String(await BlobStorage.ReadBytesAsync(userBlob.FilePath));
        //    return new ApiResponse<string>(ApiResponseStatus.Succeeded, data: base64);
        //}
        ///// <summary>
        ///// Read a blob as a base 64 string api response from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<IEnumerable<IApiResponse<string>>> ReadBlobRangeAsBase64ApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    if (userBlobs.IsNull() || !userBlobs.Any())
        //    {
        //        //await DbContext.AddAsync(userBlobs.CreateRangeAuditTrails<TUserBlobs, TAuditTrails>("Userblobs is null or empty on method ReadBlobAsBase64ApiResponseAsync", HttpContextAccessor));
        //        return Enumerable.Empty<IApiResponse<string>>();
        //    }
        //    var base64Array = new List<IApiResponse<string>>();
        //    foreach (var userBlob in userBlobs)
        //    {
        //        base64Array.Add(await ReadBlobAsBase64ApiResponseAsync(userBlob, ignoreBlobOwner));
        //    }
        //    return base64Array;
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //Task<string> ReadBlobRangeIntoZipFolderBase64Async(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    return Convert.ToBase64String(await ReadBlobRangeIntoZipFolderByteArrayAsync(userBlobs, ignoreBlobOwner));
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //Task<IApiResponse<string>> ReadBlobRangeIntoZipFolderBase64ApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    var streamApiResponse = await ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(userBlobs, ignoreBlobOwner);
        //    return new ApiResponse<string>(streamApiResponse.Status, data: Convert.ToBase64String(streamApiResponse.Data), message: streamApiResponse.Message);
        //}
        ///// <summary>
        ///// Read a blob as a base 64string from the storage.
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<string> ReadBlobAsBase64Async(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        //{
        //    return Convert.ToBase64String(await ReadBlobAsByteArrayAsync(identifier, ignoreBlobOwner));
        //}
        ///// <summary>
        ///// Read a blob as a base 64 string from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<IEnumerable<string>> ReadBlobRangeAsBase64Async(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    if (identifiers.IsNull() || !identifiers.Any())
        //    {
        //        //await DbContext.AddAsync(identifiers.CreateRangeAuditTrails<IIdentifier<string>, TAuditTrails>("Identifiers is null or empty on method ReadBlobAsBase64Async", HttpContextAccessor));
        //        return Enumerable.Empty<string>();
        //    }
        //    var BytesArray = new List<string>();
        //    foreach (var identifier in identifiers)
        //    {
        //        BytesArray.Add(await ReadBlobAsBase64Async(identifier, ignoreBlobOwner));
        //    }
        //    return BytesArray;
        //}
        ///// <summary>
        ///// Read a blob as a base 64 string api response from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<IApiResponse<string>> ReadBlobAsBase64ApiResponseAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        //{
        //    var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(identifier, ignoreBlobOwner);
        //    if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
        //    {
        //        return new ApiResponse<string>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
        //    }
        //    var userBlobsApiResponse = await GetOneUserBlobsAsync(identifier);
        //    //await DbContext.AddAsync(userBlobsApiResponse.Data.CreateAuditTrails<TUserBlobs, TAuditTrails>("File was found and readed as api response base 64 string", HttpContextAccessor));
        //    var base64UserBlob = Convert.ToBase64String(await BlobStorage.ReadBytesAsync(userBlobsApiResponse.Data.FilePath));
        //    return new ApiResponse<string>(ApiResponseStatus.Succeeded, data: base64UserBlob);
        //}
        ///// <summary>
        ///// Read a blob as a base 64 string api response from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<IEnumerable<IApiResponse<string>>> ReadBlobRangeAsBase64ApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    if (identifiers.IsNull() || !identifiers.Any())
        //    {
        //        //await DbContext.AddAsync(identifiers.CreateRangeAuditTrails<IIdentifier<string>, TAuditTrails>("Identifiers is null or empty on method ReadBlobAsBase64ApiResponseAsync", HttpContextAccessor));
        //        return Enumerable.Empty<IApiResponse<string>>();
        //    }
        //    var base64Array = new List<IApiResponse<string>>();
        //    foreach (var identifier in identifiers)
        //    {
        //        base64Array.Add(await ReadBlobAsBase64ApiResponseAsync(identifier, ignoreBlobOwner));
        //    }
        //    return base64Array;
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //Task<string> ReadBlobRangeIntoZipFolderBase64Async(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    return Convert.ToBase64String(await ReadBlobRangeIntoZipFolderByteArrayAsync(identifiers, ignoreBlobOwner));
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //Task<IApiResponse<string>> ReadBlobRangeIntoZipFolderBase64ApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    var streamApiResponse = await ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(identifiers, ignoreBlobOwner);
        //    return new ApiResponse<string>(streamApiResponse.Status, data: Convert.ToBase64String(streamApiResponse.Data), message: streamApiResponse.Message);
        //}

        //#endregion

        //#region Read as byte array

        ///// <summary>
        ///// Read a blob as a byte array from the storage
        ///// </summary>
        ///// <returns></returns>
        //Task<byte[]> ReadBlobAsByteArrayAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        //{
        //    return await (await ReadBlobAsStreamAsync(identifier, ignoreBlobOwner)).ToByteArrayAsync();
        //}
        ///// <summary>
        ///// Read a blob as a byte array from the storage
        ///// </summary>
        ///// <returns></returns>
        //Task<IEnumerable<byte[]>> ReadBlobRangeAsByteArrayAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    if (identifiers.IsNull() || !identifiers.Any())
        //    {
        //        //await CreateAuditrailsAsync(new T(), "Identifiers is null or empty on method ReadBlobAsByteArrayAsync");
        //        return Enumerable.Empty<byte[]>();
        //    }
        //    var BytesArray = new List<byte[]>();
        //    foreach (var identifier in identifiers)
        //    {
        //        BytesArray.Add(await (await ReadBlobAsStreamAsync(identifier, ignoreBlobOwner)).ToByteArrayAsync());
        //    }
        //    return BytesArray;
        //}
        ///// <summary>
        ///// Read a blob as a byte array api response from the storage
        ///// </summary>
        ///// <returns></returns>
        //Task<IApiResponse<byte[]>> ReadBlobAsByteArrayApiResponseAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        //{
        //    var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(identifier, ignoreBlobOwner);
        //    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
        //    {
        //        return new ApiResponse<byte[]>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
        //    }
        //    var userBlobsApiResponse = await GetOneUserBlobsAsync(identifier);
        //    //await CreateAuditrailsAsync(new T(), "File was found and readed as api response byte array");
        //    //await DbContext.CreateAuditrailsAsync<TUserBlobs, TAuditTrails>(userBlobsApiResponse.Data, "File was found and readed as api response byte array");
        //    var bytes = await BlobStorage.ReadBytesAsync(userBlobsApiResponse.Data.FilePath);
        //    return new ApiResponse<byte[]>(ApiResponseStatus.Succeeded, bytes);
        //}
        ///// <summary>
        ///// Read a blob as a byte array api response from the storage
        ///// </summary>
        ///// <returns></returns>
        //Task<IEnumerable<IApiResponse<byte[]>>> ReadBlobRangeAsByteArrayApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    if (identifiers.IsNull() || !identifiers.Any())
        //    {
        //        //await CreateAuditrailsAsync(new T(), "Identifier is null or empty on method ReadBlobAsByteArrayApiResponseAsync");
        //        //await dbContext.CreateAuditrailsAsync<IEnumerable<IIdentifier<string>>, TAuditTrails>(identifiers, "Identifier is null or empty on method ReadBlobAsByteArrayApiResponseAsync", httpContextAccessor);
        //        return Enumerable.Empty<IApiResponse<byte[]>>();
        //    }
        //    var bytesArray = new List<IApiResponse<byte[]>>();
        //    foreach (var identifier in identifiers)
        //    {
        //        bytesArray.Add(await ReadBlobAsByteArrayApiResponseAsync(identifier, ignoreBlobOwner));
        //    }
        //    return bytesArray;
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a byte array from the storage
        ///// </summary>
        ///// <returns></returns>
        //Task<byte[]> ReadBlobRangeIntoZipFolderByteArrayAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    return await (await ReadBlobRangeIntoZipFolderStreamAsync(identifiers, ignoreBlobOwner)).ToByteArrayAsync();
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a byte array from the storage
        ///// </summary>
        ///// <returns></returns>
        //Task<ApiResponse<byte[]>> ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    var streamApiResponse = await ReadBlobRangeIntoZipFolderStreamApiResponseAsync(identifiers, ignoreBlobOwner);
        //    return new ApiResponse<byte[]>(streamApiResponse.Status, await streamApiResponse.Data?.ToByteArrayAsync(), streamApiResponse.Message);
        //}

        ///// <summary>
        ///// Read a blob as a byte array from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<byte[]> ReadBlobAsByteArrayAsync(TUserBlobs userBlob, bool ignoreBlobOwner = false)
        //{
        //    return await (await ReadBlobAsStreamAsync(userBlob, ignoreBlobOwner)).ToByteArrayAsync();
        //}
        ///// <summary>
        ///// Read a blob as a byte array from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<IEnumerable<byte[]>> ReadBlobRangeAsByteArrayAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    if (userBlobs.IsNull() || !userBlobs.Any())
        //    {
        //        //await CreateAuditrailsAsync(new T(), "Userblobs is null or empty on method ReadBlobAsByteArrayAsync");
        //        //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "Userblobs is null or empty on method ReadBlobAsByteArrayAsync", httpContextAccessor);
        //        return Enumerable.Empty<byte[]>();
        //    }
        //    var BytesArray = new List<byte[]>();
        //    foreach (var userBlob in userBlobs)
        //    {
        //        BytesArray.Add(await (await ReadBlobAsStreamAsync(userBlob, ignoreBlobOwner)).ToByteArrayAsync());
        //    }
        //    return BytesArray;
        //}
        ///// <summary>
        ///// Read a blob as a byte array api response from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<IApiResponse<byte[]>> ReadBlobAsByteArrayApiResponseAsync(TUserBlobs userBlob, bool ignoreBlobOwner = false)
        //{
        //    var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, ignoreBlobOwner);
        //    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
        //    {
        //        return new ApiResponse<byte[]>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
        //    }
        //    //await CreateAuditrailsAsync(new T(), "File was found and readed as api response byte array");
        //    //await dbContext.CreateAuditrailsAsync<TUserBlobs, TAuditTrails>(userBlob, "File was found and readed as api response byte array", httpContextAccessor);
        //    var bytes = await BlobStorage.ReadBytesAsync(userBlob.FilePath);
        //    return new ApiResponse<byte[]>(ApiResponseStatus.Succeeded, bytes);
        //}
        ///// <summary>
        ///// Read a blob as a byte array api response from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //Task<IEnumerable<IApiResponse<byte[]>>> ReadBlobRangeAsByteArrayApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    if (userBlobs.IsNull() || !userBlobs.Any())
        //    {
        //        //await CreateAuditrailsAsync(new T(), "Userblobs is null or empty on method ReadBlobAsByteArrayApiResponseAsync");
        //        //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "Userblobs is null or empty on method ReadBlobAsByteArrayApiResponseAsync", httpContextAccessor);
        //        return Enumerable.Empty<IApiResponse<byte[]>>();
        //    }
        //    var bytesArray = new List<IApiResponse<byte[]>>();
        //    foreach (var userBlob in userBlobs)
        //    {
        //        bytesArray.Add(await ReadBlobAsByteArrayApiResponseAsync(userBlob, ignoreBlobOwner));
        //    }
        //    return bytesArray;
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a byte array from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //Task<byte[]> ReadBlobRangeIntoZipFolderByteArrayAsync(this DbContext dbContext, IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    return (await ReadBlobRangeIntoZipFolderStreamAsync(userBlobs, ignoreBlobOwner)).ToByteArray();
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a byte array from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //Task<ApiResponse<byte[]>> ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    var streamApiResponse = await ReadBlobRangeIntoZipFolderStreamApiResponseAsync(userBlobs, ignoreBlobOwner);
        //    return new ApiResponse<byte[]>(streamApiResponse.Status, await streamApiResponse.Data?.ToByteArrayAsync(), streamApiResponse.Message);
        //}

        //#endregion

        #region Read as stream

        /// <summary>
        /// Read a blob as stream from the storage
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<Stream> ReadBlobAsStreamAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read a range of blobs as streams from the storage
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IEnumerable<Stream>> ReadBlobRangeAsStreamAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read a blob as a stream api response from the storage
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<Stream>> ReadBlobAsStreamApiResponseAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read a blob as a stream api response from the storage
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IEnumerable<IApiResponse<Stream>>> ReadBlobRangeAsStreamApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as stream from the storage
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<Stream> ReadBlobRangeIntoZipFolderStreamAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a stream from the storage
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<Stream>> ReadBlobRangeIntoZipFolderStreamApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read a blob as stream from the storage
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<Stream> ReadBlobAsStreamAsync(TUserBlobs userBlobs, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read a range of blobs as streams from the storage
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IEnumerable<Stream>> ReadBlobRangeAsStreamAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read a blob as a stream api response from the storage
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<Stream>> ReadBlobAsStreamApiResponseAsync(TUserBlobs userBlobs, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read a blob as a stream api response from the storage
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IEnumerable<IApiResponse<Stream>>> ReadBlobRangeAsStreamApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a stream from the storage
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<Stream> ReadBlobRangeIntoZipFolderStreamAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false);
        /// <summary>
        /// Read multiple blobs into a compressed zip folder as a stream from the storage
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<Stream>> ReadBlobRangeIntoZipFolderStreamApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false);

        #endregion

        #region Helpers

        /// <summary>
        /// Checks if the current user is the blob owner
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <returns></returns>
        Task<IApiResponse<bool>> IsBlobOwnerAsync(TUserBlobs userBlobs);
        /// <summary>
        /// Checks if the current user is the blob owner and the file exists
        /// </summary>
        /// <param name="userBlobs"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<bool>> IsBlobOwnerAndFileExistsAsync(TUserBlobs userBlobs, bool ignoreBlobOwner = false);
        /// <summary>
        /// Validates a blob data with the form file rules
        /// </summary>
        /// <param name="blobDatas"></param>
        /// <param name="formFileRules"></param>
        /// <returns></returns>
        Task<IApiResponse<bool>> IsValidBlobDataAsync(IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules);
        /// <summary>
        /// Checks if the current user is the blob owner
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        Task<IApiResponse<bool>> IsBlobOwnerAsync(IIdentifier<string> identifier);
        /// <summary>
        /// Checks if the current user is the blob owner and the file exists
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <returns></returns>
        Task<IApiResponse<bool>> IsBlobOwnerAndFileExistsAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false);

        #endregion
    }
}

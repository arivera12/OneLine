using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Messaging;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub> :
        IApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub>
        where TDbContext : DbContext
        where T : class, new()
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
        where TSmtp : class, ISmtp, new()
        where TMessageHub : MessageHub, new()
    {
        #region Add
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> AddUserBlobsRangeAsync(IEnumerable<IUploadBlobData> uploadBlobDatas, string path = "")
        {
            var any = uploadBlobDatas.IsNotNull() && uploadBlobDatas.Any();
            if (!any)
            {
                return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("uploadBlobDatasIsNullOrEmpty");
            }
            if (any)
            {
                foreach (var uploadBlobData in uploadBlobDatas)
                {
                    var isValidFormFileApiResponse = uploadBlobData.BlobDatas.IsValidBlobDataApiResponse(uploadBlobData.FormFileRules);
                    if (isValidFormFileApiResponse.Status.Failed())
                    {
                        return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("uploadBlobDatasIsNullOrEmpty");
                    }
                }
            }
            var createdOn = DateTime.Now;
            var userId = HttpContextAccessor.HttpContext.User.UserId();
            IList<TUserBlobs> uploadedUserBlobs = new List<TUserBlobs>();
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                foreach (var file in uploadBlobData.BlobDatas)
                {
                    var fileExtension = Path.GetExtension(file.Name);
                    var uniqueFileName = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
                    var filename = $"{(string.IsNullOrWhiteSpace(path) ? "" : path)}{uniqueFileName}{fileExtension}";
                    await BlobStorageService.BlobStorage.WriteAsync(filename, file.Data);
                    var userBlob = new TUserBlobs().AutoMap(file);
                    userBlob.UserBlobId = Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier().ToString();
                    userBlob.FileName = file.Name;
                    userBlob.FilePath = filename;
                    userBlob.Length = file.Size;
                    userBlob.UserIdentifier = userId;
                    userBlob.CreatedBy = userId;
                    userBlob.CreatedOn = createdOn;
                    userBlob.TableName = typeof(T).Name;
                    uploadedUserBlobs.Add(userBlob);
                    await DbContext.AddAsync(userBlob);
                    await AddAuditrailsAsync(userBlob, "Added a userblob");
                }
            }
            return uploadedUserBlobs.AsEnumerable().ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> AddAndBindUserBlobsRangeAsync(T record, IEnumerable<IUploadBlobData> uploadBlobDatas, string path = "")
        {
            if (record.IsNull())
            {
                return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("RecordIsNull");
            }
            var any = uploadBlobDatas.IsNotNull() && uploadBlobDatas.Any();
            if (!any)
            {
                return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("uploadBlobDatasIsNullOrEmpty");
            }
            //Validate blobs datas with the rules
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var isFormFileUploadedApiResponse = await IsValidBlobDataAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules);
                if (isFormFileUploadedApiResponse.Status.Failed() || !isFormFileUploadedApiResponse.Data)
                {
                    return new ApiResponse<IEnumerable<TUserBlobs>>(isFormFileUploadedApiResponse.Status, isFormFileUploadedApiResponse.Message);
                }
            }
            //Upload the blobdata
            var userBlobsUploadedList = new List<TUserBlobs>();
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var userBlobs = await AddUserBlobsRangeAsync(uploadBlobDatas, path);
                userBlobsUploadedList.AddRange(userBlobs.Data);
                record.GetType().GetProperty(uploadBlobData.PropertyName).SetValue(record, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userBlobs)));
            }
            return userBlobsUploadedList.AsEnumerable().ToApiResponse();
        }

        #endregion

        #region Update
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> UpdateUserBlobsRangeAsync(IEnumerable<TUserBlobs> userBlobs, IEnumerable<IUploadBlobData> uploadBlobDatas, string path = "", bool ignoreBlobOwner = false)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                //await CreateAuditrailsAsync(new T(), "No userblobs to update. The userBlobs parameter is null or empty.");
                return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("RecordsIsNullOrEmpty");
            }
            var uploadedUserBlobsList = new List<TUserBlobs>();
            var uploadedUserBlobs = await AddUserBlobsRangeAsync(uploadBlobDatas, path);
            if (uploadedUserBlobs.Status.Failed())
            {
                return new ApiResponse<IEnumerable<TUserBlobs>>(uploadedUserBlobs.Status, uploadedUserBlobs.Data, uploadedUserBlobs.ErrorMessages);
            }
            //Remove
            var deleteMultipleApiResponse = await RemoveRangeUserBlobsAsync(userBlobs, ignoreBlobOwner);
            if (deleteMultipleApiResponse.Status.Failed() && !string.IsNullOrWhiteSpace(deleteMultipleApiResponse.Message))
            {
                return new ApiResponse<IEnumerable<TUserBlobs>>(deleteMultipleApiResponse.Status, userBlobs, deleteMultipleApiResponse.Message);
            }
            return uploadedUserBlobsList.AsEnumerable().ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> UpdateAndBindUserBlobsRangeAsync(T record, T originalRecord, IEnumerable<IUploadBlobData> uploadBlobDatas, bool ignoreBlobOwner = false)
        {
            if (record.IsNull() || originalRecord.IsNull())
            {
                //await CreateAuditrailsAsync(new T(), "No userblobs to update. The userBlobs parameter is null or empty.");
                return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("RecordIsNull");
            }
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var isFormFileUploadedApiResponse = await IsValidBlobDataAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules);
                if (isFormFileUploadedApiResponse.Status.Failed() || !isFormFileUploadedApiResponse.Data)
                {
                    return new ApiResponse<IEnumerable<TUserBlobs>>(isFormFileUploadedApiResponse.Status, isFormFileUploadedApiResponse.Message);
                }
            }
            var userBlobsList = new List<TUserBlobs>();
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                IEnumerable<TUserBlobs> userBlobsOriginal;
                byte[] userBlobByteArrayOriginal;
                var userBlobOriginal = typeof(T).GetProperty(uploadBlobData.PropertyName).GetValue(originalRecord);
                if (userBlobOriginal.IsNull())
                {
                    continue;
                }
                else
                {
                    userBlobByteArrayOriginal = (byte[])userBlobOriginal;
                    userBlobsOriginal = JsonConvert.DeserializeObject<IEnumerable<TUserBlobs>>(Encoding.UTF8.GetString(userBlobByteArrayOriginal));
                }
                IEnumerable<TUserBlobs> userBlobsNew = Enumerable.Empty<TUserBlobs>();
                byte[] userBlobByteArrayNew;
                var userBlobNew = typeof(T).GetProperty(uploadBlobData.PropertyName).GetValue(record);
                if (userBlobNew.IsNotNull())
                {
                    userBlobByteArrayNew = (byte[])userBlobNew;
                    userBlobsNew = JsonConvert.DeserializeObject<IEnumerable<TUserBlobs>>(Encoding.UTF8.GetString(userBlobByteArrayNew));
                }
                var userBlobsToDelete = userBlobsOriginal.Where(w => !userBlobsNew.Contains(w));
                var deleteMultipleApiResponse = await RemoveRangeUserBlobsAsync(userBlobsToDelete, ignoreBlobOwner);
                userBlobsList.AddRange(deleteMultipleApiResponse.Data);
                if (deleteMultipleApiResponse.Status.Failed() && !string.IsNullOrWhiteSpace(deleteMultipleApiResponse.Message))
                {
                    return new ApiResponse<IEnumerable<TUserBlobs>>(deleteMultipleApiResponse.Status, userBlobsList, deleteMultipleApiResponse.Message);
                }
            }
            return await AddAndBindUserBlobsRangeAsync(record, uploadBlobDatas);
        }

        #endregion

        #region Remove

        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> RemoveUserBlobAsync(TUserBlobs userBlob, bool IgnoreBlobOwner = false)
        {
            var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, IgnoreBlobOwner);
            if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<TUserBlobs>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
            DbContext.Remove(userBlob);
            return userBlob.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> RemoveUserBlobAuditedAsync(TUserBlobs userBlob, bool IgnoreBlobOwner = false)
        {
            var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, IgnoreBlobOwner);
            if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<TUserBlobs>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
            DbContext.Remove(userBlob);
            await DbContext.AddAsync(userBlob.CreateAuditTrails<TUserBlobs, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
            return userBlob.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> RemoveForcedUserBlobAsync(TUserBlobs userBlob)
        {
            await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
            DbContext.Remove(userBlob);
            return userBlob.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> RemoveForcedUserBlobAuditedAsync(TUserBlobs userBlob)
        {
            await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
            DbContext.Remove(userBlob);
            await DbContext.AddAsync(userBlob.CreateAuditTrails<TUserBlobs, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
            return userBlob.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeUserBlobsAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Succeeded, Enumerable.Empty<TUserBlobs>());
            }
            var deletedUserBlobs = new List<TUserBlobs>();
            foreach (var userBlob in userBlobs)
            {
                var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, ignoreBlobOwner);
                if (isBlobOwnerAndFileExists.Status.Failed() && isBlobOwnerAndFileExists.Message == "FileNotFound")
                {
                    continue;
                }
                else if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
                {
                    return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
                }
                await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
                DbContext.Remove(userBlob);
                deletedUserBlobs.Add(userBlob);
            }
            return deletedUserBlobs.AsEnumerable().ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeUserBlobsAuditedAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                //await CreateAuditrailsAsync(new T(), "UserBlobs is null or empty on method DeleteRange");
                return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Succeeded, Enumerable.Empty<TUserBlobs>());
            }
            var deletedUserBlobs = new List<TUserBlobs>();
            foreach (var userBlob in userBlobs)
            {
                var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, ignoreBlobOwner);
                if (isBlobOwnerAndFileExists.Status.Failed() && isBlobOwnerAndFileExists.Message == "FileNotFound")
                {
                    continue;
                }
                else if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
                {
                    return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
                }
                await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
                DbContext.Remove(userBlob);
                await DbContext.AddAsync(userBlob.CreateAuditTrails<TUserBlobs, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
                deletedUserBlobs.Add(userBlob);
            }
            return deletedUserBlobs.AsEnumerable().ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeForcedUserBlobsAsync(IEnumerable<TUserBlobs> userBlobs)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Failed, "FileNotFound");
            }
            foreach (var userBlob in userBlobs)
            {
                await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
                DbContext.Remove(userBlob);
            }
            return userBlobs.AsEnumerable().ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeForcedUserBlobsAuditedAsync(IEnumerable<TUserBlobs> userBlobs)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                //await CreateAuditrailsAsync(new T(), "UserBlobs is null or empty on method DeleteRange");
                return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Failed, "FileNotFound");
            }
            foreach (var userBlob in userBlobs)
            {
                await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
                DbContext.Remove(userBlob);
                await DbContext.AddAsync(userBlob.CreateAuditTrails<TUserBlobs, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
            }
            return userBlobs.AsEnumerable().ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> RemoveUserBlobsAuditedFromEntityAsync(T entity)
        {
            if (entity.IsNull())
            {
                return new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<T, IEnumerable<TUserBlobs>>(entity, null), "ErrorDeletingRecord");
            }
            //Helper method that deletes all files in a object
            var DeleteBlobsApiResponse = await RemoveUserBlobsFromObjectAsync(entity);
            var deletedUserBlobs = DeleteBlobsApiResponse.Data.SelectMany(s => s.Data.Select(x => x));
            if (DeleteBlobsApiResponse.Status.Failed())
            {
                return new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(entity, deletedUserBlobs), DeleteBlobsApiResponse.Message);
            }
            await RemoveAuditedAsync(entity);
            return new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(entity, deletedUserBlobs));
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<IApiResponse<IEnumerable<TUserBlobs>>>>> RemoveUserBlobsFromObjectAsync(object model)
        {
            if (model.IsNull())
            {
                return new ApiResponse<IEnumerable<IApiResponse<IEnumerable<TUserBlobs>>>>(ApiResponseStatus.Failed, "RecordIsNull");
            }
            bool anyError = false;
            var apiResponse = new ApiResponse<IEnumerable<IApiResponse<IEnumerable<TUserBlobs>>>>();
            var deletedUserBlobs = new List<IApiResponse<IEnumerable<TUserBlobs>>>();
            foreach (var property in model
                                .GetType()
                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .Where(w => w.PropertyType.IsAssignableFrom(typeof(IEnumerable<TUserBlobs>)))
                    )
            {
                if (property.PropertyType.IsAssignableFrom(typeof(TUserBlobs)))
                {
                    var UserBlobs = (TUserBlobs)property.GetValue(model);
                    var deletedBlobs = await RemoveForcedUserBlobAsync(UserBlobs);
                    if (!anyError)
                    {
                        if (deletedBlobs.Status.Failed())
                        {
                            anyError = true;
                        }
                    }
                    deletedUserBlobs.Add(new ApiResponse<IEnumerable<TUserBlobs>>(deletedBlobs.Status, new List<TUserBlobs>() { deletedBlobs.Data }, deletedBlobs.Message));
                }
                else
                {
                    var UserBlobs = (IEnumerable<TUserBlobs>)property.GetValue(model);
                    var deletedBlobs = await RemoveRangeForcedUserBlobsAsync(UserBlobs);
                    if (!anyError)
                    {
                        if (deletedBlobs.Status.Failed())
                        {
                            anyError = true;
                        }
                    }
                    deletedUserBlobs.Add(deletedBlobs);
                }
            }
            var apiResponseStatus = anyError ? ApiResponseStatus.Failed : ApiResponseStatus.Succeeded;
            apiResponse.Data = deletedUserBlobs;
            apiResponse.Status = apiResponseStatus;
            return apiResponse;
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> RemoveUserBlobAuditedFromEntityAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate.IsNull())
            {
                return new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<T, IEnumerable<TUserBlobs>>(new T(), null), "ErrorDeletingRecord");
            }
            var record = DbContext.Set<T>().Where(predicate).FirstOrDefault();
            return await RemoveUserBlobsAuditedFromEntityAsync(record);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> RemoveUserBlobsAuditedFromEntitiesAsync(IEnumerable<T> entities)
        {
            if (entities.IsNull() || !entities.Any())
            {
                return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<IEnumerable<T>, IEnumerable<TUserBlobs>>(entities, null), "ErrorDeletingRecord");
            }
            var deletedUserBlobs = new List<TUserBlobs>();
            foreach (var entity in entities)
            {
                //Helper method that deletes all files in a object
                var DeleteBlobsApiResponse = await RemoveUserBlobsFromObjectAsync(entity);
                deletedUserBlobs.AddRange(DeleteBlobsApiResponse.Data.SelectMany(s => s.Data.Select(x => x)));
                if (DeleteBlobsApiResponse.Status.Failed())
                {
                    return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(entities, deletedUserBlobs.AsEnumerable()), DeleteBlobsApiResponse.Message);
                }
                await RemoveAuditedAsync(entity);
            }
            return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(entities, deletedUserBlobs.AsEnumerable()));
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> RemoveUserBlobAuditedFromEntitiesAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate.IsNull())
            {
                return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<IEnumerable<T>, IEnumerable<TUserBlobs>>(Enumerable.Empty<T>(), null), "ErrorDeletingRecord");
            }
            var records = DbContext.Set<T>().Where(predicate).AsEnumerable();
            return await RemoveUserBlobsAuditedFromEntitiesAsync(records);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> RemoveUserBlobAsync(IIdentifier<string> identifier, bool IgnoreBlobOwner = false)
        {
            var record = await GetOneUserBlobsAsync(identifier);
            if (record.Status.Failed())
            {
                return new ApiResponse<TUserBlobs>(record.Status, record.Data, record.Message, record.ErrorMessages);
            }
            return await RemoveUserBlobAsync(record.Data, IgnoreBlobOwner);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> RemoveForcedUserBlobAsync(IIdentifier<string> identifier)
        {
            var record = await GetOneUserBlobsAsync(identifier);
            if (record.Status.Failed())
            {
                return new ApiResponse<TUserBlobs>(record.Status, record.Data, record.Message, record.ErrorMessages);
            }
            return await RemoveForcedUserBlobAsync(record.Data);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        {
            var records = await GetRangeUserBlobsAsync(identifiers);
            if (records.Status.Failed())
            {
                return new ApiResponse<IEnumerable<TUserBlobs>>(records.Status, records.Data, records.Message, records.ErrorMessages);
            }
            return await RemoveRangeUserBlobsAsync(records.Data, ignoreBlobOwner);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> RemoveRangeForcedUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers)
        {
            var records = await GetRangeUserBlobsAsync(identifiers);
            if (records.Status.Failed())
            {
                return new ApiResponse<IEnumerable<TUserBlobs>>(records.Status, records.Data, records.Message, records.ErrorMessages);
            }
            return await RemoveRangeForcedUserBlobsAsync(records.Data);
        }

        #endregion

        #region Delete

        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> DeleteUserBlobsAsync(TUserBlobs userBlob, bool IgnoreBlobOwner = false)
        {
            var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, IgnoreBlobOwner);
            if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<TUserBlobs>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
            await AddAuditrailsAsync(userBlob, "Removed user blob from database and blob storage");
            DbContext.Remove(userBlob);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlob);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> DeleteForcedUserBlobsAsync(TUserBlobs userBlob)
        {
            await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
            await AddAuditrailsAsync(userBlob, "Removed user blob from database and blob storage");
            DbContext.Remove(userBlob);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlob);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> DeleteRangeUserBlobsAsync(IEnumerable<TUserBlobs> userBlobs)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                //await dbContext.CreateAuditrailsAsync(userBlobs, "UserBlobs is null or empty on method DeleteRange", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Succeeded, Enumerable.Empty<TUserBlobs>(), "FileNotFound");
            }
            foreach (var userBlob in userBlobs)
            {
                var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob);
                if (isBlobOwnerAndFileExists.Status.Failed() && isBlobOwnerAndFileExists.Message == "FileNotFound")
                {
                    continue;
                }
                else if (isBlobOwnerAndFileExists.Status.Failed() || !isBlobOwnerAndFileExists.Data)
                {
                    return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
                }
                await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
                await AddAuditrailsAsync(userBlob, "Removed user blob from database and blob storage");
                DbContext.Remove(userBlob);
            }
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlobs);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> DeleteRangeForcedUserBlobsAsync(IEnumerable<TUserBlobs> userBlobs)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                //await dbContext.CreateAuditrailsAsync(userBlobs, "UserBlobs is null or empty on method DeleteRange", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<TUserBlobs>>(ApiResponseStatus.Failed, "FileNotFound");
            }
            foreach (var userBlob in userBlobs)
            {
                await BlobStorageService.BlobStorage.DeleteAsync(userBlob.FilePath);
                await AddAuditrailsAsync(userBlob, "Removed user blob from database and blob storage");
                DbContext.Remove(userBlob);
            }
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(userBlobs);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<IApiResponse<IEnumerable<TUserBlobs>>>>> DeleteUserBlobsFromObjectAsync(object model)
        {
            bool anyError = false;
            var apiResponse = new ApiResponse<IEnumerable<IApiResponse<IEnumerable<TUserBlobs>>>>();
            var deletedUserBlobs = new List<IApiResponse<IEnumerable<TUserBlobs>>>();
            foreach (var property in model
                                .GetType()
                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .Where(w => w.PropertyType.IsAssignableFrom(typeof(IEnumerable<TUserBlobs>))
                                      )
                    )
            {
                if (property.PropertyType.IsAssignableFrom(typeof(TUserBlobs)))
                {
                    var UserBlobs = (TUserBlobs)property.GetValue(model);
                    var deletedBlobs = await DeleteForcedUserBlobsAsync(UserBlobs);
                    if (!anyError)
                    {
                        if (deletedBlobs.Status.Failed())
                        {
                            anyError = true;
                        }
                    }
                    deletedUserBlobs.Add(new ApiResponse<IEnumerable<TUserBlobs>>(deletedBlobs.Status, new List<TUserBlobs>() { deletedBlobs.Data }, deletedBlobs.Message));
                }
                else
                {
                    var UserBlobs = (IEnumerable<TUserBlobs>)property.GetValue(model);
                    var deletedBlobs = await DeleteRangeForcedUserBlobsAsync(UserBlobs);
                    if (!anyError)
                    {
                        if (deletedBlobs.Status.Failed())
                        {
                            anyError = true;
                        }
                    }
                    deletedUserBlobs.Add(deletedBlobs);
                }
            }
            var apiResponseStatus = anyError ? ApiResponseStatus.Failed : ApiResponseStatus.Succeeded;
            apiResponse.Data = deletedUserBlobs;
            apiResponse.Status = apiResponseStatus;
            return apiResponse;
        }
        /// <inheritdoc/>
        public async Task<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> DeleteUserBlobsFromEntityAsync(T entity)
        {
            if (entity.IsNull())
            {
                return new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create<T, IEnumerable<TUserBlobs>>(entity, null), "RecordIsNull");
            }
            //Helper method that deletes all files in a object
            var DeleteBlobsApiResponse = await DeleteUserBlobsFromObjectAsync(entity);
            var deletedUserBlobs = DeleteBlobsApiResponse.Data.SelectMany(s => s.Data.Select(x => x));
            if (DeleteBlobsApiResponse.Status.Failed())
            {
                return new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(entity, deletedUserBlobs), DeleteBlobsApiResponse.Message);
            }
            await RemoveAuditedAsync(entity);
            var result = await DbContext.SaveChangesAsync();
            var message = result.Succeeded() ? "RecordSavedSuccessfully" : "ErrorSavingRecord";
            return new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(entity, deletedUserBlobs), message);
        }
        /// <inheritdoc/>
        public async Task<ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> DeleteUserBlobsFromEntityAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate.IsNull())
            {
                return new ApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create<T, IEnumerable<TUserBlobs>>(new T(), null), "RecordIsNull");
            }
            var record = DbContext.Set<T>().Where(predicate).FirstOrDefault();
            return await DeleteUserBlobsFromEntityAsync(record);
        }
        /// <inheritdoc/>
        public async Task<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> DeleteUserBlobsFromEntitiesAsync(IEnumerable<T> entities)
        {
            if (entities.IsNull() || !entities.Any())
            {
                return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create<IEnumerable<T>, IEnumerable<TUserBlobs>>(entities, null), "ErrorDeletingRecord");
            }
            var deletedUserBlobs = new List<TUserBlobs>();
            foreach (var entity in entities)
            {
                //Helper method that deletes all files in a object
                var DeleteBlobsApiResponse = await DeleteUserBlobsFromObjectAsync(entity);
                deletedUserBlobs.AddRange(DeleteBlobsApiResponse.Data.SelectMany(s => s.Data.Select(x => x)));
                if (DeleteBlobsApiResponse.Status.Failed())
                {
                    return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Failed, Tuple.Create(entities, deletedUserBlobs.AsEnumerable()), DeleteBlobsApiResponse.Message);
                }
                await RemoveAuditedAsync(entity);
            }
            var result = await DbContext.SaveChangesAsync();
            var message = result.Succeeded() ? "RecordSavedSuccessfully" : "ErrorSavingRecord";
            return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create(entities, deletedUserBlobs.AsEnumerable()), message);
        }
        /// <inheritdoc/>
        public async Task<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> DeleteUserBlobsFromEntitiesAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate.IsNull())
            {
                return new ApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>(ApiResponseStatus.Succeeded, Tuple.Create<IEnumerable<T>, IEnumerable<TUserBlobs>>(Enumerable.Empty<T>(), null), "PredicateIsNull");
            }
            var records = DbContext.Set<T>().Where(predicate);
            return await DeleteUserBlobsFromEntitiesAsync(records);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> DeleteUserBlobsAsync(IIdentifier<string> identifier)
        {
            var record = await GetOneUserBlobsAsync(identifier);
            if (record.Status.Failed())
            {
                return new ApiResponse<TUserBlobs>(record.Status, record.Data, record.Message, record.ErrorMessages);
            }
            return await DeleteUserBlobsAsync(record.Data);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> DeleteForcedUserBlobsAsync(IIdentifier<string> identifier)
        {
            var record = await GetOneUserBlobsAsync(identifier);
            if (record.Status.Failed())
            {
                return new ApiResponse<TUserBlobs>(record.Status, record.Data, record.Message, record.ErrorMessages);
            }
            return await DeleteForcedUserBlobsAsync(record.Data);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> DeleteRangeUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers)
        {
            var records = await GetRangeUserBlobsAsync(identifiers);
            if (records.Status.Failed())
            {
                return new ApiResponse<IEnumerable<TUserBlobs>>(records.Status, records.Data, records.Message, records.ErrorMessages);
            }
            return await DeleteRangeUserBlobsAsync(records.Data);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> DeleteRangeForcedUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers)
        {
            var records = await GetRangeUserBlobsAsync(identifiers);
            if (records.Status.Failed())
            {
                return new ApiResponse<IEnumerable<TUserBlobs>>(records.Status, records.Data, records.Message, records.ErrorMessages);
            }
            return await DeleteRangeForcedUserBlobsAsync(records.Data);
        }

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

        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> GetOneUserBlobsAsync(IIdentifier<string> identifier)
        {
            if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
            {
                //await CreateAuditrailsAsync(new T(), "Record not found on GetOneUserBlobsAsync");
                return new TUserBlobs().ToApiResponseFailed("RecordNotFound");
            }
            var record = await DbContext.Set<TUserBlobs>().FindAsync(identifier.Model);
            return record.IsNull() ? record.ToApiResponseFailed("RecordNotFound") : record.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<TUserBlobs>> GetOneOwnsUserBlobsAsync(IIdentifier<string> identifier)
        {
            if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
            {
                //await CreateAuditrailsAsync(new T(), "Record not found on GetOneUserBlobsAsync");
                return new TUserBlobs().ToApiResponseFailed("RecordNotFound");
            }
            var record = await DbContext.Set<TUserBlobs>().FirstOrDefaultAsync(x => x.UserBlobId == identifier.Model && x.CreatedBy == HttpContextAccessor.HttpContext.User.UserId());
            return record.IsNull() ? record.ToApiResponseFailed("RecordNotFound") : record.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> GetRangeUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                //await CreateAuditrailsAsync(new T(), "Record identifiers was null or empty on delete operation");
                return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("RecordNotFound");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    //await CreateAuditrailsAsync(new T(), "Record identifier was null or empty on delete operation");
                    return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("RecordNotFound");
                }
            }
            var ids = identifiers.Select(s => s.Model);
            var records = await DbContext.Set<TUserBlobs>().Where(w => ids.Contains(w.UserBlobId)).ToListAsync();
            if (records.IsNull() || !records.Any())
            {
                //await CreateAuditrailsAsync(new T(), "Records not found on select in operation");
                return records.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            return records.AsEnumerable().ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<TUserBlobs>>> GetRangeOwnsUserBlobsAsync(IEnumerable<IIdentifier<string>> identifiers)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                //await CreateAuditrailsAsync(new T(), "Record identifiers was null or empty on delete operation");
                return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("RecordNotFound");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    //await CreateAuditrailsAsync(new T(), "Record identifier was null or empty on delete operation");
                    return Enumerable.Empty<TUserBlobs>().ToApiResponseFailed("RecordNotFound");
                }
            }
            var ids = identifiers.Select(s => s.Model);
            var records = await DbContext.Set<TUserBlobs>().Where(w => ids.Contains(w.UserBlobId)).ToListAsync();
            if (records.IsNull() || !records.Any())
            {
                //await CreateAuditrailsAsync(new T(), "Records not found on select in operation");
                return records.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            return records.AsEnumerable().ToApiResponse();
        }

        #endregion

        //#region Read as base 64 string

        //public async Task<string> ReadBlobAsBase64Async(TUserBlobs userBlob, bool ignoreBlobOwner = false)
        //{
        //    return Convert.ToBase64String(await ReadBlobAsByteArrayAsync(userBlob, ignoreBlobOwner));
        //}
        //public async Task<IEnumerable<string>> ReadBlobRangeAsBase64Async(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
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
        //public async Task<IApiResponse<string>> ReadBlobAsBase64ApiResponseAsync(TUserBlobs userBlob, bool ignoreBlobOwner = false)
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
        //public async Task<IEnumerable<IApiResponse<string>>> ReadBlobRangeAsBase64ApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
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
        //public async Task<string> ReadBlobRangeIntoZipFolderBase64Async(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    return Convert.ToBase64String(await ReadBlobRangeIntoZipFolderByteArrayAsync(userBlobs, ignoreBlobOwner));
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //public async Task<IApiResponse<string>> ReadBlobRangeIntoZipFolderBase64ApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    var streamApiResponse = await ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(userBlobs, ignoreBlobOwner);
        //    return new ApiResponse<string>(streamApiResponse.Status, data: Convert.ToBase64String(streamApiResponse.Data), message: streamApiResponse.Message);
        //}
        ///// <summary>
        ///// Read a blob as a base 64string from the storage.
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //public async Task<string> ReadBlobAsBase64Async(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        //{
        //    return Convert.ToBase64String(await ReadBlobAsByteArrayAsync(identifier, ignoreBlobOwner));
        //}
        ///// <summary>
        ///// Read a blob as a base 64 string from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //public async Task<IEnumerable<string>> ReadBlobRangeAsBase64Async(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
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
        //public async Task<IApiResponse<string>> ReadBlobAsBase64ApiResponseAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
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
        //public async Task<IEnumerable<IApiResponse<string>>> ReadBlobRangeAsBase64ApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
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
        //public async Task<string> ReadBlobRangeIntoZipFolderBase64Async(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    return Convert.ToBase64String(await ReadBlobRangeIntoZipFolderByteArrayAsync(identifiers, ignoreBlobOwner));
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a base 64 string from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //public async Task<IApiResponse<string>> ReadBlobRangeIntoZipFolderBase64ApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
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
        //public async Task<byte[]> ReadBlobAsByteArrayAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        //{
        //    return await (await ReadBlobAsStreamAsync(identifier, ignoreBlobOwner)).ToByteArrayAsync();
        //}
        ///// <summary>
        ///// Read a blob as a byte array from the storage
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IEnumerable<byte[]>> ReadBlobRangeAsByteArrayAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
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
        //public async Task<IApiResponse<byte[]>> ReadBlobAsByteArrayApiResponseAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
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
        //public async Task<IEnumerable<IApiResponse<byte[]>>> ReadBlobRangeAsByteArrayApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
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
        //public async Task<byte[]> ReadBlobRangeIntoZipFolderByteArrayAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    return await (await ReadBlobRangeIntoZipFolderStreamAsync(identifiers, ignoreBlobOwner)).ToByteArrayAsync();
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a byte array from the storage
        ///// </summary>
        ///// <returns></returns>
        //public async Task<ApiResponse<byte[]>> ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        //{
        //    var streamApiResponse = await ReadBlobRangeIntoZipFolderStreamApiResponseAsync(identifiers, ignoreBlobOwner);
        //    return new ApiResponse<byte[]>(streamApiResponse.Status, await streamApiResponse.Data?.ToByteArrayAsync(), streamApiResponse.Message);
        //}

        ///// <summary>
        ///// Read a blob as a byte array from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //public async Task<byte[]> ReadBlobAsByteArrayAsync(TUserBlobs userBlob, bool ignoreBlobOwner = false)
        //{
        //    return await (await ReadBlobAsStreamAsync(userBlob, ignoreBlobOwner)).ToByteArrayAsync();
        //}
        ///// <summary>
        ///// Read a blob as a byte array from the storage
        ///// </summary>
        ///// <param name="UserBlobs">The user blob to search</param>
        ///// <returns></returns>
        //public async Task<IEnumerable<byte[]>> ReadBlobRangeAsByteArrayAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
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
        //public async Task<IApiResponse<byte[]>> ReadBlobAsByteArrayApiResponseAsync(TUserBlobs userBlob, bool ignoreBlobOwner = false)
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
        //public async Task<IEnumerable<IApiResponse<byte[]>>> ReadBlobRangeAsByteArrayApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
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
        //public async Task<byte[]> ReadBlobRangeIntoZipFolderByteArrayAsync(this DbContext dbContext, IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    return (await ReadBlobRangeIntoZipFolderStreamAsync(userBlobs, ignoreBlobOwner)).ToByteArray();
        //}
        ///// <summary>
        ///// Read multiple blobs into a compressed zip folder as a byte array from the storage
        ///// </summary>
        ///// <param name="UserBlobsViewModels">The user blob to search</param>
        ///// <returns></returns>
        //public async Task<ApiResponse<byte[]>> ReadBlobRangeIntoZipFolderByteArrayApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        //{
        //    var streamApiResponse = await ReadBlobRangeIntoZipFolderStreamApiResponseAsync(userBlobs, ignoreBlobOwner);
        //    return new ApiResponse<byte[]>(streamApiResponse.Status, await streamApiResponse.Data?.ToByteArrayAsync(), streamApiResponse.Message);
        //}

        //#endregion

        #region Read as stream

        /// <inheritdoc/>
        public async Task<Stream> ReadBlobAsStreamAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        {
            var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(identifier, ignoreBlobOwner);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                //await DbContext.Set<TAuditTrails>().AddAsync(new T().CreateAuditTrails<T, TAuditTrails>("File was not found or the user is not the file owner", HttpContextAccessor));
                return null;
            }
            var userBlobsApiResponse = await GetOneUserBlobsAsync(identifier);
            await DbContext.Set<TAuditTrails>().AddAsync(userBlobsApiResponse.Data.CreateAuditTrails<TUserBlobs, TAuditTrails>("File was downloaded", HttpContextAccessor));
            return await BlobStorageService.BlobStorage.OpenReadAsync(userBlobsApiResponse.Data.FilePath);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Stream>> ReadBlobRangeAsStreamAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                //await CreateAuditrailsAsync(new T(), "identifiers is null or empty on method ReadBlobAsStream");
                //await dbContext.CreateAuditrailsAsync<IEnumerable<IIdentifier<string>>, TAuditTrails>(identifiers, "identifiers is null or empty on method ReadBlobAsStream", httpContextAccesor);
                return null;
            }
            var streams = new List<Stream>();
            foreach (var identifier in identifiers)
            {
                streams.Add(await ReadBlobAsStreamAsync(identifier, ignoreBlobOwner));
            }
            return streams;
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<Stream>> ReadBlobAsStreamApiResponseAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        {
            var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(identifier, ignoreBlobOwner);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<Stream>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            var userBlobsApiResponse = await GetOneUserBlobsAsync(identifier);
            //await CreateAuditrailsAsync(new T(), "UserBlob was readed as api response");
            //await dbContext.CreateAuditrailsAsync<TUserBlobs, TAuditTrails>(userBlobsApiResponse.Data, "UserBlob was readed as api response", httpContextAccesor);
            var stream = await BlobStorageService.BlobStorage.OpenReadAsync(userBlobsApiResponse.Data.FilePath);
            return new ApiResponse<Stream>(ApiResponseStatus.Succeeded, stream);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<IApiResponse<Stream>>> ReadBlobRangeAsStreamApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                //await dbContext.CreateAuditrailsAsync<IEnumerable<IIdentifier<string>>, TAuditTrails>(identifiers, "Identifier is null or empty on method ReadBlobAsStreamApiResponse", httpContextAccesor);
                return Enumerable.Empty<IApiResponse<Stream>>();
            }
            var streams = new List<IApiResponse<Stream>>();
            foreach (var identifier in identifiers)
            {
                streams.Add(await ReadBlobAsStreamApiResponseAsync(identifier, ignoreBlobOwner));
            }
            return streams;
        }
        /// <inheritdoc/>
        public async Task<Stream> ReadBlobRangeIntoZipFolderStreamAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                //await dbContext.CreateAuditrailsAsync<IEnumerable<IIdentifier<string>>, TAuditTrails>(identifiers, "Identifier is null or empty on method ReadBlobRangeIntoZipFolderStreamAsync", httpContextAccesor);
                return null;
            }
            var userBlobs = new List<TUserBlobs>();
            using MemoryStream zipStream = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var identifier in identifiers)
                {
                    var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(identifier, ignoreBlobOwner);
                    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                    {
                        return null;
                    }
                    var userBlobsApiResponse = await GetOneUserBlobsAsync(identifier);
                    userBlobs.Add(userBlobsApiResponse.Data);
                    var stream = await BlobStorageService.BlobStorage.OpenReadAsync(userBlobsApiResponse.Data.FilePath);
                    var entry = zip.CreateEntry(userBlobsApiResponse.Data.FileName);
                    using var entryStream = entry.Open();
                    await stream.CopyToAsync(entryStream);
                }
            }
            zipStream.Position = 0;
            //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "A list of UserBlobs were readed into a compressed zip folder as stream result", httpContextAccesor);
            return zipStream;
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<Stream>> ReadBlobRangeIntoZipFolderStreamApiResponseAsync(IEnumerable<IIdentifier<string>> identifiers, bool ignoreBlobOwner = false)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                //await dbContext.CreateAuditrailsAsync<IEnumerable<IIdentifier<string>>, TAuditTrails>(identifiers, "Identifier is null or empty on method ReadBlobsIntoZipFolderStreamApiResponseAsync", httpContextAccesor);
                return new ApiResponse<Stream>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            var userBlobs = new List<TUserBlobs>();
            using MemoryStream zipStream = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var identifier in identifiers)
                {
                    var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(identifier, ignoreBlobOwner);
                    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                    {
                        return new ApiResponse<Stream>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
                    }
                    var userBlobsApiResponse = await GetOneUserBlobsAsync(identifier);
                    userBlobs.Add(userBlobsApiResponse.Data);
                    var stream = await BlobStorageService.BlobStorage.OpenReadAsync(userBlobsApiResponse.Data.FilePath);
                    var entry = zip.CreateEntry(userBlobsApiResponse.Data.FileName);
                    using var entryStream = entry.Open();
                    await stream.CopyToAsync(entryStream);
                }
            }
            zipStream.Position = 0;
            //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "A list of UserBlobs were readed into a compressed zip folder as stream result", httpContextAccesor);
            return new ApiResponse<Stream>(ApiResponseStatus.Succeeded, zipStream);
        }
        /// <inheritdoc/>
        public async Task<Stream> ReadBlobAsStreamAsync(TUserBlobs userBlobs, bool ignoreBlobOwner = false)
        {
            var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlobs, ignoreBlobOwner);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return null;
            }
            //await dbContext.CreateAuditrailsAsync<TUserBlobs, TAuditTrails>(userBlobs, "File was found and read as stream", httpContextAccessor);
            return await BlobStorageService.BlobStorage.OpenReadAsync(userBlobs.FilePath);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Stream>> ReadBlobRangeAsStreamAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "Userblobs is null or empty on method ReadBlobAsStream", httpContextAccessor);
                return null;
            }
            var streams = new List<Stream>();
            foreach (var userBlob in userBlobs)
            {
                streams.Add(await ReadBlobAsStreamAsync(userBlob, ignoreBlobOwner));
            }
            return streams;
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<Stream>> ReadBlobAsStreamApiResponseAsync(TUserBlobs userBlobs, bool ignoreBlobOwner = false)
        {
            var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlobs, ignoreBlobOwner);
            if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
            {
                return new ApiResponse<Stream>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
            }
            //await dbContext.CreateAuditrailsAsync<TUserBlobs, TAuditTrails>(userBlobs, "UserBlob was readed as api response", httpContextAccessor);
            var stream = await BlobStorageService.BlobStorage.OpenReadAsync(userBlobs.FilePath);
            return new ApiResponse<Stream>(ApiResponseStatus.Succeeded, stream);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<IApiResponse<Stream>>> ReadBlobRangeAsStreamApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "Userblobs is null or empty on method ReadBlobAsStreamApiResponse", httpContextAccessor);
                return Enumerable.Empty<IApiResponse<Stream>>();
            }
            var streams = new List<IApiResponse<Stream>>();
            foreach (var userBlob in userBlobs)
            {
                streams.Add(await ReadBlobAsStreamApiResponseAsync(userBlob, ignoreBlobOwner));
            }
            return streams;
        }
        /// <inheritdoc/>
        public async Task<Stream> ReadBlobRangeIntoZipFolderStreamAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        {
            using MemoryStream zipStream = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var userBlob in userBlobs)
                {
                    var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, ignoreBlobOwner);
                    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                    {
                        return null;
                    }
                    var stream = await BlobStorageService.BlobStorage.OpenReadAsync(userBlob.FilePath);
                    var entry = zip.CreateEntry(userBlob.FileName);
                    using var entryStream = entry.Open();
                    await stream.CopyToAsync(entryStream);
                }
            }
            zipStream.Position = 0;
            //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "A list of UserBlobs were readed into a compressed zip folder as stream result", httpContextAccessor);
            return zipStream;
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<Stream>> ReadBlobRangeIntoZipFolderStreamApiResponseAsync(IEnumerable<TUserBlobs> userBlobs, bool ignoreBlobOwner = false)
        {
            if (userBlobs.IsNull() || !userBlobs.Any())
            {
                //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "Userblobs is null or empty on method ReadBlobsIntoZipFolderStreamApiResponseAsync", httpContextAccessor);
                return new ApiResponse<Stream>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            using MemoryStream zipStream = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var userBlob in userBlobs)
                {
                    var isBlobOwnerAndFileExists = await IsBlobOwnerAndFileExistsAsync(userBlob, ignoreBlobOwner);
                    if (isBlobOwnerAndFileExists.Status == ApiResponseStatus.Failed || !isBlobOwnerAndFileExists.Data)
                    {
                        return new ApiResponse<Stream>(ApiResponseStatus.Failed, isBlobOwnerAndFileExists.Message);
                    }
                    var stream = await BlobStorageService.BlobStorage.OpenReadAsync(userBlob.FilePath);
                    var entry = zip.CreateEntry(userBlob.FileName);
                    using var entryStream = entry.Open();
                    await stream.CopyToAsync(entryStream);
                }
            }
            zipStream.Position = 0;
            //await dbContext.CreateAuditrailsAsync<IEnumerable<TUserBlobs>, TAuditTrails>(userBlobs, "A list of UserBlobs were readed into a compressed zip folder as stream result", httpContextAccessor);
            return new ApiResponse<Stream>(ApiResponseStatus.Succeeded, zipStream);
        }

        #endregion

        #region Helpers

        /// <inheritdoc/>
        public async Task<IApiResponse<bool>> IsBlobOwnerAsync(TUserBlobs userBlobs)
        {
            if (userBlobs.IsNull())
            {
                return new ApiResponse<bool>(ApiResponseStatus.Failed, "FileNotFound");
            }
            var result = (await DbContext.Set<TUserBlobs>().FirstOrDefaultAsync(x => x.UserBlobId == userBlobs.UserBlobId && x.CreatedBy == HttpContextAccessor.HttpContext.User.UserId())).IsNotNull();
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, result);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<bool>> IsBlobOwnerAndFileExistsAsync(TUserBlobs userBlobs, bool ignoreBlobOwner = false)
        {
            if (userBlobs.IsNull())
            {
                return new ApiResponse<bool>(ApiResponseStatus.Failed, "FileNotFound");
            }
            if (!ignoreBlobOwner)
            {
                var isBlobOwner = await IsBlobOwnerAsync(userBlobs);
                if (isBlobOwner.Status == ApiResponseStatus.Failed || !isBlobOwner.Data)
                {
                    return new ApiResponse<bool>(ApiResponseStatus.Failed, isBlobOwner.Message);
                }
            }
            var fileExist = await BlobStorageService.BlobStorage.ExistsAsync(userBlobs.FilePath);
            if (!fileExist)
            {
                return new ApiResponse<bool>(ApiResponseStatus.Failed, fileExist, "FileNotFound");
            }
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, true);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<bool>> IsValidBlobDataAsync(IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules)
        {
            var isValidFormFileApiResponse = blobDatas.IsValidBlobDataApiResponse(formFileRules);
            if (isValidFormFileApiResponse.Status == ApiResponseStatus.Failed)
            {
                await CreateAuditrailsAsync(new T(), isValidFormFileApiResponse.Message);
                return new ApiResponse<bool>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message, true);
            }
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, true);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<bool>> IsBlobOwnerAsync(IIdentifier<string> identifier)
        {
            if (identifier.IsNull() || identifier.Model.IsNull())
            {
                return new ApiResponse<bool>(ApiResponseStatus.Failed, "FileNotFound");
            }
            var result = (await DbContext.Set<TUserBlobs>().AsNoTracking().FirstOrDefaultAsync(x => x.UserBlobId == identifier.Model && x.CreatedBy == HttpContextAccessor.HttpContext.User.UserId())).IsNotNull();
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, result);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<bool>> IsBlobOwnerAndFileExistsAsync(IIdentifier<string> identifier, bool ignoreBlobOwner = false)
        {
            if (identifier.IsNull() || identifier.Model.IsNull())
            {
                return new ApiResponse<bool>(ApiResponseStatus.Failed, "FileNotFound");
            }
            if (!ignoreBlobOwner)
            {
                var isBlobOwner = await IsBlobOwnerAsync(identifier);
                if (isBlobOwner.Status == ApiResponseStatus.Failed || !isBlobOwner.Data)
                {
                    return new ApiResponse<bool>(ApiResponseStatus.Failed, isBlobOwner.Message);
                }
            }
            var userBlobsApiResponse = await GetOneUserBlobsAsync(identifier);
            if (userBlobsApiResponse.Status.Failed())
            {
                return new ApiResponse<bool>(userBlobsApiResponse.Status, false, userBlobsApiResponse.Message);
            }
            var fileExist = await BlobStorageService.BlobStorage.ExistsAsync(userBlobsApiResponse.Data.FilePath);
            if (!fileExist)
            {
                return new ApiResponse<bool>(ApiResponseStatus.Failed, fileExist, "FileNotFound");
            }
            return new ApiResponse<bool>(ApiResponseStatus.Succeeded, true);
        }

        #endregion
    }
}

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using OneLine.Validations;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static class BaseDbContextExtensions
    {
        #region Save

        public static async Task<IApiResponse<T>> SaveAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, SaveOperation saveOperation, Action beforeSave = null, Action afterSave = null)
            where T : class, new()
        {
            beforeSave?.Invoke();
            if (saveOperation.IsAdd())
            {
                await dbContext.AddAsync(record);
            }
            else if (saveOperation.IsUpdate())
            {
                dbContext.Update(record);
            }
            var result = await dbContext.SaveChangesAsync();
            if (afterSave.IsNotNull() && result.Succeeded())
            {
                afterSave?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<T>> SaveValidatedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, SaveOperation saveOperation, Action beforeSave = null, Action afterSave = null)
            where T : class, new()
        {
            var apiResponse = record == null ? await new T().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await dbContext.SaveAsync(record, saveOperation, beforeSave, afterSave);
        }
        public static async Task<IApiResponse<T>> SaveAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, SaveOperation saveOperation, string userId, Action beforeSave = null, Action afterSave = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            beforeSave?.Invoke();
            if (saveOperation.IsAdd())
            {
                await dbContext.AddAuditedAsync(record, userId, controllerName, actionName, remoteIpAddress);
            }
            else if (saveOperation.IsUpdate())
            {
                await dbContext.UpdateAuditedAsync(record, userId, controllerName, actionName, remoteIpAddress);
            }
            var result = await dbContext.SaveChangesAsync();
            if (afterSave.IsNotNull() && result.Succeeded())
            {
                afterSave?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<T>> SaveValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, SaveOperation saveOperation, string userId, Action beforeSave = null, Action afterSave = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var apiResponse = await dbContext.ValidateAuditedAsync(record, validator, userId, controllerName, actionName, remoteIpAddress);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await dbContext.SaveAuditedAsync(record, saveOperation, userId, beforeSave, afterSave, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Save Range

        public static async Task<IApiResponse<IEnumerable<T>>> SaveRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, SaveOperation saveOperation, Action beforeSaveRange = null, Action afterSaveRange = null)
            where T : class, new()
        {
            beforeSaveRange?.Invoke();
            if (saveOperation.IsAdd())
            {
                await dbContext.AddRangeAsync(records);
            }
            else if (saveOperation.IsUpdate())
            {
                dbContext.UpdateRange(records);
            }
            var result = await dbContext.SaveChangesAsync();
            if (afterSaveRange.IsNotNull() && result.Succeeded())
            {
                afterSaveRange?.Invoke();
            }
            return result.TransactionResultApiResponse(records, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<IEnumerable<T>>> SaveRangeValidatedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, Action beforeSaveRange = null, Action afterSaveRange = null)
            where T : class, new()
        {
            var apiResponse = records.IsNullOrEmpty() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await dbContext.SaveRangeAsync(records, saveOperation, beforeSaveRange, afterSaveRange);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> SaveRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, SaveOperation saveOperation, string userId, Action beforeSaveRange = null, Action afterSaveRange = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            beforeSaveRange?.Invoke();
            if (saveOperation.IsAdd())
            {
                await dbContext.AddRangeAuditedAsync(records, userId, controllerName, actionName, remoteIpAddress);
            }
            else if (saveOperation.IsUpdate())
            {
                await dbContext.UpdateRangeAuditedAsync(records, userId, controllerName, actionName, remoteIpAddress);
            }
            var result = await dbContext.SaveChangesAsync();
            if (afterSaveRange.IsNotNull() && result.Succeeded())
            {
                afterSaveRange?.Invoke();
            }
            return result.TransactionResultApiResponse(records, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<IEnumerable<T>>> SaveRangeValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, string userId, Action beforeSaveRange = null, Action afterSaveRange = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var apiResponse = await dbContext.ValidateRangeAuditedAsync(records, validator, userId, controllerName, actionName, remoteIpAddress);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await dbContext.SaveRangeAuditedAsync(records, saveOperation, userId, beforeSaveRange, afterSaveRange, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Save Or Replace Reference List

        public static async Task<IApiResponse<IEnumerable<T>>> SaveOrReplaceRangeReferenceAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IEnumerable<T> records, Action<Action<Expression<Func<T, bool>>>> onDeletePredicate, Action beforeDeleteRange = null, Action afterDeleteRange = null)
            where T : class, new()
        {
            beforeDeleteRange?.Invoke();
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            Expression<Func<T, bool>> deletePredicate = default;
            onDeletePredicate.Invoke(predicate => deletePredicate = predicate);
            var toDeletedRecords = dbContext.Set<T>().Where(deletePredicate);
            if (toDeletedRecords.IsNullOrEmpty())
            {
                return toDeletedRecords.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            dbContext.RemoveRange(toDeletedRecords);
            await dbContext.AddRangeAsync(records);
            var result = await dbContext.SaveChangesAsync();
            if (afterDeleteRange.IsNotNull() && result.Succeeded())
            {
                afterDeleteRange?.Invoke();
            }
            return result.TransactionResultApiResponse(records, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<IEnumerable<T>>> SaveOrReplaceRangeReferenceValidatedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IEnumerable<T> records, IValidator validator, Action<Action<Expression<Func<T, bool>>>> onDeletePredicate, Action beforeDeleteRange = null, Action afterDeleteRange = null)
            where T : class, new()
        {
            var apiResponse = records.IsNullOrEmpty() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await dbContext.SaveOrReplaceRangeReferenceAsync(identifier, records, onDeletePredicate, beforeDeleteRange, afterDeleteRange);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> SaveOrReplaceRangeReferenceAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IEnumerable<T> records, Action<Action<Expression<Func<T, bool>>>> onDeletePredicate, string userId, Action beforeDeleteRange = null, Action afterDeleteRange = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            beforeDeleteRange?.Invoke();
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                await dbContext.CreateAuditrailsAsync(identifier, "Record indentifier or model was null in validation operation", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            Expression<Func<T, bool>> deletePredicate = default;
            onDeletePredicate.Invoke(predicate => deletePredicate = predicate);
            var toDeletedRecords = dbContext.Set<T>().Where(deletePredicate);
            if (toDeletedRecords.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(toDeletedRecords, "Records collection to delete was null or empty in validation operation", userId, controllerName, actionName, remoteIpAddress);
                return toDeletedRecords.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            await dbContext.RemoveRangeAuditedAsync(toDeletedRecords, userId, controllerName, actionName, remoteIpAddress);
            await dbContext.AddRangeAuditedAsync(records, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDeleteRange.IsNotNull() && result.Succeeded())
            {
                afterDeleteRange?.Invoke();
            }
            return result.TransactionResultApiResponse(records, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<IEnumerable<T>>> SaveOrReplaceRangeReferenceValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IEnumerable<T> records, IValidator validator, Action<Action<Expression<Func<T, bool>>>> onDeletePredicate, string userId, Action beforeDeleteRange = null, Action afterDeleteRange = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var apiResponse = records.IsNullOrEmpty() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await dbContext.SaveOrReplaceRangeReferenceAuditedAsync(identifier, records, onDeletePredicate, userId, beforeDeleteRange, afterDeleteRange, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Save Blobs

        public static async Task<IApiResponse<T>> SaveValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, T originalRecord, IValidator validator, string userId, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas, IBlobStorage blobsStorageService, bool ignoreBlobOwner = false, Action beforeSave = null, Action afterSave = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var validationApiResponse = await dbContext.ValidatedAuditedAsync(record, validator, userId, saveOperation, uploadBlobDatas, controllerName, actionName, remoteIpAddress);
            if (validationApiResponse.Status.Failed())
            {
                return validationApiResponse;
            }
            if (saveOperation.IsAdd())
            {
                await dbContext.CreateAndBindUserBlobsRangeAsync(record, uploadBlobDatas, blobsStorageService, userId, typeof(T).Name, controllerName, actionName, remoteIpAddress);
            }
            else if (saveOperation.IsUpdate())
            {
                validationApiResponse = await dbContext.ValidatedAuditedAsync(originalRecord, validator, userId, saveOperation, uploadBlobDatas, controllerName, actionName, remoteIpAddress);
                if (validationApiResponse.Status.Failed())
                {
                    return validationApiResponse;
                }
                await dbContext.UpdateAndBindUserBlobsRangeAsync(record, originalRecord, uploadBlobDatas, blobsStorageService, userId, typeof(T).Name, ignoreBlobOwner, controllerName, actionName, remoteIpAddress);
            }
            return await dbContext.SaveAsync(record, saveOperation, beforeSave, afterSave);
        }

        #endregion

        #region Search

        public static IApiResponse<IEnumerable<T>> Search<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, Action<Action<IQueryable<T>>> beforePredicate = null, Action<Action<IQueryable<T>>> afterPredicate = null, Action<IQueryable<T>> queryChanged = null, bool NoTracking = true)
            where T : class
        {
            IQueryable<T> query;
            if (NoTracking)
            {
                query = dbContext.Set<T>().AsNoTracking().AsQueryable();
            }
            else
            {
                query = dbContext.Set<T>().AsQueryable<T>();
            }
            queryChanged?.Invoke(query);
            beforePredicate?.Invoke(beforeQuery => query = beforeQuery);
            queryChanged?.Invoke(query);
            query = query.Where(predicate).AsQueryable();
            queryChanged?.Invoke(query);
            afterPredicate?.Invoke(afterQuery => query = afterQuery);
            queryChanged?.Invoke(query);
            return query.AsEnumerable().ToApiResponse();
        }
        public static async Task<IApiResponse<IEnumerable<T>>> SearchAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, string userId, Action<Action<IQueryable<T>>> beforePredicate = null, Action<Action<IQueryable<T>>> afterPredicate = null, Action<IQueryable<T>> queryChanged = null, string controllerName = null, string actionName = null, string remoteIpAddress = null, bool NoTracking = true)
            where T : class
        {
            IQueryable<T> query;
            if (NoTracking)
            {
                query = dbContext.Set<T>().AsNoTracking().AsQueryable();
            }
            else
            {
                query = dbContext.Set<T>().AsQueryable<T>();
            }
            queryChanged?.Invoke(query);
            beforePredicate?.Invoke(beforeQuery => query = beforeQuery);
            queryChanged?.Invoke(query);
            query = query.Where(predicate).AsQueryable();
            queryChanged?.Invoke(query);
            afterPredicate?.Invoke(afterQuery => query = afterQuery);
            queryChanged?.Invoke(query);
            var results = query.AsEnumerable();
            await dbContext.CreateAuditrailsAsync(results, "Records on search operation", userId, controllerName, actionName, remoteIpAddress);
            return results.ToApiResponse();
        }
        public static IApiResponse<IPaged<IEnumerable<T>>> SearchPaged<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, int? pageIndex, int? pageSize, string sortBy, bool? descending, out int count, Action<Action<IQueryable<T>>> beforePredicate = null, Action<Action<IQueryable<T>>> afterPredicateBeforeSorting = null, Action<Action<IQueryable<T>>> afterSortingBeforePaging = null, Action<Action<IPaged<IEnumerable<T>>>> afterPaging = null, Action<IQueryable<T>> queryChanged = null, bool NoTracking = true)
            where T : class
        {
            IQueryable<T> query;
            if (NoTracking)
            {
                query = dbContext.Set<T>().AsNoTracking().AsQueryable();
            }
            else
            {
                query = dbContext.Set<T>().AsQueryable<T>();
            }
            queryChanged?.Invoke(query);
            beforePredicate?.Invoke(beforeQuery => query = beforeQuery);
            queryChanged?.Invoke(query);
            query = query.Where(predicate).AsQueryable();
            queryChanged?.Invoke(query);
            afterPredicateBeforeSorting?.Invoke(afterQuery => query = afterQuery);
            queryChanged?.Invoke(query);
            if (descending.HasValue && !string.IsNullOrWhiteSpace(sortBy))
            {
                if (descending.Value)
                {
                    query = query.OrderByPropertyDescending(sortBy);
                }
                else
                {
                    query = query.OrderByProperty(sortBy);
                }
                queryChanged?.Invoke(query);
            }
            afterSortingBeforePaging?.Invoke(afterSorting => query = afterSorting);
            queryChanged?.Invoke(query);
            var pagedQuery = query.ToPaged(pageIndex, pageSize, out count);
            queryChanged?.Invoke(query);
            afterPaging?.Invoke(afterPaging => pagedQuery = afterPaging);
            queryChanged?.Invoke(query);
            return pagedQuery.ToApiResponse();
        }
        public static async Task<IApiResponse<IPaged<IEnumerable<T>>>> SearchPagedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, int? pageIndex, int? pageSize, string sortBy, bool? descending, string userId, Action<Action<IQueryable<T>>> beforePredicate = null, Action<Action<IQueryable<T>>> afterPredicateBeforeSorting = null, Action<Action<IQueryable<T>>> afterSortingBeforePaging = null, Action<Action<IPaged<IEnumerable<T>>>> afterPaging = null, Action<IQueryable<T>> queryChanged = null, string controllerName = null, string actionName = null, string remoteIpAddress = null, bool NoTracking = true)
            where T : class
        {
            IQueryable<T> query;
            if (NoTracking)
            {
                query = dbContext.Set<T>().AsNoTracking().AsQueryable();
            }
            else
            {
                query = dbContext.Set<T>().AsQueryable<T>();
            }
            queryChanged?.Invoke(query);
            beforePredicate?.Invoke(beforeQuery => query = beforeQuery);
            queryChanged?.Invoke(query);
            query = query.Where(predicate).AsQueryable();
            queryChanged?.Invoke(query);
            afterPredicateBeforeSorting?.Invoke(afterQuery => query = afterQuery);
            queryChanged?.Invoke(query);
            if (descending.HasValue && !string.IsNullOrWhiteSpace(sortBy))
            {
                if (descending.Value)
                {
                    query = query.OrderByPropertyDescending(sortBy);
                }
                else
                {
                    query = query.OrderByProperty(sortBy);
                }
                queryChanged?.Invoke(query);
            }
            afterSortingBeforePaging?.Invoke(afterSorting => query = afterSorting);
            queryChanged?.Invoke(query);
            var pagedQuery = query.ToPaged(pageIndex, pageSize, out _);
            queryChanged?.Invoke(query);
            afterPaging?.Invoke(afterPaging => pagedQuery = afterPaging);
            queryChanged?.Invoke(query);
            await dbContext.CreateAuditrailsAsync(pagedQuery, "Records paged on search operation", userId, controllerName, actionName, remoteIpAddress);
            return pagedQuery.ToApiResponse();
        }

        #endregion

        #region Search and Convert to Csv

        public static IApiResponse<byte[]> SearchAndConvertToCsvByteArray<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, Action<Action<IQueryable<T>>> beforePredicate = null, Action<Action<IQueryable<T>>> afterPredicate = null, Action<IQueryable<T>> queryChanged = null, bool NoTracking = true)
            where T : class
        {
            var result = dbContext.Search(predicate, beforePredicate, afterPredicate, queryChanged, NoTracking);
            return result?.Data?.ToCsvByteArray().ToApiResponse();
        }
        public static async Task<IApiResponse<byte[]>> SearchAuditedAndConvertToCsvByteArrayAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, string userId, Action<Action<IQueryable<T>>> beforePredicate = null, Action<Action<IQueryable<T>>> afterPredicate = null, Action<IQueryable<T>> queryChanged = null, string controllerName = null, string actionName = null, string remoteIpAddress = null, bool NoTracking = true)
            where T : class
        {
            var result = await dbContext.SearchAuditedAsync(predicate, userId, beforePredicate, afterPredicate, queryChanged, controllerName, actionName, remoteIpAddress, NoTracking);
            return result?.Data?.ToCsvByteArray().ToApiResponse();
        }
        public static IApiResponse<byte[]> SearchPagedAndConvertToCsvByteArray<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, int? pageIndex, int? pageSize, string sortBy, bool? descending, out int count, Action<Action<IQueryable<T>>> beforePredicate = null, Action<Action<IQueryable<T>>> afterPredicateBeforeSorting = null, Action<Action<IQueryable<T>>> afterSortingBeforePaging = null, Action<Action<IPaged<IEnumerable<T>>>> afterPaging = null, Action<IQueryable<T>> queryChanged = null, bool NoTracking = true)
            where T : class
        {
            var result = dbContext.SearchPaged(predicate, pageIndex, pageSize, sortBy, descending, out count, beforePredicate, afterPredicateBeforeSorting, afterSortingBeforePaging, afterPaging, queryChanged, NoTracking);
            return result?.Data?.Data?.ToCsvByteArray().ToApiResponse();
        }
        public static async Task<IApiResponse<byte[]>> SearchPagedAuditedAndConvertToCsvByteArrayAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, int? pageIndex, int? pageSize, string sortBy, bool? descending, string userId, Action<Action<IQueryable<T>>> beforePredicate = null, Action<Action<IQueryable<T>>> afterPredicateBeforeSorting = null, Action<Action<IQueryable<T>>> afterSortingBeforePaging = null, Action<Action<IPaged<IEnumerable<T>>>> afterPaging = null, Action<IQueryable<T>> queryChanged = null, string controllerName = null, string actionName = null, string remoteIpAddress = null, bool NoTracking = true)
            where T : class
        {
            var result = await dbContext.SearchPagedAuditedAsync(predicate, pageIndex, pageSize, sortBy, descending, userId, beforePredicate, afterPredicateBeforeSorting, afterSortingBeforePaging, afterPaging, queryChanged, controllerName, actionName, remoteIpAddress, NoTracking);
            return result?.Data?.Data?.ToCsvByteArray().ToApiResponse();
        }
       
        #endregion

        #region Import Csv

        public static async Task<IApiResponse<IEnumerable<T>>> ImportCsvUploadAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IUploadBlobData uploadBlobDatas, SaveOperation saveOperation, Action beforeSaveRange = null, Action afterSaveRange = null)
            where T : class, new()
        {
            var any = uploadBlobDatas.IsNotNull() && uploadBlobDatas.BlobDatas.IsNotNullAndNotEmpty();
            if (!any)
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            if (any && uploadBlobDatas.FormFileRules.IsNotNull())
            {
                var isValidFormFileApiResponse = uploadBlobDatas.BlobDatas.IsValidBlobDataApiResponse(uploadBlobDatas.FormFileRules);
                if (isValidFormFileApiResponse.Status.Failed())
                {
                    return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message);
                }
            }
            var records = new List<T>();
            foreach (var blobData in uploadBlobDatas.BlobDatas)
            {
                records.AddRange(blobData.Data.ReadCsv<T>());
            }
            return await dbContext.SaveRangeAsync(records, saveOperation, beforeSaveRange, afterSaveRange);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> ImportCsvUploadAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IUploadBlobData uploadBlobDatas, SaveOperation saveOperation, string userId, Action beforeSaveRange = null, Action afterSaveRange = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var any = uploadBlobDatas.IsNotNull() && uploadBlobDatas.BlobDatas.IsNotNullAndNotEmpty();
            if (!any)
            {
                await dbContext.CreateAuditrailsAsync(new UserBlobs(), "No file uploaded", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            if (any && uploadBlobDatas.FormFileRules.IsNotNull())
            {
                var isValidFormFileApiResponse = uploadBlobDatas.BlobDatas.IsValidBlobDataApiResponse(uploadBlobDatas.FormFileRules);
                if (isValidFormFileApiResponse.Status.Failed())
                {
                    return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message);
                }
            }
            var records = new List<T>();
            foreach (var blobData in uploadBlobDatas.BlobDatas)
            {
                records.AddRange(blobData.Data.ReadCsv<T>());
            }
            return await dbContext.SaveRangeAuditedAsync(records, saveOperation, userId, beforeSaveRange, afterSaveRange, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Get One

        public static async Task<IApiResponse<T>> GetOneAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, bool NoTracking = true)
            where T : class, new()
        {
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                return new T().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var record = await dbContext.Set<T>().FindAsync(identifier.Model);
            if (record == null)
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            if (NoTracking)
            {
                dbContext.Entry(record).State = EntityState.Detached;
            }
            return record.ToApiResponse();
        }
        public static async Task<IApiResponse<T>> GetOneAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null, bool NoTracking = true)
            where T : class, new()
        {
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                await dbContext.CreateAuditrailsAsync(identifier, "Record indentifier or model was null on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return new T().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var record = await dbContext.Set<T>().FindAsync(identifier.Model);
            if (record == null)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record not found on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return record.ToApiResponseFailed("RecordNotFound");
            }
            if (NoTracking)
            {
                dbContext.Entry(record).State = EntityState.Detached;
            }
            await dbContext.CreateAuditrailsAsync(record, "Record selected on get one operation", userId, controllerName, actionName, remoteIpAddress);
            return record.ToApiResponse();
        }
        public static async Task<IApiResponse<T>> GetOneAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, bool NoTracking = true)
            where T : class, new()
        {
            if (predicate == null)
            {
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            T record = await Task.Run(() => dbContext.Set<T>().Where(predicate).FirstOrDefault());
            if (record == null)
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            if (NoTracking)
            {
                dbContext.Entry(record).State = EntityState.Detached;
            }
            return record.ToApiResponse();
        }
        public static async Task<IApiResponse<T>> GetOneAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null, bool NoTracking = true)
            where T : class, new()
        {
            if (predicate == null)
            {
                await dbContext.CreateAuditrailsAsync(predicate, "predicate was null on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            T record = await Task.Run(() => dbContext.Set<T>().Where(predicate).FirstOrDefault());
            if (record == null)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record not found on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return record.ToApiResponseFailed("RecordNotFound");
            }
            if (NoTracking)
            {
                dbContext.Entry(record).State = EntityState.Detached;
            }
            await dbContext.CreateAuditrailsAsync(record, "Record selected on get one operation", userId, controllerName, actionName, remoteIpAddress);
            return record.ToApiResponse();
        }
        public static IApiResponse<T> GetOne<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Action<Action<T>> onGetOne)
            where T : class, new()
        {
            if (onGetOne == null)
            {
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            T record = default;
            onGetOne.Invoke(result => record = result);
            if (record == null)
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            return record.ToApiResponse();
        }
        public static async Task<IApiResponse<T>> GetOneAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, string userId, Action<Action<T>> onGetOne, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (onGetOne == null)
            {
                await dbContext.CreateAuditrailsAsync(onGetOne, "predicate was null on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            T record = default;
            onGetOne.Invoke(result => record = result);
            if (record == null)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record not found on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return record.ToApiResponseFailed("RecordNotFound");
            }
            await dbContext.CreateAuditrailsAsync(record, "Record selected on get one operation", userId, controllerName, actionName, remoteIpAddress);
            return record.ToApiResponse();
        }
        
        #endregion

        #region Get Range

        public static IApiResponse<IEnumerable<T>> GetRange<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, Action<Action<IList<T>>> onGetRange)
            where T : class, new()
        {
            if (identifiers.IsNullOrEmpty())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
                }
            }
            IList<T> records = default;
            onGetRange.Invoke(result => records = result);
            if (records.IsNullOrEmpty())
            {
                return records.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            return records.AsEnumerable().ToApiResponse();
        }
        public static async Task<IApiResponse<IEnumerable<T>>> GetRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, string userId, Action<Action<IList<T>>> onGetRange, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (identifiers.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Record identifiers was null or empty on delete operation", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    await dbContext.CreateAuditrailsAsync(identifier, "Record identifier was null or empty on delete operation", userId, controllerName, actionName, remoteIpAddress);
                    return Enumerable.Empty<T>().ToApiResponseFailed("RecordNotFound");
                }
            }
            IList<T> records = default;
            onGetRange.Invoke(result => records = result);
            if (records.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(records, "Records not found on select in operation", userId, controllerName, actionName, remoteIpAddress);
                return records.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            await dbContext.CreateAuditrailsAsync(records, "Records selected on select range operation", userId, controllerName, actionName, remoteIpAddress);
            return records.AsEnumerable().ToApiResponse();
        }
        public static async Task<IApiResponse<IEnumerable<T>>> GetRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, bool NoTracking = true)
            where T : class, new()
        {
            if (predicate == null)
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            IList<T> records = default;
            if (NoTracking)
            {
                records = await Task.Run(() => dbContext.Set<T>().AsNoTracking().Where(predicate).ToList());
            }
            else
            {
                records = await Task.Run(() => dbContext.Set<T>().Where(predicate).ToList());
            }
            if (records.IsNullOrEmpty())
            {
                return records.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            return records.AsEnumerable().ToApiResponse();
        }
        public static async Task<IApiResponse<IEnumerable<T>>> GetRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null, bool NoTracking = true)
            where T : class, new()
        {
            if (predicate == null)
            {
                await dbContext.CreateAuditrailsAsync(predicate, "predicate was null or empty on delete operation", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            IList<T> records = default;
            if (NoTracking)
            {
                records = await Task.Run(() => dbContext.Set<T>().AsNoTracking().Where(predicate).ToList());
            }
            else
            {
                records = await Task.Run(() => dbContext.Set<T>().Where(predicate).ToList());
            }
            if (records.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(records, "Records not found on select in operation", userId, controllerName, actionName, remoteIpAddress);
                return records.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            await dbContext.CreateAuditrailsAsync(records, "Records selected on select range operation", userId, controllerName, actionName, remoteIpAddress);
            return records.AsEnumerable().ToApiResponse();
        }

        #endregion

        #region Delete

        public static async Task<IApiResponse<T>> DeleteAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            beforeDelete?.Invoke();
            dbContext.Remove(record);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<T>> DeleteValidatedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            var apiResponse = record == null ? await new T().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await dbContext.DeleteAsync(record, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<T>> DeleteAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            beforeDelete?.Invoke();
            await dbContext.RemoveAuditedAsync(record, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<T>> DeleteValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var apiResponse = record == null ? await new T().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            beforeDelete?.Invoke();
            await dbContext.RemoveAuditedAsync(record, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }

        #endregion

        #region Delete Range

        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            beforeDelete?.Invoke();
            dbContext.RemoveRange(records);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(records, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            var apiResponse = records.IsNullOrEmpty() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await dbContext.DeleteRangeAsync(records, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            beforeDelete?.Invoke();
            await dbContext.RemoveRangeAuditedAsync(records, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(records, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var apiResponse = records == null ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            beforeDelete?.Invoke();
            await dbContext.RemoveRangeAuditedAsync(records, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(records, "TransactionCompletedSuccessfully", "TransactionFailed");
        }

        #endregion

        #region Delete By Identifier

        public static async Task<IApiResponse<T>> DeleteAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                return new T().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var record = await dbContext.Set<T>().FindAsync(identifier.Model);
            beforeDelete?.Invoke();
            dbContext.Remove(record);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<T>> DeleteAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                await dbContext.CreateAuditrailsAsync(identifier, "Record indentifier or model was null on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return new T().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            beforeDelete?.Invoke();
            var record = await dbContext.Set<T>().FindAsync(identifier.Model);
            await dbContext.RemoveAuditedAsync(record, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }

        #endregion

        #region Delete Range By Identifier

        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, Action<Action<IList<T>>> onGetRange, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            if (identifiers.IsNullOrEmpty())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
                }
            }
            IList<T> records = default;
            onGetRange.Invoke(result => records = result);
            beforeDelete?.Invoke();
            dbContext.RemoveRange(records);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(records.AsEnumerable(), "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, string userId, Action<Action<IList<T>>> onGetRange, string controllerName = null, string actionName = null, string remoteIpAddress = null, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            if (identifiers.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(identifiers, "Record indentifier or model was null on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            beforeDelete?.Invoke();
            IList<T> records = default;
            onGetRange.Invoke(result => records = result);
            await dbContext.RemoveRangeAuditedAsync(records, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(records.AsEnumerable(), "TransactionCompletedSuccessfully", "TransactionFailed");
        }

        #endregion

        #region Delete By Predicate

        public static async Task<IApiResponse<T>> DeleteAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            if (predicate == null)
            {
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            var record = await Task.Run(() => dbContext.Set<T>().Where(predicate).FirstOrDefault());
            beforeDelete?.Invoke();
            dbContext.Remove(record);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<T>> DeleteAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (predicate == null)
            {
                await dbContext.CreateAuditrailsAsync(predicate, "predicate was null on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            beforeDelete?.Invoke();
            var record = await Task.Run(() => dbContext.Set<T>().Where(predicate).FirstOrDefault());
            await dbContext.RemoveAuditedAsync(record, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(record, "TransactionCompletedSuccessfully", "TransactionFailed");
        }

        #endregion

        #region Delete Range By Predicate

        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            if (predicate == null)
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            var records = await Task.Run(() => dbContext.Set<T>().Where(predicate).ToList());
            beforeDelete?.Invoke();
            dbContext.RemoveRange(records);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(records.AsEnumerable(), "TransactionCompletedSuccessfully", "TransactionFailed");
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (predicate == null)
            {
                await dbContext.CreateAuditrailsAsync(predicate, "predicate was null on select one operation", userId, controllerName, actionName, remoteIpAddress);
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            beforeDelete?.Invoke();
            var records = await Task.Run(() => dbContext.Set<T>().Where(predicate).ToList());
            await dbContext.RemoveRangeAuditedAsync(records, userId, controllerName, actionName, remoteIpAddress);
            var result = await dbContext.SaveChangesAsync();
            if (afterDelete.IsNotNull() && result.Succeeded())
            {
                afterDelete?.Invoke();
            }
            return result.TransactionResultApiResponse(records.AsEnumerable(), "TransactionCompletedSuccessfully", "TransactionFailed");
        }

        #endregion

        #region Delete Blobs

        public static async Task<IApiResponse<T>> DeleteAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IBlobStorage blobsStorageService, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntityAsync(record, blobsStorageService, null);
            return await dbContext.DeleteAsync(record, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<T>> DeleteValidatedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, IBlobStorage blobsStorageService, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntityAsync(record, blobsStorageService, null);
            return await dbContext.DeleteValidatedAsync(record, validator, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<T>> DeleteAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IBlobStorage blobsStorageService, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntityAsync(record, blobsStorageService, userId, controllerName, actionName, remoteIpAddress);
            return await dbContext.DeleteAuditedAsync(record, blobsStorageService, userId, beforeDelete, afterDelete, controllerName, actionName, remoteIpAddress);
        }
        public static async Task<IApiResponse<T>> DeleteValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, IBlobStorage blobsStorageService, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntityAsync(record, blobsStorageService, userId, controllerName, actionName, remoteIpAddress);
            return await dbContext.DeleteValidatedAuditedAsync(record, validator, blobsStorageService, userId, beforeDelete, afterDelete, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Delete Range Blobs

        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IBlobStorage blobsStorageService, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntitiesAsync(records, blobsStorageService, null);
            return await dbContext.DeleteRangeAsync(records, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, IBlobStorage blobsStorageService, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntitiesAsync(records, blobsStorageService, null);
            return await dbContext.DeleteRangeValidatedAsync(records, validator, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IBlobStorage blobsStorageService, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntitiesAsync(records, blobsStorageService, userId, controllerName, actionName, remoteIpAddress);
            return await dbContext.DeleteRangeAuditedAsync(records, blobsStorageService, userId, beforeDelete, afterDelete, controllerName, actionName, remoteIpAddress);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, IBlobStorage blobsStorageService, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntitiesAsync(records, blobsStorageService, userId, controllerName, actionName, remoteIpAddress);
            return await dbContext.DeleteRangeValidatedAuditedAsync(records, validator, blobsStorageService, userId, beforeDelete, afterDelete, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Delete By Identifier Blobs

        public static async Task<IApiResponse<T>> DeleteAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobsStorageService, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntityAsync(identifier, blobsStorageService, null);
            return await dbContext.DeleteAsync<T>(identifier, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<T>> DeleteAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IIdentifier<string> identifier, IBlobStorage blobsStorageService, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntityAsync(identifier, blobsStorageService, userId, controllerName, actionName, remoteIpAddress);
            return await dbContext.DeleteAuditedAsync<T>(identifier, userId, beforeDelete, afterDelete, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Delete Range By Identifier Blobs

        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobsStorageService, Action<Action<IList<T>>> onGetRange, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntitiesAsync(identifiers, blobsStorageService, null);
            return await dbContext.DeleteRangeAsync(identifiers, onGetRange, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<IIdentifier<string>> identifiers, IBlobStorage blobsStorageService, string userId, Action<Action<IList<T>>> onGetRange, string controllerName = null, string actionName = null, string remoteIpAddress = null, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntitiesAsync(identifiers, blobsStorageService, userId, controllerName, actionName, remoteIpAddress);
            return await dbContext.DeleteRangeAuditedAsync(identifiers, blobsStorageService, userId, onGetRange, controllerName, actionName, remoteIpAddress, beforeDelete, afterDelete);
        }

        #endregion

        #region Delete By Predicate Blobs

        public static async Task<IApiResponse<T>> DeleteAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, IBlobStorage blobsStorageService, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntityAsync(predicate, blobsStorageService, null);
            return await dbContext.DeleteAsync(predicate, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<T>> DeleteAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, IBlobStorage blobsStorageService, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntityAsync(predicate, blobsStorageService, userId, controllerName, actionName, remoteIpAddress);
            return await dbContext.DeleteAuditedAsync(predicate, blobsStorageService, userId, beforeDelete, afterDelete, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Delete Range By Predicate

        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, IBlobStorage blobsStorageService, Action beforeDelete = null, Action afterDelete = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntitiesAsync(predicate, blobsStorageService, null);
            return await dbContext.DeleteRangeAsync(predicate, beforeDelete, afterDelete);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, Expression<Func<T, bool>> predicate, IBlobStorage blobsStorageService, string userId, Action beforeDelete = null, Action afterDelete = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            await dbContext.DeleteUserBlobsFromEntitiesAsync(predicate, blobsStorageService, userId, controllerName, actionName, remoteIpAddress);
            return await dbContext.DeleteRangeAuditedAsync(predicate, blobsStorageService, userId, beforeDelete, afterDelete, controllerName, actionName, remoteIpAddress);
        }

        #endregion

        #region Add Audit Trails

        public static void AddAuditrails<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Add(entity.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void AddAuditrails<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Add(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditrailsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditrailsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Add Range Audit Trails

        public static void AddRangeAuditrails<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
           where T : class
        {
            dbContext.AddRange(entities.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddRangeAuditrailsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddRangeAsync(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Create Audit Trails

        public static void CreateAuditrails<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Add(entity.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
            dbContext.SaveChanges();
        }
        public static async Task CreateAuditrailsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
            await dbContext.SaveChangesAsync();
        }

        #endregion

        #region Create Range Audit Trails

        public static void CreateRangeAuditrails<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
           where T : class
        {
            dbContext.AddRange(entities.CreateRangeAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
            dbContext.SaveChanges();
        }
        public static async Task CreateRangeAuditrailsAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddRangeAsync(entities.CreateRangeAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
            await dbContext.SaveChangesAsync();
        }

        #endregion

        #region Add Audited

        public static void AddAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Add(entity);
            dbContext.Add(entity.CreateAuditTrails(TransactionType.Add, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddAsync(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(TransactionType.Add, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void AddAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Add(entity);
            dbContext.Add(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddAsync(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Add Range Audited

        public static void AddRangeAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.AddRange(entities);
            dbContext.AddRange(entities.CreateRangeAuditTrails(TransactionType.Add, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.AddRangeAsync(entities.CreateRangeAuditTrails(TransactionType.Add, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void AddRangeAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.AddRange(entities);
            dbContext.AddRange(entities.CreateRangeAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.AddRangeAsync(entities.CreateRangeAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Update Audited

        public static void UpdateAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Update(entity);
            dbContext.Add(entity.CreateAuditTrails(TransactionType.Update, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task UpdateAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Update(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(TransactionType.Update, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void UpdateAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Update(entity);
            dbContext.Add(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task UpdateAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Update(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Update Range Audited

        public static void UpdateRangeAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
           where T : class
        {
            dbContext.UpdateRange(entities);
            dbContext.AddRange(entities.CreateRangeAuditTrails(TransactionType.Update, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task UpdateRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.UpdateRange(entities);
            await dbContext.AddAsync(entities.CreateRangeAuditTrails(TransactionType.Update, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void UpdateRangeAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.UpdateRange(entities);
            dbContext.Add(entities.CreateRangeAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task UpdateRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.UpdateRange(entities);
            await dbContext.AddAsync(entities.CreateRangeAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Remove Audited

        public static void RemoveAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Remove(entity);
            dbContext.Add(entity.CreateAuditTrails(TransactionType.Delete, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task RemoveAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Remove(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(TransactionType.Delete, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void RemoveAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Remove(entity);
            dbContext.Add(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task RemoveAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.Remove(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Remove Range Audited

        public static void RemoveRangeAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.RemoveRange(entities);
            dbContext.Add(entities.CreateRangeAuditTrails(TransactionType.Delete, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task RemoveRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.RemoveRange(entities);
            await dbContext.AddAsync(entities.CreateRangeAuditTrails(TransactionType.Delete, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void RemoveRangeAudited<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.RemoveRange(entities);
            dbContext.Add(entities.CreateRangeAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task RemoveRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class
        {
            dbContext.RemoveRange(entities);
            await dbContext.AddAsync(entities.CreateRangeAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Validate Audited

        public static async Task<IApiResponse<T>> ValidateAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (record == null)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record was null on validation operation", null, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "ErrorSavingRecord");
            }
            if (validator == null)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record validator was null on validation operation", null, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "ModelStateInvalid");
            }
            var validationResult = await validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record validation failed on validation operation", null, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, record);
        }
        public static async Task<IApiResponse<T>> ValidateAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (record == null)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record was null on validation operation", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "RecordIsNull");
            }
            if (validator == null)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record validator was null on validation operation", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "ValidatorIsNull");
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                await dbContext.CreateAuditrailsAsync(record, "The userId was null on validation operation", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, "UserIdIsNullOrEmpty");
            }
            var validationResult = await validator.ValidateAsync(record);
            if (!validationResult.IsValid)
            {
                await dbContext.CreateAuditrailsAsync(record, "Record validation failed on validation operation", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<T>(ApiResponseStatus.Failed, record, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, record);
        }

        #endregion

        #region Validate Range Audited

        public static async Task<IApiResponse<IEnumerable<T>>> ValidateRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (records.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(records, "Records collection was null or empty on validation operation", null, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "InvalidModelState");
            }
            if (validator == null)
            {
                await dbContext.CreateAuditrailsAsync(records, "Records validator was null on validation operation", null, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "ModelStateInvalid");
            }
            var validationResult = await validator.ValidateAsync(records);
            if (!validationResult.IsValid)
            {
                await dbContext.CreateAuditrailsAsync(records, "A record/s validation failed in the collection on validation operation", null, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Succeeded, records);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> ValidateRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            if (records.IsNullOrEmpty())
            {
                await dbContext.CreateAuditrailsAsync(records, "Records collection was null or empty on validation operation", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "InvalidModelState");
            }
            if (validator == null)
            {
                await dbContext.CreateAuditrailsAsync(records, "Records validator was null on validation operation", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "ModelStateInvalid");
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                await dbContext.CreateAuditrailsAsync(records, "The userId was null on validation operation", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, "ErrorSavingRecord");
            }
            var validationResult = await validator.ValidateAsync(records);
            if (!validationResult.IsValid)
            {
                await dbContext.CreateAuditrailsAsync(records, "A record/s validation failed in the collection on validation operation", userId, controllerName, actionName, remoteIpAddress);
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, records, validationResult.Errors.Select(x => x.ErrorMessage));
            }
            return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Succeeded, records);
        }

        #endregion

        #region UserBlobs Validation

        public static async Task<IApiResponse<T>> ValidatedAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, T record, IValidator validator, string userId, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var apiResponse = await dbContext.ValidateAuditedAsync(record, validator, userId, controllerName, actionName, remoteIpAddress);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            //Lets check if our files uploaded comply with our rules first
            foreach (var uploadBlobData in uploadBlobDatas)
            {
                var formFileUploadedApiResponse = await dbContext.IsValidBlobDataAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules, userId, controllerName, actionName, remoteIpAddress);
                //Lets check if something went wrong
                if (formFileUploadedApiResponse.Status.Failed() || !formFileUploadedApiResponse.Data)
                {
                    //Does this file upload was required and we are creating it?
                    //Do we forced file upload on update operation?
                    if ((uploadBlobData.FormFileRules.IsRequired && saveOperation.IsAdd()) ||
                        (uploadBlobData.ForceUpload && saveOperation.IsUpdate()))
                    {
                        await dbContext.CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} on a field was required on create and the file was null or empty", userId, controllerName, actionName, remoteIpAddress);
                        return record.ToApiResponseFailed("FileUploadIsRequired");
                    }
                    //Does the file is required but we are updating?
                    else if (uploadBlobData.FormFileRules.IsRequired && saveOperation.IsUpdate())
                    {
                        var propertyName = uploadBlobData.FormFileRules.PropertyName;
                        //Lets check that the required file was not deleted on update
                        var propertyValue = record.GetType().GetProperty(propertyName).GetValue(record);
                        if (propertyValue == null)
                        {
                            await dbContext.CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required on update and the file was null or empty", userId, controllerName, actionName, remoteIpAddress);
                            return record.ToApiResponseFailed("FileUploadIsRequired");
                        }
                        //Lets validate that userblobs were not overrided with random values 
                        //and that the value parses to user blobs and is valid userblobs
                        try
                        {
                            var userBlobs = JsonConvert.DeserializeObject<IEnumerable<UserBlobs>>(Encoding.UTF8.GetString((byte[])propertyValue));
                            var userBlobsCollectionValidator = new UserBlobsCollectionValidator();
                            var validationResult = await userBlobsCollectionValidator.ValidateAsync(userBlobs);
                            if (!validationResult.IsValid)
                            {
                                await dbContext.CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required and the file was null or empty", userId, controllerName, actionName, remoteIpAddress);
                                return new T().ToApiResponseFailed("FileUploadIsRequired");
                            }

                        }
                        catch (Exception)
                        {
                            await dbContext.CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required and the file was null or empty", userId, controllerName, actionName, remoteIpAddress);
                            return new T().ToApiResponseFailed("FileUploadIsRequired");
                        }
                    }
                }
            }
            return new ApiResponse<T>(ApiResponseStatus.Succeeded, record);
        }
        public static async Task<IApiResponse<IEnumerable<T>>> ValidatedRangeAuditedAsync<T>(this BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs> dbContext, IEnumerable<T> records, IValidator validator, string userId, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where T : class, new()
        {
            var apiResponse = await dbContext.ValidateRangeAuditedAsync(records, validator, userId, controllerName, actionName, remoteIpAddress);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            foreach (var record in records)
            {
                //Lets check if our files uploaded comply with our rules first
                foreach (var uploadBlobData in uploadBlobDatas)
                {
                    var formFileUploadedApiResponse = await dbContext.IsValidBlobDataAsync(uploadBlobData.BlobDatas, uploadBlobData.FormFileRules, userId, controllerName, actionName, remoteIpAddress);
                    //Lets check if something went wrong
                    if (formFileUploadedApiResponse.Status.Failed() || !formFileUploadedApiResponse.Data)
                    {
                        //Does this file upload was required and we are creating it?
                        //Do we forced file upload on update operation?
                        if ((uploadBlobData.FormFileRules.IsRequired && saveOperation.IsAdd()) ||
                            (uploadBlobData.ForceUpload && saveOperation.IsUpdate()))
                        {
                            await dbContext.CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} on a field was required on create and the file was null or empty", userId, controllerName, actionName, remoteIpAddress);
                            return records.ToApiResponseFailed("FileUploadIsRequired");
                        }
                        //Does the file is required but we are updating?
                        else if (uploadBlobData.FormFileRules.IsRequired && saveOperation.IsUpdate())
                        {
                            var propertyName = uploadBlobData.FormFileRules.PropertyName;
                            //Lets check that the required file was not deleted on update
                            var propertyValue = record.GetType().GetProperty(propertyName).GetValue(record);
                            if (propertyValue == null)
                            {
                                await dbContext.CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required on update and the file was null or empty", userId, controllerName, actionName, remoteIpAddress);
                                return records.ToApiResponseFailed("FileUploadIsRequired");
                            }
                            //Lets validate that userblobs were not overrided with random values 
                            //and that the value parses to user blobs and is valid userblobs
                            try
                            {
                                var userBlobs = JsonConvert.DeserializeObject<IEnumerable<UserBlobs>>(Encoding.UTF8.GetString((byte[])propertyValue));
                                var userBlobsCollectionValidator = new UserBlobsCollectionValidator();
                                var validationResult = await userBlobsCollectionValidator.ValidateAsync(userBlobs);
                                if (!validationResult.IsValid)
                                {
                                    await dbContext.CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required and the file was null or empty", userId, controllerName, actionName, remoteIpAddress);
                                    return records.ToApiResponseFailed("FileUploadIsRequired");
                                }
                            }
                            catch (Exception)
                            {
                                await dbContext.CreateAuditrailsAsync(record, $@"A file upload on entity {typeof(T).Name} field {propertyName} was required and the file was null or empty or manipulated on the client side", userId, controllerName, actionName, remoteIpAddress);
                                return records.ToApiResponseFailed("FileUploadIsRequired");
                            }
                        }
                    }
                }
            }
            return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Succeeded, records);
        }

        #endregion
    }
}
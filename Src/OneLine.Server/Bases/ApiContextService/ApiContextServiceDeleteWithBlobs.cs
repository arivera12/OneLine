using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, TAuditTrails, TUserBlobs, TBlobStorage>
        where TDbContext : DbContext
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
    {
        /// <summary>
        /// Deletes a record within it's blobs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync<T>(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = await DeleteUserBlobsFromEntityAsync(record);
            return new ApiResponse<T>(apiResponse.Status, apiResponse.Data.Item1, apiResponse.Status.Succeeded() ? transactionSuccessMessage : transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a range of records within it's blobs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedWithBlobsAsync<T>(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = await DeleteUserBlobsFromEntitiesAsync<T>(records);
            return new ApiResponse<IEnumerable<T>>(apiResponse.Status, apiResponse.Data.Item1, apiResponse.Status.Succeeded() ? transactionSuccessMessage : transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record within it's blobs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync<T>(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
            {
                return Activator.CreateInstance<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var record = await DbContext.Set<T>().FindAsync(identifier.Model);
            if (record.IsNull())
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            var apiResponse = await DeleteUserBlobsFromEntityAsync(record);
            return new ApiResponse<T>(apiResponse.Status, apiResponse.Data.Item1, apiResponse.Status.Succeeded() ? transactionSuccessMessage : transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record within it's blobs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync<T>(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            if (predicate.IsNull())
            {
                return Activator.CreateInstance<T>().ToApiResponseFailed("PredicateIsNull");
            }
            var record = await DbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
            if (record.IsNull())
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            await DeleteUserBlobsFromEntityAsync(record);
            return await DeleteAuditedAsync(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a range of records within it's blobs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedWithBlobsAsync<T>(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            if (predicate.IsNull())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            IEnumerable<T> records = DbContext.Set<T>().Where(predicate);
            if (records.IsNull() || !records.Any())
            {
                return records.ToApiResponseFailed("RecordNotFound");
            }
            await RemoveRangeAuditedAsync(records);
            var apiResponse = await DeleteUserBlobsFromEntitiesAsync(records);
            return new ApiResponse<IEnumerable<T>>(apiResponse.Status, apiResponse.Data.Item1, apiResponse.Status.Succeeded() ? transactionSuccessMessage : transactionErrorMessage);
        }
    }
}

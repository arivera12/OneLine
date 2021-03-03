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
        /// Replaces a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveReplaceRangeAsync<T>(IEnumerable<T> records, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            if (deletePredicate.IsNull())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            var toDeletedRecords = DbContext.Set<T>().Where(deletePredicate);
            if (toDeletedRecords.IsNull() || !toDeletedRecords.Any())
            {
                return toDeletedRecords.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            DbContext.RemoveRange(toDeletedRecords);
            await DbContext.AddRangeAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Replaces a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveReplaceRangeValidatedAsync<T>(IEnumerable<T> records, IValidator validator, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveReplaceRangeAsync(records, deletePredicate, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Replaces a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveReplaceRangeAuditedAsync<T>(IEnumerable<T> records, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            if (deletePredicate.IsNull())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            var toDeletedRecords = DbContext.Set<T>().Where(deletePredicate).AsEnumerable();
            if (toDeletedRecords.IsNull() || !toDeletedRecords.Any())
            {
                return toDeletedRecords.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            await RemoveRangeAuditedAsync(toDeletedRecords);
            await AddRangeAuditedAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Replaces a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveReplaceRangeValidatedAuditedAsync<T>(IEnumerable<T> records, IValidator validator, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveReplaceRangeAuditedAsync(records, deletePredicate, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Replaces a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> ReplaceRangeAsync<T>(IEnumerable<T> records, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            if (deletePredicate.IsNull())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            var toDeletedRecords = DbContext.Set<T>().Where(deletePredicate);
            if (toDeletedRecords.IsNull() || !toDeletedRecords.Any())
            {
                return toDeletedRecords.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            DbContext.RemoveRange(toDeletedRecords);
            await DbContext.AddRangeAsync(records);
            return records.ToApiResponse();
        }
        /// <summary>
        /// Replaces a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> ReplaceRangeValidatedAsync<T>(IEnumerable<T> records, IValidator validator, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await ReplaceRangeAsync(records, deletePredicate, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Replaces a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> ReplaceRangeAuditedAsync<T>(IEnumerable<T> records, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            if (deletePredicate.IsNull())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            var toDeletedRecords = DbContext.Set<T>().Where(deletePredicate).AsEnumerable();
            if (toDeletedRecords.IsNull() || !toDeletedRecords.Any())
            {
                return toDeletedRecords.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            await RemoveRangeAuditedAsync(toDeletedRecords);
            await AddRangeAuditedAsync(records);
            return records.ToApiResponse();
        }
        /// <summary>
        /// Replaces a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> ReplaceRangeValidatedAuditedAsync<T>(IEnumerable<T> records, IValidator validator, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
            where T : class
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await ReplaceRangeAuditedAsync(records, deletePredicate, transactionSuccessMessage, transactionErrorMessage);
        }
    }
}

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneLine.Extensions;
using OneLine.Messaging;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub>
        where TDbContext : DbContext
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
        where TSmtp : class, ISmtp, new()
        where TMessageHub : MessageHub, new()
    {
        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAsync<T>(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (record.IsNull())
            {
                return new ApiResponse<T>(Enums.ApiResponseStatus.Failed, "RecordIsNull");
            }
            DbContext.Remove(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteValidatedAsync<T>(T record, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = record.IsNull() ? await Activator.CreateInstance<T>().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await DeleteAsync(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAuditedAsync<T>(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (record.IsNull())
            {
                return new ApiResponse<T>(Enums.ApiResponseStatus.Failed, "RecordIsNull");
            }
            await RemoveAuditedAsync(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteValidatedAuditedAsync<T>(T record, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = record.IsNull() ? await Activator.CreateInstance<T>().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            await RemoveAuditedAsync(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync<T>(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (records.IsNull() || !records.Any())
            {
                return new ApiResponse<IEnumerable<T>>(Enums.ApiResponseStatus.Failed, "RecordsIsNullOrEmpty");
            }
            DbContext.RemoveRange(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAsync<T>(IEnumerable<T> records, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await DeleteRangeAsync(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync<T>(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (records.IsNull() || !records.Any())
            {
                return new ApiResponse<IEnumerable<T>>(Enums.ApiResponseStatus.Failed, "RecordsIsNullOrEmpty");
            }
            await RemoveRangeAuditedAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAuditedAsync<T>(IEnumerable<T> records, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            await RemoveRangeAuditedAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAsync<T>(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            DbContext.Remove(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identifier"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAuditedAsync<T>(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            await RemoveAuditedAsync(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAsync<T>(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            DbContext.Remove(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> DeleteAuditedAsync<T>(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            await RemoveAuditedAsync(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync<T>(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            DbContext.RemoveRange(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Deletes a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync<T>(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
    }
}

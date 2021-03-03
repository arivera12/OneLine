using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Saves a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> SaveAsync<T>(T record, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (record.IsNull())
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, "RecordIsNull");
            }
            if (saveOperation.IsAdd())
            {
                await DbContext.AddAsync(record);
            }
            else if (saveOperation.IsUpdate())
            {
                DbContext.Update(record);
            }
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Saves a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> SaveValidatedAsync<T>(T record, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = record.IsNull() ? await Activator.CreateInstance<T>().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveAsync(record, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Saves a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> SaveAuditedAsync<T>(T record, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (record.IsNull())
            {
                return new ApiResponse<T>(ApiResponseStatus.Failed, "RecordIsNull");
            }
            if (saveOperation.IsAdd())
            {
                await AddAuditedAsync(record);
            }
            else if (saveOperation.IsUpdate())
            {
                await UpdateAuditedAsync(record);
            }
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Saves a record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> SaveValidatedAuditedAsync<T>(T record, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveAuditedAsync(record, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Saves a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveRangeAsync<T>(IEnumerable<T> records, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (records.IsNull() || !records.Any())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("RecordsIsNullOrEmpty");
            }
            if (saveOperation.IsAdd())
            {
                await DbContext.AddRangeAsync(records);
            }
            else if (saveOperation.IsUpdate())
            {
                DbContext.UpdateRange(records);
            }
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Saves a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveRangeValidatedAsync<T>(IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveRangeAsync(records, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Saves a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveRangeAuditedAsync<T>(IEnumerable<T> records, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (records.IsNull() || !records.Any())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("RecordsIsNullOrEmpty");
            }
            if (saveOperation.IsAdd())
            {
                await AddRangeAuditedAsync(records);
            }
            else if (saveOperation.IsUpdate())
            {
                await UpdateRangeAuditedAsync(records);
            }
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Saves a range of records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveRangeValidatedAuditedAsync<T>(IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = await records.ValidateRangeAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveRangeAuditedAsync(records, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
    }
}

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Messaging;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub> :
        IApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub>
        where TDbContext : DbContext
        where T : class, new()
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorage, new()
        where TSmtp : class, ISmtp, new()
        where TMessageHub : class, ISendMessageHub, new()
    {
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> SaveAsync(T record, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> SaveValidatedAsync(T record, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = record.IsNull() ? await new T().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveAsync(record, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> SaveAuditedAsync(T record, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (record.IsNull())
            {
                //await CreateAuditrailsAsync(record, $"Record was null on method {MethodBase.GetCurrentMethod().Name}");
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
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> SaveValidatedAuditedAsync(T record, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = await ValidateAsync(record, validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveAuditedAsync(record, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> SaveRangeAsync(IEnumerable<T> records, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> SaveRangeValidatedAsync(IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveRangeAsync(records, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> SaveRangeAuditedAsync(IEnumerable<T> records, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (records.IsNull() || !records.Any())
            {
                //await CreateAuditrailsAsync(records, $"Records was null or empty on method {MethodBase.GetCurrentMethod().Name}");
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
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> SaveRangeValidatedAuditedAsync(IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = await ValidateRangeAsync(records, validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await SaveRangeAuditedAsync(records, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
    }
}

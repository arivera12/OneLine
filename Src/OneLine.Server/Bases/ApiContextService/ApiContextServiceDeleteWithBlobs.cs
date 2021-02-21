using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneLine.Contracts;
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
    public partial class ApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub> :
        IApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub>
        where TDbContext : DbContext
        where T : class, new()
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
        where TSmtp : class, ISmtp, new()
        where TMessageHub : class, ISendMessageHub, new()
    {
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = await DeleteUserBlobsFromEntityAsync(record);
            return new ApiResponse<T>(apiResponse.Status, apiResponse.Data.Item1, apiResponse.Status.Succeeded() ? transactionSuccessMessage : transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedWithBlobsAsync(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = await DeleteUserBlobsFromEntitiesAsync(records);
            return new ApiResponse<IEnumerable<T>>(apiResponse.Status, apiResponse.Data.Item1, apiResponse.Status.Succeeded() ? transactionSuccessMessage : transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
            {
                return new T().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var record = await DbContext.Set<T>().FindAsync(identifier.Model);
            if (record.IsNull())
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            var apiResponse = await DeleteUserBlobsFromEntityAsync(record);
            return new ApiResponse<T>(apiResponse.Status, apiResponse.Data.Item1, apiResponse.Status.Succeeded() ? transactionSuccessMessage : transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedWithBlobsAsync(IEnumerable<IIdentifier<string>> identifiers, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (identifiers.IsNull() || !identifiers.Any())
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
            var ids = identifiers.Select(s => s.Model);
            var tablePrimaryKey = GetTablePrimaryKeyFieldName();
            IEnumerable<T> records = System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(DbContext.Set<T>(), $"{tablePrimaryKey} in @0", ids);
            if (records.IsNull() || !records.Any())
            {
                return records.ToApiResponseFailed("RecordNotFound");
            }
            var apiResponse = await DeleteUserBlobsFromEntitiesAsync(records);
            return new ApiResponse<IEnumerable<T>>(apiResponse.Status, apiResponse.Data.Item1, apiResponse.Status.Succeeded() ? transactionSuccessMessage : transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (predicate.IsNull())
            {
                //await CreateAuditrailsAsync(new T(), "predicate was null on select one operation");
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            var record = await DbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
            if (record.IsNull())
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            await DeleteUserBlobsFromEntityAsync(record);
            return await DeleteAuditedAsync(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedWithBlobsAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (predicate.IsNull())
            {
                //await CreateAuditrailsAsync(new T(), "predicate was null on select one operation");
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

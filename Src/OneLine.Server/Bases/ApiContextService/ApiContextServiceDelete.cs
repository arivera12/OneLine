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
        public async Task<IApiResponse<T>> DeleteAsync(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (record.IsNull())
            {
                return new ApiResponse<T>(Enums.ApiResponseStatus.Failed, "RecordIsNull");
            }
            DbContext.Remove(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteValidatedAsync(T record, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = record.IsNull() ? await new T().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await DeleteAsync(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteAuditedAsync(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (record.IsNull())
            {
                //await CreateAuditrailsAsync(record, $"Record was null on method {MethodBase.GetCurrentMethod().Name}");
                return new ApiResponse<T>(Enums.ApiResponseStatus.Failed, "RecordIsNull");
            }
            await RemoveAuditedAsync(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteValidatedAuditedAsync(T record, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = record.IsNull() ? await new T().ValidateAsync(validator) : await record.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                //await CreateAuditrailsAsync(record, $"Record was null or validation failed on method {MethodBase.GetCurrentMethod().Name}");
                return apiResponse;
            }
            await RemoveAuditedAsync(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (records.IsNull() || !records.Any())
            {
                return new ApiResponse<IEnumerable<T>>(Enums.ApiResponseStatus.Failed, "RecordsIsNullOrEmpty");
            }
            DbContext.RemoveRange(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAsync(IEnumerable<T> records, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await DeleteRangeAsync(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (records.IsNull() || !records.Any())
            {
                //await CreateAuditrailsAsync(records, $"Records where null or empty on method {MethodBase.GetCurrentMethod().Name}");
                return new ApiResponse<IEnumerable<T>>(Enums.ApiResponseStatus.Failed, "RecordsIsNullOrEmpty");
            }
            await RemoveRangeAuditedAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAuditedAsync(IEnumerable<T> records, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                //await CreateAuditrailsAsync(records, $"Records where null, empty or validation failed on method {MethodBase.GetCurrentMethod().Name}");
                return apiResponse;
            }
            await RemoveRangeAuditedAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteAsync(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            DbContext.Remove(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteAuditedAsync(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
            {
                //await CreateAuditrailsAsync(identifier, $"Record indentifier or model was null on method {MethodBase.GetCurrentMethod().Name}");
                return new T().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var record = await DbContext.Set<T>().FindAsync(identifier.Model);
            if (record.IsNull())
            {
                //await CreateAuditrailsAsync(record, $"Record was null on method {MethodBase.GetCurrentMethod().Name}");
                return record.ToApiResponseFailed("RecordNotFound");
            }
            await RemoveAuditedAsync(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync(IEnumerable<IIdentifier<string>> identifiers, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            return await DeleteRangeAsync(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync(IEnumerable<IIdentifier<string>> identifiers, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                //await CreateAuditrailsAsync(identifiers, $"Record indentifier or model was null on method {MethodBase.GetCurrentMethod().Name}");
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var ids = identifiers.Select(s => s.Model);
            var tablePrimaryKey = GetTablePrimaryKeyFieldName();
            IEnumerable<T> records = System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(DbContext.Set<T>(), $"{tablePrimaryKey} in @0", ids);
            if (records.IsNull() || !records.Any())
            {
                //await CreateRangeAuditrailsAsync(records, $"Records was null or empty on method {MethodBase.GetCurrentMethod().Name}");
                return records.ToApiResponseFailed("RecordNotFound");
            }
            await RemoveRangeAuditedAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (predicate.IsNull())
            {
                return new T().ToApiResponseFailed("PredicateIsNull");
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
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> DeleteAuditedAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (predicate.IsNull())
            {
                //await CreateAuditrailsAsync(predicate, $"predicate was null on method {MethodBase.GetCurrentMethod().Name}");
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            var record = await DbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
            if (record.IsNull())
            {
                //await CreateAuditrailsAsync(record, $"Record was null on method {MethodBase.GetCurrentMethod().Name}");
                return record.ToApiResponseFailed("RecordNotFound");
            }
            await RemoveAuditedAsync(record);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(record, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (predicate.IsNull())
            {
                //await CreateAuditrailsAsync(predicate, $"predicate was null on method {MethodBase.GetCurrentMethod().Name}");
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            IEnumerable<T> records = DbContext.Set<T>().Where(predicate);
            if (records.IsNull() || !records.Any())
            {
                //await CreateRangeAuditrailsAsync(records, $"predicate was null on method {MethodBase.GetCurrentMethod().Name}");
                return records.ToApiResponseFailed("RecordNotFound");
            }
            await RemoveRangeAuditedAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
    }
}

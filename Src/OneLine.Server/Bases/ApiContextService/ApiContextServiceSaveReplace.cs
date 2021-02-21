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
        where TMessageHub : MessageHub, new()
    {
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> ReplaceRangeAsync(IEnumerable<T> records, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> ReplaceRangeValidatedAsync(IEnumerable<T> records, IValidator validator, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var apiResponse = records.IsNull() || !records.Any() ? await Enumerable.Empty<T>().ValidateAsync(validator) : await records.ValidateAsync(validator);
            if (apiResponse.Status.Failed())
            {
                return apiResponse;
            }
            return await ReplaceRangeAsync(records, deletePredicate, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> ReplaceRangeAuditedAsync(IEnumerable<T> records, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            if (deletePredicate.IsNull())
            {
                //await CreateAuditrailsAsync(deletePredicate, "Records collection to delete was null or empty in validation operation");
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            var toDeletedRecords = DbContext.Set<T>().Where(deletePredicate).AsEnumerable();
            if (toDeletedRecords.IsNull() || !toDeletedRecords.Any())
            {
                //await CreateRangeAuditrailsAsync(toDeletedRecords, "Records collection to delete was null or empty in validation operation");
                return toDeletedRecords.AsEnumerable().ToApiResponseFailed("RecordNotFound");
            }
            await RemoveRangeAuditedAsync(toDeletedRecords);
            await AddRangeAuditedAsync(records);
            var result = await DbContext.SaveChangesAsync();
            return result.TransactionResultApiResponse(records, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> ReplaceRangeValidatedAuditedAsync(IEnumerable<T> records, IValidator validator, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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

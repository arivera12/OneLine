using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IDeletableWithBlobsApiContext<T>
    {
        /// <summary>
        /// Delete a record with blobs and audit
        /// </summary>
        /// <param name="record"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a range of records with blobs and audit
        /// </summary>
        /// <param name="records"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedWithBlobsAsync(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a record with blobs and audit using it's primary key
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a range of records with blobs and audit using it's primary key
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedWithBlobsAsync(IEnumerable<IIdentifier<string>> identifiers, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a record with blobs by predicate and audit
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAuditedWithBlobsAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a range of records with blobs by predicate and audit
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedWithBlobsAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
    }
}

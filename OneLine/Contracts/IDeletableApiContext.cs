using FluentValidation;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IDeletableApiContext<T>
    {
        /// <summary>
        /// Deletes a record from <typeparamref name="T"/>
        /// </summary>
        /// <param name="record"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAsync(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Deletes a record from <typeparamref name="T"/> after validating
        /// </summary>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteValidatedAsync(T record, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Deletes a record from <typeparamref name="T"/> and audits
        /// </summary>
        /// <param name="record"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAuditedAsync(T record, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Deletes a record from <typeparamref name="T"/> after validate and audit it
        /// </summary>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteValidatedAuditedAsync(T record, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Deletes a range of records from <typeparamref name="T"/>
        /// </summary>
        /// <param name="records"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Deletes a range of records from <typeparamref name="T"/> after validate
        /// </summary>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAsync(IEnumerable<T> records, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Deletes a range of records from <typeparamref name="T"/> and audit
        /// </summary>
        /// <param name="records"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync(IEnumerable<T> records, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Deletes a range of records from <typeparamref name="T"/> after validate and and audit it
        /// </summary>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeValidatedAuditedAsync(IEnumerable<T> records, IValidator validator, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a record by it's primary key
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAsync(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a record by its primary key and audit
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAuditedAsync(IIdentifier<string> identifier, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a range of records by it's primary key
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync(IEnumerable<IIdentifier<string>> identifiers, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a range of records by it's primary key and audit
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync(IEnumerable<IIdentifier<string>> identifiers, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a record by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a record by predicate and audit
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> DeleteAuditedAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a range of records by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Delete a range of records by predicate and audit
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> DeleteRangeAuditedAsync(Expression<Func<T, bool>> predicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
    }
}

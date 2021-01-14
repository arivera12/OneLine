using FluentValidation;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface ISaveableReplaceableApiContext<T>
    {
        /// <summary>
        /// Replaces a list of records by the delete predicate
        /// </summary>
        /// <param name="records"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> ReplaceRangeAsync(IEnumerable<T> records, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Replaces a list of records by the delete predicate after validate
        /// </summary>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> ReplaceRangeValidatedAsync(IEnumerable<T> records, IValidator validator, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Replaces a slist of records by the delete predicate with audit trails
        /// </summary>
        /// <param name="records"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> ReplaceRangeAuditedAsync(IEnumerable<T> records, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Replaces a slist of records by the delete predicate after validate with audit trails 
        /// </summary>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="deletePredicate"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> ReplaceRangeValidatedAuditedAsync(IEnumerable<T> records, IValidator validator, Expression<Func<T, bool>> deletePredicate, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
    }
}

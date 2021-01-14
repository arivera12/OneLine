using FluentValidation;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface ISaveableApiContext<T>
    {
        /// <summary>
        /// The api save method within the current context
        /// </summary>
        /// <param name="record"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> SaveAsync(T record, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// The api save method within the current context with validator
        /// </summary>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> SaveValidatedAsync(T record, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// The api save method within the current context with validator and audit trails
        /// </summary>
        /// <param name="record"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> SaveAuditedAsync(T record, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// The api save method within the current context with audit trails
        /// </summary>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> SaveValidatedAuditedAsync(T record, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// The api save range method withing the current context
        /// </summary>
        /// <param name="records"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> SaveRangeAsync(IEnumerable<T> records, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// The api save range method withing the current context with validator
        /// </summary>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> SaveRangeValidatedAsync(IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// The api save range method withing the current context with audit trails
        /// </summary>
        /// <param name="records"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> SaveRangeAuditedAsync(IEnumerable<T> records, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// The api save range method withing the current context with validator and audit trails
        /// </summary>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> SaveRangeValidatedAuditedAsync(IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
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
        /// <summary>
        /// Saves a record with blobs references validated and audited.
        /// </summary>
        /// <param name="record"></param>
        /// <param name="originalRecord"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> SaveValidatedAuditedWithBlobsAsync(T record, T originalRecord, IValidator validator, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas, bool ignoreBlobOwner = false, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");

        /// <summary>
        /// Imports a flat csv file into the data base
        /// </summary>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> ImportCsvUploadAsync(IUploadBlobData uploadBlobDatas, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
        /// <summary>
        /// Imports a flat csv file into the data base
        /// </summary>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> ImportCsvUploadAuditedAsync(IUploadBlobData uploadBlobDatas, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed");
    }
}

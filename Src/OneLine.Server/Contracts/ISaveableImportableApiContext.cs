using FluentValidation;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface ISaveableImportableApiContext<T>
    {
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

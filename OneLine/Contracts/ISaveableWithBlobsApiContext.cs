using FluentValidation;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface ISaveableWithBlobsApiContext<T>
    {
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
    }
}

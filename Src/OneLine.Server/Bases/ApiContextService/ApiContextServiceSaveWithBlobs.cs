using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, TAuditTrails, TUserBlobs, TBlobStorage>
        where TDbContext : DbContext
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
    {
        /// <summary>
        /// Saves a record with blobs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        /// <param name="originalRecord"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="ignoreBlobOwner"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<T>> SaveValidatedAuditedWithBlobsAsync<T>(T record, T originalRecord, IValidator validator, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas, bool ignoreBlobOwner = false, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var validationApiResponse = await ValidatedWithBlobsAsync(record, validator, saveOperation, uploadBlobDatas);
            if (validationApiResponse.Status.Failed())
            {
                return validationApiResponse;
            }
            if (saveOperation.IsAdd())
            {
                await AddAndBindUserBlobsRangeAsync(record, uploadBlobDatas);
            }
            else if (saveOperation.IsUpdate())
            {
                validationApiResponse = await ValidatedWithBlobsAsync(originalRecord, validator, saveOperation, uploadBlobDatas);
                if (validationApiResponse.Status.Failed())
                {
                    return validationApiResponse;
                }
                await UpdateAndBindUserBlobsRangeAsync(record, originalRecord, uploadBlobDatas, ignoreBlobOwner);
            }
            return await SaveAuditedAsync(record, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
    }
}

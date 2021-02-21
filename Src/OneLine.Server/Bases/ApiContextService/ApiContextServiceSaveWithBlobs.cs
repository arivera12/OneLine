using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Messaging;
using OneLine.Models;
using System.Collections.Generic;
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
        public async Task<IApiResponse<T>> SaveValidatedAuditedWithBlobsAsync(T record, T originalRecord, IValidator validator, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas, bool ignoreBlobOwner = false, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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

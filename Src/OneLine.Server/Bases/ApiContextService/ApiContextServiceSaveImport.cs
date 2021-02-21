using Microsoft.EntityFrameworkCore;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Messaging;
using OneLine.Models;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IApiResponse<IEnumerable<T>>> ImportCsvUploadAsync(IUploadBlobData uploadBlobDatas, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var any = uploadBlobDatas.IsNotNull() && uploadBlobDatas.BlobDatas.IsNotNull() && uploadBlobDatas.BlobDatas.Any();
            if (!any)
            {
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            if (any && uploadBlobDatas.FormFileRules.IsNotNull())
            {
                var isValidFormFileApiResponse = uploadBlobDatas.BlobDatas.IsValidBlobDataApiResponse(uploadBlobDatas.FormFileRules);
                if (isValidFormFileApiResponse.Status.Failed())
                {
                    return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message);
                }
            }
            var records = new List<T>();
            foreach (var blobData in uploadBlobDatas.BlobDatas)
            {
                records.AddRange(blobData.Data.ReadCsv<T>());
            }
            return await SaveRangeAsync(records, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> ImportCsvUploadAuditedAsync(IUploadBlobData uploadBlobDatas, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
        {
            var any = uploadBlobDatas.IsNotNull() && uploadBlobDatas.BlobDatas.IsNotNull() && uploadBlobDatas.BlobDatas.Any();
            if (!any)
            {
                //await CreateAuditrailsAsync(uploadBlobDatas, "No file uploaded");
                return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, "FileIsNullOrEmpty");
            }
            if (any && uploadBlobDatas.FormFileRules.IsNotNull())
            {
                var isValidFormFileApiResponse = uploadBlobDatas.BlobDatas.IsValidBlobDataApiResponse(uploadBlobDatas.FormFileRules);
                if (isValidFormFileApiResponse.Status.Failed())
                {
                    return new ApiResponse<IEnumerable<T>>(ApiResponseStatus.Failed, isValidFormFileApiResponse.Message);
                }
            }
            var records = new List<T>();
            foreach (var blobData in uploadBlobDatas.BlobDatas)
            {
                records.AddRange(blobData.Data.ReadCsv<T>());
            }
            return await SaveRangeAuditedAsync(records, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
    }
}

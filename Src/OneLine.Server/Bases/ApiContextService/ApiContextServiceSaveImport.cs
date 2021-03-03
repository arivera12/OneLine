using Microsoft.EntityFrameworkCore;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System.Collections.Generic;
using System.Linq;
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
        /// Imports data from csv file to the specified <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveImportCsvUploadAsync<T>(IUploadBlobData uploadBlobDatas, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            if (saveOperation.IsAdd())
            {
                foreach (var blobData in uploadBlobDatas.BlobDatas)
                {
                    var csvRecords = blobData.Data.ReadCsv<T>();
                    records.AddRange(csvRecords);
                }
            }
            else if (saveOperation.IsUpdate())
            {
                foreach (var blobData in uploadBlobDatas.BlobDatas)
                {
                    var csvRecords = blobData.Data.ReadCsv<T>();
                    records.AddRange(csvRecords);
                    DbContext.AttachRange(csvRecords);
                }
            }
            return await SaveRangeAsync(records, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Imports data from csv file to the specified <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public async Task<IApiResponse<IEnumerable<T>>> SaveImportCsvUploadAuditedAsync<T>(IUploadBlobData uploadBlobDatas, SaveOperation saveOperation, string transactionSuccessMessage = "TransactionCompletedSuccessfully", string transactionErrorMessage = "TransactionFailed")
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
            if (saveOperation.IsAdd())
            {
                foreach (var blobData in uploadBlobDatas.BlobDatas)
                {
                    var csvRecords = blobData.Data.ReadCsv<T>();
                    records.AddRange(csvRecords);
                }
            }
            else if (saveOperation.IsUpdate())
            {
                foreach (var blobData in uploadBlobDatas.BlobDatas)
                {
                    var csvRecords = blobData.Data.ReadCsv<T>();
                    records.AddRange(csvRecords);
                    DbContext.AttachRange(csvRecords);
                }
            }
            return await SaveRangeAuditedAsync(records, saveOperation, transactionSuccessMessage, transactionErrorMessage);
        }
        /// <summary>
        /// Imports data from csv file to the specified <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="saveOperation"></param>
        /// <param name="transactionSuccessMessage"></param>
        /// <param name="transactionErrorMessage"></param>
        /// <returns></returns>
        public IApiResponse<IEnumerable<T>> ImportCsvUploadAsync<T>(IUploadBlobData uploadBlobDatas, SaveOperation saveOperation)
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
            if (saveOperation.IsAdd())
            {
                foreach (var blobData in uploadBlobDatas.BlobDatas)
                {
                    var csvRecords = blobData.Data.ReadCsv<T>();
                    records.AddRange(csvRecords);
                }
            }
            else if (saveOperation.IsUpdate())
            {
                foreach (var blobData in uploadBlobDatas.BlobDatas)
                {
                    var csvRecords = blobData.Data.ReadCsv<T>();
                    records.AddRange(csvRecords);
                    DbContext.AttachRange(csvRecords);
                    DbContext.UpdateRange(csvRecords);
                }
            }
            return records.AsEnumerable().ToApiResponse();
        }
        /// <summary>
        /// Imports data from csv file to the specified <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uploadBlobDatas"></param>
        /// <param name="saveOperation"></param>
        /// <returns></returns>
        public IApiResponse<IEnumerable<T>> ImportCsvUploadAuditedAsync<T>(IUploadBlobData uploadBlobDatas, SaveOperation saveOperation)
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
            if (saveOperation.IsAdd())
            {
                foreach (var blobData in uploadBlobDatas.BlobDatas)
                {
                    var csvRecords = blobData.Data.ReadCsv<T>();
                    records.AddRange(csvRecords);
                }
            }
            else if (saveOperation.IsUpdate())
            {
                foreach (var blobData in uploadBlobDatas.BlobDatas)
                {
                    var csvRecords = blobData.Data.ReadCsv<T>();
                    records.AddRange(csvRecords);
                    DbContext.AttachRange(csvRecords);
                    DbContext.UpdateRange(csvRecords);
                }
            }
            return records.AsEnumerable().ToApiResponse();
        }
    }
}

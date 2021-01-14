using FluentValidation;
using OneLine.Enums;
using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IValidatableAuditableApiContext<T>
    {
        /// <summary>
        /// Validates a <typeparamref name="T"/>
        /// </summary>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> ValidateAsync(T record, IValidator validator);
        /// <summary>
        /// Validates a range of <typeparamref name="T"/>
        /// </summary>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> ValidateRangeAsync(IEnumerable<T> records, IValidator validator);
        /// <summary>
        /// Validates a <typeparamref name="T"/> with blobs with audit trails
        /// </summary>
        /// <param name="record"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="uploadBlobDatas"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> ValidatedWithBlobsAsync(T record, IValidator validator, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas);
        /// <summary>
        /// Validates a range of <typeparamref name="T"/> with blobs with audit trails
        /// </summary>
        /// <param name="records"></param>
        /// <param name="validator"></param>
        /// <param name="saveOperation"></param>
        /// <param name="uploadBlobDatas"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> ValidatedRangeWithBlobsAsync(IEnumerable<T> records, IValidator validator, SaveOperation saveOperation, IEnumerable<IUploadBlobData> uploadBlobDatas);

    }
}

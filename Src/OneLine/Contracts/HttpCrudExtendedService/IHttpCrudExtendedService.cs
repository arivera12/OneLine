using FluentValidation;
using OneLine.Models;

namespace OneLine.Contracts
{
    /// <summary>
    /// Http crud service extended with import and export of data using csv files
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    public interface IHttpCrudExtendedService<T, TIdentifier> : IHttpCrudService<T, TIdentifier>
    {
        /// <summary>
        /// The download csv server method
        /// </summary>
        string DownloadCsvMethod { get; set; }
        /// <summary>
        /// The upload csv server method
        /// </summary>
        string UploadCsvMethod { get; set; }
        /// <summary>
        /// The download csv server method as a <see cref="byte[]"/>
        /// </summary>
        /// <param name="SearchPaging"></param>
        /// <param name="searchExtraParams"></param>
        /// <returns></returns>
        Task<IResponseResult<byte[]>> DownloadCsvAsByteArrayAsync(ISearchPaging SearchPaging, object searchExtraParams);
        ///// <summary>
        ///// The dowload csv server method as a <see cref="Stream"/>
        ///// </summary>
        ///// <param name="SearchPaging"></param>
        ///// <param name="searchExtraParams"></param>
        ///// <returns></returns>
        //Task<IResponseResult<Stream>> DownloadCsvAsStreamAsync(ISearchPaging SearchPaging, object searchExtraParams);
        /// <summary>
        /// The download csv server method as a <see cref="HttpResponseMessage"/>
        /// </summary>
        /// <param name="SearchPaging"></param>
        /// <param name="searchExtraParams"></param>
        /// <returns></returns>
        Task<IResponseResult<HttpResponseMessage>> DownloadCsvAsHttpResponseMessageAsync(ISearchPaging SearchPaging, object searchExtraParams);
        /// <summary>
        /// The upload csv server method as a <see cref="IResponseResult{ApiResponse{IEnumerable{T}}}"/>
        /// </summary>
        /// <param name="blobDatas"></param>
        /// <param name="validator"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        Task<IResponseResult<ApiResponse<IEnumerable<T>>>> UploadCsvAsync(IEnumerable<BlobData> blobDatas, IValidator validator, HttpMethod httpMethod);
    }
}

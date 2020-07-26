using FluentValidation;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    interface IHttpUserBlobsService<T, TIdentifier> : IHttpCrudExtendedService<T, TIdentifier>
    {
        string DownloadBinaryMethod { get; set; }
        string DownloadRangeBinaryMethod { get; set; }
        string DownloadBase64Method { get; set; }
        string DownloadRangeBase64Method { get; set; }
        string GetOneOwnsMethod { get; set; }
        string SearchOwnsMethod { get; set; }
        string ListMethod { get; set; }
        string ListOwnsMethod { get; set; }
        string DownloadCsvOwnsMethod { get; set; }
        Task<IResponseResult<ApiResponse<UserBlobs>>> AddAsync(IEnumerable<BlobData> blobDatas);
        Task<IResponseResult<ApiResponse<IEnumerable<UserBlobs>>>> AddRangeAsync(IEnumerable<BlobData> blobDatas);
        Task<IResponseResult<ApiResponse<Tuple<UserBlobs, UserBlobs>>>> UpdateAsync(UserBlobs userBlobs, IEnumerable<BlobData> blobDatas);
        Task<IResponseResult<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>>> UpdateRangeAsync(IEnumerable<UserBlobs> userBlobs, IEnumerable<BlobData> blobDatas);
        Task<IResponseResult<HttpResponseMessage>> DownloadBinaryAsync(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<HttpResponseMessage>> DownloadRangeBinaryAsync(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<ApiResponse<Tuple<string, UserBlobs>>>> DownloadBase64Async(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<ApiResponse<Tuple<IEnumerable<string>, IEnumerable<UserBlobs>>>>> DownloadRangeBase64Async(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<ApiResponse<TResponse>>> GetOneOwnsAsync<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> SearchOwnsAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> ListAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> ListOwnsAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<byte[]>> DownloadCsvOwnsAsByteArrayAsync(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<Stream>> DownloadCsvOwnsAsStreamAsync(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<HttpResponseMessage>> DownloadCsvOwnsAsHttpResponseMessageAsync(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
using FluentValidation;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    interface IHttpUserBlobsService<TIdentifier> : IHttpService
    {
        string ControllerName { get; set; }
        string AddMethod { get; set; }
        string AddRangeMethod { get; set; }
        string UpdateMethod { get; set; }
        string UpdateRangeMethod { get; set; }
        string DeleteMethod { get; set; }
        string DeleteRangeMethod { get; set; }
        string DownloadBinaryMethod { get; set; }
        string DownloadRangeBinaryMethod { get; set; }
        string DownloadBase64Method { get; set; }
        string DownloadRangeBase64Method { get; set; }
        string GetOneMethod { get; set; }
        string GetOneOwnsMethod { get; set; }
        string SearchMethod { get; set; }
        string SearchOwnsMethod { get; set; }
        string ListMethod { get; set; }
        string ListOwnsMethod { get; set; }
        string DownloadCsvExcelMethod { get; set; }
        string DownloadCsvExcelOwnsMethod { get; set; }
        Task<IResponseResult<ApiResponse<UserBlobs>>> Add(IEnumerable<BlobData> blobDatas);
        Task<IResponseResult<ApiResponse<IEnumerable<UserBlobs>>>> AddRange(IEnumerable<BlobData> blobDatas);
        Task<IResponseResult<ApiResponse<Tuple<UserBlobs, UserBlobs>>>> Update(UserBlobs userBlobs, IEnumerable<BlobData> blobDatas);
        Task<IResponseResult<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>>> UpdateRange(IEnumerable<UserBlobs> userBlobs, IEnumerable<BlobData> blobDatas);
        Task<IResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<HttpResponseMessage>> DownloadBinary(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<HttpResponseMessage>> DownloadRangeBinary(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<ApiResponse<Tuple<string, UserBlobs>>>> DownloadBase64(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<ApiResponse<Tuple<IEnumerable<string>, IEnumerable<UserBlobs>>>>> DownloadRangeBase64(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<ApiResponse<TResponse>>> GetOneOwns<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> SearchOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> List<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> ListOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<HttpResponseMessage>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<HttpResponseMessage>> DownloadCsvExcelOwns(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
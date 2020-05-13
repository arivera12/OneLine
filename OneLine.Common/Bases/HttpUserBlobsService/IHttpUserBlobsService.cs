using FluentValidation;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    interface IHttpUserBlobsService<TUserBlobs, TIdentifier, TBlobData, TBlobValidator> : IHttpService
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
        TBlobValidator BlobValidator { get; set; }
        Task<ResponseResult<ApiResponse<TUserBlobs>>> Add(IEnumerable<TBlobData> blobDatas);
        Task<ResponseResult<ApiResponse<IEnumerable<TUserBlobs>>>> AddRange(IEnumerable<TBlobData> blobDatas);
        Task<ResponseResult<ApiResponse<Tuple<TUserBlobs, TUserBlobs>>>> Update(TUserBlobs userBlobs, IEnumerable<TBlobData> blobDatas);
        Task<ResponseResult<ApiResponse<Tuple<IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<TUserBlobs> userBlobs, IEnumerable<TBlobData> blobDatas);
        Task<ResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator);
        Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<ResponseResult<Stream>> DownloadBinary(TIdentifier identifier, IValidator validator);
        Task<ResponseResult<Stream>> DownloadRangeBinary(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<ResponseResult<ApiResponse<Tuple<string, TUserBlobs>>>> DownloadBase64(TIdentifier identifier, IValidator validator);
        Task<ResponseResult<ApiResponse<Tuple<IEnumerable<string>, IEnumerable<TUserBlobs>>>>> DownloadRangeBase64(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<ResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator);
        Task<ResponseResult<ApiResponse<TResponse>>> GetOneOwns<TResponse>(TIdentifier identifier, IValidator validator);
        Task<ResponseResult<ApiResponse<IPaged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<ResponseResult<ApiResponse<IPaged<IEnumerable<TResponse>>>>> SearchOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<ResponseResult<ApiResponse<IPaged<IEnumerable<TResponse>>>>> List<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<ResponseResult<ApiResponse<IPaged<IEnumerable<TResponse>>>>> ListOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<ResponseResult<Stream>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams);
        Task<ResponseResult<Stream>> DownloadCsvExcelOwns(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
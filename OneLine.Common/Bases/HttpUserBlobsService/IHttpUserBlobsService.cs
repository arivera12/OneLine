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
        Task<IResponseResult<IApiResponse<TUserBlobs>>> Add(IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<IEnumerable<TUserBlobs>>>> AddRange(IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<Tuple<TUserBlobs, TUserBlobs>>>> Update(TUserBlobs userBlobs, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<Tuple<IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<TUserBlobs> userBlobs, IEnumerable<TBlobData> blobDatas);
        Task<IResponseResult<IApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<Stream>> DownloadBinary(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<Stream>> DownloadRangeBinary(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<IApiResponse<Tuple<string, TUserBlobs>>>> DownloadBase64(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<IApiResponse<Tuple<IEnumerable<string>, IEnumerable<TUserBlobs>>>>> DownloadRangeBase64(IEnumerable<TIdentifier> identifiers, IValidator validator);
        Task<IResponseResult<IApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<IApiResponse<TResponse>>> GetOneOwns<TResponse>(TIdentifier identifier, IValidator validator);
        Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> SearchOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> List<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> ListOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<Stream>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<Stream>> DownloadCsvExcelOwns(ISearchPaging SearchPaging, object searchExtraParams);
    }
}
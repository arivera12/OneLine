using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class HttpBaseUserBlobsService<TUserBlobs, TIdentifier, TId, TBlobData, TBlobValidator> : HttpBaseService,
        IHttpUserBlobsService<TUserBlobs, TIdentifier, TBlobData, TBlobValidator>
        where TUserBlobs : class, IUserBlobs
        where TIdentifier : class, IIdentifier<TId>
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
    {
        public override string Api { get; set; } = "api";
        public virtual string ControllerName { get; set; } = "userblobs";
        public virtual string AddMethod { get; set; } = "add";
        public virtual string AddRangeMethod { get; set; } = "addrange";
        public virtual string UpdateMethod { get; set; } = "update";
        public virtual string UpdateRangeMethod { get; set; } = "updaterange";
        public virtual string DeleteMethod { get; set; } = "delete";
        public virtual string DeleteRangeMethod { get; set; } = "deleterange";
        public virtual string DownloadBinaryMethod { get; set; } = "downloadbinary";
        public virtual string DownloadRangeBinaryMethod { get; set; } = "downloadrangebinary";
        public virtual string DownloadBase64Method { get; set; } = "dowloadbase64";
        public virtual string DownloadRangeBase64Method { get; set; } = "downloadrangebase64";
        public virtual string GetOneMethod { get; set; } = "getone";
        public virtual string GetOneOwnsMethod { get; set; } = "getoneowns";
        public virtual string SearchMethod { get; set; } = "search";
        public virtual string SearchOwnsMethod { get; set; } = "searchowns";
        public virtual string ListMethod { get; set; } = "list";
        public virtual string ListOwnsMethod { get; set; } = "listowns";
        public virtual string DownloadCsvExcelMethod { get; set; } = "downloadcsvexcel";
        public virtual string DownloadCsvExcelOwnsMethod { get; set; } = "downloadcsvexcelowns";
        public virtual TBlobValidator BlobValidator { get; set; }
        public HttpBaseUserBlobsService() : base()
        {
        }
        public HttpBaseUserBlobsService(HttpClient httpClient) : base(httpClient)
        {
        }
        public HttpBaseUserBlobsService(Uri baseAddress) : base(baseAddress)
        {
        }
        public HttpBaseUserBlobsService(string AuthorizationToken, bool AddBearerScheme = true) : base(AuthorizationToken, AddBearerScheme)
        {
        }
        public HttpBaseUserBlobsService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
        }
        public virtual async Task<ResponseResult<ApiResponse<TUserBlobs>>> Add(IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonResponseResultAsync<TUserBlobs, TBlobData>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", blobDatas.FirstOrDefault(), new BlobDataValidator());
        }
        public virtual async Task<ResponseResult<ApiResponse<IEnumerable<TUserBlobs>>>> AddRange(IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonResponseResultAsync<IEnumerable<TUserBlobs>, IEnumerable<TBlobData>>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", blobDatas, new BlobDataCollectionValidator());
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteRangeMethod}", identifiers, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<string, TUserBlobs>>>> DownloadBase64(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<Tuple<string, TUserBlobs>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadBase64Method}", identifier, validator);
        }
        public virtual async Task<ResponseResult<Stream>> DownloadBinary(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadBinaryMethod}"), identifier, validator);
        }
        public virtual async Task<ResponseResult<Stream>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{DownloadCsvExcelMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<ResponseResult<Stream>> DownloadCsvExcelOwns(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{DownloadCsvExcelOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<IEnumerable<string>, IEnumerable<TUserBlobs>>>>> DownloadRangeBase64(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<Tuple<IEnumerable<string>, IEnumerable<TUserBlobs>>, TIdentifier>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{DownloadRangeBase64Method}", identifiers, validator);
        }
        public virtual async Task<ResponseResult<Stream>> DownloadRangeBinary(IEnumerable<TIdentifier>  identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{DownloadRangeBase64Method}"), identifiers, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<TResponse>>> GetOneOwns<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{GetOneOwnsMethod}", identifier, validator);
        }
        public virtual async Task<ResponseResult<ApiResponse<IPaged<IEnumerable<TResponse>>>>> List<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<IPaged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{ListMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<ResponseResult<ApiResponse<IPaged<IEnumerable<TResponse>>>>> ListOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<IPaged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{ListOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<ResponseResult<ApiResponse<IPaged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<IPaged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{SearchMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<ResponseResult<ApiResponse<IPaged<IEnumerable<TResponse>>>>> SearchOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<IPaged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{SearchOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<TUserBlobs, TUserBlobs>>>> Update(TUserBlobs userBlobs, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonResponseResultAsync<Tuple<TUserBlobs, TUserBlobs>, TBlobData>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", blobDatas.FirstOrDefault(), new BlobDataValidator());
        }
        public virtual async Task<ResponseResult<ApiResponse<Tuple<IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<TUserBlobs> userBlobs, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendJsonResponseResultAsync<Tuple<IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, IEnumerable<TBlobData>>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", blobDatas, new BlobDataCollectionValidator());

        }
    }
}

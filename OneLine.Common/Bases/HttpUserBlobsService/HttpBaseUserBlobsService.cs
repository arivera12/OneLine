using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class HttpBaseUserBlobsService<TIdentifier, TId> : HttpBaseService,
        IHttpUserBlobsService<TIdentifier>
        where TIdentifier : class, IIdentifier<TId>
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
        public virtual async Task<IResponseResult<ApiResponse<UserBlobs>>> Add(IEnumerable<BlobData> blobDatas)
        {
            return await HttpClient.SendJsonResponseResultAsync<UserBlobs, BlobData>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", blobDatas.FirstOrDefault(), new BlobDataValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<IEnumerable<UserBlobs>>>> AddRange(IEnumerable<BlobData> blobDatas)
        {
            return await HttpClient.SendJsonResponseResultAsync<IEnumerable<UserBlobs>, IEnumerable<BlobData>>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", blobDatas, new BlobDataCollectionValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"{GetApi()}/{ControllerName}/{DeleteRangeMethod}", identifiers, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<Tuple<string, UserBlobs>>>> DownloadBase64(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<Tuple<string, UserBlobs>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadBase64Method}", identifier, validator);
        }
        public virtual async Task<IResponseResult<HttpResponseMessage>> DownloadBinary(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonRequestResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadBinaryMethod}"), identifier, validator);
        }
        public virtual async Task<IResponseResult<HttpResponseMessage>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonRequestResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvExcelMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<HttpResponseMessage>> DownloadCsvExcelOwns(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonRequestResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvExcelOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<ApiResponse<Tuple<IEnumerable<string>, IEnumerable<UserBlobs>>>>> DownloadRangeBase64(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<Tuple<IEnumerable<string>, IEnumerable<UserBlobs>>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadRangeBase64Method}", identifiers, validator);
        }
        public virtual async Task<IResponseResult<HttpResponseMessage>> DownloadRangeBinary(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRequestResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadRangeBase64Method}"), identifiers, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> GetOneOwns<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneOwnsMethod}", identifier, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> List<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{ListMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> ListOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{ListOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{SearchMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> SearchOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{SearchOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<ApiResponse<Tuple<UserBlobs, UserBlobs>>>> Update(UserBlobs userBlobs, IEnumerable<BlobData> blobDatas)
        {
            return await HttpClient.SendJsonResponseResultAsync<Tuple<UserBlobs, UserBlobs>, BlobData>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", blobDatas.FirstOrDefault(), new BlobDataValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>>> UpdateRange(IEnumerable<UserBlobs> userBlobs, IEnumerable<BlobData> blobDatas)
        {
            return await HttpClient.SendJsonResponseResultAsync<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>, IEnumerable<BlobData>>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", blobDatas, new BlobDataCollectionValidator());

        }
    }
}

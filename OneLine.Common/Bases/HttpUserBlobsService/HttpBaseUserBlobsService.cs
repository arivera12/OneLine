using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.IO;
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
        public string ControllerName { get; set; } = "userblobs";
        public string AddMethod { get; set; } = "add";
        public string AddRangeMethod { get; set; } = "addrange";
        public string UpdateMethod { get; set; } = "update";
        public string UpdateRangeMethod { get; set; } = "updaterange";
        public string DeleteMethod { get; set; } = "delete";
        public string DeleteRangeMethod { get; set; } = "deleterange";
        public string DownloadBinaryMethod { get; set; } = "downloadbinary";
        public string DownloadRangeBinaryMethod { get; set; } = "downloadrangebinary";
        public string DownloadBase64Method { get; set; } = "dowloadbase64";
        public string DownloadRangeBase64Method { get; set; } = "downloadrangebase64";
        public string GetOneMethod { get; set; } = "getone";
        public string GetOneOwnsMethod { get; set; } = "getoneowns";
        public string SearchMethod { get; set; } = "search";
        public string SearchOwnsMethod { get; set; } = "searchowns";
        public string ListMethod { get; set; } = "list";
        public string ListOwnsMethod { get; set; } = "listowns";
        public string DownloadCsvExcelMethod { get; set; } = "downloadcsvexcel";
        public string DownloadCsvExcelOwnsMethod { get; set; } = "downloadcsvexcelowns";
        public TBlobValidator BlobValidator { get; set; }
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
        public async Task<IResponseResult<IApiResponse<TUserBlobs>>> Add(IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendBlobDataResponseResultAsync<TUserBlobs, TBlobData>(new HttpRequestMessage(HttpMethod.Post, $"/{ControllerName}/{AddMethod}"), blobDatas, new BlobDataValidator());
        }
        public async Task<IResponseResult<IApiResponse<IEnumerable<TUserBlobs>>>> AddRange(IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendBlobDataResponseResultAsync<IEnumerable<TUserBlobs>, TBlobData>(new HttpRequestMessage(HttpMethod.Post, $"/{ControllerName}/{AddRangeMethod}"), blobDatas, new BlobDataValidator());
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> Delete<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Delete, $"/{ControllerName}/{DeleteMethod}", identifier, validator);
        }
        public async Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> DeleteRange<TResponse>(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<IEnumerable<TResponse>, TIdentifier>(HttpMethod.Delete, $"/{ControllerName}/{DeleteRangeMethod}", identifiers, validator);
        }
        public async Task<IResponseResult<IApiResponse<Tuple<string, TUserBlobs>>>> DownloadBase64(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<Tuple<string, TUserBlobs>, TIdentifier>(HttpMethod.Post, $"/{ControllerName}/{DownloadBase64Method}", identifier, validator);
        }
        public async Task<IResponseResult<Stream>> DownloadBinary(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"/{ControllerName}/{DownloadBinaryMethod}"), identifier, validator);
        }
        public async Task<IResponseResult<Stream>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"/{ControllerName}/{DownloadCsvExcelMethod}"), new { SearchPaging, searchExtraParams });
        }
        public async Task<IResponseResult<Stream>> DownloadCsvExcelOwns(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"/{ControllerName}/{DownloadCsvExcelOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public async Task<IResponseResult<IApiResponse<Tuple<IEnumerable<string>, IEnumerable<TUserBlobs>>>>> DownloadRangeBase64(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeResponseResultAsync<Tuple<IEnumerable<string>, IEnumerable<TUserBlobs>>, TIdentifier>(HttpMethod.Get, $"/{ControllerName}/{DownloadRangeBase64Method}", identifiers, validator);
        }
        public async Task<IResponseResult<Stream>> DownloadRangeBinary(IEnumerable<TIdentifier>  identifiers, IValidator validator)
        {
            return await HttpClient.SendJsonRangeDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"/{ControllerName}/{DownloadRangeBase64Method}"), identifiers, validator);
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> GetOne<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Get, $"/{ControllerName}/{GetOneMethod}", identifier, validator);
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> GetOneOwns<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Get, $"/{ControllerName}/{GetOneOwnsMethod}", identifier, validator);
        }
        public async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> List<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<TResponse>>>>($"/{ControllerName}/{ListMethod}", new { SearchPaging, searchExtraParams });
        }
        public async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> ListOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<TResponse>>>>($"/{ControllerName}/{ListOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> Search<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<TResponse>>>>($"/{ControllerName}/{SearchMethod}", new { SearchPaging, searchExtraParams });
        }
        public async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> SearchOwns<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<TResponse>>>>($"/{ControllerName}/{SearchOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public async Task<IResponseResult<IApiResponse<Tuple<TUserBlobs, TUserBlobs>>>> Update(TUserBlobs userBlobs, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendBlobDataResponseResultAsync<Tuple<TUserBlobs, TUserBlobs>, TBlobData>(new HttpRequestMessage(HttpMethod.Put, $"/{ControllerName}/{UpdateMethod}"), blobDatas, new BlobDataValidator());
        }
        public async Task<IResponseResult<IApiResponse<Tuple<IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> UpdateRange(IEnumerable<TUserBlobs> userBlobs, IEnumerable<TBlobData> blobDatas)
        {
            return await HttpClient.SendBlobDataResponseResultAsync<Tuple<IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>, TBlobData>(new HttpRequestMessage(HttpMethod.Put, $"/{ControllerName}/{UpdateMethod}"), blobDatas, new BlobDataValidator());

        }
    }
}

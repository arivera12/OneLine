using FluentValidation;
using OneLine.Contracts;
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
    public class HttpBaseUserBlobsService<T, TIdentifier, TId> : HttpBaseCrudExtendedService<T, TIdentifier, TId>,
        IHttpUserBlobsService<T, TIdentifier>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>
    {
        public override string Api { get; set; } = "api";
        public override string ControllerName { get; set; } = typeof(T).Name.ToLower();
        public string DownloadBinaryMethod { get; set; } = "downloadbinary";
        public string DownloadRangeBinaryMethod { get; set; } = "downloadrangebinary";
        public string DownloadBase64Method { get; set; } = "dowloadbase64";
        public string DownloadRangeBase64Method { get; set; } = "downloadrangebase64";
        public string GetOneOwnsMethod { get; set; } = "getoneowns";
        public string SearchOwnsMethod { get; set; } = "searchowns";
        public string ListMethod { get; set; } = "list";
        public string ListOwnsMethod { get; set; } = "listowns";
        public string DownloadCsvOwnsMethod { get; set; } = "downloadcsvowns";

        public HttpBaseUserBlobsService() : base()
        {
        }
        public HttpBaseUserBlobsService(HttpClient httpClient) : base(httpClient)
        {
        }
        public HttpBaseUserBlobsService(string baseAddress) : base(baseAddress)
        {
        }
        public HttpBaseUserBlobsService(Uri baseAddress) : base(baseAddress)
        {
        }
        public HttpBaseUserBlobsService(string AuthorizationToken, bool AddBearerScheme = true) : base(AuthorizationToken, AddBearerScheme)
        {
        }
        public HttpBaseUserBlobsService(string baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
        }
        public HttpBaseUserBlobsService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
        }
        public Task<IResponseResult<ApiResponse<T>>> AddAsync(IEnumerable<BlobData> blobDatas)
        {
            return HttpClient.SendJsonResponseResultAsync<T, BlobData>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", blobDatas.FirstOrDefault(), new BlobDataValidator());
        }
        public Task<IResponseResult<ApiResponse<IEnumerable<T>>>> AddRangeAsync(IEnumerable<BlobData> blobDatas)
        {
            return HttpClient.SendJsonResponseResultAsync<IEnumerable<T>, IEnumerable<BlobData>>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", blobDatas, new BlobDataCollectionValidator());
        }
        public Task<IResponseResult<ApiResponse<Tuple<string, T>>>> DownloadBase64Async(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<Tuple<string, T>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadBase64Method}", identifier, validator);
        }
        public Task<IResponseResult<HttpResponseMessage>> DownloadBinaryAsync(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadBinaryMethod}"), identifier, validator);
        }
        public Task<IResponseResult<HttpResponseMessage>> DownloadCsvOwnsAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public Task<IResponseResult<ApiResponse<Tuple<IEnumerable<string>, IEnumerable<T>>>>> DownloadRangeBase64Async(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<Tuple<IEnumerable<string>, IEnumerable<T>>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadRangeBase64Method}", identifiers, validator);
        }
        public Task<IResponseResult<HttpResponseMessage>> DownloadRangeBinaryAsync(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadRangeBase64Method}"), identifiers, validator);
        }
        public Task<IResponseResult<ApiResponse<TResponse>>> GetOneOwnsAsync<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneOwnsMethod}", identifier, validator);
        }
        public Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> ListAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{ListMethod}", new { SearchPaging, searchExtraParams });
        }
        public Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> ListOwnsAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{ListOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> SearchOwnsAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{SearchOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public Task<IResponseResult<ApiResponse<Tuple<T, T>>>> UpdateAsync(T userBlobs, IEnumerable<BlobData> blobDatas)
        {
            return HttpClient.SendJsonResponseResultAsync<Tuple<T, T>, BlobData>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", blobDatas.FirstOrDefault(), new BlobDataValidator());
        }
        public Task<IResponseResult<ApiResponse<Tuple<IEnumerable<T>, IEnumerable<T>>>>> UpdateRangeAsync(IEnumerable<T> userBlobs, IEnumerable<BlobData> blobDatas)
        {
            return HttpClient.SendJsonResponseResultAsync<Tuple<IEnumerable<T>, IEnumerable<T>>, IEnumerable<BlobData>>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", blobDatas, new BlobDataCollectionValidator());
        }
        public Task<IResponseResult<byte[]>> DownloadCsvOwnsAsByteArrayAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonDownloadAsByteArrayResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public Task<IResponseResult<Stream>> DownloadCsvOwnsAsStreamAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonDownloadAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public Task<IResponseResult<HttpResponseMessage>> DownloadCsvOwnsAsHttpResponseMessageAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
    }
}

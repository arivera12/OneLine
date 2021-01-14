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
        public override string ControllerName { get; set; } = nameof(UserBlobs).ToLower();
        public virtual string DownloadBinaryMethod { get; set; } = "downloadbinary";
        public virtual string DownloadRangeBinaryMethod { get; set; } = "downloadrangebinary";
        public virtual string DownloadBase64Method { get; set; } = "dowloadbase64";
        public virtual string DownloadRangeBase64Method { get; set; } = "downloadrangebase64";
        public virtual string GetOneOwnsMethod { get; set; } = "getoneowns";
        public virtual string SearchOwnsMethod { get; set; } = "searchowns";
        public virtual string ListMethod { get; set; } = "list";
        public virtual string ListOwnsMethod { get; set; } = "listowns";
        public virtual string DownloadCsvOwnsMethod { get; set; } = "downloadcsvowns";

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
        public virtual Task<IResponseResult<ApiResponse<UserBlobs>>> AddAsync(IEnumerable<BlobData> blobDatas)
        {
            return HttpClient.SendJsonResponseResultAsync<UserBlobs, BlobData>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", blobDatas.FirstOrDefault(), new BlobDataValidator());
        }
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<UserBlobs>>>> AddRangeAsync(IEnumerable<BlobData> blobDatas)
        {
            return HttpClient.SendJsonResponseResultAsync<IEnumerable<UserBlobs>, IEnumerable<BlobData>>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{AddMethod}", blobDatas, new BlobDataCollectionValidator());
        }
        public virtual Task<IResponseResult<ApiResponse<Tuple<string, UserBlobs>>>> DownloadBase64Async(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<Tuple<string, UserBlobs>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadBase64Method}", identifier, validator);
        }
        public virtual Task<IResponseResult<HttpResponseMessage>> DownloadBinaryAsync(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadBinaryMethod}"), identifier, validator);
        }
        public virtual Task<IResponseResult<HttpResponseMessage>> DownloadCsvOwnsAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<ApiResponse<Tuple<IEnumerable<string>, IEnumerable<UserBlobs>>>>> DownloadRangeBase64Async(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return HttpClient.SendJsonRangeResponseResultAsync<Tuple<IEnumerable<string>, IEnumerable<UserBlobs>>, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadRangeBase64Method}", identifiers, validator);
        }
        public virtual Task<IResponseResult<HttpResponseMessage>> DownloadRangeBinaryAsync(IEnumerable<TIdentifier> identifiers, IValidator validator)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadRangeBase64Method}"), identifiers, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<TResponse>>> GetOneOwnsAsync<TResponse>(TIdentifier identifier, IValidator validator)
        {
            return HttpClient.SendJsonResponseResultAsync<TResponse, TIdentifier>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{GetOneOwnsMethod}", identifier, validator);
        }
        public virtual Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> ListAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{ListMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> ListOwnsAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{ListOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<ApiResponse<Paged<IEnumerable<TResponse>>>>> SearchOwnsAsync<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.GetJsonResponseResultAsync<ApiResponse<Paged<IEnumerable<TResponse>>>>($"{GetApi()}/{ControllerName}/{SearchOwnsMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<ApiResponse<Tuple<UserBlobs, UserBlobs>>>> UpdateAsync(UserBlobs userBlobs, IEnumerable<BlobData> blobDatas)
        {
            return HttpClient.SendJsonResponseResultAsync<Tuple<UserBlobs, UserBlobs>, BlobData>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", blobDatas.FirstOrDefault(), new BlobDataValidator());
        }
        public virtual Task<IResponseResult<ApiResponse<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>>>> UpdateRangeAsync(IEnumerable<UserBlobs> userBlobs, IEnumerable<BlobData> blobDatas)
        {
            return HttpClient.SendJsonResponseResultAsync<Tuple<IEnumerable<UserBlobs>, IEnumerable<UserBlobs>>, IEnumerable<BlobData>>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{UpdateMethod}", blobDatas, new BlobDataCollectionValidator());
        }
        public virtual Task<IResponseResult<byte[]>> DownloadCsvOwnsAsByteArrayAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonDownloadAsByteArrayResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<Stream>> DownloadCsvOwnsAsStreamAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonDownloadAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<HttpResponseMessage>> DownloadCsvOwnsAsHttpResponseMessageAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvOwnsMethod}"), new { SearchPaging, searchExtraParams });
        }
    }
}

using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class HttpBaseCrudExtendedService<T, TIdentifier, TId> :
        HttpBaseCrudService<T, TIdentifier, TId>,
        IHttpCrudExtendedService<T, TIdentifier>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>
    {
        public virtual string DownloadCsvMethod { get; set; } = "downloadcsv";
        public virtual string UploadCsvMethod { get; set; } = "uploadcsv";
        public HttpBaseCrudExtendedService() : base()
        {
        }
        public HttpBaseCrudExtendedService(HttpClient httpClient) : base(httpClient)
        {
        }
        public HttpBaseCrudExtendedService(Uri baseAddress) : base(baseAddress)
        {
        }
        public HttpBaseCrudExtendedService(string AuthorizationToken, bool AddBearerScheme = true) : base(AuthorizationToken, AddBearerScheme)
        {
        }
        public HttpBaseCrudExtendedService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
        }
        public virtual Task<IResponseResult<byte[]>> DownloadCsvAsByteArrayAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonDownloadAsByteArrayResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<Stream>> DownloadCsvAsStreamAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonDownloadAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}"), new { SearchPaging, searchExtraParams });
        }
        public Task<IResponseResult<HttpResponseMessage>> DownloadCsvAsHttpResponseMessageAsync(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonRequestHttpResponseMessageResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<T>>>> UploadCsvAsync(IEnumerable<BlobData> blobDatas, IValidator validator, HttpMethod httpMethod)
        {
            return HttpClient.SendJsonResponseResultAsync<IEnumerable<T>, IEnumerable<BlobData>>(httpMethod, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}", blobDatas, validator);
        }
    }
}

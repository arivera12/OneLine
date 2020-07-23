using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
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
        public virtual Task<IResponseResult<byte[]>> DownloadCsvAsByteArray(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonDownloadBlobAsByteArrayResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<HttpResponseMessage>> DownloadCsvAsStream(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return HttpClient.SendJsonRequestResponseResultAsync(new HttpRequestMessage(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual Task<IResponseResult<ApiResponse<IEnumerable<T>>>> UploadCsv(IEnumerable<BlobData> blobDatas, IValidator validator, HttpMethod httpMethod)
        {
            return HttpClient.SendJsonResponseResultAsync<IEnumerable<T>, IEnumerable<BlobData>>(httpMethod, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}", blobDatas, validator);
        }
    }
}

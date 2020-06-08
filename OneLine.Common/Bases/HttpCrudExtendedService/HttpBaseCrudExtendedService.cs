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
    public class HttpBaseCrudExtendedService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs> :
        HttpBaseCrudService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs>,
        IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
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
        public virtual async Task<ResponseResult<byte[]>> DownloadCsvAsByteArray(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonDownloadBlobAsByteArrayResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<ResponseResult<Stream>> DownloadCsvAsStream(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonDownloadBlobAsStreamResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<ResponseResult<ApiResponse<IEnumerable<T>>>> UploadCsv(IEnumerable<IBlobData> blobDatas, IValidator validator, HttpMethod httpMethod)
        {
            return await HttpClient.SendJsonResponseResultAsync<IEnumerable<T>, IEnumerable<IBlobData>>(httpMethod, $"{GetApi()}/{ControllerName}/{DownloadCsvMethod}", blobDatas, validator);
        }
    }
}

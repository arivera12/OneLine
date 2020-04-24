using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class HttpBaseCrudExtendedService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs> :
        HttpBaseCrudService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs>,
        IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
        where T : new()
        where TIdentifier : IIdentifier<TId>
        where TId : class
        where TBlobData : IBlobData
        where TBlobValidator : IValidator, new()
        where TUserBlobs : IUserBlobs
    {
        public virtual string ListMethod { get; set; } = "list";
        public virtual string DownloadCsvExcelMethod { get; set; } = "downloadcsvexcel";
        public virtual string SaveReplaceListMethod { get; set; } = "savereplacelist";
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
        public virtual async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> List<TResponse>(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<TResponse>>>>($"/{ControllerName}/{ListMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<byte[]>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonDownloadBlobAsByteArrayResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"/{ControllerName}/{DownloadCsvExcelMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> SaveReplaceList<TResponse>(ISaveReplaceList<IEnumerable<T>> SaveReplaceListModel)
        {
            return await HttpClient.PostJsonResponseResultAsync<IApiResponse<IEnumerable<TResponse>>>($"/{ControllerName}/{SaveReplaceListMethod}", SaveReplaceListModel);
        }
    }
}

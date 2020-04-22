using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class HttpServiceExtended<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs> :
        HttpServiceBase<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs>,
        IHttpServiceExtended<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
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
        public virtual async Task<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> List(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<IPaged<IEnumerable<T>>>>($"/{ControllerName}/{ListMethod}", new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<byte[]>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams)
        {
            return await HttpClient.SendJsonDownloadBlobAsByteArrayResponseResultAsync(new HttpRequestMessage(HttpMethod.Get, $"/{ControllerName}/{DownloadCsvExcelMethod}"), new { SearchPaging, searchExtraParams });
        }
        public virtual async Task<IResponseResult<IApiResponse<IEnumerable<T>>>> SaveReplaceList(ISaveReplaceList<IEnumerable<T>> SaveReplaceListModel)
        {
            return await HttpClient.PostJsonResponseResultAsync<IApiResponse<IEnumerable<T>>>($"/{ControllerName}/{SaveReplaceListMethod}", SaveReplaceListModel);
        }
    }
}

using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs> : IHttpCrudService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
    {
        string ListMethod { get; set; }
        string DownloadCsvExcelMethod { get; set; }
        string SaveReplaceListMethod { get; set; }
        Task<IResponseResult<IApiResponse<IPaged<IEnumerable<TResponse>>>>> List<TResponse>(ISearchPaging SearchPaging, object searchExtraParams);
        Task<IResponseResult<IApiResponse<IEnumerable<TResponse>>>> SaveReplaceList<TResponse>(ISaveReplaceList<IEnumerable<T>> SaveReplaceListModel);
        Task<IResponseResult<byte[]>> DownloadCsvExcel(ISearchPaging SearchPaging, object searchExtraParams);
    }
}

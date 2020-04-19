using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpServiceExtended<T, TValidator, TIdentifier, TIdentifierValidator, TSearchExtraParams, TBlobData, TBlobValidator, TUserBlobs>
    {
        string ListMethod { get; set; }
        string DownloadCsvExcelMethod { get; set; }
        string SaveReplaceListMethod { get; set; }
        Task<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> List(ISearchPaging SearchPaging, TSearchExtraParams searchExtraParams);
        Task<IResponseResult<byte[]>> DownloadCsvExcel(ISearchPaging SearchPaging, TSearchExtraParams searchExtraParams);
        Task<IResponseResult<IApiResponse<IEnumerable<T>>>> SaveReplaceList(ISaveReplaceList<IEnumerable<T>> SaveReplaceListModel);
    }
}

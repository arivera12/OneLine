using Microsoft.AspNetCore.Mvc;
using OneLine.Models;
using Storage.Net.Blobs;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IController<T, TViewModel, TIdentifier, TSearchPaging, TSearchExtraParams, TSaveReplaceList, TAuditTrails, TExceptionLogs, TUserBlobs>
        where TAuditTrails : AuditTrails, IAuditTrails
        where TExceptionLogs : ExceptionLogs, IExceptionLogs
        where TUserBlobs : UserBlobs, IUserBlobs
    {
        BaseDbContext<TAuditTrails, TExceptionLogs, TUserBlobs> BaseDbContext { get; set; }
        IBlobStorage BaseBlobStorage { get; set; }
        Task<IActionResult> Add([FromBody]TViewModel viewModel);
        Task<IActionResult> Update([FromBody]TViewModel viewModel);
        Task<IActionResult> Delete([FromQuery]TIdentifier identifier);
        Task<IActionResult> GetOne([FromQuery]TIdentifier identifier);
        IActionResult Search([FromQuery]TSearchPaging SearchPaging, [FromQuery]TSearchExtraParams searchExtraParams);
        IActionResult List([FromQuery]TSearchPaging SearchPaging, [FromQuery]TSearchExtraParams searchExtraParams);
        IActionResult DownloadCsvExcel([FromQuery]TSearchPaging SearchPaging, [FromQuery]TSearchExtraParams searchExtraParams);
        Task<IActionResult> SaveReplaceList([FromBody]TSaveReplaceList TSaveReplaceList);
    }
}

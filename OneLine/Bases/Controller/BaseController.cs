//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using OneLine.Attributes;
//using OneLine.Constants;
//using OneLine.Models;
//using Storage.Net.Blobs;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace OneLine.Bases
//{
//    [Route(Routes.Api.Default)]
//    [Authorize(AuthenticationSchemes = JwtAuthenticationSchemes.Bearer)]
//    public class BaseController<T, TViewModel, TIdentifier, TId, TSearchPaging, TSearchExtraParams, TSaveReplaceList, TAuditTrails, TExceptionsLogs, TUserBlobs> : Controller,
//        IController<T, TViewModel, TIdentifier, TSearchPaging, TSearchExtraParams, TSaveReplaceList, TAuditTrails, TExceptionsLogs, TUserBlobs>
//        where T : class
//        where TViewModel : class
//        where TIdentifier : IIdentifier<TId>
//        where TId : class
//        where TSearchPaging : ISearchPaging
//        where TSearchExtraParams : class
//        where TSaveReplaceList : ISaveReplaceList<IEnumerable<T>>
//        where TAuditTrails : AuditTrails, IAuditTrails
//        where TExceptionsLogs : ExceptionsLogs, IExceptionsLogs
//        where TUserBlobs : UserBlobs, IUserBlobs
//    {
//        public virtual BaseDbContext<TAuditTrails, TExceptionsLogs, TUserBlobs> BaseDbContext { get; set; }
//        public virtual IBlobStorage BaseBlobStorage { get; set; }

//        [Route(Routes.BaseController.Add)]
//        [HttpPost]
//        [ValidateModelState]
//        [Authorize]
//        public virtual async Task<IActionResult> Add([FromBody] TViewModel viewModel)
//        {
//            throw new NotImplementedException();
//        }
//        [Route(Routes.BaseController.Update)]
//        [HttpPut]
//        [ValidateModelState]
//        [Authorize]
//        public virtual async Task<IActionResult> Update([FromBody] TViewModel viewModel)
//        {
//            throw new NotImplementedException();
//        }
//        [Route(Routes.BaseController.Delete)]
//        [HttpDelete]
//        [ValidateModelState]
//        [Authorize]
//        public virtual async Task<IActionResult> Delete([FromQuery] TIdentifier identifier)
//        {
//            throw new NotImplementedException();
//        }
//        [Route(Routes.BaseController.GetOne)]
//        [HttpDelete]
//        [ValidateModelState]
//        [Authorize]
//        public virtual async Task<IActionResult> GetOne([FromQuery] TIdentifier identifier)
//        {
//            throw new NotImplementedException();
//        }
//        [Route(Routes.BaseController.Search)]
//        [HttpGet]
//        [ValidateModelState]
//        [Authorize]
//        public virtual IActionResult Search([FromQuery] TSearchPaging SearchPaging, [FromQuery] TSearchExtraParams searchExtraParams)
//        {
//            throw new NotImplementedException();
//        }
//        [Route(Routes.BaseController.DownloadCsvExcel)]
//        [HttpGet]
//        [ValidateModelState]
//        [Authorize]
//        public virtual IActionResult DownloadCsvExcel([FromQuery] TSearchPaging SearchPaging, [FromQuery] TSearchExtraParams searchExtraParams)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

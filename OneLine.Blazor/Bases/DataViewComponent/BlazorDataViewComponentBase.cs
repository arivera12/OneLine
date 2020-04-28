using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract partial class BlazorDataViewComponentBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        DataViewBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs>,
        IBlazorDataViewComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : new()
        where TIdentifier : IIdentifier<TId>, new()
        where TId : class
        where THttpService : HttpBaseCrudExtendedService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : IBlobData
        where TBlobValidator : IValidator, new()
        where TUserBlobs : IUserBlobs
    {
        [Inject] public override IConfiguration Configuration { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public BlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
        [Inject] public BlazorDownloadFileService BlazorDownloadFileService { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; }
        [Parameter] public override TIdentifier Identifier { get; set; } = new TIdentifier();
        [Parameter] public override IEnumerable<TIdentifier> Identifiers { get; set; } = new List<TIdentifier>();
        [Parameter] public override T Record { get; set; } = new T();
        [Parameter] public override ObservableRangeCollection<T> Records { get; set; }
        [Parameter] public override object SearchExtraParams { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnResponse { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnResponseSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnResponseException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnResponseFailed { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollection { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionFailed { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePaged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> OnResponsePagedFailed { get; set; }
        [Parameter] public override ISearchPaging SearchPaging { get; set; }
        [Parameter] public override RecordsSelectionMode RecordsSelectionMode { get; set; }
        [Parameter] public override ObservableRangeCollection<T> SelectedRecords { get; set; }
        [Parameter] public override long MinimunRecordSelections { get; set; }
        [Parameter] public override long MaximumRecordSelections { get; set; }
        [Parameter] public override Action<T> OnSelectedRecord { get; set; }
        [Parameter] public override Action<IEnumerable<T>, bool, bool> OnSelectedRecords { get; set; }
        [Parameter] public override Action<bool> OnMinimunRecordSelectionsReached { get; set; }
        [Parameter] public override Action<bool> OnMaximumRecordSelectionsReached { get; set; }
        [Parameter] public override Action<T> OnLoad { get; set; }
        [Parameter] public override Action<T> OnLoadSucceeded { get; set; }
        [Parameter] public override Action<T> OnLoadException { get; set; }
        [Parameter] public override Action<T> OnLoadFailed { get; set; }
        [Parameter] public override CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        public bool IsDesktop { get; set; }
        public bool IsTablet { get; set; }
        public bool IsMobile { get; set; }
        public async Task OnAfterRenderInitializeAsync()
        {
            IsMobile = await BlazorCurrentDeviceService.Mobile();
            IsTablet = await BlazorCurrentDeviceService.Tablet();
            IsDesktop = await BlazorCurrentDeviceService.Desktop();
            if (Record == null && (Records == null || !Records.Any()))
            {
                if ((Identifier != null && Identifier.Model != null) ||
                    (Identifiers != null && Identifiers.Any()))
                {
                    await Load();
                }
            }
        }
    }
}

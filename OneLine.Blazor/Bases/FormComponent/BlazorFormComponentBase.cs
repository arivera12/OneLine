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
    /// <summary>
    /// Base component implementation with form default behaviors
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="THttpService"></typeparam>
    /// <typeparam name="TBlobData"></typeparam>
    /// <typeparam name="TBlobValidator"></typeparam>
    /// <typeparam name="TUserBlobs"></typeparam>
    public abstract partial class BlazorFormComponentBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        FormBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs>,
        IBlazorFormComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>, new()
        where TId : class
        where THttpService : HttpBaseCrudExtendedService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        [Inject] public override IConfiguration Configuration { get; set; }
        [Inject] public virtual IJSRuntime JSRuntime { get; set; }
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
        [Inject] public virtual BlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
        [Inject] public virtual BlazorDownloadFileService BlazorDownloadFileService { get; set; }
        [Inject] public virtual SweetAlertService SweetAlertService { get; set; }
        [Parameter] public override T Record { get; set; }
        [Parameter] public override ObservableRangeCollection<T> Records { get; set; }
        [Parameter] public override TIdentifier Identifier { get; set; }
        [Parameter] public override IEnumerable<TIdentifier> Identifiers { get; set; }
        [Parameter] public override THttpService HttpService { get; set; }
        [Parameter] public override IList<TBlobData> BlobDatas { get; set; }
        [Parameter] public override FormState FormState { get; set; }
        [Parameter] public override FormMode FormMode { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnLoad { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnLoadSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnLoadException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnLoadFailed { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollection { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollectionSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollectionException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnLoadCollectionFailed { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<T>> Response { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnResponse { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnResponseSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnResponseException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> OnResponseFailed { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollection { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> OnResponseCollectionFailed { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> ResponseAddWithBlobs { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobs { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> OnResponseAddWithBlobsFailed { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> ResponseAddCollectionWithBlobs { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobs { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobsSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobsException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> OnResponseAddCollectionWithBlobsFailed { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateWithBlobs { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobs { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateWithBlobsFailed { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateCollectionWithBlobs { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobs { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobsSucceeded { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobsException { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> OnResponseUpdateCollectionWithBlobsFailed { get; set; }
        [Parameter] public override Action<Action> OnBeforeReset { get; set; }
        [Parameter] public override Action OnAfterReset { get; set; }
        [Parameter] public override Action<Action> OnBeforeCancel { get; set; }
        [Parameter] public override Action OnAfterCancel { get; set; }
        [Parameter] public override Action<Action> OnBeforeSave { get; set; }
        [Parameter] public override Action OnAfterSave { get; set; }
        [Parameter] public override Action<Action> OnBeforeDelete { get; set; }
        [Parameter] public override Action<T> OnAfterDelete { get; set; }
        [Parameter] public override Action OnFailedSave { get; set; }
        [Parameter] public override Action OnFailedValidation { get; set; }
        [Parameter] public override CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        public virtual bool IsDesktop { get; set; }
        public virtual bool IsTablet { get; set; }
        public virtual bool IsMobile { get; set; }
        public virtual async Task OnAfterRenderInitializeAsync()
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
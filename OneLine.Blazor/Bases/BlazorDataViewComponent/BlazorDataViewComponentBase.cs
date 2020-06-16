using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract partial class BlazorDataViewComponentBase<T, TIdentifier, TId, THttpService> :
        DataViewBase<T, TIdentifier, TId, THttpService>,
        IBlazorDataViewComponent<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        [Inject] public override IConfiguration Configuration { get; set; }
        [Inject] public virtual IJSRuntime JSRuntime { get; set; }
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
        [Inject] public virtual IBlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
        [Inject] public virtual IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
        [Inject] public virtual SweetAlertService SweetAlertService { get; set; }
        [Inject] public virtual HttpClient HttpClient { get; set; }
        [Parameter] public override TIdentifier Identifier { get; set; }
        [Parameter] public override IEnumerable<TIdentifier> Identifiers { get; set; }
        [Parameter] public override T Record { get; set; }
        [Parameter] public override ObservableRangeCollection<T> Records { get; set; }
        [Parameter] public override object[] SearchExtraParams { get; set; }
        [Parameter] public override Func<T, bool> FilterPredicate { get; set; }
        [Parameter] public override string FilterSortBy { get; set; }
        [Parameter] public override bool FilterDescending { get; set; }
        [Parameter] public override ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        [Parameter] public override IResponseResult<ApiResponse<T>> Response { get; set; }
        [Parameter] public override IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        [Parameter] public override IResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
        [Parameter] public override IPaging Paging { get; set; }
        [Parameter] public override ISearchPaging SearchPaging { get; set; }
        [Parameter] public override RecordsSelectionMode RecordsSelectionMode { get; set; }
        [Parameter] public override CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        [Parameter] public override T SelectedRecord { get; set; }
        [Parameter] public override ObservableRangeCollection<T> SelectedRecords { get; set; }
        [Parameter] public override long MinimunRecordsSelections { get; set; }
        [Parameter] public override long MaximumRecordsSelections { get; set; }
        [Parameter] public override bool MinimunRecordsSelectionsReached { get; set; }
        [Parameter] public override bool MaximumRecordsSelectionsReached { get; set; }
        [Parameter] public override Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        [Parameter] public override Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        [Parameter] public override Action<IResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
        [Parameter] public override Action OnBeforeSearch { get; set; }
        [Parameter] public override Action OnAfterSearch { get; set; }
        [Parameter] public override Action<TIdentifier> IdentifierChanged { get; set; }
        [Parameter] public override Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        [Parameter] public override Action<T> RecordChanged { get; set; }
        [Parameter] public override Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        [Parameter] public override Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
        [Parameter] public override Action<T> SelectedRecordChanged { get; set; }
        [Parameter] public override Action BeforeSelectedRecord { get; set; }
        [Parameter] public override Action AfterSelectedRecord { get; set; }
        [Parameter] public override Action<IEnumerable<T>, bool, bool> SelectedRecordsChanged { get; set; }
        [Parameter] public override Action<bool> MinimunRecordsSelectionsReachedChanged { get; set; }
        [Parameter] public override Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        [Parameter] public override Action<IPaging> PagingChanged { get; set; }
        [Parameter] public override Action<ISearchPaging> SearchPagingChanged { get; set; }
        [Parameter] public override Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        [Parameter] public override Action<string> FilterSortByChanged { get; set; }
        [Parameter] public override Action<bool> FilterDescendingChanged { get; set; }
        [Parameter] public virtual int DebounceInterval { get; set; }
        public bool IsDesktop { get; set; }
        public bool IsTablet { get; set; }
        public bool IsMobile { get; set; }
        public virtual async Task OnAfterFirstRenderAsync()
        {
            HttpService.HttpClient = HttpClient;
            IsMobile = await BlazorCurrentDeviceService.Mobile();
            IsTablet = await BlazorCurrentDeviceService.Tablet();
            IsDesktop = await BlazorCurrentDeviceService.Desktop();
            if (RecordsSelectionMode.IsSingle())
            {
                if (Record.IsNull())
                {
                    if (Identifier.IsNotNull() && Identifier.Model.IsNotNull())
                    {
                        await Load();
                    }
                }
            }
            else if (RecordsSelectionMode.IsMultiple())
            {
                if (Records.IsNullOrEmpty())
                {
                    if (Identifiers.IsNotNullAndNotEmpty())
                    {
                        await Load();
                    }
                }
            }
            StateHasChanged();
        }
    }
}

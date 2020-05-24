﻿using BlazorCurrentDevice;
using BlazorDownloadFile;
using BlazorStrap;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OneLine.Bases;
using OneLine.Blazor.Extensions;
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
    public abstract partial class BlazorStrapDataViewComponentBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        DataViewBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs>,
        IBlazorStrapDataViewComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        [Inject] public override IConfiguration Configuration { get; set; }
        [Inject] public virtual IJSRuntime JSRuntime { get; set; }
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
        [Inject] public virtual BlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
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
        [Parameter] public override ResponseResult<ApiResponse<T>> Response { get; set; }
        [Parameter] public override ResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        [Parameter] public override ResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
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
        [Parameter] public override Action<ResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        [Parameter] public override Action<ResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        [Parameter] public override Action<ResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
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
        public bool ShowActivityIndicator { get; set; }
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
            //This null check allows to prevent override the listeners from parent if it's listening to any of this events
            OnBeforeSearch ??= new Action(async () => await BeforeSearch());
            OnAfterSearch ??= new Action(async () => await AfterSearch());
            StateHasChanged();
        }
        public virtual async Task BeforeSearch()
        {
            if(IsDesktop)
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Search()), Resourcer.GetString("ProcessingRequest"), Resourcer.GetString("PleaseWait"));
                StateHasChanged();
            }
            else
            {
                ShowActivityIndicator = true;
                StateHasChanged();
                await Search();
            }
        }
        public virtual async Task AfterSearch()
        {
            if (IsDesktop)
            {
                await SweetAlertService.HideLoaderAsync(); 
            }
            else
            {
                ShowActivityIndicator = false;
            }
            StateHasChanged();
        }
        public virtual Size InputSize()
        {
            return IsDesktop ? Size.Large : IsTablet ? Size.None : IsMobile ? Size.Small : Size.None;
        }
        public virtual Size ButtonSize()
        {
            return IsDesktop ? Size.Large : IsTablet ? Size.None : IsMobile ? Size.Small : Size.None;
        }
        public virtual async Task PagingChange(IPaging paging)
        {
            SearchPaging.AutoMap(paging);
            await BeforeSearch();
        }
        public virtual void SearchTermChanged(string searchTerm)
        {
            SearchPaging.SearchTerm = searchTerm;
            RateLimitingExtensionForObject.Debounce(SearchPaging, DebounceInterval, async (searchPagingDebounced) =>
            {
                await BeforeSearch();
            });
        }
        public virtual async Task LoadMore()
        {
            await GoNextPage();
            await BeforeSearch();
        }
        public virtual TColor HighlightItem<TColor>(T record, TColor selectedColor, TColor unSelectedColor)
        {
            if (RecordsSelectionMode.IsSingle())
            {
                if (SelectedRecord == record)
                {
                    return selectedColor;
                }
                else
                {
                    return unSelectedColor;
                }
            }
            else if (RecordsSelectionMode.IsMultiple())
            {
                if (SelectedRecords.Contains(record))
                {
                    return selectedColor;
                }
                else
                {
                    return unSelectedColor;
                }
            }
            return unSelectedColor;
        }
    }
}
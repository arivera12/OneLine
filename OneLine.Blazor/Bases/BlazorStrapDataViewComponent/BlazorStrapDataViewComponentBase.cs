using BlazorStrap;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation;
using OneLine.Bases;
using OneLine.Blazor.Extensions;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Models.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract partial class BlazorStrapDataViewComponentBase<T, TIdentifier, TId, THttpService> :
        BlazorDataViewComponentBase<T, TIdentifier, TId, THttpService>,
        IBlazorStrapDataViewComponent<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public bool ShowActivityIndicator { get; set; }
        public override async Task OnAfterFirstRenderAsync()
        {
            HttpService.HttpClient = HttpClient;
            IsMobile = await BlazorCurrentDeviceService.Mobile();
            IsTablet = await BlazorCurrentDeviceService.Tablet();
            IsDesktop = await BlazorCurrentDeviceService.Desktop();
            //This null check allows to prevent override the listeners from parent if it's listening to any of this events
            OnBeforeSearch ??= new Action(async () => await BeforeSearch());
            OnAfterSearch ??= new Action(async () => await AfterSearch());
            if (AutoLoad)
            {
                await Load();
            }
            if (InitialAutoSearch)
            {
                if (OnBeforeSearch.IsNotNull())
                {
                    OnBeforeSearch.Invoke();
                }
                else
                {
                    await Search();
                }
            }
            StateHasChanged();
        }
        public virtual async Task BeforeSearch()
        {
            Paging.PageIndex = 0;
            if (IsDesktop)
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Search()), Resourcer.GetString("ProcessingRequest"), Resourcer.GetString("PleaseWait"));
                StateHasChanged();
            }
            else
            {
                ShowActivityIndicator = true;
                await Search();
            }
        }
        public virtual async Task AfterSearch()
        {
            CollectionAppendReplaceMode = CollectionAppendReplaceMode.Replace;
            if (IsDesktop)
            {
                await SweetAlertService.HideLoaderAsync();
            }
            if (ResponsePaged.IsNull())
            {
                await SweetAlertService.FireAsync(Resourcer.GetString("UnknownErrorOccurred"), Resourcer.GetString("TheServerResponseIsNull"), SweetAlertIcon.Warning);
            }
            else if (ResponsePaged.IsNotNull() &&
                    ResponsePaged.Succeed &&
                    ResponsePaged.Response.IsNotNull() &&
                    ResponsePaged.Response.Status.Succeeded() &&
                    ResponsePaged.HttpResponseMessage.IsNotNull() &&
                    ResponsePaged.HttpResponseMessage.IsSuccessStatusCode)
            {
                if (!IsDesktop && ShowActivityIndicator)
                {
                    ShowActivityIndicator = false;
                    StateHasChanged();
                }
            }
            else if (ResponsePaged.HttpResponseMessage.IsNotNull() && ResponsePaged.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(Resourcer.GetString("SessionExpired"), Resourcer.GetString("YourSessionHasExpiredPleaseLoginInBackAgain"), SweetAlertIcon.Warning);
                await ApplicationState<AspNetUsersViewModel>.LogoutAndNavigateTo("/login");
            }
            else if (ResponsePaged.HasException)
            {
                await SweetAlertService.FireAsync(null, ResponsePaged.Exception.Message, SweetAlertIcon.Error);
            }
            else
            {
                await SweetAlertService.FireAsync(null, Resourcer.GetString(ResponsePaged.Response.Message), SweetAlertIcon.Error);
            }
        }
        public virtual async Task<IEnumerable<T>> TypeaheadSearch(string searchTerm)
        {
            SearchPaging.SearchTerm = searchTerm;
            await Search();
            return Records;
        }
        public virtual async Task LoadMore()
        {
            CollectionAppendReplaceMode = CollectionAppendReplaceMode.Add;
            await GoNextPage();
            await BeforeSearch();
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
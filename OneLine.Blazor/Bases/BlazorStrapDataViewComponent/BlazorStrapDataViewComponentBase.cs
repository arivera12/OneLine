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
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract partial class BlazorStrapDataViewComponentBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        BlazorDataViewComponentBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs>,
        IBlazorStrapDataViewComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public bool ShowActivityIndicator { get; set; }
        public override async Task OnAfterFirstRenderAsync()
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
            if (IsDesktop)
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
            if (ResponsePaged.IsNull())
            {
                await SweetAlertService.FireAsync(Resourcer.GetString("UnknownErrorOccurred"), Resourcer.GetString("TheServerResponseIsNull"), SweetAlertIcon.Warning);
            }
            else if (ResponsePaged.HttpResponseMessage.IsNotNull() && ResponsePaged.Succeed && 
                ResponsePaged.Response.Status.Succeeded() && ResponsePaged.HttpResponseMessage.IsSuccessStatusCode)
            {
                if (IsDesktop)
                {
                    await SweetAlertService.HideLoaderAsync();
                }
                else
                {
                    ShowActivityIndicator = false;
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
        public virtual async Task LoadMore()
        {
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
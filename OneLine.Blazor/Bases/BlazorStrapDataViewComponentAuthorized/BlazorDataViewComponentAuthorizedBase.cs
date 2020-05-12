using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Models.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract partial class BlazorDataViewComponentAuthorizedBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        DataViewBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs>,
        IBlazorDataViewComponentAuthorized<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
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
        [Inject] public virtual BlazorDownloadFileService BlazorDownloadFileService { get; set; }
        [Inject] public virtual SweetAlertService SweetAlertService { get; set; }
        [Inject] public virtual HttpClient HttpClient { get; set; }
        [Parameter] public override TIdentifier Identifier { get; set; }
        [Parameter] public override IEnumerable<TIdentifier> Identifiers { get; set; }
        [Parameter] public override T Record { get; set; }
        [Parameter] public override ObservableRangeCollection<T> Records { get; set; }
        [Parameter] public override object SearchExtraParams { get; set; }
        [Parameter] public override Func<T, bool> FilterPredicate { get; set; }
        [Parameter] public override string FilterSortBy { get; set; }
        [Parameter] public override bool FilterDescending { get; set; }
        [Parameter] public override ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<T>> Response { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>> ResponsePaged { get; set; }
        [Parameter] public override ISearchPaging SearchPaging { get; set; }
        [Parameter] public override RecordsSelectionMode RecordsSelectionMode { get; set; }
        [Parameter] public override CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        [Parameter] public override T SelectedRecord { get; set; }
        [Parameter] public override ObservableRangeCollection<T> SelectedRecords { get; set; }
        [Parameter] public override long MinimunRecordsSelections { get; set; }
        [Parameter] public override long MaximumRecordsSelections { get; set; }
        [Parameter] public override bool MinimunRecordsSelectionsReached { get; set; }
        [Parameter] public override bool MaximumRecordsSelectionsReached { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> ResponseChanged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IPaged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
        [Parameter] public override Action<Action> OnBeforeSearch { get; set; }
        [Parameter] public override Action OnAfterSearch { get; set; }
        [Parameter] public override Action<TIdentifier> IdentifierChanged { get; set; }
        [Parameter] public override Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        [Parameter] public override Action<T> RecordChanged { get; set; }
        [Parameter] public override Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        [Parameter] public override Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
        [Parameter] public override Action<T> SelectedRecordChanged { get; set; }
        [Parameter] public override Action<IEnumerable<T>, bool, bool> SelectedRecordsChanged { get; set; }
        [Parameter] public override Action<bool> MinimunRecordsSelectionsReachedChanged { get; set; }
        [Parameter] public override Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        [Parameter] public override Action<ISearchPaging> SearchPagingChanged { get; set; }
        [Parameter] public override Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        [Parameter] public override Action<string> FilterSortByChanged { get; set; }
        [Parameter] public override Action<bool> FilterDescendingChanged { get; set; }
        [Parameter] public virtual IEnumerable<string> AuthorizedRoles { get; set; }
        public bool IsDesktop { get; set; }
        public bool IsTablet { get; set; }
        public bool IsMobile { get; set; }
        public AspNetUsersViewModel User { get; set; }
        public virtual async Task OnAfterFirstRenderAsync()
        {
            HttpService.HttpClient = HttpClient;
            User = await ApplicationState<AspNetUsersViewModel>.GetApplicationUserSecure();
            if (!await ApplicationState<AspNetUsersViewModel>.IsLoggedInSecure() || User == null ||
                (!AuthorizedRoles.IsNullOrEmpty() && !AuthorizedRoles.Any(w => User.Roles.Contains(w))))
            {
                await ApplicationState<AspNetUsersViewModel>.Logout();
                NavigationManager.NavigateTo($@"/login/{NavigationManager.Uri.Split().Last()}");
            }
            else
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
        public virtual async Task PagingChange(IPaging paging)
        {
            SearchPaging.AutoMap(paging);
            await Search();
        }
    }
}

using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OneLine.Bases;
using OneLine.Blazor.Extensions;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Models.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract partial class BlazorCoreViewComponentBase<T, TIdentifier, TId, THttpService> :
        CoreViewBase<T, TIdentifier, TId, THttpService>,
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
        [Inject] public override THttpService HttpService { get; set; }
        [Parameter] public override TIdentifier Identifier { get; set; }
        [Parameter] public override IEnumerable<TIdentifier> Identifiers { get; set; }
        [Parameter] public override T Record { get; set; }
        [Parameter] public override ObservableRangeCollection<T> Records { get; set; }
        [Parameter] public override IValidator Validator { get; set; }
        [Parameter] public override ValidationResult ValidationResult { get; set; }
        [Parameter] public override bool IsValidModelState { get; set; }
        [Parameter] public override FormState FormState { get; set; }
        [Parameter] public override FormMode FormMode { get; set; }
        [Parameter] public override bool AutoLoad { get; set; }
        [Parameter] public override bool AllowDuplicates { get; set; }
        [Parameter] public override bool InitialAutoSearch { get; set; }
        [Parameter] public override object SearchExtraParams { get; set; }
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
        [Parameter] public override long MinimumRecordsSelections { get; set; }
        [Parameter] public override long MaximumRecordsSelections { get; set; }
        [Parameter] public override bool MinimumRecordsSelectionsReached { get; set; }
        [Parameter] public override bool MaximumRecordsSelectionsReached { get; set; }
        [Parameter] public virtual int DebounceInterval { get; set; }
        [Parameter] public virtual bool HideCancelOrBackButton { get; set; }
        [Parameter] public virtual bool HideResetButton { get; set; }
        [Parameter] public virtual bool HideSaveButton { get; set; }
        [Parameter] public virtual bool HideDeleteButton { get; set; }
        [Parameter] public virtual bool HideCreateOrNewButton { get; set; }
        [Parameter] public virtual string RecordId { get; set; }
        [Parameter] public virtual string RedirectUrl { get; set; }
        [Parameter] public virtual bool ShowOptionsDialog { get; set; }
        [Parameter] public virtual bool HideDetailsDialogOption { get; set; }
        [Parameter] public virtual bool HideCopyDialogOption { get; set; }
        [Parameter] public virtual bool HideEditDialogOption { get; set; }
        [Parameter] public virtual bool HideDeleteDialogOption { get; set; }
        [Parameter] public virtual bool ShowForm { get; set; }
        [Parameter] public virtual bool Hide { get; set; }
        [Parameter] public virtual bool Hidden { get; set; }
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
        [Parameter] public override Action<ObservableRangeCollection<T>> SelectedRecordsChanged { get; set; }
        [Parameter] public override Action<bool> MinimumRecordsSelectionsReachedChanged { get; set; }
        [Parameter] public override Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        [Parameter] public override Action<IPaging> PagingChanged { get; set; }
        [Parameter] public override Action<ISearchPaging> SearchPagingChanged { get; set; }
        [Parameter] public override Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        [Parameter] public override Action<string> FilterSortByChanged { get; set; }
        [Parameter] public override Action<bool> FilterDescendingChanged { get; set; }
        [Parameter] public override Action<ValidationResult> ValidationResultChanged { get; set; }
        [Parameter] public override Action<bool> IsValidModelStateChanged { get; set; }
        [Parameter] public override Action<FormState> FormStateChanged { get; set; }
        [Parameter] public override Action<FormMode> FormModeChanged { get; set; }
        [Parameter] public override Action OnBeforeReset { get; set; }
        [Parameter] public override Action OnAfterReset { get; set; }
        [Parameter] public override Action OnBeforeCancel { get; set; }
        [Parameter] public override Action OnAfterCancel { get; set; }
        [Parameter] public override Action OnBeforeSave { get; set; }
        [Parameter] public override Action OnAfterSave { get; set; }
        [Parameter] public override Action OnBeforeDelete { get; set; }
        [Parameter] public override Action OnAfterDelete { get; set; }
        [Parameter] public override Action OnBeforeValidate { get; set; }
        [Parameter] public override Action OnAfterValidate { get; set; }
        [Parameter] public virtual Action<bool> ShowOptionsDialogChanged { get; set; }
        [Parameter] public virtual Action<bool> ShowFormChanged { get; set; }
        public bool IsChained { get { return !string.IsNullOrWhiteSpace(RedirectUrl); } }
        public bool ShowActivityIndicator { get; set; }
        public bool IsDesktop { get; set; }
        public bool IsTablet { get; set; }
        public bool IsMobile { get; set; }
        public virtual async Task OnAfterFirstRenderAsync()
        {
            IsMobile = await BlazorCurrentDeviceService.Mobile();
            IsTablet = await BlazorCurrentDeviceService.Tablet();
            IsDesktop = await BlazorCurrentDeviceService.Desktop();
            OnBeforeSearch ??= new Action(async () => await BeforeSearch());
            OnAfterSearch ??= new Action(async () => await AfterSearch());
            OnBeforeSave ??= new Action(async () => await BeforeSave());
            OnAfterSave ??= new Action(async () => await AfterSave());
            OnBeforeDelete ??= new Action(async () => await BeforeDelete());
            OnAfterDelete ??= new Action(async () => await AfterDelete());
            OnBeforeCancel ??= new Action(async () => await BeforeCancel());
            OnAfterCancel ??= new Action(async () => await AfterCancel());
            OnBeforeReset ??= new Action(async () => await BeforeReset());
            OnAfterReset ??= new Action(async () => await AfterReset());
            if (!string.IsNullOrWhiteSpace(RecordId))
            {
                Identifier = new TIdentifier
                {
                    Model = (TId)Convert.ChangeType(RecordId, typeof(TId))
                };
            }
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
                await ApplicationState.LogoutAndNavigateTo("/login");
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
        public virtual string FormStateTitle()
        {
            if (FormState.IsCreate() || FormState.IsCopy())
                return "Create";
            else if (FormState.IsEdit())
                return "Edit";
            else if (FormState.IsDetails() || FormState.IsDelete() || FormState.IsDeleted())
                return "Details";
            else
                return "Create";
        }
        public virtual bool IsReadOnlyOrPlainText()
        {
            return FormState.IsDetails() || FormState.IsDelete() || FormState.IsDeleted();
        }
        public virtual async Task BeforeSave()
        {
            if (GetMutableBlobDatasWithRulesProperties().IsNotNullAndNotEmpty())
            {
                await ValidateMutableBlobDatas();
                if (!IsValidModelState)
                {
                    await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult);
                    return;
                }
            }
            await Validate();
            if (IsValidModelState && await SweetAlertService.ShowConfirmAlertAsync(title: Resourcer.GetString("Confirm"), text: Resourcer.GetString("AreYouSureYouWantToSaveTheRecord"),
                                                                                    confirmButtonText: Resourcer.GetString("Yes"), cancelButtonText: Resourcer.GetString("Cancel")))
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Save()), Resourcer.GetString("ProcessingRequest"), Resourcer.GetString("PleaseWait"));
            }
            else if (!IsValidModelState)
            {
                await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult);
            }
        }
        public virtual async Task InvalidSubmit()
        {
            await Validate();
            await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult);
        }
        public virtual async Task AfterSave()
        {
            await SweetAlertService.HideLoaderAsync();
            if (Response.IsNull())
            {
                await SweetAlertService.FireAsync(Resourcer.GetString("UnknownErrorOccurred"), Resourcer.GetString("TheServerResponseIsNull"), SweetAlertIcon.Warning);
            }
            else if (Response.IsNotNull() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(Resourcer.GetString("SessionExpired"), Resourcer.GetString("YourSessionHasExpiredPleaseLoginInBackAgain"), SweetAlertIcon.Warning);
                await ApplicationState.LogoutAndNavigateTo("/login");
            }
            else if (Response.IsNotNull() &&
                    Response.Succeed &&
                    Response.Response.IsNotNull() &&
                    Response.Response.Status.Succeeded() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.IsSuccessStatusCode)
            {
                //TODO: Parameters!!!!
                if (IsChained)
                {
                    NavigationManager.NavigateTo(RedirectUrl);
                }
                else
                {
                    StateHasChanged();
                    await SweetAlertService.FireAsync(null, Resourcer.GetString(Response.Response.Message), SweetAlertIcon.Success);
                }
            }
            else if (Response.IsNotNull() &&
                    Response.HasException)
            {
                await SweetAlertService.FireAsync(null, Response.Exception.Message, SweetAlertIcon.Error);
            }
            else
            {
                await SweetAlertService.FireAsync(null, Resourcer.GetString(Response.Response?.Message), SweetAlertIcon.Error);
            }
        }
        public virtual async Task BeforeDelete()
        {
            if (Identifier.IsNotNull() && Identifier.Model.IsNotNull() && await SweetAlertService.ShowConfirmAlertAsync(title: Resourcer.GetString("Confirm"), text: Resourcer.GetString("AreYouSureYouWantToDeleteTheRecord"),
                                                                                    confirmButtonText: Resourcer.GetString("Yes"), cancelButtonText: Resourcer.GetString("Cancel")))
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Delete()), Resourcer.GetString("ProcessingRequest"), Resourcer.GetString("PleaseWait"));
            }
        }
        public virtual async Task AfterDelete()
        {
            await SweetAlertService.HideLoaderAsync();
            if (Response.IsNull())
            {
                await SweetAlertService.FireAsync(Resourcer.GetString("UnknownErrorOccurred"), Resourcer.GetString("TheServerResponseIsNull"), SweetAlertIcon.Warning);
            }
            else if (Response.IsNotNull() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(Resourcer.GetString("SessionExpired"), Resourcer.GetString("YourSessionHasExpiredPleaseLoginInBackAgain"), SweetAlertIcon.Warning);
                await ApplicationState.LogoutAndNavigateTo("/login");
            }
            else if (Response.IsNotNull() &&
                    Response.Succeed &&
                    Response.Response.IsNotNull() &&
                    Response.Response.Status.Succeeded() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.IsSuccessStatusCode)
            {
                StateHasChanged();
                await SweetAlertService.FireAsync(null, Resourcer.GetString(Response.Response.Message), SweetAlertIcon.Success);
            }
            else if (Response.IsNotNull() &&
                    Response.HasException)
            {
                await SweetAlertService.FireAsync(null, Response.Exception.Message, SweetAlertIcon.Error);
            }
            else
            {
                await SweetAlertService.FireAsync(null, Resourcer.GetString(Response.Response?.Message), SweetAlertIcon.Error);
            }
        }
        public virtual async Task BeforeCancel()
        {
            var text = IsChained ? "AreYouSureYouWantToGoBack" : "AreYouSureYouWantToCancel";
            if (await SweetAlertService.ShowConfirmAlertAsync(title: Resourcer.GetString("Confirm"), text: Resourcer.GetString(text),
                                                                confirmButtonText: Resourcer.GetString("Yes"), cancelButtonText: Resourcer.GetString("Cancel")))
            {
                await Cancel();
            }
        }
        public virtual async Task AfterCancel()
        {
            await Reset();
            await JSRuntime.InvokeVoidAsync("window.history.back");
        }
        public virtual async Task BeforeReset()
        {
            if (await SweetAlertService.ShowConfirmAlertAsync(title: Resourcer.GetString("Confirm"), text: Resourcer.GetString("AreYouSureYouWantToReset"),
                                                                confirmButtonText: Resourcer.GetString("Yes"), cancelButtonText: Resourcer.GetString("Cancel")))
            {
                await Reset();
            }
        }
        public virtual Task AfterReset()
        {
            StateHasChanged();
            return Task.CompletedTask;
        }
        public virtual Task ShowFormChangeFormState(FormState formState)
        {
            ShowForm = true;
            ShowFormChanged?.Invoke(ShowForm);
            FormState = formState;
            FormStateChanged?.Invoke(FormState);
            return Task.CompletedTask;
        }
        public virtual void HideFormAfterFormCancel()
        {
            ShowForm = false;
            ShowFormChanged?.Invoke(ShowForm);
            FormState = FormState.Create;
            FormStateChanged?.Invoke(FormState);
            StateHasChanged();
        }
        public virtual Task HideOptionsDialog()
        {
            ShowOptionsDialog = false;
            ShowOptionsDialogChanged?.Invoke(ShowOptionsDialog);
            return Task.CompletedTask;
        }
        public virtual async Task ShowFormChangeFormStateHideOptionsDialog(FormState formState)
        {
            await ShowFormChangeFormState(formState);
            await HideOptionsDialog();
            AfterSelectedRecord?.Invoke();
        }
    }
}

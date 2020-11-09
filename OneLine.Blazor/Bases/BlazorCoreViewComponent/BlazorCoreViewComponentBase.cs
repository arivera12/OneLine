using CurrieTechnologies.Razor.SweetAlert2;
using DeviceDetectorNET;
using FluentValidation;
using FluentValidation.Results;
using JsonLanguageLocalizerNet;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OneLine.Bases;
using OneLine.Blazor.Extensions;
using OneLine.Blazor.Services;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        /// <inheritdoc/>
        [Inject] public override IConfiguration Configuration { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual IJSRuntime JSRuntime { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual ISaveFile SaveFile { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual SweetAlertService SweetAlertService { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual HttpClient HttpClient { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual IApplicationState ApplicationState { get; set; }
        /// <inheritdoc/>
        [Inject] public override THttpService HttpService { get; set; }
        /// <inheritdoc/>
        [Parameter] public override TIdentifier Identifier { get; set; }
        /// <inheritdoc/>
        [Parameter] public override IEnumerable<TIdentifier> Identifiers { get; set; }
        /// <inheritdoc/>
        [Parameter] public override T Record { get; set; }
        /// <inheritdoc/>
        [Parameter] public override ObservableRangeCollection<T> Records { get; set; }
        /// <inheritdoc/>
        [Parameter] public override IValidator Validator { get; set; }
        /// <inheritdoc/>
        [Parameter] public override ValidationResult ValidationResult { get; set; }
        /// <inheritdoc/>
        [Parameter] public override bool IsValidModelState { get; set; }
        /// <inheritdoc/>
        [Parameter] public override FormState FormState { get; set; }
        /// <inheritdoc/>
        [Parameter] public override FormMode FormMode { get; set; }
        /// <inheritdoc/>
        [Parameter] public override bool AutoLoad { get; set; }
        /// <inheritdoc/>
        [Parameter] public override bool AllowDuplicates { get; set; }
        /// <inheritdoc/>
        [Parameter] public override bool InitialAutoSearch { get; set; }
        /// <inheritdoc/>
        [Parameter] public override object SearchExtraParams { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Func<T, bool> FilterPredicate { get; set; }
        /// <inheritdoc/>
        [Parameter] public override string FilterSortBy { get; set; }
        /// <inheritdoc/>
        [Parameter] public override bool FilterDescending { get; set; }
        /// <inheritdoc/>
        [Parameter] public override ObservableRangeCollection<T> RecordsFilteredSorted { get; set; }
        /// <inheritdoc/>
        [Parameter] public override IResponseResult<ApiResponse<T>> Response { get; set; }
        /// <inheritdoc/>
        [Parameter] public override IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        /// <inheritdoc/>
        [Parameter] public override IResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override IPaging Paging { get; set; }
        /// <inheritdoc/>
        [Parameter] public override ISearchPaging SearchPaging { get; set; }
        /// <inheritdoc/>
        [Parameter] public override RecordsSelectionMode RecordsSelectionMode { get; set; }
        /// <inheritdoc/>
        [Parameter] public override CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        /// <inheritdoc/>
        [Parameter] public override T SelectedRecord { get; set; }
        /// <inheritdoc/>
        [Parameter] public override ObservableRangeCollection<T> SelectedRecords { get; set; }
        /// <inheritdoc/>
        [Parameter] public override long MinimumRecordsSelections { get; set; }
        /// <inheritdoc/>
        [Parameter] public override long MaximumRecordsSelections { get; set; }
        /// <inheritdoc/>
        [Parameter] public override bool MinimumRecordsSelectionsReached { get; set; }
        /// <inheritdoc/>
        [Parameter] public override bool MaximumRecordsSelectionsReached { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual int DebounceInterval { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideCancelOrBackButton { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideResetButton { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideSaveButton { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideDeleteButton { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideCreateOrNewButton { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual string RecordId { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual string RedirectUrlAfterSave { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool ShowOptionsDialog { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideDetailsDialogOption { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideCopyDialogOption { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideEditDialogOption { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool HideDeleteDialogOption { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool ShowForm { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool Hide { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool Hidden { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool ReadOnly { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool Disabled { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool EnableConfirmOnSave { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool EnableConfirmOnReset { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool EnableConfirmOnDelete { get; set; } = true;
        /// <inheritdoc/>
        [Parameter] public virtual bool EnableConfirmOnCancel { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool CloseFormAfterSaveOrDelete { get; set; } = true;
        /// <inheritdoc/>
        [Parameter] public virtual bool AutoSearchAfterFormClose { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool TriggerSearchMethod { get; set; }
        /// <inheritdoc/>
        [Parameter]
        public virtual bool TriggerSearch
        {
            set
            {
                if (value)
                {
                    new Action(async () => await BeforeSearch()).Invoke();
                }
                TriggerSearchChanged?.Invoke(false);
            }
            get { return false; }
        }
        /// <inheritdoc/>
        [Parameter] public override Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<IResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnBeforeSearch { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnAfterSearch { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<TIdentifier> IdentifierChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<T> RecordChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<T> SelectedRecordChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action BeforeSelectedRecord { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action AfterSelectedRecord { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<ObservableRangeCollection<T>> SelectedRecordsChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<bool> MinimumRecordsSelectionsReachedChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<IPaging> PagingChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<ISearchPaging> SearchPagingChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<string> FilterSortByChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<bool> FilterDescendingChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<ValidationResult> ValidationResultChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<bool> IsValidModelStateChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<FormState> FormStateChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action<FormMode> FormModeChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnBeforeLoad { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnAfterLoad { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnBeforeReset { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnAfterReset { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnBeforeCancel { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnAfterCancel { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnBeforeSave { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnAfterSave { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnBeforeDelete { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnAfterDelete { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnBeforeValidate { get; set; }
        /// <inheritdoc/>
        [Parameter] public override Action OnAfterValidate { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> ShowOptionsDialogChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> ShowFormChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> TriggerSearchChanged { get; set; }
        /// <inheritdoc/>
        public virtual DeviceDetector DeviceDetector { get; set; }
        /// <inheritdoc/>
        public bool ShowActivityIndicator { get; set; }
        /// <inheritdoc/>
        public bool IsDesktop { get; set; }
        /// <inheritdoc/>
        public bool IsTablet { get; set; }
        /// <inheritdoc/>
        public bool IsMobile { get; set; }
        /// <inheritdoc/>
        public virtual Task OnAfterFirstRenderAsync()
            => Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task BeforeSearch()
        {
            if (IsDesktop)
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Search()), LanguageLocalizer["ProcessingRequest"], LanguageLocalizer["PleaseWait"]);
                StateHasChanged();
            }
            else
            {
                ShowActivityIndicator = true;
                await Search();
            }
        }
        /// <inheritdoc/>
        public virtual async Task AfterSearch()
        {
            CollectionAppendReplaceMode = CollectionAppendReplaceMode.Replace;
            if (IsDesktop)
            {
                await SweetAlertService.HideLoaderAsync();
            }
            if (ResponsePaged.IsNull())
            {
                await SweetAlertService.FireAsync(LanguageLocalizer["UnknownErrorOccurred"], LanguageLocalizer["TheServerResponseIsNull"], SweetAlertIcon.Warning);
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
                await SweetAlertService.FireAsync(LanguageLocalizer["SessionExpired"], LanguageLocalizer["YourSessionHasExpiredPleaseLoginInBackAgain"], SweetAlertIcon.Warning);
                await ApplicationState.LogoutAndNavigateTo("/login");
            }
            else if (ResponsePaged.HasException)
            {
                await SweetAlertService.FireAsync(null, ResponsePaged.Exception.Message, SweetAlertIcon.Error);
            }
            else
            {
                await SweetAlertService.FireAsync(null, LanguageLocalizer[ResponsePaged.Response.Message], SweetAlertIcon.Error);
            }
        }
        /// <inheritdoc/>
        public virtual async Task<IEnumerable<T>> TypeaheadSearch(string searchTerm)
        {
            SearchPaging.SearchTerm = searchTerm;
            await Search();
            return Records;
        }
        /// <inheritdoc/>
        public virtual async Task LoadMore()
        {
            CollectionAppendReplaceMode = CollectionAppendReplaceMode.Add;
            await GoNextPage();
            await BeforeSearch();
        }
        /// <inheritdoc/>
        public virtual async Task PagingChange(IPaging paging)
        {
            SearchPaging.AutoMap(paging);
            await BeforeSearch();
        }
        /// <inheritdoc/>
        public virtual void SearchTermChanged(string searchTerm)
        {
            SearchPaging.SearchTerm = searchTerm;
            RateLimitingExtensionForObject.Debounce(SearchPaging, DebounceInterval, async (searchPagingDebounced) =>
            {
                await BeforeSearch();
            });
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public virtual bool IsReadOnlyOrPlainText()
        {
            return FormState.IsDetails() || FormState.IsDelete() || FormState.IsDeleted();
        }
        /// <inheritdoc/>
        public virtual async Task BeforeSave()
        {
            if (GetMutableBlobDatasWithRulesProperties().IsNotNull() && GetMutableBlobDatasWithRulesProperties().Any())
            {
                await ValidateMutableBlobDatas();
                if (!IsValidModelState)
                {
                    await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult, LanguageLocalizer);
                    return;
                }
            }
            await Validate();
            if (EnableConfirmOnSave && IsValidModelState && await SweetAlertService.ShowConfirmAlertAsync(title: LanguageLocalizer["Confirm"], text: LanguageLocalizer["AreYouSureYouWantToSaveTheRecord"],
                                                                                    confirmButtonText: LanguageLocalizer["Yes"], cancelButtonText: LanguageLocalizer["Cancel"]))
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Save()), LanguageLocalizer["ProcessingRequest"], LanguageLocalizer["PleaseWait"]);
            }
            else if (!EnableConfirmOnSave && IsValidModelState)
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Save()), LanguageLocalizer["ProcessingRequest"], LanguageLocalizer["PleaseWait"]);
            }
            else if (!IsValidModelState)
            {
                await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult, LanguageLocalizer);
            }
        }
        /// <inheritdoc/>
        public virtual async Task InvalidSubmit()
        {
            await Validate();
            await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult, LanguageLocalizer);
        }
        /// <inheritdoc/>
        public virtual async Task AfterSave()
        {
            await SweetAlertService.HideLoaderAsync();
            if (Response.IsNull())
            {
                await SweetAlertService.FireAsync(LanguageLocalizer["UnknownErrorOccurred"], LanguageLocalizer["TheServerResponseIsNull"], SweetAlertIcon.Warning);
            }
            else if (Response.IsNotNull() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(LanguageLocalizer["SessionExpired"], LanguageLocalizer["YourSessionHasExpiredPleaseLoginInBackAgain"], SweetAlertIcon.Warning);
                await ApplicationState.LogoutAndNavigateTo("/login");
            }
            else if (Response.IsNotNull() &&
                    Response.Succeed &&
                    Response.Response.IsNotNull() &&
                    Response.Response.Status.Succeeded() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.IsSuccessStatusCode)
            {
                if (!string.IsNullOrWhiteSpace(RedirectUrlAfterSave))
                {
                    NavigationManager.NavigateTo(RedirectUrlAfterSave);
                }
                else
                {
                    StateHasChanged();
                    await SweetAlertService.FireAsync(null, LanguageLocalizer[Response.Response.Message], SweetAlertIcon.Success);
                    if (CloseFormAfterSaveOrDelete)
                    {
                        OnAfterCancel?.Invoke();
                    }
                }
            }
            else if (Response.IsNotNull() &&
                    Response.HasException)
            {
                await SweetAlertService.FireAsync(null, Response.Exception.Message, SweetAlertIcon.Error);
            }
            else
            {
                await SweetAlertService.FireAsync(null, LanguageLocalizer[Response.Response?.Message], SweetAlertIcon.Error);
            }
        }
        /// <inheritdoc/>
        public virtual async Task BeforeDelete()
        {
            if (EnableConfirmOnDelete && Identifier.IsNotNull() && Identifier.Model.IsNotNull() && await SweetAlertService.ShowConfirmAlertAsync(title: LanguageLocalizer["Confirm"], text: LanguageLocalizer["AreYouSureYouWantToDeleteTheRecord"],
                                                                                    confirmButtonText: LanguageLocalizer["Yes"], cancelButtonText: LanguageLocalizer["Cancel"]))
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Delete()), LanguageLocalizer["ProcessingRequest"], LanguageLocalizer["PleaseWait"]);
            }
            else if (!EnableConfirmOnDelete && Identifier.IsNotNull() && Identifier.Model.IsNotNull())
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Delete()), LanguageLocalizer["ProcessingRequest"], LanguageLocalizer["PleaseWait"]);
            }
        }
        /// <inheritdoc/>
        public virtual async Task AfterDelete()
        {
            await SweetAlertService.HideLoaderAsync();
            if (Response.IsNull())
            {
                await SweetAlertService.FireAsync(LanguageLocalizer["UnknownErrorOccurred"], LanguageLocalizer["TheServerResponseIsNull"], SweetAlertIcon.Warning);
            }
            else if (Response.IsNotNull() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(LanguageLocalizer["SessionExpired"], LanguageLocalizer["YourSessionHasExpiredPleaseLoginInBackAgain"], SweetAlertIcon.Warning);
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
                await SweetAlertService.FireAsync(null, LanguageLocalizer[Response.Response.Message], SweetAlertIcon.Success);
                if (CloseFormAfterSaveOrDelete)
                {
                    OnAfterCancel?.Invoke();
                }
            }
            else if (Response.IsNotNull() &&
                    Response.HasException)
            {
                await SweetAlertService.FireAsync(null, Response.Exception.Message, SweetAlertIcon.Error);
            }
            else
            {
                await SweetAlertService.FireAsync(null, LanguageLocalizer[Response.Response?.Message], SweetAlertIcon.Error);
            }
        }
        /// <inheritdoc/>
        public virtual async Task BeforeCancel()
        {
            var text = !string.IsNullOrWhiteSpace(RedirectUrlAfterSave) ? "AreYouSureYouWantToGoBack" : "AreYouSureYouWantToCancel";
            if (EnableConfirmOnCancel && await SweetAlertService.ShowConfirmAlertAsync(title: LanguageLocalizer["Confirm"], text: LanguageLocalizer[text],
                                                                confirmButtonText: LanguageLocalizer["Yes"], cancelButtonText: LanguageLocalizer["Cancel"]))
            {
                await Cancel();
            }
            else if (!EnableConfirmOnCancel)
            {
                await Cancel();
            }
        }
        /// <inheritdoc/>
        public virtual async Task AfterCancel()
        {
            await Reset();
            await JSRuntime.InvokeVoidAsync("window.history.back");
        }
        /// <inheritdoc/>
        public virtual async Task BeforeReset()
        {
            if (EnableConfirmOnReset && await SweetAlertService.ShowConfirmAlertAsync(title: LanguageLocalizer["Confirm"], text: LanguageLocalizer["AreYouSureYouWantToReset"],
                                                                confirmButtonText: LanguageLocalizer["Yes"], cancelButtonText: LanguageLocalizer["Cancel"]))
            {
                await Reset();
            }
            else if (!EnableConfirmOnReset)
            {
                await Reset();
            }
        }
        /// <inheritdoc/>
        public virtual Task AfterReset()
        {
            StateHasChanged();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task ShowFormChangeFormState(FormState formState)
        {
            ShowForm = true;
            ShowFormChanged?.Invoke(ShowForm);
            FormState = formState;
            FormStateChanged?.Invoke(FormState);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual void HideFormAfterFormCancel()
        {
            ShowForm = false;
            ShowFormChanged?.Invoke(ShowForm);
            StateHasChanged();
            if (RecordsSelectionMode.IsSingle())
            {
                SelectedRecord = null;
            }
            else
            {
                SelectedRecords = null;
            }
            FormState = FormState.Create;
            FormStateChanged?.Invoke(FormState);
            if (AutoSearchAfterFormClose)
            {
                TriggerSearchMethod = true;
                StateHasChanged();
            }
        }
        /// <inheritdoc/>
        public virtual Task HideOptionsDialog()
        {
            ShowOptionsDialog = false;
            ShowOptionsDialogChanged?.Invoke(ShowOptionsDialog);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual async Task ShowFormChangeFormStateHideOptionsDialog(FormState formState)
        {
            await ShowFormChangeFormState(formState);
            await HideOptionsDialog();
            AfterSelectedRecord?.Invoke();
        }
    }
}

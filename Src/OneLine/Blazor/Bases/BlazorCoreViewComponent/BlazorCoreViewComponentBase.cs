using CurrieTechnologies.Razor.SweetAlert2;
using DeviceDetectorNET;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneLine.Blazor.Contracts;
using OneLine.Blazor.Extensions;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Services;
using OneLine.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public class BlazorCoreViewComponentBase<T, TIdentifier, TId, THttpService> : ComponentBase,
        IBlazorDataViewComponent<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        /// <inheritdoc/>
        [Inject] public IJSRuntime JSRuntime { get; set; }
        /// <inheritdoc/>
        [Inject] public NavigationManager NavigationManager { get; set; }
        /// <inheritdoc/>
        [Inject] public SweetAlertService SweetAlertService { get; set; }
        /// <inheritdoc/>
        [Inject] public HttpClient HttpClient { get; set; }
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
        [Parameter] public virtual Action<bool> ShowOptionsDialogChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> ShowFormChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> TriggerSearchChanged { get; set; }
        /// <inheritdoc/>
        public virtual DeviceDetector DeviceDetector { get; set; }
        /// <inheritdoc/>
        public virtual bool ShowActivityIndicator { get; set; }
        /// <inheritdoc/>
        [Inject] public IDevice Device { get; set; }
        /// <inheritdoc/>
        [Inject] public IDeviceStorage DeviceStorage { get; set; }
        /// <inheritdoc/>
        [Inject] public ISaveFile SaveFile { get; set; }
        /// <inheritdoc/>
        [Inject] public IApplicationState ApplicationState { get; set; }
        /// <inheritdoc/>
        [Inject] public IResourceManagerLocalizer ResourceManagerLocalizer { get; set; }
        /// <inheritdoc/>
        [Inject] public IApplicationConfiguration ApplicationConfiguration { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual TIdentifier Identifier { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<TIdentifier> IdentifierChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual IEnumerable<TIdentifier> Identifiers { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual T Record { get; set; } = new T();
        /// <inheritdoc/>
        [Parameter] public virtual Action<T> RecordChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual ObservableRangeCollection<T> Records { get; set; } = new ObservableRangeCollection<T>();
        /// <inheritdoc/>
        [Parameter] public virtual Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool AllowDuplicates { get; set; }
        /// <inheritdoc/>
        [Inject] public virtual THttpService HttpService { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual object SearchExtraParams { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnBeforeSearch { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnAfterSearch { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool InitialAutoSearch { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Func<T, bool> FilterPredicate { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<Func<T, bool>> FilterPredicateChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual string FilterSortBy { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<string> FilterSortByChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool FilterDescending { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> FilterDescendingChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual ObservableRangeCollection<T> RecordsFilteredSorted { get; set; } = new ObservableRangeCollection<T>();
        /// <inheritdoc/>
        [Parameter] public virtual Action<ObservableRangeCollection<T>> RecordsFilteredSortedChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual RecordsSelectionMode RecordsSelectionMode { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual T SelectedRecord { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual ObservableRangeCollection<T> SelectedRecords { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual long MinimumRecordsSelections { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual long MaximumRecordsSelections { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool MinimumRecordsSelectionsReached { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool MaximumRecordsSelectionsReached { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action BeforeSelectedRecord { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action AfterSelectedRecord { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<T> SelectedRecordChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<ObservableRangeCollection<T>> SelectedRecordsChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> MinimumRecordsSelectionsReachedChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> MaximumRecordsSelectionsReachedChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual IPaging Paging { get; set; } = new Paging();
        /// <inheritdoc/>
        [Parameter] public virtual Action<IPaging> PagingChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual ISearchPaging SearchPaging { get; set; } = new SearchPaging();
        /// <inheritdoc/>
        [Parameter] public virtual Action<ISearchPaging> SearchPagingChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual IResponseResult<ApiResponse<T>> Response { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual IResponseResult<ApiResponse<Paged<IEnumerable<T>>>> ResponsePaged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<IResponseResult<ApiResponse<Paged<IEnumerable<T>>>>> ResponsePagedChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnBeforeLoad { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnAfterLoad { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool AutoLoad { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnBeforeSave { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnAfterSave { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual IValidator Validator { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual ValidationResult ValidationResult { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<ValidationResult> ValidationResultChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual bool IsValidModelState { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<bool> IsValidModelStateChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnBeforeValidate { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnAfterValidate { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual FormState FormState { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<FormState> FormStateChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual FormMode FormMode { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action<FormMode> FormModeChanged { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnBeforeDelete { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnAfterDelete { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnBeforeReset { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnAfterReset { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnBeforeCancel { get; set; }
        /// <inheritdoc/>
        [Parameter] public virtual Action OnAfterCancel { get; set; }
        /// <inheritdoc/>
        public bool FirstRenderOcurred { get; set; }
        public BlazorCoreViewComponentBase() : base()
        {
        }
        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                FirstRenderOcurred = firstRender;
                await InitializeComponentAsync();
            }
        }
        /// <inheritdoc/>
        public virtual async Task InitializeComponentAsync()
        {
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
            if (!string.IsNullOrWhiteSpace(RecordId) && (Identifier.IsNull() || Identifier.Model.IsNull()))
            {
                Identifier = new TIdentifier
                {
                    Model = (TId)Convert.ChangeType(RecordId, typeof(TId))
                };
            }
            if (AutoLoad)
            {
                if (OnBeforeLoad.IsNotNull())
                {
                    OnBeforeLoad.Invoke();
                }
                else
                {
                    await Load();
                }
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
        /// <inheritdoc/>
        public virtual async Task BeforeSearch()
        {
            if (Device.IsDesktop)
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Search()), ResourceManagerLocalizer["ProcessingRequest"], ResourceManagerLocalizer["PleaseWait"]);
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
            if (Device.IsDesktop)
            {
                await SweetAlertService.HideLoaderAsync();
            }
            if (ResponsePaged.IsNull())
            {
                await SweetAlertService.FireAsync(ResourceManagerLocalizer["UnknownErrorOccurred"], ResourceManagerLocalizer["TheServerResponseIsNull"], SweetAlertIcon.Warning);
            }
            else if (ResponsePaged.IsNotNull() &&
                    ResponsePaged.Succeed &&
                    ResponsePaged.Response.IsNotNull() &&
                    ResponsePaged.Response.Status.Succeeded() &&
                    ResponsePaged.HttpResponseMessage.IsNotNull() &&
                    ResponsePaged.HttpResponseMessage.IsSuccessStatusCode)
            {
                if (!Device.IsDesktop && ShowActivityIndicator)
                {
                    ShowActivityIndicator = false;
                    StateHasChanged();
                }
            }
            else if (ResponsePaged.HttpResponseMessage.IsNotNull() && ResponsePaged.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(ResourceManagerLocalizer["SessionExpired"], ResourceManagerLocalizer["YourSessionHasExpiredPleaseLoginInBackAgain"], SweetAlertIcon.Warning);
                await ApplicationState.LogoutAndNavigateTo("/login");
            }
            else if (ResponsePaged.HasException)
            {
                await SweetAlertService.FireAsync(null, ResponsePaged.Exception.Message, SweetAlertIcon.Error);
            }
            else
            {
                await SweetAlertService.FireAsync(null, ResourceManagerLocalizer[ResponsePaged.Response.Message], SweetAlertIcon.Error);
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
                    await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult, ResourceManagerLocalizer);
                    return;
                }
            }
            await Validate();
            if (EnableConfirmOnSave && IsValidModelState && await SweetAlertService.ShowConfirmAlertAsync(title: ResourceManagerLocalizer["Confirm"], text: ResourceManagerLocalizer["AreYouSureYouWantToSaveTheRecord"],
                                                                                    confirmButtonText: ResourceManagerLocalizer["Yes"], cancelButtonText: ResourceManagerLocalizer["Cancel"]))
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Save()), ResourceManagerLocalizer["ProcessingRequest"], ResourceManagerLocalizer["PleaseWait"]);
            }
            else if (!EnableConfirmOnSave && IsValidModelState)
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Save()), ResourceManagerLocalizer["ProcessingRequest"], ResourceManagerLocalizer["PleaseWait"]);
            }
            else if (!IsValidModelState)
            {
                await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult, ResourceManagerLocalizer);
            }
        }
        /// <inheritdoc/>
        public virtual async Task InvalidSubmit()
        {
            await Validate();
            await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult, ResourceManagerLocalizer);
        }
        /// <inheritdoc/>
        public virtual async Task AfterSave()
        {
            await SweetAlertService.HideLoaderAsync();
            if (Response.IsNull())
            {
                await SweetAlertService.FireAsync(ResourceManagerLocalizer["UnknownErrorOccurred"], ResourceManagerLocalizer["TheServerResponseIsNull"], SweetAlertIcon.Warning);
            }
            else if (Response.IsNotNull() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(ResourceManagerLocalizer["SessionExpired"], ResourceManagerLocalizer["YourSessionHasExpiredPleaseLoginInBackAgain"], SweetAlertIcon.Warning);
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
                    await SweetAlertService.FireAsync(null, ResourceManagerLocalizer[Response.Response.Message], SweetAlertIcon.Success);
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
                await SweetAlertService.FireAsync(null, ResourceManagerLocalizer[Response.Response?.Message], SweetAlertIcon.Error);
            }
        }
        /// <inheritdoc/>
        public virtual async Task BeforeDelete()
        {
            if (EnableConfirmOnDelete && Identifier.IsNotNull() && Identifier.Model.IsNotNull() && await SweetAlertService.ShowConfirmAlertAsync(title: ResourceManagerLocalizer["Confirm"], text: ResourceManagerLocalizer["AreYouSureYouWantToDeleteTheRecord"],
                                                                                    confirmButtonText: ResourceManagerLocalizer["Yes"], cancelButtonText: ResourceManagerLocalizer["Cancel"]))
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Delete()), ResourceManagerLocalizer["ProcessingRequest"], ResourceManagerLocalizer["PleaseWait"]);
            }
            else if (!EnableConfirmOnDelete && Identifier.IsNotNull() && Identifier.Model.IsNotNull())
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Delete()), ResourceManagerLocalizer["ProcessingRequest"], ResourceManagerLocalizer["PleaseWait"]);
            }
        }
        /// <inheritdoc/>
        public virtual async Task AfterDelete()
        {
            await SweetAlertService.HideLoaderAsync();
            if (Response.IsNull())
            {
                await SweetAlertService.FireAsync(ResourceManagerLocalizer["UnknownErrorOccurred"], ResourceManagerLocalizer["TheServerResponseIsNull"], SweetAlertIcon.Warning);
            }
            else if (Response.IsNotNull() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(ResourceManagerLocalizer["SessionExpired"], ResourceManagerLocalizer["YourSessionHasExpiredPleaseLoginInBackAgain"], SweetAlertIcon.Warning);
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
                await SweetAlertService.FireAsync(null, ResourceManagerLocalizer[Response.Response.Message], SweetAlertIcon.Success);
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
                await SweetAlertService.FireAsync(null, ResourceManagerLocalizer[Response.Response?.Message], SweetAlertIcon.Error);
            }
        }
        /// <inheritdoc/>
        public virtual async Task BeforeCancel()
        {
            var text = !string.IsNullOrWhiteSpace(RedirectUrlAfterSave) ? "AreYouSureYouWantToGoBack" : "AreYouSureYouWantToCancel";
            if (EnableConfirmOnCancel && await SweetAlertService.ShowConfirmAlertAsync(title: ResourceManagerLocalizer["Confirm"], text: ResourceManagerLocalizer[text],
                                                                confirmButtonText: ResourceManagerLocalizer["Yes"], cancelButtonText: ResourceManagerLocalizer["Cancel"]))
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
            if (EnableConfirmOnReset && await SweetAlertService.ShowConfirmAlertAsync(title: ResourceManagerLocalizer["Confirm"], text: ResourceManagerLocalizer["AreYouSureYouWantToReset"],
                                                                confirmButtonText: ResourceManagerLocalizer["Yes"], cancelButtonText: ResourceManagerLocalizer["Cancel"]))
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
        /// <inheritdoc/>
        public virtual async Task Load()
        {
            if (Identifier.IsNotNull() && Identifier.Model.IsNotNull())
            {
                Response = await HttpService.GetOneAsync<T>(Identifier, new EmptyValidator());
                ResponseChanged?.Invoke(Response);
                if (Response.IsNotNull() &&
                    Response.Succeed &&
                    Response.Response.IsNotNull() &&
                    Response.Response.Status.Succeeded() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.IsSuccessStatusCode)
                {
                    Record = Response.Response.Data;
                    RecordChanged?.Invoke(Record);
                    await SelectRecord(Record);
                    FormStateChanged?.Invoke(FormState);
                }
                OnAfterLoad?.Invoke();
            }
            else if (Identifiers.IsNotNull() && Identifiers.Any())
            {
                ResponseCollection = await HttpService.GetRangeAsync<T>(Identifiers, new EmptyValidator());
                ResponseCollectionChanged?.Invoke(ResponseCollection);
                if (ResponseCollection.IsNotNull() &&
                    ResponseCollection.Succeed &&
                    ResponseCollection.Response.IsNotNull() &&
                    ResponseCollection.Response.Status.Succeeded() &&
                    ResponseCollection.HttpResponseMessage.IsNotNull() &&
                    ResponseCollection.HttpResponseMessage.IsSuccessStatusCode)
                {
                    if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Replace)
                    {
                        Records.ReplaceRange(ResponseCollection.Response.Data);
                        RecordsFilteredSorted.ReplaceRange(Records);
                    }
                    else if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Add)
                    {
                        Records.AddRange(ResponseCollection.Response.Data);
                        RecordsFilteredSorted.AddRange(Records);
                    }
                    RecordsChanged?.Invoke(Records);
                    RecordsFilteredSortedChanged?.Invoke(RecordsFilteredSorted);
                    await SelectRecords(Records);
                    FormStateChanged?.Invoke(FormState);
                }
                OnAfterLoad?.Invoke();
            }
        }
        /// <inheritdoc/>
        public virtual async Task Search()
        {
            ResponsePaged = await HttpService.SearchAsync<T>(SearchPaging, SearchExtraParams);
            ResponsePagedChanged?.Invoke(ResponsePaged);
            if (ResponsePaged.IsNotNull() &&
                ResponsePaged.Succeed &&
                ResponsePaged.Response.IsNotNull() &&
                ResponsePaged.Response.Status.Succeeded() &&
                ResponsePaged.HttpResponseMessage.IsNotNull() &&
                ResponsePaged.HttpResponseMessage.IsSuccessStatusCode)
            {
                if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Replace)
                {
                    if (Records.IsNull() || RecordsFilteredSorted.IsNull())
                    {
                        Records = new ObservableRangeCollection<T>(ResponsePaged.Response.Data.Data);
                        RecordsFilteredSorted = new ObservableRangeCollection<T>(ResponsePaged.Response.Data.Data);
                    }
                    else
                    {
                        Records.ReplaceRange(ResponsePaged.Response.Data.Data);
                        RecordsFilteredSorted.ReplaceRange(Records);
                    }
                }
                else if (CollectionAppendReplaceMode == CollectionAppendReplaceMode.Add)
                {
                    if (Records.IsNull() || RecordsFilteredSorted.IsNull())
                    {
                        Records = new ObservableRangeCollection<T>(ResponsePaged.Response.Data.Data);
                        RecordsFilteredSorted = new ObservableRangeCollection<T>(ResponsePaged.Response.Data.Data);
                    }
                    else
                    {
                        Records.AddRange(ResponsePaged.Response.Data.Data);
                        RecordsFilteredSorted.AddRange(Records);
                    }
                }
                RecordsChanged?.Invoke(Records);
                RecordsFilteredSortedChanged?.Invoke(RecordsFilteredSorted);
            }
            OnAfterSearch?.Invoke();
        }
        /// <inheritdoc/>
        public virtual Task SelectRecord(T selectedRecord)
        {
            if (RecordsSelectionMode.IsSingle())
            {
                SelectedRecord = selectedRecord;
                SelectedRecordChanged?.Invoke(SelectedRecord);
            }
            else if (RecordsSelectionMode.IsMultiple())
            {
                if (SelectedRecords.Contains(selectedRecord))
                {
                    SelectedRecords.Remove(selectedRecord);
                }
                else if (MaximumRecordsSelections <= 0 || (MaximumRecordsSelections > 0 && SelectedRecords.Count < MaximumRecordsSelections))
                {
                    SelectedRecords.Add(selectedRecord);
                }
                MinimumRecordsSelectionsReached = SelectedRecords.Count >= MinimumRecordsSelections;
                MinimumRecordsSelectionsReachedChanged?.Invoke(MinimumRecordsSelectionsReached);
                MaximumRecordsSelectionsReached = SelectedRecords.Count >= MaximumRecordsSelections;
                MaximumRecordsSelectionsReachedChanged?.Invoke(MaximumRecordsSelectionsReached);
                SelectedRecordsChanged?.Invoke(SelectedRecords);
            }
            AfterSelectedRecord?.Invoke();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SelectRecords(IEnumerable<T> selectedRecords)
        {
            if (MaximumRecordsSelections <= 0 || (MaximumRecordsSelections > 0 && selectedRecords.Count() < MaximumRecordsSelections))
            {
                SelectedRecords = new ObservableRangeCollection<T>(selectedRecords);
            }
            MinimumRecordsSelectionsReached = SelectedRecords.Count >= MinimumRecordsSelections;
            MinimumRecordsSelectionsReachedChanged?.Invoke(MinimumRecordsSelectionsReached);
            MaximumRecordsSelectionsReached = SelectedRecords.Count >= MaximumRecordsSelections;
            MaximumRecordsSelectionsReachedChanged?.Invoke(MaximumRecordsSelectionsReached);
            SelectedRecordsChanged?.Invoke(SelectedRecords);
            AfterSelectedRecord?.Invoke();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual async Task Validate()
        {
            if (FormMode.IsSingle())
            {
                ValidationResult = await Validator.ValidateAsync(Record);
            }
            else
            {
                foreach (var record in Records)
                {
                    var result = await Validator.ValidateAsync(record);
                    foreach (var validationFailure in result.Errors)
                    {
                        ValidationResult.Errors.Add(validationFailure);
                    }
                }
            }
            ValidationResultChanged?.Invoke(ValidationResult);
            IsValidModelState = ValidationResult.IsValid;
            IsValidModelStateChanged?.Invoke(IsValidModelState);
            OnAfterValidate?.Invoke();
        }
        /// <inheritdoc/>
        public virtual async Task Save()
        {
            if (FormState.IsCreate() || FormState.IsEdit() || FormState.IsCopy())
            {
                if (FormMode.IsSingle())
                {
                    if (FormState.IsCopy() || FormState.IsCreate())
                    {
                        Response = await HttpService.AddAsync<T>(Record);
                    }
                    else if (FormState.IsEdit())
                    {
                        Response = await HttpService.UpdateAsync<T>(Record);
                    }
                    ResponseChanged?.Invoke(Response);
                    if (Response.IsNotNull() &&
                        Response.Succeed &&
                        Response.Response.IsNotNull() &&
                        Response.Response.Status.Succeeded() &&
                        Response.HttpResponseMessage.IsNotNull() &&
                        Response.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        Record = Response.Response.Data;
                        RecordChanged?.Invoke(Record);
                        FormState = FormState.Edit;
                        FormStateChanged?.Invoke(FormState);
                        ClearMutableBlobDatasWithRules();
                    }
                }
                else if (FormMode.IsMultiple())
                {
                    if (FormState.IsCopy() || FormState.IsCreate())
                    {
                        ResponseCollection = await HttpService.AddRangeAsync<IEnumerable<T>>(Records);
                    }
                    else if (FormState.IsEdit())
                    {
                        ResponseCollection = await HttpService.UpdateRangeAsync<IEnumerable<T>>(Records);
                    }
                    ResponseCollectionChanged?.Invoke(ResponseCollection);
                    if (ResponseCollection.IsNotNull() &&
                        ResponseCollection.Succeed &&
                        ResponseCollection.Response.IsNotNull() &&
                        ResponseCollection.Response.Status.Succeeded() &&
                        ResponseCollection.HttpResponseMessage.IsNotNull() &&
                        ResponseCollection.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        Records.ReplaceRange(ResponseCollection.Response.Data);
                        RecordsChanged?.Invoke(Records);
                        FormState = FormState.Edit;
                        FormStateChanged?.Invoke(FormState);
                        ClearMutableBlobDatasWithRules();
                    }
                }
                OnAfterSave?.Invoke();
            }
        }
        /// <inheritdoc/>
        public virtual async Task Delete()
        {
            if (FormState.IsDelete())
            {
                if (FormMode.IsSingle())
                {
                    Response = await HttpService.DeleteAsync<T>(Identifier);
                    ResponseChanged?.Invoke(Response);
                    if (Response.IsNotNull() &&
                        Response.Succeed &&
                        Response.Response.IsNotNull() &&
                        Response.Response.Status.Succeeded() &&
                        Response.HttpResponseMessage.IsNotNull() &&
                        Response.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        Record = Response.Response.Data;
                        RecordChanged?.Invoke(Record);
                        FormState = FormState.Deleted;
                        FormStateChanged?.Invoke(FormState);
                    }
                }
                else if (FormMode.IsMultiple())
                {
                    ResponseCollection = await HttpService.DeleteRangeAsync<T>(Identifiers);
                    ResponseCollectionChanged?.Invoke(ResponseCollection);
                    if (ResponseCollection.IsNotNull() &&
                        ResponseCollection.Succeed &&
                        ResponseCollection.Response.IsNotNull() &&
                        ResponseCollection.Response.Status.Succeeded() &&
                        ResponseCollection.HttpResponseMessage.IsNotNull() &&
                        ResponseCollection.HttpResponseMessage.IsSuccessStatusCode)
                    {
                        Records.ReplaceRange(ResponseCollection.Response.Data);
                        RecordsChanged?.Invoke(Records);
                        FormState = FormState.Deleted;
                        FormStateChanged?.Invoke(FormState);
                    }
                }
                OnAfterDelete?.Invoke();
            }
        }
        /// <inheritdoc/>
        public virtual IEnumerable<PropertyInfo> GetMutableBlobDatasWithRulesProperties()
        {
            if (FormMode.IsSingle())
            {
                return Record.GetType().GetProperties().Where(w => w.PropertyType == typeof(Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>));
            }
            else if (FormMode.IsMultiple())
            {
                var propertiesInfos = new List<PropertyInfo>();
                foreach (var record in Records)
                {
                    foreach (var property in record.GetType().GetProperties().Where(w => w.PropertyType == typeof(Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)))
                    {
                        propertiesInfos.Add(property);
                    }
                }
                return propertiesInfos;
            }
            else
            {
                return Enumerable.Empty<PropertyInfo>();
            }
        }
        /// <inheritdoc/>
        public virtual void ClearMutableBlobDatasWithRules()
        {
            foreach (var blobDataProperty in GetMutableBlobDatasWithRulesProperties())
            {
                if (FormMode.IsSingle())
                {
                    var blobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(Record);
                    blobDataProperty.SetValue(Record, new Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>(blobDatas.Item1, Enumerable.Empty<BlobData>(), blobDatas.Item3));
                }
                else
                {
                    foreach (var record in Records)
                    {
                        var blobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(record);
                        blobDataProperty.SetValue(record, new Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>(blobDatas.Item1, Enumerable.Empty<BlobData>(), blobDatas.Item3));
                    }
                }
            }
        }
        /// <inheritdoc/>
        public virtual async Task ValidateMutableBlobDatas()
        {
            foreach (var blobDataProperty in GetMutableBlobDatasWithRulesProperties())
            {
                if (FormMode.IsSingle())
                {
                    var mutableBlobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(Record);
                    var formFileRules = mutableBlobDatas.Item1;
                    var blobDatas = mutableBlobDatas.Item2;
                    var userBlobs = mutableBlobDatas.Item3;
                    ValidationResult = new ValidationResult();
                    var currentBlobsCount = 0;
                    if (formFileRules.ForceUpload)
                    {
                        if (blobDatas.IsNull() || !blobDatas.Any())
                        {
                            ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                        }
                        else
                        {
                            await InternalValidateBlobDatas(blobDatas, formFileRules);
                        }
                    }
                    else if (!formFileRules.IsRequired && blobDatas.IsNull() || !blobDatas.Any())
                    {
                        continue;
                    }
                    else if (!formFileRules.IsRequired && blobDatas.IsNotNull() && blobDatas.Any())
                    {
                        await InternalValidateBlobDatas(blobDatas, formFileRules);
                    }
                    else if (formFileRules.IsRequired)
                    {
                        if ((blobDatas.IsNull() || !blobDatas.Any()) && (userBlobs.IsNull() || !userBlobs.Any()))
                        {
                            ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                            continue;
                        }
                        if (userBlobs.IsNotNull() && userBlobs.Any())
                        {
                            currentBlobsCount += userBlobs.Count();
                        }
                        if (blobDatas.IsNotNull() && blobDatas.Any())
                        {
                            currentBlobsCount += blobDatas.Count();
                        }
                        if (currentBlobsCount >= formFileRules.AllowedMinimunFiles)
                        {
                            if (blobDatas.IsNotNull() && blobDatas.Any())
                            {
                                await InternalValidateBlobDatas(blobDatas, formFileRules);
                            }
                        }
                        else
                        {
                            ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                        }
                    }
                }
                else
                {
                    ValidationResult = new ValidationResult();
                    foreach (var record in Records)
                    {
                        var mutableBlobDatas = (Mutable<FormFileRules, IEnumerable<BlobData>, IEnumerable<UserBlobs>>)blobDataProperty.GetValue(record);
                        var formFileRules = mutableBlobDatas.Item1;
                        var blobDatas = mutableBlobDatas.Item2;
                        var userBlobs = mutableBlobDatas.Item3;
                        var currentBlobsCount = 0;
                        if (formFileRules.ForceUpload)
                        {
                            if (blobDatas.IsNull() || !blobDatas.Any())
                            {
                                ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                            }
                            else
                            {
                                await InternalValidateBlobDatas(blobDatas, formFileRules);
                            }
                        }
                        else if (!formFileRules.IsRequired && blobDatas.IsNull() || !blobDatas.Any())
                        {
                            continue;
                        }
                        else if (!formFileRules.IsRequired && blobDatas.IsNotNull() && blobDatas.Any())
                        {
                            await InternalValidateBlobDatas(blobDatas, formFileRules);
                        }
                        else if (formFileRules.IsRequired)
                        {
                            if ((blobDatas.IsNull() || !blobDatas.Any()) && (userBlobs.IsNull() || !userBlobs.Any()))
                            {
                                ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                                continue;
                            }
                            if (userBlobs.IsNotNull() && userBlobs.Any())
                            {
                                currentBlobsCount += userBlobs.Count();
                            }
                            if (blobDatas.IsNotNull() && blobDatas.Any())
                            {
                                currentBlobsCount += blobDatas.Count();
                            }
                            if (currentBlobsCount >= formFileRules.AllowedMinimunFiles)
                            {
                                if (blobDatas.IsNotNull() && blobDatas.Any())
                                {
                                    await InternalValidateBlobDatas(blobDatas, formFileRules);
                                }
                            }
                            else
                            {
                                ValidationResult.Errors.Add(new ValidationFailure(formFileRules.PropertyName, $"{typeof(T).Name.Replace("ViewModel", "")}{formFileRules.PropertyName}IsRequired"));
                            }
                        }
                    }
                }
            }
            IsValidModelState = ValidationResult.IsValid;
        }
        private async Task InternalValidateBlobDatas(IEnumerable<BlobData> blobDatas, FormFileRules formFileRules)
        {
            var validator = new BlobDataCollectionValidator();
            var result = await validator.ValidateFormFileRulesAsync(blobDatas, formFileRules);
            if (!result.IsValid)
            {
                foreach (var validationFailure in result.Errors)
                {
                    ValidationResult.Errors.Add(validationFailure);
                }
            }
        }
        /// <inheritdoc/>
        public virtual Task Reset()
        {
            FormState = FormState.Create;
            FormStateChanged?.Invoke(FormState);
            Record = new T();
            RecordChanged?.Invoke(Record);
            Records?.Clear();
            RecordsChanged?.Invoke(Records);
            Identifier = new TIdentifier();
            IdentifierChanged?.Invoke(Identifier);
            Identifiers = new List<TIdentifier>();
            IdentifiersChanged?.Invoke(Identifiers);
            var mutableBlobDatasWithRulesProperties = GetMutableBlobDatasWithRulesProperties();
            if (mutableBlobDatasWithRulesProperties.IsNotNull() && mutableBlobDatasWithRulesProperties.Any())
            {
                ClearMutableBlobDatasWithRules();
            }
            OnAfterReset?.Invoke();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task Cancel()
        {
            OnAfterCancel?.Invoke();
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task FilterAndSort(string sortBy, bool descending)
        {
            FilterSortBy = sortBy;
            FilterSortByChanged?.Invoke(FilterSortBy);
            FilterDescending = descending;
            FilterDescendingChanged?.Invoke(FilterDescending);
            return FilterAndSort();
        }
        /// <inheritdoc/>
        public virtual Task FilterAndSort(string sortBy, bool descending, Func<T, bool> filterPredicate)
        {
            FilterSortBy = sortBy;
            FilterSortByChanged?.Invoke(FilterSortBy);
            FilterDescending = descending;
            FilterDescendingChanged?.Invoke(FilterDescending);
            FilterPredicate = filterPredicate;
            FilterPredicateChanged?.Invoke(FilterPredicate);
            return FilterAndSort();
        }
        /// <inheritdoc/>
        public virtual Task FilterAndSort()
        {
            if (Records.IsNotNull() && Records.Any())
            {
                IEnumerable<T> recordsFilteredSorted;
                if (FilterPredicate.IsNotNull())
                {
                    recordsFilteredSorted = Records.Where(FilterPredicate);
                }
                else
                {
                    recordsFilteredSorted = Records;
                }
                if (FilterSortBy.IsNotNull())
                {
                    if (FilterDescending)
                    {
                        recordsFilteredSorted = recordsFilteredSorted.OrderByPropertyDescending(FilterSortBy);
                    }
                    else
                    {
                        recordsFilteredSorted = recordsFilteredSorted.OrderByProperty(FilterSortBy);
                    }
                }
                //Creates a deep copy prevent deleting the original collection.
                RecordsFilteredSorted.ReplaceRange(recordsFilteredSorted.AutoMap<T>());
                RecordsFilteredSortedChanged?.Invoke(RecordsFilteredSorted);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoPreviousPage()
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoPreviousPage(int pageSize)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.PageSize = pageSize;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoPreviousPage(string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoPreviousPage(int pageSize, string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasPreviousPage)
            {
                SearchPaging.PageIndex--;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoNextPage()
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoNextPage(int pageSize)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.PageSize = pageSize;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoNextPage(string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoNextPage(int pageSize, string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.HasNextPage)
            {
                SearchPaging.PageIndex++;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoToPage(int pageIndex, int pageSize)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.PageSize = pageSize;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoToPage(int pageIndex, string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task GoToPage(int pageIndex, int pageSize, string sortBy)
        {
            if (ResponsePaged.IsNotNull() && ResponsePaged.Response.Data.LastPage <= pageIndex)
            {
                SearchPaging.PageIndex = pageIndex;
                SearchPaging.PageSize = pageSize;
                SearchPaging.SortBy = sortBy;
                SearchPagingChanged?.Invoke(SearchPaging);
            }
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task Sort()
        {
            SearchPaging.Descending = !SearchPaging.Descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task Sort(bool descending)
        {
            SearchPaging.Descending = descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortAscending()
        {
            SearchPaging.Descending = false;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortDescending()
        {
            SearchPaging.Descending = true;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortBy(string sortBy)
        {
            if (SearchPaging.SortBy.Equals(sortBy))
            {
                SearchPaging.Descending = !SearchPaging.Descending;
            }
            else
            {
                SearchPaging.SortBy = sortBy;
            }
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortBy(string sortBy, bool descending)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = descending;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortByAscending(string sortBy)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = false;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
        /// <inheritdoc/>
        public virtual Task SortByDescending(string sortBy)
        {
            SearchPaging.SortBy = sortBy;
            SearchPaging.Descending = true;
            SearchPagingChanged?.Invoke(SearchPaging);
            return Task.CompletedTask;
        }
    }
}

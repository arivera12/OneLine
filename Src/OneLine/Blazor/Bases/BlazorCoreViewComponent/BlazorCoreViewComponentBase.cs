using CurrieTechnologies.Razor.SweetAlert2;
using DeviceDetectorNET;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneLine.Bases;
using OneLine.Blazor.Contracts;
using OneLine.Blazor.Extensions;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public class BlazorCoreViewComponentBase<T, TIdentifier, TId, THttpService> :
        CoreViewBase<T, TIdentifier, TId, THttpService>,
        IBlazorDataViewComponent<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        /// <inheritdoc/>
        public virtual IJSRuntime JSRuntime { get; set; }
        /// <inheritdoc/>
        public virtual NavigationManager NavigationManager { get; set; }
        /// <inheritdoc/>
        public virtual SweetAlertService SweetAlertService { get; set; }
        /// <inheritdoc/>
        public virtual HttpClient HttpClient { get; set; }
        /// <inheritdoc/>
        public virtual int DebounceInterval { get; set; }
        /// <inheritdoc/>
        public virtual bool HideCancelOrBackButton { get; set; }
        /// <inheritdoc/>
        public virtual bool HideResetButton { get; set; }
        /// <inheritdoc/>
        public virtual bool HideSaveButton { get; set; }
        /// <inheritdoc/>
        public virtual bool HideDeleteButton { get; set; }
        /// <inheritdoc/>
        public virtual bool HideCreateOrNewButton { get; set; }
        /// <inheritdoc/>
        public virtual string RecordId { get; set; }
        /// <inheritdoc/>
        public virtual string RedirectUrlAfterSave { get; set; }
        /// <inheritdoc/>
        public virtual bool ShowOptionsDialog { get; set; }
        /// <inheritdoc/>
        public virtual bool HideDetailsDialogOption { get; set; }
        /// <inheritdoc/>
        public virtual bool HideCopyDialogOption { get; set; }
        /// <inheritdoc/>
        public virtual bool HideEditDialogOption { get; set; }
        /// <inheritdoc/>
        public virtual bool HideDeleteDialogOption { get; set; }
        /// <inheritdoc/>
        public virtual bool ShowForm { get; set; }
        /// <inheritdoc/>
        public virtual bool Hide { get; set; }
        /// <inheritdoc/>
        public virtual bool Hidden { get; set; }
        /// <inheritdoc/>
        public virtual bool ReadOnly { get; set; }
        /// <inheritdoc/>
        public virtual bool Disabled { get; set; }
        /// <inheritdoc/>
        public virtual bool EnableConfirmOnSave { get; set; }
        /// <inheritdoc/>
        public virtual bool EnableConfirmOnReset { get; set; }
        /// <inheritdoc/>
        public virtual bool EnableConfirmOnDelete { get; set; } = true;
        /// <inheritdoc/>
        public virtual bool EnableConfirmOnCancel { get; set; }
        /// <inheritdoc/>
        public virtual bool CloseFormAfterSaveOrDelete { get; set; } = true;
        /// <inheritdoc/>
        public virtual bool AutoSearchAfterFormClose { get; set; }
        /// <inheritdoc/>
        public virtual bool TriggerSearchMethod { get; set; }
        /// <inheritdoc/>
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
        public virtual Action<bool> ShowOptionsDialogChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<bool> ShowFormChanged { get; set; }
        /// <inheritdoc/>
        public virtual Action<bool> TriggerSearchChanged { get; set; }
        /// <inheritdoc/>
        public virtual DeviceDetector DeviceDetector { get; set; }
        /// <inheritdoc/>
        public virtual bool ShowActivityIndicator { get; set; }
        public BlazorCoreViewComponentBase(
            IApplicationConfiguration applicationConfiguration,
            IJSRuntime jSRuntime,
            NavigationManager navigationManager,
            ISaveFile saveFile,
            IDevice device,
            IResourceManagerLocalizer resourceManagerLocalizer,
            SweetAlertService sweetAlertService,
            HttpClient httpClient,
            IApplicationState applicationState,
            THttpService httpService) : base(applicationConfiguration, resourceManagerLocalizer, applicationState, device, saveFile)
        {
            JSRuntime = jSRuntime;
            NavigationManager = navigationManager;
            SweetAlertService = sweetAlertService;
            HttpClient = httpClient;
            HttpService = httpService;
        }
        public BlazorCoreViewComponentBase(
            IApplicationConfiguration applicationConfiguration,
            ISaveFile saveFile,
            IDevice device,
            IResourceManagerLocalizer resourceManagerLocalizer,
            HttpClient httpClient,
            IApplicationState applicationState,
            THttpService httpService) : base(applicationConfiguration, resourceManagerLocalizer, applicationState, device, saveFile)
        {
            HttpClient = httpClient;
            HttpService = httpService;
        }
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
            if (!string.IsNullOrWhiteSpace(RecordId))
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
        }
        /// <inheritdoc/>
        public virtual async Task BeforeSearch()
        {
            if (Device.IsDesktop)
            {
                await SweetAlertService.ShowLoaderAsync(new SweetAlertCallback(async () => await Search()), ResourceManagerLocalizer["ProcessingRequest"], ResourceManagerLocalizer["PleaseWait"]);
                //StateHasChanged();
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
                    //StateHasChanged();
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
                    //StateHasChanged();
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
                //StateHasChanged();
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
            //StateHasChanged();
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
            //StateHasChanged();
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
                //StateHasChanged();
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

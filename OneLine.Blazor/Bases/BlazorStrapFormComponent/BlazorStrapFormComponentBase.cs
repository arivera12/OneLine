using BlazorCurrentDevice;
using BlazorDownloadFile;
using BlazorStrap;
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
    public abstract partial class BlazorStrapFormComponentBase<T, TIdentifier, TId, THttpService> :
        FormViewBase<T, TIdentifier, TId, THttpService>,
        IBlazorStrapFormComponent<T, TIdentifier, THttpService>
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
        [Parameter] public override T Record { get; set; }
        [Parameter] public override ObservableRangeCollection<T> Records { get; set; }
        [Parameter] public override bool AllowDuplicates { get; set; }
        [Parameter] public override bool AutoLoad { get; set; }
        [Parameter] public override TIdentifier Identifier { get; set; }
        [Parameter] public override IEnumerable<TIdentifier> Identifiers { get; set; }
        [Parameter] public override CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        [Parameter] public override IValidator Validator { get; set; }
        [Parameter] public override ValidationResult ValidationResult { get; set; }
        [Parameter] public override bool IsValidModelState { get; set; }
        [Parameter] public override THttpService HttpService { get; set; }
        [Parameter] public override FormState FormState { get; set; }
        [Parameter] public override FormMode FormMode { get; set; }
        [Parameter] public override IResponseResult<ApiResponse<T>> Response { get; set; }
        [Parameter] public override IResponseResult<ApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        [Parameter] public override Action<TIdentifier> IdentifierChanged { get; set; }
        [Parameter] public override Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        [Parameter] public override Action<T> RecordChanged { get; set; }
        [Parameter] public override Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        [Parameter] public override Action<ValidationResult> ValidationResultChanged { get; set; }
        [Parameter] public override Action<bool> IsValidModelStateChanged { get; set; }
        [Parameter] public override Action<FormState> FormStateChanged { get; set; }
        [Parameter] public override Action<FormMode> FormModeChanged { get; set; }
        [Parameter] public override Action<IResponseResult<ApiResponse<T>>> ResponseChanged { get; set; }
        [Parameter] public override Action<IResponseResult<ApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
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
        [Parameter] public virtual bool HideCancelOrBackButton { get; set; }
        [Parameter] public virtual bool HideResetButton { get; set; }
        [Parameter] public virtual bool HideSaveButton { get; set; }
        [Parameter] public virtual bool HideDeleteButton { get; set; }
        [Parameter] public virtual int DebounceInterval { get; set; }
        [Parameter] public virtual string RecordId { get; set; }
        [Parameter] public virtual string RedirectUrl { get; set; }
        public bool IsChained { get { return !string.IsNullOrWhiteSpace(RedirectUrl);  } }
        public bool IsDesktop { get; set; }
        public bool IsTablet { get; set; }
        public bool IsMobile { get; set; }
        public bool IsFormOpen { get; set; }
        public BSModal Modal { get; set; }
        public virtual async Task OnAfterFirstRenderAsync()
        {
            HttpService.HttpClient = HttpClient;
            IsMobile = await BlazorCurrentDeviceService.Mobile();
            IsTablet = await BlazorCurrentDeviceService.Tablet();
            IsDesktop = await BlazorCurrentDeviceService.Desktop();
            //This null check allows to prevent override the listeners from parent if it's listening to any of this events
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
            StateHasChanged();
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
            else if(Response.IsNotNull() &&
                    Response.HttpResponseMessage.IsNotNull() &&
                    Response.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await SweetAlertService.FireAsync(Resourcer.GetString("SessionExpired"), Resourcer.GetString("YourSessionHasExpiredPleaseLoginInBackAgain"), SweetAlertIcon.Warning);
                await ApplicationState<AspNetUsersViewModel>.LogoutAndNavigateTo("/login");
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
                await ApplicationState<AspNetUsersViewModel>.LogoutAndNavigateTo("/login");
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
    }
}
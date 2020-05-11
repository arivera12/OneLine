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
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract partial class BlazorStrapFormComponentBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        FormBase<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs>,
        IBlazorStrapFormComponent<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator :class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        [Inject] public override IConfiguration Configuration { get; set; }
        [Inject] public virtual IJSRuntime JSRuntime { get; set; }
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
        [Inject] public virtual BlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
        [Inject] public virtual BlazorDownloadFileService BlazorDownloadFileService { get; set; }
        [Inject] public virtual SweetAlertService SweetAlertService { get; set; }
        [Inject] public virtual HttpClient HttpClient { get; set; }
        [Parameter] public override T Record { get; set; }
        [Parameter] public override ObservableRangeCollection<T> Records { get; set; }
        [Parameter] public override TIdentifier Identifier { get; set; }
        [Parameter] public override IEnumerable<TIdentifier> Identifiers { get; set; }
        [Parameter] public override CollectionAppendReplaceMode CollectionAppendReplaceMode { get; set; }
        [Parameter] public override IValidator Validator { get; set; }
        [Parameter] public override ValidationResult ValidationResult { get; set; }
        [Parameter] public override bool IsValidModelState { get; set; }
        [Parameter] public override THttpService HttpService { get; set; }
        [Parameter] public override IList<TBlobData> BlobDatas { get; set; }
        [Parameter] public override FormState FormState { get; set; }
        [Parameter] public override FormMode FormMode { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<T>> Response { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<IEnumerable<T>>> ResponseCollection { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>> ResponseAddWithBlobs { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>> ResponseAddCollectionWithBlobs { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateWithBlobs { get; set; }
        [Parameter] public override IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>> ResponseUpdateCollectionWithBlobs { get; set; }
        [Parameter] public override Action<TIdentifier> IdentifierChanged { get; set; }
        [Parameter] public override Action<IEnumerable<TIdentifier>> IdentifiersChanged { get; set; }
        [Parameter] public override Action<T> RecordChanged { get; set; }
        [Parameter] public override Action<ObservableRangeCollection<T>> RecordsChanged { get; set; }
        [Parameter] public override Action<IList<TBlobData>> BlobDatasChanged { get; set; }
        [Parameter] public override Action<ValidationResult> ValidationResultChanged { get; set; }
        [Parameter] public override Action<bool> IsValidModelStateChanged { get; set; }
        [Parameter] public override Action<FormState> FormStateChanged { get; set; }
        [Parameter] public override Action<FormMode> FormModeChanged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<T>>> ResponseChanged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<IEnumerable<T>>>> ResponseCollectionChanged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>>>>> ResponseAddWithBlobsChanged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>>>>> ResponseAddCollectionWithBlobsChanged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<T, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> ResponseUpdateWithBlobsChanged { get; set; }
        [Parameter] public override Action<IResponseResult<IApiResponse<Tuple<IEnumerable<T>, IEnumerable<TUserBlobs>, IEnumerable<TUserBlobs>>>>> ResponseUpdateCollectionWithBlobsChanged { get; set; }
        [Parameter] public override Action<Action> OnBeforeReset { get; set; }
        [Parameter] public override Action OnAfterReset { get; set; }
        [Parameter] public override Action<Action> OnBeforeCancel { get; set; }
        [Parameter] public override Action OnAfterCancel { get; set; }
        [Parameter] public override Action<Action> OnBeforeSave { get; set; }
        [Parameter] public override Action OnAfterSave { get; set; }
        [Parameter] public override Action<Action> OnBeforeDelete { get; set; }
        [Parameter] public override Action OnAfterDelete { get; set; }
        [Parameter] public override Action OnDeleteFailed { get; set; }
        [Parameter] public override Action OnSaveFailed { get; set; }
        [Parameter] public override Action OnValidationFailed { get; set; }
        [Parameter] public override Action OnValidationSucceeded { get; set; }
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
            if (Record == null && (Records == null || !Records.Any()))
            {
                if ((Identifier != null && Identifier.Model != null) ||
                    (Identifiers != null && Identifiers.Any()))
                {
                    await Load();
                }
            }
            //This null check allows to prevent override the listeners from parent if it's listening to any of this events
            OnBeforeSave = OnBeforeSave ?? new Action<Action>(async (callback) => await BeforeSave(callback));
            OnAfterSave = OnAfterSave ?? new Action(async () => await AfterSave());
            OnSaveFailed = OnSaveFailed ?? new Action(async () => await SaveFailed());
            OnValidationFailed = OnValidationFailed ?? new Action(async () => await ValidationFailed());
            OnBeforeDelete = OnBeforeDelete ?? new Action<Action>(async (callback) => await BeforeDelete(callback));
            OnAfterDelete = OnAfterDelete ?? new Action(async () => await AfterDelete());
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
        public virtual Size InputSize()
        {
            return IsDesktop ? Size.Large : IsTablet ? Size.None : IsMobile ? Size.Small : Size.None;
        }
        public virtual async Task BeforeSave(Action Callback)
        {
            await Validate();
            if (IsValidModelState && await SweetAlertService.ShowConfirmAlertAsync())
            {
                await SweetAlertService.ShowLoaderAsync(Resourcer.GetString("ProcessingRequest"), Resourcer.GetString("PleaseWait"));
                Callback();
            }
        }
        public virtual async Task ValidationFailed()
        {
            await SweetAlertService.ShowFluentValidationsAlertMessageAsync(ValidationResult);
        }
        public virtual async Task AfterSave()
        {
            await SweetAlertService.HideLoaderAsync();
            await SweetAlertService.FireAsync(null, Resourcer.GetString(Response.Response.Message), SweetAlertIcon.Success);
        }
        public virtual async Task SaveFailed()
        {
            await SweetAlertService.HideLoaderAsync();
            if (Response.HasException)
            {
                await SweetAlertService.FireAsync(null, Response.Exception.Message, SweetAlertIcon.Error);
            }
            else
            {
                await SweetAlertService.FireAsync(null, Resourcer.GetString(Response.Response.Message), SweetAlertIcon.Error);
            }
        }
        public virtual async Task BeforeDelete(Action Callback)
        {
            if (await SweetAlertService.ShowConfirmAlertAsync())
            {
                await SweetAlertService.ShowLoaderAsync(Resourcer.GetString("ProcessingRequest"), Resourcer.GetString("PleaseWait"));
                //This should and is always overrided cause there is no way to determine the Identifier field in a reference type.
                //Technically its possible is we use an data annotation attribute 
                //Or the same nomenclature for database table like TableNameId for main identifier.
                Identifier.Model = (TId)Convert.ChangeType(Record.GetType().GetProperty($"{nameof(T)}Id").GetValue(Record), typeof(TId));
                Callback();
            }
        }
        public virtual async Task AfterDelete()
        {
            await SweetAlertService.HideLoaderAsync();
            await SweetAlertService.FireAsync(null, Resourcer.GetString(Response.Response.Message), SweetAlertIcon.Success);
        }
    }
}
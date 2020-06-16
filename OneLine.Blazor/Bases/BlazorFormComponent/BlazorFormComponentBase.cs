using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OneLine.Bases;
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
    public abstract partial class BlazorFormComponentBase<T, TIdentifier, TId, THttpService> :
        FormBase<T, TIdentifier, TId, THttpService>,
        IBlazorFormComponent<T, TIdentifier, THttpService>
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
        [Parameter] public virtual int DebounceInterval { get; set; }
        public bool IsDesktop { get; set; }
        public bool IsTablet { get; set; }
        public bool IsMobile { get; set; }
        public virtual async Task OnAfterFirstRenderAsync()
        {
            HttpService.HttpClient = HttpClient;
            IsMobile = await BlazorCurrentDeviceService.Mobile();
            IsTablet = await BlazorCurrentDeviceService.Tablet();
            IsDesktop = await BlazorCurrentDeviceService.Desktop();
            if (FormMode.IsSingle())
            {
                if (Record.IsNull())
                {
                    if (Identifier.IsNotNull() && Identifier.Model.IsNotNull())
                    {
                        await Load();
                    }
                }
            }
            else if (FormMode.IsMultiple())
            {
                if (Records.IsNullOrEmpty())
                {
                    if (Identifiers.IsNotNullAndNotEmpty())
                    {
                        await Load();
                    }
                }
            }
            StateHasChanged();
        }
    }
}
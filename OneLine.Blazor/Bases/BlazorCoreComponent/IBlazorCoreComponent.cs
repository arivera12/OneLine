using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using JsonLanguageLocalizerNet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneLine.Blazor.Services;
using OneLine.Enums;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorCoreComponent : IComponent, IHandleEvent, IHandleAfterRender
    {
        IJSRuntime JSRuntime { get; set; }
        NavigationManager NavigationManager { get; set; }
        IBlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
        IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
        SweetAlertService SweetAlertService { get; set; }
        HttpClient HttpClient { get; set; }
        IApplicationState ApplicationState { get; set; }
        IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        bool IsDesktop { get; set; }
        bool IsTablet { get; set; }
        bool IsMobile { get; set; }
        bool ShowForm { get; set; }
        Action<bool> ShowFormChanged { get; set; }
        bool HideCancelOrBackButton { get; set; }
        bool HideResetButton { get; set; }
        bool HideSaveButton { get; set; }
        bool HideDeleteButton { get; set; }
        bool HideCreateOrNewButton { get; set; }
        bool ShowOptionsDialog { get; set; }
        Action<bool> ShowOptionsDialogChanged { get; set; }
        bool HideDetailsDialogOption { get; set; }
        bool HideCopyDialogOption { get; set; }
        bool HideEditDialogOption { get; set; }
        bool HideDeleteDialogOption { get; set; }
        bool Hide { get; set; }
        bool Hidden { get; set; }
        bool ReadOnly { get; set; }
        bool Disabled { get; set; }
        bool EnableConfirmOnSave { get; set; }
        bool EnableConfirmOnReset { get; set; }
        bool EnableConfirmOnDelete { get; set; }
        bool EnableConfirmOnCancel { get; set; }
        bool CloseFormAfterSaveOrDelete { get; set; }
        bool AutoSearchAfterFormClose { get; set; }
        int DebounceInterval { get; set; }
        Task OnAfterFirstRenderAsync();
        Task ShowFormChangeFormState(FormState formState);
        Task ShowFormChangeFormStateHideOptionsDialog(FormState formState);
        Task HideFormAfterFormCancel();
        Task HideOptionsDialog();
    }
}

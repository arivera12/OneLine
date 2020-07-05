using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        int DebounceInterval { get; set; }
        Task OnAfterFirstRenderAsync();
        Task ShowFormChangeFormState(FormState formState);
        Task ShowFormChangeFormStateHideOptionsDialog(FormState formState);
        void HideFormAfterFormCancel();
        Task HideOptionsDialog();
    }
}

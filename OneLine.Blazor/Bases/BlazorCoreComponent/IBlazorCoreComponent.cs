using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        bool IsFormOpen { get; set; }
        bool ShowModal { get; set; }
        bool HideCancelOrBackButton { get; set; }
        bool HideResetButton { get; set; }
        bool HideSaveButton { get; set; }
        bool HideDeleteButton { get; set; }
        int DebounceInterval { get; set; }
        Task OnAfterFirstRenderAsync();
    }
}

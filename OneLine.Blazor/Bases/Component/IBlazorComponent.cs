﻿using BlazorCurrentDevice;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorComponent : IComponent, IHandleEvent, IHandleAfterRender
    {
        IJSRuntime JSRuntime { get; set; }
        NavigationManager NavigationManager { get; set; }
        BlazorCurrentDeviceService BlazorCurrentDeviceService { get; set; }
        BlazorDownloadFileService BlazorDownloadFileService { get; set; }
        SweetAlertService SweetAlertService { get; set; }
        bool IsDesktop { get; set; }
        bool IsTablet { get; set; }
        bool IsMobile { get; set; }
        Task OnAfterRenderInitializeAsync();
    }
}

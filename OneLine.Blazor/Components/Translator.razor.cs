using BlazorMobile.Common;
using JsonLanguageLocalizerNet;
using JsonLanguageLocalizerNet.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace OneLine.Blazor
{
    public class TranslatorComponentModel : ComponentBase
    {
        [Parameter] public virtual bool ReloadOnLanguageChange { get; set; }
        [Parameter] public virtual bool AutoSetDefaultThreadCurrentCultureOnLanguageChange { get; set; }
        public static Action<JsonLanguageLocalizerService, CultureInfo> OnLanguageChanged { get; set; }
        [Parameter] public Action<JsonLanguageLocalizerService, CultureInfo> OnChanged { get; set; }
        [Parameter] public virtual string ApplicationLocale { get; set; }
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
        [Inject] public virtual HttpClient HttpClient { get; set; }
        [Inject] public virtual IJSRuntime JSRuntime { get; set; }
        [Inject] public virtual IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        [Inject] public virtual IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        [Inject] public virtual ISecureStorage SecureStorage { get; set; }
        public bool IsBlazorMobileDevice { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IsBlazorMobileDevice = BlazorDevice.RuntimePlatform == BlazorDevice.Android ||
                   BlazorDevice.RuntimePlatform == BlazorDevice.iOS ||
                   BlazorDevice.RuntimePlatform == BlazorDevice.UWP;
                if (IsBlazorMobileDevice)
                {
                    if (string.IsNullOrWhiteSpace(ApplicationLocale))
                    {
                        ApplicationLocale = await SecureStorage.GetAsync("ApplicationLocale");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(ApplicationLocale))
                    {
                        ApplicationLocale = await JSRuntime.InvokeAsync<string>("window.localStorage.getItem", "ApplicationLocale");
                    }
                }
                if (!string.IsNullOrWhiteSpace(ApplicationLocale))
                {
                    if (!LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures().SupportedCultures.Any(w => w.Name == ApplicationLocale))
                    {
                        throw new ArgumentException($"The ApplicationLocale {ApplicationLocale} doesn't exists in the LanguageLocalizerSupportedCultures");
                    }
                    var jsonLanguageLocalizerService = await JsonLanguageLocalizerServiceHelper.GetJsonLanguageLocalizerServiceFromSupportedCulturesAsync(HttpClient, LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures(), ApplicationLocale);
                    LanguageLocalizer.ChangeLanguageLocalizer(jsonLanguageLocalizerService);
                }
                StateHasChanged();
            }
        }
        protected async Task OnValueChanged(string value)
        {
            ApplicationLocale = value;
            if (!LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures().SupportedCultures.Any(w => w.Name == ApplicationLocale))
            {
                throw new ArgumentException($"The ApplicationLocale {ApplicationLocale} doesn't exists in the LanguageLocalizerSupportedCultures");
            }
            var jsonLanguageLocalizerService = await JsonLanguageLocalizerServiceHelper.GetJsonLanguageLocalizerServiceFromSupportedCulturesAsync(HttpClient, LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures(), ApplicationLocale);
            LanguageLocalizer.ChangeLanguageLocalizer(jsonLanguageLocalizerService);
            if (IsBlazorMobileDevice)
            {
                await SecureStorage.SetAsync("ApplicationLocale", ApplicationLocale);
            }
            else
            {
                await JSRuntime.InvokeAsync<string>("window.localStorage.setItem", "ApplicationLocale", ApplicationLocale);
            }
            if (ReloadOnLanguageChange)
            {
                NavigationManager.NavigateTo(NavigationManager.Uri);
            }
            else
            {
                var cultureInfoSelected = LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures().SupportedCultures.FirstOrDefault(w => w.Name == ApplicationLocale)?.CultureInfo;
                if (AutoSetDefaultThreadCurrentCultureOnLanguageChange)
                {
                    CultureInfo.DefaultThreadCurrentCulture = cultureInfoSelected;
                    CultureInfo.DefaultThreadCurrentUICulture = cultureInfoSelected;
                }
                OnLanguageChanged?.Invoke(jsonLanguageLocalizerService, cultureInfoSelected);
                OnChanged?.Invoke(jsonLanguageLocalizerService, cultureInfoSelected);
            }
            StateHasChanged();
        }
    }
}

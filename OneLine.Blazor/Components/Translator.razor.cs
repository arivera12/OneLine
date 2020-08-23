using BlazorBrowserStorage;
using JsonLanguageLocalizerNet;
using JsonLanguageLocalizerNet.Blazor.Helpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor
{
    public class TranslatorComponentModel : ComponentBase
    {
        public virtual bool ReloadOnLanguageChange { get; set; }
        public virtual bool AutoSetDefaultThreadCurrentCultureOnLanguageChange { get; set; }
        public static Action<CultureInfo> OnLanguageChanged { get; set; }
        public virtual string ApplicationLocale { get; set; }
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
        [Inject] public virtual HttpClient HttpClient { get; set; }
        [Inject] public virtual ILocalStorage LocalStorage { get; set; }
        [Inject] public virtual IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        [Inject] public virtual IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if(string.IsNullOrWhiteSpace(ApplicationLocale))
                {
                    ApplicationLocale = await LocalStorage.GetItem<string>("ApplicationLocale");
                }
                if (!string.IsNullOrWhiteSpace(ApplicationLocale))
                {
                    if (!LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures().SupportedCultures.Any(w => w.Name == ApplicationLocale))
                    {
                        throw new ArgumentException($"The ApplicationLocale {ApplicationLocale} doesn't exists in the LanguageLocalizerSupportedCultures");
                    }
                    var jsonLanguageLocalizerService = await JsonLanguageLocalizerServiceHelper.GetJsonLanguageLocalizerServiceFromSupportedCulturesAsync(HttpClient, LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures());
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
            var jsonLanguageLocalizerService = await JsonLanguageLocalizerServiceHelper.GetJsonLanguageLocalizerServiceFromSupportedCulturesAsync(HttpClient, LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures());
            LanguageLocalizer.ChangeLanguageLocalizer(jsonLanguageLocalizerService);
            await LocalStorage.SetItem("ApplicationLocale", ApplicationLocale);
            if(ReloadOnLanguageChange)
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
                OnLanguageChanged?.Invoke(cultureInfoSelected);
            }
            StateHasChanged();
        }
    }
}

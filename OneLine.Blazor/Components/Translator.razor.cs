using JsonLanguageLocalizerNet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneLine.Blazor.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Blazor
{
    public class TranslatorComponentModel : ComponentBase
    {
        [Parameter] public virtual bool ReloadOnLanguageChange { get; set; }
        [Parameter] public virtual bool AutoSetDefaultThreadCurrentCultureOnLanguageChange { get; set; }
        public static Action<JsonLanguageLocalizerService, CultureInfo> OnLanguageChanged { get; set; }
        [Parameter] public Action<JsonLanguageLocalizerService, CultureInfo> OnChanged { get; set; }
        [Parameter] public virtual string ApplicationLocale { get; set; }
        [Inject] public virtual ITranslator Translator { get; set; }
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
        [Inject] public virtual IJSRuntime JSRuntime { get; set; }
        [Inject] public virtual IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        [Inject] public virtual IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ApplicationLocale = await Translator.GetCurrentApplicationLocale();
                StateHasChanged();
            }
        }
        protected async Task OnValueChanged(string applicationLocale)
        {
            var jsonLanguageLocalizerService = await Translator.Translate(applicationLocale);
            if (ReloadOnLanguageChange)
            {
                NavigationManager.NavigateTo(NavigationManager.Uri);
            }
            else
            {
                var cultureInfoSelected = LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures().SupportedCultures.FirstOrDefault(w => w.Name == applicationLocale)?.CultureInfo;
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

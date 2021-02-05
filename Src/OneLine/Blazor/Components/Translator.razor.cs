using Microsoft.AspNetCore.Components;
using OneLine.Services;
using System;
using System.Threading.Tasks;

namespace OneLine.Blazor
{
    public class TranslatorComponentModel : ComponentBase
    {
        [Parameter] public string CssClass { get; set; } = "custom-select";
        public static Action<IResourceManagerLocalizer> OnLanguageChanged { get; set; }
        [Parameter] public Action<IResourceManagerLocalizer> OnChanged { get; set; }
        [Parameter] public string ApplicationLocale { get; set; }
        [Inject] public IResourceManagerLocalizer ResourceManagerLocalizer { get; set; }
        [Inject] public ISupportedCultures SupportedCultures { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ApplicationLocale = await ResourceManagerLocalizer.GetApplicationLocale();
                StateHasChanged();
            }
        }
        protected async Task OnValueChanged(string applicationLocale)
        {
            ApplicationLocale = applicationLocale;
            await ResourceManagerLocalizer.SetApplicationLocale(applicationLocale);
            await ResourceManagerLocalizer.SetCurrentThreadCulture(applicationLocale);
            OnLanguageChanged?.Invoke(ResourceManagerLocalizer);
            OnChanged?.Invoke(ResourceManagerLocalizer);
            StateHasChanged();
        }
    }
}

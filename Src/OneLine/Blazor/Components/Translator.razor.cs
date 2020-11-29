using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneLine.Services;
using System;
using System.Threading.Tasks;

namespace OneLine.Blazor
{
    public class TranslatorComponentModel : ComponentBase
    {
        [Parameter] public virtual bool ReloadOnLanguageChange { get; set; }
        public static Action<IResourceManagerLocalizer> OnLanguageChanged { get; set; }
        [Parameter] public Action<IResourceManagerLocalizer> OnChanged { get; set; }
        [Parameter] public virtual string ApplicationLocale { get; set; }
        [Inject] public virtual IResourceManagerLocalizer ResourceManagerLocalizer { get; set; }
        [Inject] public virtual ISupportedCultures SupportedCultures { get; set; }
        [Inject] public virtual NavigationManager NavigationManager { get; set; }
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
            await ResourceManagerLocalizer.SetApplicationLocale(applicationLocale);
            if (ReloadOnLanguageChange)
            {
                NavigationManager.NavigateTo(NavigationManager.Uri);
            }
            else
            {
                OnLanguageChanged?.Invoke(ResourceManagerLocalizer);
                OnChanged?.Invoke(ResourceManagerLocalizer);
            }
            StateHasChanged();
        }
    }
}

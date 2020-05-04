using Blazor.Extensions.Storage.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Blazor
{
    public class TranslatorComponentModel : ComponentBase
    {
        public string ApplicationLocale { get; set; }
        [Inject] public ILocalStorage LocalStorage { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ApplicationLocale = await LocalStorage.GetItem<string>("ApplicationLocale");
                if (!string.IsNullOrWhiteSpace(ApplicationLocale))
                {
                    if (!Resourcer.CurrentResourcers.Any(w => w.Key == ApplicationLocale))
                    {
                        throw new ArgumentException($"The ApplicationLocale {ApplicationLocale} doesn't exists in the Resourcers dictionary");
                    }
                    var ResourcerSource = Resourcer.CurrentResourcers.FirstOrDefault(w => w.Key == ApplicationLocale).Value.Item2;
                    Resourcer.CurrentResourcerSource = ResourcerSource;
                }
                StateHasChanged();
            }
        }
        protected async Task OnValueChanged(string value)
        {
            ApplicationLocale = value;
            if (!Resourcer.CurrentResourcers.Any(w => w.Key == ApplicationLocale))
            {
                throw new ArgumentException($"The ApplicationLocale {ApplicationLocale} doesn't exists in the Resourcers dictionary");
            }
            var ResourcerSource = Resourcer.CurrentResourcers.FirstOrDefault(w => w.Key == ApplicationLocale).Value.Item2;
            Resourcer.CurrentResourcerSource = ResourcerSource;
            await LocalStorage.SetItem("ApplicationLocale", ApplicationLocale);
            StateHasChanged();
        }
    }
}

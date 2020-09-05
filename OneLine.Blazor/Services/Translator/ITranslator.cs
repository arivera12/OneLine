using JsonLanguageLocalizerNet;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace OneLine.Blazor.Services
{
    public interface ITranslator
    {
        HttpClient HttpClient { get; set; }
        IJSRuntime JSRuntime { get; set; }
        IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        ISecureStorage SecureStorage { get; set; }
        Task<JsonLanguageLocalizerService> Translate(string applicationLocale);
        Task<string> GetCurrentApplicationLocale();
    }
}

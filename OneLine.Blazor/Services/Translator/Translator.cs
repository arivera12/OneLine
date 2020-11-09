using JsonLanguageLocalizerNet;
using JsonLanguageLocalizerNet.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace OneLine.Blazor.Services
{
    public class Translator : ITranslator
    {
        public HttpClient HttpClient { get; set; }
        public IJSRuntime JSRuntime { get; set; }
        public IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        public IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        public Translator(HttpClient httpClient, IJSRuntime jSRuntime, IJsonLanguageLocalizerService languageLocalizer, IJsonLanguageLocalizerSupportedCulturesService languageLocalizerSupportedCultures)
        {
            HttpClient = httpClient;
            JSRuntime = jSRuntime;
            LanguageLocalizer = languageLocalizer;
            LanguageLocalizerSupportedCultures = languageLocalizerSupportedCultures;
        }
        public async Task<JsonLanguageLocalizerService> Translate(string applicationLocale)
        {
            if (!LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures().SupportedCultures.Any(w => w.Name == applicationLocale))
            {
                throw new ArgumentException($"The ApplicationLocale {applicationLocale} doesn't exists in the LanguageLocalizerSupportedCultures");
            }
            var jsonLanguageLocalizerService = await JsonLanguageLocalizerServiceHelper.GetJsonLanguageLocalizerServiceFromSupportedCulturesAsync(HttpClient, LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures(), applicationLocale);
            LanguageLocalizer.ChangeLanguageLocalizer(jsonLanguageLocalizerService);
            if (Helpers.Device.IsXamarinPlatform)
            {
                await SecureStorage.SetAsync("ApplicationLocale", applicationLocale);
            }
            else if (Helpers.Device.IsWebPlatform)
            {
                await JSRuntime.InvokeAsync<string>("window.localStorage.setItem", "ApplicationLocale", applicationLocale);
            }
            else
            {
                new PlatformNotSupportedException("Translator seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
            return jsonLanguageLocalizerService;
        }
        public async Task<string> GetCurrentApplicationLocale()
        {
            if (Helpers.Device.IsXamarinPlatform)
            {
                return await SecureStorage.GetAsync("ApplicationLocale");
            }
            else if (Helpers.Device.IsWebPlatform)
            {
                return await JSRuntime.InvokeAsync<string>("window.localStorage.getItem", "ApplicationLocale");
            }
            else
            {
                new PlatformNotSupportedException("Translator seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
            return default;
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTranslator(this IServiceCollection services)
        {
            return services.AddScoped<ITranslator, Translator>();
        }
    }
}

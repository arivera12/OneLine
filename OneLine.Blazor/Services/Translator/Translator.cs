using JsonLanguageLocalizerNet;
using JsonLanguageLocalizerNet.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using OneLine.Extensions;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace OneLine.Blazor.Services
{
    public class Translator : ITranslator
    {
        public HttpClient HttpClient { get; set; }
        public IJSRuntime JSRuntime { get; set; }
        public IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        public IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        public ISecureStorage SecureStorage { get; set; }
        public Translator(HttpClient httpClient, IJSRuntime jSRuntime, IJsonLanguageLocalizerService languageLocalizer, IJsonLanguageLocalizerSupportedCulturesService languageLocalizerSupportedCultures)
        {
            HttpClient = httpClient;
            JSRuntime = jSRuntime;
            LanguageLocalizer = languageLocalizer;
            LanguageLocalizerSupportedCultures = languageLocalizerSupportedCultures;
        }
        public Translator(HttpClient httpClient, IJSRuntime jSRuntime, IJsonLanguageLocalizerService languageLocalizer, IJsonLanguageLocalizerSupportedCulturesService languageLocalizerSupportedCultures, ISecureStorage secureStorage)
        {
            HttpClient = httpClient;
            JSRuntime = jSRuntime;
            LanguageLocalizer = languageLocalizer;
            LanguageLocalizerSupportedCultures = languageLocalizerSupportedCultures;
            SecureStorage = secureStorage;
        }
        public async Task<JsonLanguageLocalizerService> Translate(string applicationLocale)
        {
            if (!LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures().SupportedCultures.Any(w => w.Name == applicationLocale))
            {
                throw new ArgumentException($"The ApplicationLocale {applicationLocale} doesn't exists in the LanguageLocalizerSupportedCultures");
            }
            var jsonLanguageLocalizerService = await JsonLanguageLocalizerServiceHelper.GetJsonLanguageLocalizerServiceFromSupportedCulturesAsync(HttpClient, LanguageLocalizerSupportedCultures.GetLanguageLocalizerSupportedCultures(), applicationLocale);
            LanguageLocalizer.ChangeLanguageLocalizer(jsonLanguageLocalizerService);
            if (SecureStorage.IsNotNull())
            {
                await SecureStorage.SetAsync("ApplicationLocale", applicationLocale);
            }
            else
            {
                await JSRuntime.InvokeAsync<string>("window.localStorage.setItem", "ApplicationLocale", applicationLocale);
            }
            return jsonLanguageLocalizerService;
        }
        public async Task<string> GetCurrentApplicationLocale()
        {
            if (SecureStorage.IsNotNull())
            {
                return await SecureStorage.GetAsync("ApplicationLocale");
            }
            else
            { 
                return await JSRuntime.InvokeAsync<string>("window.localStorage.getItem", "ApplicationLocale");
            }
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

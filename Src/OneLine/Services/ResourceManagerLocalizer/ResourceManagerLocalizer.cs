using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace OneLine.Services
{
    public class ResourceManagerLocalizer : IResourceManagerLocalizer
    {
        public IApplicationConfigurationSource ApplicationConfigurationSource { get; set; }
        public IDevice Device { get; set; }
        public IJSRuntime JSRuntime { get; set; }
        public ResourceManager ResourceManager { get; set; }
        public string this[string key] { get => ResourceManager.GetString(key); }
        public ResourceManagerLocalizer(IApplicationConfigurationSource applicationConfigurationSource, IDevice device)
        {
            ApplicationConfigurationSource = applicationConfigurationSource;
            Device = device;
            InitializeCurrentCulture();
        }
        public ResourceManagerLocalizer(IApplicationConfigurationSource applicationConfigurationSource, IDevice device, IJSRuntime jSRuntime)
        {
            ApplicationConfigurationSource = applicationConfigurationSource;
            Device = device;
            JSRuntime = jSRuntime;
            InitializeCurrentCulture();
        }
        private void InitializeCurrentCulture()
        {
            ResourceManager = new ResourceManager(ApplicationConfigurationSource.ResourceFilesBasePath, Assembly.GetExecutingAssembly());
        }
        public async Task<string> GetApplicationLocale()
        {
            if (Device.IsXamarinPlatform)
            {
                return await SecureStorage.GetAsync("ApplicationLocale");
            }
            else if (Device.IsWebPlatform)
            {
                return await JSRuntime.InvokeAsync<string>("window.localStorage.getItem", "ApplicationLocale");
            }
            else
            {
                new PlatformNotSupportedException("Translator seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
            return default;
        }
        public async Task SetApplicationLocale(string applicationLocale)
        {
            var currentCulture = new CultureInfo(applicationLocale);
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;
            if (Device.IsXamarinPlatform)
            {
                await SecureStorage.SetAsync("ApplicationLocale", applicationLocale);
            }
            else if (Device.IsWebPlatform)
            {
                await JSRuntime.InvokeVoidAsync("window.localStorage.setItem", "ApplicationLocale", applicationLocale);
            }
            else
            {
                new PlatformNotSupportedException("Translator seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddResourceManagerLocalizer(this IServiceCollection services)
        {
            return services.AddScoped<IResourceManagerLocalizer, ResourceManagerLocalizer>();
        }
    }
}

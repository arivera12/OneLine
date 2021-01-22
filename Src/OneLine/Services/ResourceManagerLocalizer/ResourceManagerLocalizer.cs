using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace OneLine.Services
{
    public class ResourceManagerLocalizer : IResourceManagerLocalizer 
    {
        private IApplicationConfigurationSource ApplicationConfigurationSource { get; set; }
        private IDevice Device { get; set; }
        private IJSRuntime JSRuntime { get; set; }
        public ResourceManager ResourceManager { get; set; }
        public string this[string key] { get => ResourceManager.GetString(key); }
        public ResourceManagerLocalizer(IApplicationConfigurationSource applicationConfigurationSource, IDevice device)
        {
            ApplicationConfigurationSource = applicationConfigurationSource;
            Device = device;
            ResourceManager = new ResourceManager(ApplicationConfigurationSource.ResourceFilesBasePath, ApplicationConfigurationSource.ResourceFilesAssemblyFile);
            new Action(async () => await SetCurrentThreadCulture()).Invoke();
        }
        public ResourceManagerLocalizer(IApplicationConfigurationSource applicationConfigurationSource, IDevice device, IJSRuntime jSRuntime)
        {
            ApplicationConfigurationSource = applicationConfigurationSource;
            Device = device;
            JSRuntime = jSRuntime;
            ResourceManager = new ResourceManager(ApplicationConfigurationSource.ResourceFilesBasePath, ApplicationConfigurationSource.ResourceFilesAssemblyFile);
            new Action(async () => await SetCurrentThreadCulture()).Invoke();
        }
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public async Task SetApplicationLocale(string applicationLocale)
        {
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
        /// <inheritdoc/>
        public async Task SetCurrentThreadCulture()
        {
            var applicationLocale = await GetApplicationLocale();
            if(!string.IsNullOrWhiteSpace(applicationLocale))
            {
                var currentCulture = new CultureInfo(applicationLocale);
                Thread.CurrentThread.CurrentCulture = currentCulture;
                Thread.CurrentThread.CurrentUICulture = currentCulture;
            }
        }
        /// <inheritdoc/>
        public Task SetCurrentThreadCulture(string applicationLocale)
        {
            var currentCulture = new CultureInfo(applicationLocale);
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;
            return Task.CompletedTask;
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

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace OneLine.Services
{
    public class ResourceManagerLocalizer : IResourceManagerLocalizer
    {
        private IApplicationConfigurationSource ApplicationConfigurationSource { get; set; }
        private IApplicationLocale ApplicationLocale { get; set; }
        private string CurrentApplicationLocale { get; set; }
        public ResourceManager ResourceManager { get; set; }
        public string this[string key]
        {
            get => string.IsNullOrWhiteSpace(CurrentApplicationLocale) ?
                ResourceManager.GetString(key) :
                ResourceManager.GetString(key, new CultureInfo(CurrentApplicationLocale));
        }
        public ResourceManagerLocalizer(IApplicationConfigurationSource applicationConfigurationSource)
        {
            ApplicationConfigurationSource = applicationConfigurationSource;
            ResourceManager = new ResourceManager(ApplicationConfigurationSource.ResourceFilesBasePath, ApplicationConfigurationSource.ResourceFilesAssemblyFile);
        }
        public ResourceManagerLocalizer(IApplicationConfigurationSource applicationConfigurationSource, IApplicationLocale applicationLocale)
        {
            ApplicationConfigurationSource = applicationConfigurationSource;
            ResourceManager = new ResourceManager(ApplicationConfigurationSource.ResourceFilesBasePath, ApplicationConfigurationSource.ResourceFilesAssemblyFile);
            ApplicationLocale = applicationLocale;
            new Action(async () =>
            {
                CurrentApplicationLocale = await ApplicationLocale.GetApplicationLocale();
                await SetCurrentThreadCulture();
            }).Invoke();
        }
        /// <inheritdoc/>
        public async Task SetCurrentThreadCulture()
        {
            var applicationLocale = await ApplicationLocale.GetApplicationLocale();
            if (!string.IsNullOrWhiteSpace(applicationLocale))
            {
                CurrentApplicationLocale = applicationLocale;
                var currentCulture = new CultureInfo(applicationLocale);
                CultureInfo.CurrentCulture = currentCulture;
                CultureInfo.CurrentUICulture = currentCulture;
                CultureInfo.DefaultThreadCurrentCulture = currentCulture;
                CultureInfo.DefaultThreadCurrentUICulture = currentCulture;
                Thread.CurrentThread.CurrentCulture = currentCulture;
                Thread.CurrentThread.CurrentUICulture = currentCulture;
            }
        }
        /// <inheritdoc/>
        public async Task SetCurrentThreadCulture(string applicationLocale)
        {
            await ApplicationLocale.SetApplicationLocale(applicationLocale);
            CurrentApplicationLocale = applicationLocale;
            var currentCulture = new CultureInfo(applicationLocale);
            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentCulture;
            CultureInfo.DefaultThreadCurrentCulture = currentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = currentCulture;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;
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

using Microsoft.Extensions.DependencyInjection;

namespace OneLine.Services
{
    public static class HybridServices
    {
        /// <summary>
        /// Adds the current services <see cref="IApplicationConfigurationSource"/>, <see cref="ISupportedCultures"/>, <see cref="IApplicationConfiguration"/>, <see cref="IResourceManagerLocalizer"/>, <see cref="IApplicationState"/>, <see cref="IDevice"/> and <see cref="ISaveFile"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOneLineHybridServices(this IServiceCollection services)
        {
            return services
                .AddApplicationConfigurationSource()
                .AddSupportedCultures()
                .AddApplicationConfiguration()
                .AddResourceManagerLocalizer()
                .AddApplicationState()
                .AddDevice()
                .AddDeviceStorage()
                .AddSaveFile();
        }
    }
}

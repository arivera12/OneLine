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
        public static IServiceCollection AddOneLineHybridServices<
            TApplicationConfigurationSourceImplementation, 
            TSupportedCulturesImplementation>
            (this IServiceCollection services)
            where TApplicationConfigurationSourceImplementation : class, IApplicationConfigurationSource
            where TSupportedCulturesImplementation : class, ISupportedCultures
        {
            return services
                .AddApplicationConfigurationSource< TApplicationConfigurationSourceImplementation>()
                .AddSupportedCultures<TSupportedCulturesImplementation>()
                .AddApplicationConfiguration()
                .AddResourceManagerLocalizer()
                .AddApplicationState()
                .AddDevice()
                .AddDeviceStorage()
                .AddSaveFile();
        }
    }
}

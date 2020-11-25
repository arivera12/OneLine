using Microsoft.Extensions.DependencyInjection;

namespace OneLine.Services
{
    public static class HybridServices
    {
        /// <summary>
        /// Adds the current services <see cref="IApplicationConfigurationSource"/>, <see cref="IApplicationConfiguration"/>, <see cref="IResourceManagerLocalizer"/>, <see cref="IApplicationState"/>, <see cref="IDevice"/> and <see cref="ISaveFile"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOneLineHybridServices(this IServiceCollection services, ApplicationConfigurationSource applicationConfigurationSource)
        {
            return services
                .AddApplicationConfigurationSource(applicationConfigurationSource)
                .AddApplicationConfiguration()
                .AddResourceManagerLocalizer()
                .AddApplicationState()
                .AddDevice()
                .AddSaveFile();
        }
    }
}

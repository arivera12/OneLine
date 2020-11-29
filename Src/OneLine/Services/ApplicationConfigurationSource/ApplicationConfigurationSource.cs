using Microsoft.Extensions.DependencyInjection;

namespace OneLine.Services
{
    public class ApplicationConfigurationSource : IApplicationConfigurationSource
    {
        public string ConfigurationFilePath { get; set; }
        public string ResourceFilesBasePath { get; set; }
        public ApplicationConfigurationSource()
        {
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationConfigurationSource(this IServiceCollection services, ApplicationConfigurationSource applicationConfigurationSource)
        {
            return services.AddSingleton<IApplicationConfigurationSource, ApplicationConfigurationSource>(sp => applicationConfigurationSource);
        }
    }
}

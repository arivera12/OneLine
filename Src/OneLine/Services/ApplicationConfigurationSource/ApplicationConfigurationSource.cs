using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OneLine.Services
{
    public class ApplicationConfigurationSource : IApplicationConfigurationSource
    {
        public string ConfigurationFilePath { get { return configurationFilePath; } }
        public Assembly ConfigurationFileAssemblyFile { get { return configurationFileAssemblyFile; } }
        public string ResourceFilesBasePath { get { return resourceFilesBasePath; } }
        public Assembly ResourceFilesAssemblyFile { get { return resourceFilesAssemblyFile; } }
        public static string configurationFilePath;
        public static Assembly configurationFileAssemblyFile;
        public static string resourceFilesBasePath;
        public static Assembly resourceFilesAssemblyFile;
        public ApplicationConfigurationSource()
        {
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationConfigurationSource(this IServiceCollection services)
        {
            return services.AddScoped<IApplicationConfigurationSource, ApplicationConfigurationSource>();
        }
    }
}

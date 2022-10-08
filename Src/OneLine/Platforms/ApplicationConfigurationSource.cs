using OneLine.Contracts;
using System.Reflection;

namespace OneLine
{
    public class ApplicationConfigurationSource : IApplicationConfigurationSource
    {
        public string ConfigurationFilePath { get; set; }
        public Assembly ConfigurationFileAssemblyFile { get; set; }
        public string ResourceFilesBasePath { get; set; }
        public Assembly ResourceFilesAssemblyFile { get; set; }
        public ApplicationConfigurationSource()
        {
        }
        public ApplicationConfigurationSource(string configurationFilePath, Assembly configurationFileAssemblyFile, string resourceFilesBasePath, Assembly resourceFilesAssemblyFile)
        {
            ConfigurationFilePath = configurationFilePath;
            ConfigurationFileAssemblyFile = configurationFileAssemblyFile;
            ResourceFilesBasePath = resourceFilesBasePath;
            ResourceFilesAssemblyFile = resourceFilesAssemblyFile;
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationConfigurationSource<TApplicationConfigurationSourceImplementation>(this IServiceCollection services)
            where TApplicationConfigurationSourceImplementation : class, IApplicationConfigurationSource
        {
            return services.AddScoped<IApplicationConfigurationSource, TApplicationConfigurationSourceImplementation>();
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace OneLine.Contracts
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public ApplicationConfiguration(IApplicationConfigurationSource applicationConfigurationSource)
        {
            var stream = applicationConfigurationSource.ConfigurationFileAssemblyFile.GetManifestResourceStream(applicationConfigurationSource.ConfigurationFilePath);
            Configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
        }
        private protected IConfiguration Configuration { get; set; }
        public string this[string key] { get => Configuration[key]; set => Configuration[key] = value; }
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return Configuration.GetChildren();
        }
        public IChangeToken GetReloadToken()
        {
            return Configuration.GetReloadToken();
        }
        public IConfigurationSection GetSection(string key)
        {
            return Configuration.GetSection(key);
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            return services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();
        }
    }
}

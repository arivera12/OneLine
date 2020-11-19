using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Reflection;

namespace OneLine.Services
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public ApplicationConfiguration(string manifestResourceName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestResourceName);
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
            return services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>();
        }
    }
}

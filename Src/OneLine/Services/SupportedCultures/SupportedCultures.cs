using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace OneLine.Services
{
    public class SupportedCultures : ISupportedCultures
    {
        public IEnumerable<CultureInfo> Cultures { get; set; }
        public SupportedCultures()
        {
        }
        public SupportedCultures(IEnumerable<CultureInfo> cultures)
        {
            Cultures = cultures;
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSupportedCultures(this IServiceCollection services, SupportedCultures supportedCultures)
        {
            return services.AddSingleton<ISupportedCultures, SupportedCultures>(sp => supportedCultures);
        }
    }
}

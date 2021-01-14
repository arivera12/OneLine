using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace OneLine.Services
{
    public class SupportedCultures : ISupportedCultures
    {
        public IEnumerable<CultureInfo> Cultures { get { return cultures; } }
        public static IEnumerable<CultureInfo> cultures;
        public SupportedCultures()
        {
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSupportedCultures(this IServiceCollection services)
        {
            return services.AddScoped<ISupportedCultures, SupportedCultures>();
        }
    }
}

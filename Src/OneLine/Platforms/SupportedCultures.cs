using OneLine.Contracts;
using System.Globalization;

namespace OneLine
{
    public class SupportedCultures : ISupportedCultures
    {
        public IEnumerable<CultureInfo> Cultures { get; set; }
        public SupportedCultures()
        {
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSupportedCultures<TSupportedCulturesImplementation>(this IServiceCollection services)
            where TSupportedCulturesImplementation : class, ISupportedCultures
        {
            return services.AddScoped<ISupportedCultures, TSupportedCulturesImplementation>();
        }
    }
}

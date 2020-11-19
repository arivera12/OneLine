using Microsoft.Extensions.DependencyInjection;

namespace OneLine.Services
{
    public static class HybridServices
    {
        public static IServiceCollection AddOneLineHybridServices(this IServiceCollection services)
        {
            return services.AddApplicationState()
                .AddDevice()
                .AddSaveFile()
                .AddResourceManagerLocalizer();
        }
    }
}

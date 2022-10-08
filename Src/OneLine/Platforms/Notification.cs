using BlazorNotification;
using Microsoft.Extensions.DependencyInjection;
using OneLine.Contracts

namespace OneLine
{
    public class Notification : INotification
    {
        public IBlazorNotificationService BlazorNotificationService { get; set; }
        public Notification(IBlazorNotificationService blazorNotificationService)
        {
            BlazorNotificationService = blazorNotificationService;
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNotification(this IServiceCollection services)
        {
            return services.AddScoped<INotification, Notification>();
        }
    }
}

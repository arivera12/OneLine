using Microsoft.Extensions.DependencyInjection;

namespace OneLine.Services
{
    public static class NotificationsExtensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            return services.AddScoped<INotificationService, NotificationService>();
        }
    }
}

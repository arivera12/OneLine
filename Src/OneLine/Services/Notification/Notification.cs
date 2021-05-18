using BlazorNotification;
using Microsoft.Extensions.DependencyInjection;
using Shiny.Notifications;

namespace OneLine.Services
{
    public class Notification : INotification
    {
        public IBlazorNotificationService BlazorNotificationService { get; set; }
        public INotificationManager NotificationManager { get; set; }
        public Notification(IDevice device)
        {
            if (device.IsXamarinPlatform &&
                (device.IsiOSDevice || device.IsAndroidDevice || device.IsWindowsOSPlatform) &&
                !device.IsDesktop)
            {
                NotificationManager = Shiny.ShinyHost.Resolve<INotificationManager>();
            }
        }
        public Notification(IBlazorNotificationService blazorNotificationService, IDevice device)
        {
            BlazorNotificationService = blazorNotificationService;
            if(device.IsXamarinPlatform &&
                (device.IsiOSDevice || device.IsAndroidDevice || device.IsWindowsOSPlatform) &&
                !device.IsDesktop)
            {
                NotificationManager = Shiny.ShinyHost.Resolve<INotificationManager>();
            }
        }
        public Notification(INotificationManager notificationManager)
        {
            NotificationManager = notificationManager;
        }
        public Notification(IBlazorNotificationService blazorNotificationService, INotificationManager notificationManager)
        {
            BlazorNotificationService = blazorNotificationService;
            NotificationManager = notificationManager;
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

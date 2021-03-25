using BlazorNotification;
using Shiny.Notifications;

namespace OneLine.Services
{
    public interface INotification
    {
        IBlazorNotificationService BlazorNotificationService { get; set; }
        INotificationManager NotificationManager { get; set; }
    }
}
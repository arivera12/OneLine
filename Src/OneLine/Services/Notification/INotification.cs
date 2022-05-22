using BlazorNotification;

namespace OneLine.Services
{
    public interface INotification
    {
        IBlazorNotificationService BlazorNotificationService { get; set; }
    }
}
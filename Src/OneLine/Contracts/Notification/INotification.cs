using BlazorNotification;

namespace OneLine.Contracts
{
    public interface INotification
    {
        IBlazorNotificationService BlazorNotificationService { get; set; }
    }
}
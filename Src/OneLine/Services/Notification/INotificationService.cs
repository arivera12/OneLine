using System.Threading.Tasks;

namespace OneLine.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Checks if the Notifications' API is Support by the browser.
        /// </summary>
        /// <returns></returns>
        ValueTask<bool> IsSupportedByBrowserAsync();
        /// <summary>
        /// Request the user for his permission to send notifications.
        /// </summary>
        /// <returns></returns>
        ValueTask<NotificationPermissionType> RequestPermissionAsync();
        /// <summary>
        /// Create a Notification with <seealso cref="NotificationOptions"/>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ValueTask CreateAsync(string title, NotificationOptions options);
        /// <summary>
        /// Create a Notification with <paramref name="title"/>, <paramref name="body"/> and <paramref name="iconUrl"/>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="iconUrl"></param>
        /// <returns></returns>
        ValueTask CreateAsync(string title, string body, string iconUrl);
    }
}
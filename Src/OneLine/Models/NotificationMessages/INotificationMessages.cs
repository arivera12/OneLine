using System;

namespace OneLine.Models
{
    public interface INotificationMessages
    {
        /// <summary>
        /// Notification identifier
        /// </summary>
        string NotificationMessageId { get; set; }
        /// <summary>
        /// Notification user identifier
        /// </summary>
        string UserId { get; set; }
        /// <summary>
        /// The notification title
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// The notification message
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// The notification icon
        /// </summary>
        string IconUri { get; set; }
        /// <summary>
        /// The path to redirect to.
        /// </summary>
        string DestinationUri { get; set; }
        /// <summary>
        /// The is deleted is to allow soft delete
        /// </summary>
        bool? IsDeleted { get; set; }
        /// <summary>
        /// The is readed is to know when the user readed the notification
        /// </summary>
        bool? IsReaded { get; set; }
        /// <summary>
        /// Created on
        /// </summary>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// Created by
        /// </summary>
        string CreatedBy { get; set; }
    }
}

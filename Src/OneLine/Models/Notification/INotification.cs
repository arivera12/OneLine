using System;

namespace OneLine.Models
{
    public interface INotification<T>
    {
        /// <summary>
        /// Notification identifier
        /// </summary>
        public string NotificationId { get; set; }
        /// <summary>
        /// Notification user id of the receiver user
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Notification user sender
        /// </summary>
        public string SenderUser { get; set; }
        /// <summary>
        /// The notification title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The notification message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The notification icon or image url for web
        /// </summary>
        public string WebIconOrImageUrl { get; set; }
        /// <summary>
        /// The path to redirect to web.
        /// </summary>
        public string WebRedirectTo { get; set; }
        /// <summary>
        /// The path to redirect to native app. This may use app scheme.
        /// </summary>
        public string NativeRedirectTo { get; set; }
        /// <summary>
        /// The notification icon or image url for native app.
        /// This can be locally or remote image.
        /// </summary>
        public string NativeIconOrImageUrl { get; set; }
        /// <summary>
        /// The notification <typeparamref name="T"/> data property.
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// The is deleted user blob is to allow soft delete
        /// </summary>
        bool IsDeleted { get; set; }
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

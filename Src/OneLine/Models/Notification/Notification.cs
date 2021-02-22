using System;

namespace OneLine.Models
{
    public class Notification<T> : INotification<T>
    {
        /// <inheritdoc/>
        public string NotificationId { get; set; }
        /// <inheritdoc/>
        public string UserId { get; set; }
        /// <inheritdoc/>
        public string SenderUser { get; set; }
        /// <inheritdoc/>
        public string Title { get; set; }
        /// <inheritdoc/>
        public string Message { get; set; }
        /// <inheritdoc/>
        public string WebIconOrImageUrl { get; set; }
        /// <inheritdoc/>
        public string WebRedirectTo { get; set; }
        /// <inheritdoc/>
        public string NativeIconOrImageUrl { get; set; }
        /// <inheritdoc/>
        public string NativeRedirectTo { get; set; }
        /// <inheritdoc/>
        public T Data { get; set; }
        /// <inheritdoc/>
        public bool IsDeleted { get; set; }
        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }
        /// <inheritdoc/>
        public string CreatedBy { get; set; }
    }
}

using System;

namespace OneLine.Models
{
    public class Notification : INotification
    {
        /// <inheritdoc/>
        public string NotificationId { get; set; }
        /// <inheritdoc/>
        public string Title { get; set; }
        /// <inheritdoc/>
        public string Message { get; set; }
        /// <inheritdoc/>
        public string IconUri { get; set; }
        /// <inheritdoc/>
        public string DestinationUri { get; set; }
        /// <inheritdoc/>
        public bool IsDeleted { get; set; }
        /// <inheritdoc/>
        public bool IsReaded { get; set; }
        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }
        /// <inheritdoc/>
        public string CreatedBy { get; set; }
    }
}

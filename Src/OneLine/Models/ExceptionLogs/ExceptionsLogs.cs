using System;

namespace OneLine.Models
{
    /// <summary>
    /// This class implements a structure for savings exceptions
    /// </summary>
    public class ExceptionLogs : IExceptionLogs
    {
        /// <inheritdoc/>
        public string ExceptionLogId { get; set; }
        /// <inheritdoc/>
        public int? HResult { get; set; }
        /// <inheritdoc/>
        public string HelpLink { get; set; }
        /// <inheritdoc/>
        public string InnerException { get; set; }
        /// <inheritdoc/>
        public string Message { get; set; }
        /// <inheritdoc/>
        public string Source { get; set; }
        /// <inheritdoc/>
        public string StackTrace { get; set; }
        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }
        /// <inheritdoc/>
        public string CreatedBy { get; set; }
    }
    /// <summary>
    /// This class implements a structure for savings exceptions
    /// </summary>
    public class ExceptionLogsViewModel : IExceptionLogs
    {
        /// <inheritdoc/>
        public string ExceptionLogId { get; set; }
        /// <inheritdoc/>
        public int? HResult { get; set; }
        /// <inheritdoc/>
        public string HelpLink { get; set; }
        /// <inheritdoc/>
        public string InnerException { get; set; }
        /// <inheritdoc/>
        public string Message { get; set; }
        /// <inheritdoc/>
        public string Source { get; set; }
        /// <inheritdoc/>
        public string StackTrace { get; set; }
        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }
        /// <inheritdoc/>
        public string CreatedBy { get; set; }
    }
}

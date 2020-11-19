using System;

namespace OneLine.Models
{
    /// <summary>
    /// This class implements a structure for savings exceptions
    /// </summary>
    public class ExceptionLogs : IExceptionLogs
    {
        /// <inheritdoc/>
        public virtual string ExceptionLogId { get; set; }
        /// <inheritdoc/>
        public virtual int? HResult { get; set; }
        /// <inheritdoc/>
        public virtual string HelpLink { get; set; }
        /// <inheritdoc/>
        public virtual string InnerException { get; set; }
        /// <inheritdoc/>
        public virtual string Message { get; set; }
        /// <inheritdoc/>
        public virtual string Source { get; set; }
        /// <inheritdoc/>
        public virtual string StackTrace { get; set; }
        /// <inheritdoc/>
        public virtual DateTime CreatedOn { get; set; }
        /// <inheritdoc/>
        public virtual string CreatedBy { get; set; }
    }
    /// <summary>
    /// This class implements a structure for savings exceptions
    /// </summary>
    public class ExceptionLogsViewModel : IExceptionLogs
    {
        /// <inheritdoc/>
        public virtual string ExceptionLogId { get; set; }
        /// <inheritdoc/>
        public virtual int? HResult { get; set; }
        /// <inheritdoc/>
        public virtual string HelpLink { get; set; }
        /// <inheritdoc/>
        public virtual string InnerException { get; set; }
        /// <inheritdoc/>
        public virtual string Message { get; set; }
        /// <inheritdoc/>
        public virtual string Source { get; set; }
        /// <inheritdoc/>
        public virtual string StackTrace { get; set; }
        /// <inheritdoc/>
        public virtual DateTime CreatedOn { get; set; }
        /// <inheritdoc/>
        public virtual string CreatedBy { get; set; }
    }
}

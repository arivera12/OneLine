
using System;

namespace OneLine.Models
{
    public class ExceptionsLogs : IExceptionsLogs
    {
        /// <summary>
        /// The System Exception Log Identifier
        /// </summary>
        public virtual string ExceptionLogId { get; set; }
        /// <summary>
        /// The HResult from the exception
        /// </summary>
        public virtual int? HResult { get; set; }
        /// <summary>
        /// The HelpLink from the exception
        /// </summary>
        public virtual string HelpLink { get; set; }
        /// <summary>
        /// The InnerException from the exception
        /// </summary>
        public virtual string InnerException { get; set; }
        /// <summary>
        /// The Message from the exception
        /// </summary>
        public virtual string Message { get; set; }
        /// <summary>
        /// The Source from the exception
        /// </summary>
        public virtual string Source { get; set; }
        /// <summary>
        /// The Source from the exception
        /// </summary>
        public virtual string StackTrace { get; set; }
        /// <summary>
        /// The date time stamp that this trasaction was performed.
        /// </summary>
        public virtual DateTime CreatedOn { get; set; }
        /// <summary>
        /// The user that created the transaction.
        /// </summary>
        public virtual string CreatedBy { get; set; }
    }
}

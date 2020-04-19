using System;

namespace OneLine.Models
{
    public interface IExceptionsLogs
    {
        /// <summary>
        /// The System Exception Log Identifier
        /// </summary>
        string ExceptionLogId { get; set; }
        /// <summary>
        /// The HResult from the exception
        /// </summary>
        int? HResult { get; set; }
        /// <summary>
        /// The HelpLink from the exception
        /// </summary>
        string HelpLink { get; set; }
        /// <summary>
        /// The InnerException from the exception
        /// </summary>
        string InnerException { get; set; }
        /// <summary>
        /// The Message from the exception
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// The Source from the exception
        /// </summary>
        string Source { get; set; }
        /// <summary>
        /// The Source from the exception
        /// </summary>
        string StackTrace { get; set; }
        /// <summary>
        /// The date time stamp that this trasaction was performed.
        /// </summary>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// The user that created the transaction.
        /// </summary>
        string CreatedBy { get; set; }
    }
}

using OneLine.Enums;
using System.Collections.Generic;

namespace OneLine.Models
{
    /// <summary>
    /// This interface defines a base api response structure to be used as an standard object in every api response result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiResponse<T> : IDataHolder<T>
    {
        /// <summary>
        /// The api response status is the result of the transaction wether it succeeded or failed. 
        /// </summary>
        ApiResponseStatus Status { get; set; }
        /// <summary>
        /// The message from response whether there is anything that needs to be notified about like an error message or success message.
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// The validation status is whether the validation result passed or failed
        /// </summary>
        bool ValidationFailed { get; set; }
        /// <summary>
        /// The errors messages from response when validation result failed
        /// </summary>
        IEnumerable<string> ErrorMessages { get; set; }
    }
}

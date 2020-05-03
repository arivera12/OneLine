using OneLine.Enums;
using System.Collections.Generic;

namespace OneLine.Models
{
    public interface IApiResponse<T>
    {
        /// <summary>
        /// The response status
        /// </summary>
        ApiResponseStatus Status { get; set; }
        /// <summary>
        /// The data from reponse
        /// </summary>
        T Data { get; set; }
        /// <summary>
        /// The message from response
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// The validation status
        /// </summary>
        bool ValidationFailed { get; set; }
        /// <summary>
        /// The errors messages from response
        /// </summary>
        IEnumerable<string> ErrorMessages { get; set; }
    }
}

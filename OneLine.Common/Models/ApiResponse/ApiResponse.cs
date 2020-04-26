using OneLine.Enums;
using System.Collections.Generic;

namespace OneLine.Models
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponse()
        {

        }
        public ApiResponse(ApiResponseStatus status, T data, string message, IEnumerable<string> errorMessages)
        {
            Status = status;
            Data = data;
            Message = message;
            ErrorMessages = errorMessages;
        }
        /// <summary>
        /// The response status
        /// </summary>
        public virtual ApiResponseStatus Status { get; set; }
        /// <summary>
        /// The data from reponse
        /// </summary>
        public virtual T Data { get; set; }
        /// <summary>
        /// The message from response
        /// </summary>
        public virtual string Message { get; set; }
        /// <summary>
        /// The errors messages from response
        /// </summary>
        public virtual IEnumerable<string> ErrorMessages { get; set; }
    }
}

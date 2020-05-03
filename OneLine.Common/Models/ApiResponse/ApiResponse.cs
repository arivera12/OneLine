using OneLine.Enums;
using System.Collections.Generic;
using System.Linq;

namespace OneLine.Models
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponse()
        {

        }
        public ApiResponse(ApiResponseStatus status, T data)
        {
            Status = status;
            Data = data;
        }
        public ApiResponse(ApiResponseStatus status, string message)
        {
            Status = status;
            Message = message;
        }
        public ApiResponse(ApiResponseStatus status, T data, string message)
        {
            Status = status;
            Data = data;
            Message = message;
        }
        public ApiResponse(ApiResponseStatus status, T data, IEnumerable<string> errorMessages)
        {
            Status = status;
            Data = data;
            ErrorMessages = errorMessages;
            ValidationFailed = ErrorMessages != null && ErrorMessages.Any();
        }
        public ApiResponse(ApiResponseStatus status, IEnumerable<string> errorMessages)
        {
            Status = status;
            ErrorMessages = errorMessages;
            ValidationFailed = ErrorMessages != null && ErrorMessages.Any();
        }
        public ApiResponse(ApiResponseStatus status, T data, string message, IEnumerable<string> errorMessages)
        {
            Status = status;
            Data = data;
            Message = message;
            ErrorMessages = errorMessages;
            ValidationFailed = ErrorMessages != null && ErrorMessages.Any();
        }
        public ApiResponse(ApiResponseStatus status, string message, IEnumerable<string> errorMessages)
        {
            Status = status;
            Message = message;
            ErrorMessages = errorMessages;
            ValidationFailed = ErrorMessages != null && ErrorMessages.Any();
        }
        public ApiResponse(ApiResponseStatus status, T data, string message, bool validationFailed)
        {
            Status = status;
            Data = data;
            Message = message;
            ValidationFailed = validationFailed;
        }
        public ApiResponse(ApiResponseStatus status, string message, bool validationFailed)
        {
            Status = status;
            Message = message;
            ValidationFailed = validationFailed;
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
        /// The validation status
        /// </summary>
        public virtual bool ValidationFailed { get; set; }
        /// <summary>
        /// The errors messages from response
        /// </summary>
        public virtual IEnumerable<string> ErrorMessages { get; set; }
    }
}

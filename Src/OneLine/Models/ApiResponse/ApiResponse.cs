using OneLine.Enums;
using System.Collections.Generic;
using System.Linq;

namespace OneLine.Models
{
    /// <summary>
    /// This class is a base api response structure to be used as an standard object in every api response result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T> : IApiResponse<T>
    {
        /// <summary>
        /// Default contructor
        /// </summary>
        public ApiResponse()
        {

        }
        /// <summary>
        /// ApiResponse with an api response status
        /// </summary>
        /// <param name="status">The api response status</param>
        public ApiResponse(ApiResponseStatus status)
        {
            Status = status;
        }
        /// <summary>
        /// ApiResponse with status and data
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="data">The response data</param>
        public ApiResponse(ApiResponseStatus status, T data)
        {
            Status = status;
            Data = data;
        }
        /// <summary>
        /// ApiResponse with status and message
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="message">The api response message</param>
        public ApiResponse(ApiResponseStatus status, string message)
        {
            Status = status;
            Message = message;
        }
        /// <summary>
        ///  ApiResponse with status, data and message
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="data">The response data</param>
        /// <param name="message">The api response message</param>
        public ApiResponse(ApiResponseStatus status, T data, string message)
        {
            Status = status;
            Data = data;
            Message = message;
        }
        /// <summary>
        ///  ApiResponse with status, data and error messages
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="data">The response data</param>
        /// <param name="errorMessages">The response error messages</param>
        public ApiResponse(ApiResponseStatus status, T data, IEnumerable<string> errorMessages)
        {
            Status = status;
            Data = data;
            ErrorMessages = errorMessages;
            ValidationFailed = ErrorMessages != null && ErrorMessages.Any();
        }
        /// <summary>
        ///  ApiResponse with status and error messages
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="errorMessages">The response error messages</param>
        public ApiResponse(ApiResponseStatus status, IEnumerable<string> errorMessages)
        {
            Status = status;
            ErrorMessages = errorMessages;
            ValidationFailed = ErrorMessages != null && ErrorMessages.Any();
        }
        /// <summary>
        /// ApiResponse with status, data, message and error messages
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="data">The response data</param>
        /// <param name="message">The api response message</param>
        /// <param name="errorMessages">The response error messages</param>
        public ApiResponse(ApiResponseStatus status, T data, string message, IEnumerable<string> errorMessages)
        {
            Status = status;
            Data = data;
            Message = message;
            ErrorMessages = errorMessages;
            ValidationFailed = ErrorMessages != null && ErrorMessages.Any();
        }
        /// <summary>
        /// ApiResponse with status message and error messages.
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="message">The api response message</param>
        /// <param name="errorMessages">The response error messages</param>
        public ApiResponse(ApiResponseStatus status, string message, IEnumerable<string> errorMessages)
        {
            Status = status;
            Message = message;
            ErrorMessages = errorMessages;
            ValidationFailed = ErrorMessages != null && ErrorMessages.Any();
        }
        /// <summary>
        /// ApiResponse with status, data, message and validation failed indicator
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="data">The response data</param>
        /// <param name="message">The api response message</param>
        /// <param name="validationFailed">The validation failed indicator</param>
        public ApiResponse(ApiResponseStatus status, T data, string message, bool validationFailed)
        {
            Status = status;
            Data = data;
            Message = message;
            ValidationFailed = validationFailed;
        }
        /// <summary>
        /// ApiResponse with status, message and validation failed indicator 
        /// </summary>
        /// <param name="status">The api response status</param>
        /// <param name="message">The api response message</param>
        /// <param name="validationFailed">The validation failed indicator</param>
        public ApiResponse(ApiResponseStatus status, string message, bool validationFailed)
        {
            Status = status;
            Message = message;
            ValidationFailed = validationFailed;
        }
        /// <inheritdoc/>
        public ApiResponseStatus Status { get; set; }
        /// <inheritdoc/>
        public T Data { get; set; }
        /// <inheritdoc/>
        public string Message { get; set; }
        /// <inheritdoc/>
        public bool ValidationFailed { get; set; }
        /// <inheritdoc/>
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}

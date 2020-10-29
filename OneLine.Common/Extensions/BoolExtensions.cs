using OneLine.Models;
using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class BoolExtensions
    {
        /// <summary>
        /// Converts <paramref name="source"/> to <see cref="ApiResponse{string}"/>
        /// </summary>
        /// <param name="source">The source</param>
        /// <returns></returns>
        public static ApiResponse<string> TransactionResultApiResponse(this bool source)
        {
            return source ?
                new ApiResponse<string>(ApiResponseStatus.Succeeded) :
                new ApiResponse<string>(ApiResponseStatus.Failed);
        }
        /// <summary>
        /// Converts <paramref name="source"/> to <see cref="ApiResponse{string}"/> with a <paramref name="message"/>
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="message">The message</param>
        /// <returns></returns>
        public static ApiResponse<string> TransactionResultApiResponse(this bool source, string message)
        {
            return source ?
                new ApiResponse<string>(ApiResponseStatus.Succeeded, message: message) :
                new ApiResponse<string>(ApiResponseStatus.Failed, message: message);
        }
        /// <summary>
        /// Converts <paramref name="source"/> to <see cref="ApiResponse{T}"/> with <paramref name="data"/>
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="data">The data</param>
        /// <typeparam name="T">The data type</typeparam>
        /// <returns></returns>
        public static ApiResponse<T> TransactionResultApiResponse<T>(this bool source, T data)
        {
            return source ?
                new ApiResponse<T>(ApiResponseStatus.Succeeded, data) :
                new ApiResponse<T>(ApiResponseStatus.Failed, data);
        }
        /// <summary>
        /// Converts <paramref name="source"/> to <see cref="ApiResponse{T}"/> with <paramref name="data"/> and a <paramref name="message"/>
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="data">The data</param>
        /// <param name="message">The message</param>
        /// <typeparam name="T">The data type</typeparam>
        /// <returns></returns>
        public static ApiResponse<T> TransactionResultApiResponse<T>(this bool source, T data, string message)
        {
            return source ?
                new ApiResponse<T>(ApiResponseStatus.Succeeded, data, message) :
                new ApiResponse<T>(ApiResponseStatus.Failed, data, message);
        }
    }
}


using OneLine.Enums;
using OneLine.Models;
using System.Collections.Generic;
using System.Linq;

namespace OneLine.Extensions
{
    public static class IntergerExtensions
    {
        /// <summary>
        /// True when a value is greater than zero (0) otherwise false
        /// </summary>
        /// <param name="value">The int value</param>
        /// <returns></returns>
        public static bool Succeeded(this int value)
        {
            return value > 0;
        }
        /// <summary>
        /// True when a value is greater than zero (0) otherwise false
        /// </summary>
        /// <typeparam name="T">The type param</typeparam>
        /// <param name="value">The int value</param>
        /// <param name="data">The data of type of <see cref="{T}"/></param>
        /// <returns></returns>
        public static IApiResponse<T> TransactionResultApiResponse<T>(this int value, T data)
        {
            var status = value.Succeeded() ? ApiResponseStatus.Succeeded : ApiResponseStatus.Failed;
            return new ApiResponse<T>(status, data);
        }
        /// <summary>
        /// True when a value is greater than zero (0) otherwise false
        /// </summary>
        /// <typeparam name="T">The type param</typeparam>
        /// <param name="value">The int value</param>
        /// <param name="data">The data of type of <see cref="IEnumerable{T}"/></param>
        /// <returns></returns>
        public static IApiResponse<IEnumerable<T>> TransactionResultApiResponse<T>(this int value, IEnumerable<T> data)
        {
            var status = value.Succeeded() && value == data.Count() ? ApiResponseStatus.Succeeded : ApiResponseStatus.Failed;
            return new ApiResponse<IEnumerable<T>>(status, data);
        }
        /// <summary>
        /// True when a value is greater than zero (0) otherwise false
        /// </summary>
        /// <typeparam name="T">The type param</typeparam>
        /// <param name="value">The int value</param>
        /// <param name="data">The data of type of <see cref="{T}"/></param>
        /// <param name="successMessage">The success message</param>
        /// <param name="errorMessage">The error message</param>
        /// <returns></returns>
        public static IApiResponse<T> TransactionResultApiResponse<T>(this int value, T data, string successMessage, string errorMessage)
        {
            var success = value.Succeeded();
            var status = success ? ApiResponseStatus.Succeeded : ApiResponseStatus.Failed;
            var message = success ? successMessage : errorMessage;
            return new ApiResponse<T>(status, data, message);
        }
        /// <summary>
        /// True when a value is greater than zero (0) otherwise false
        /// </summary>
        /// <typeparam name="T">The type param</typeparam>
        /// <param name="value">The int value</param>
        /// <param name="data">The data of type of <see cref="IEnumerable{T}"/></param>
        /// <param name="successMessage">The success message</param>
        /// <param name="errorMessage">The error message</param>
        /// <returns></returns>
        public static IApiResponse<IEnumerable<T>> TransactionResultApiResponse<T>(this int value, IEnumerable<T> data, string successMessage, string errorMessage)
        {
            var success = value.Succeeded();
            var status = success && value == data.Count() ? ApiResponseStatus.Succeeded : ApiResponseStatus.Failed;
            var message = success && value == data.Count() ? successMessage : errorMessage;
            return new ApiResponse<IEnumerable<T>> (status, data, message);
        }
    }
}
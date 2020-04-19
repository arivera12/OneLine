using OneLine.Enums;
using OneLine.Models;

namespace OneLine.Extensions
{
    public static class ApiResponseExtensions
    {
        /// <summary>
        /// Checks if the Api Response Status Succeeded
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        public static bool Succeeded<T>(this IApiResponse<T> apiResponse)
        {
            return apiResponse.Status == ApiResponseStatus.Succeeded;
        }
        /// <summary>
        /// Checks if the Api Response Status Failed 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        public static bool Failed<T>(this IApiResponse<T> apiResponse)
        {
            return apiResponse.Status == ApiResponseStatus.Failed;
        }
        /// <summary>
        /// Checks if the Api Response Status Succeeded
        /// </summary>
        /// <param name="apiResponseStatus"></param>
        /// <returns></returns>

        public static bool Succeeded(this ApiResponseStatus apiResponseStatus)
        {
            return apiResponseStatus == ApiResponseStatus.Succeeded;
        }
        /// <summary>
        /// Checks if the Api Response Status Failed 
        /// </summary>
        /// <param name="apiResponseStatus"></param>
        /// <returns></returns>

        public static bool Failed(this ApiResponseStatus apiResponseStatus)
        {
            return apiResponseStatus == ApiResponseStatus.Failed;
        }
    }
}

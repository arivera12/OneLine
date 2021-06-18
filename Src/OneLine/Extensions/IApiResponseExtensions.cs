using OneLine.Enums;
using OneLine.Models;

namespace OneLine.Extensions
{
    public static class IApiResponseExtensions
    {
        /// <summary>
        /// Checks if the Api Response Status Succeeded
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        public static bool Succeeded<T>(this IApiResponse<T> apiResponse)
        {
            return apiResponse.Status.Equals(ApiResponseStatus.Succeeded);
        }
        /// <summary>
        /// Checks if the Api Response Status Failed 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        public static bool Failed<T>(this IApiResponse<T> apiResponse)
        {
            return apiResponse.Status.Equals(ApiResponseStatus.Failed);
        }
    }
}

using OneLine.Enums;
using OneLine.Models;

namespace OneLine.Extensions
{
    public static class ApiResponseStatusExtensions
    {
        /// <summary>
        /// Checks if the Api Response Status Succeeded
        /// </summary>
        /// <param name="apiResponseStatus"></param>
        /// <returns></returns>

        public static bool Succeeded(this ApiResponseStatus apiResponseStatus)
        {
            return apiResponseStatus.Equals(ApiResponseStatus.Succeeded);
        }
        /// <summary>
        /// Checks if the Api Response Status Failed 
        /// </summary>
        /// <param name="apiResponseStatus"></param>
        /// <returns></returns>

        public static bool Failed(this ApiResponseStatus apiResponseStatus)
        {
            return apiResponseStatus.Equals(ApiResponseStatus.Failed);
        }
    }
}

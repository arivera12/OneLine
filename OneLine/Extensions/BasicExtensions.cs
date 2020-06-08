using Microsoft.AspNetCore.Mvc;
using OneLine.Enums;
using OneLine.Models;

namespace OneLine.Extensions
{
    public static class BasicExtensions
    {
        public static IActionResult ToJsonActionResult<T>(this T objType, bool ConverCamelCaseNaming = false) where T : class
        {
            return new ContentResult().OutputJson(objType, ConverCamelCaseNaming);
        }

        public static IActionResult ToJsonActionResultApiResponse<T>(this T objType, bool ConverCamelCaseNaming = false) where T : class
        {
            return new ContentResult().OutputJson(new ApiResponse<T> { Status = ApiResponseStatus.Succeeded, Data = objType }, ConverCamelCaseNaming);
        }

        public static IActionResult ToJsonActionResultApiResponse<T>(this T objType, string message, bool ConverCamelCaseNaming = false) where T : class
        {
            return new ContentResult().OutputJson(new ApiResponse<T> { Status = ApiResponseStatus.Succeeded, Data = objType, Message = message }, ConverCamelCaseNaming);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OneLine.Extensions
{
    public static class BasicExtensions
    {
        /// <summary>
        /// Converts <typeparamref name="T"/> to a json <see cref="IActionResult"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objType"></param>
        /// <param name="ConverCamelCaseNaming"></param>
        /// <returns></returns>
        public static IActionResult ToJsonActionResult<T>(this T objType, bool ConverCamelCaseNaming = false) where T : class
        {
            return new ContentResult().OutputJson(objType, ConverCamelCaseNaming);
        }
        /// <summary>
        /// Converts <typeparamref name="T"/> to a json <see cref="IActionResult"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objType"></param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns></returns>
        public static IActionResult ToJsonActionResult<T>(this T objType, JsonSerializerSettings jsonSerializerSettings) where T : class
        {
            return new ContentResult().OutputJson(objType, jsonSerializerSettings);
        }
    }
}
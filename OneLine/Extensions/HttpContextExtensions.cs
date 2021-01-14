using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace OneLine.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets the current executing controller name
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string CurrentControllerName(this HttpContext httpContext)
        {
            return httpContext
                .GetRouteData()
                ?.Values["controller"]
                ?.ToString();
        }
        /// <summary>
        /// Gets the current executing controller action name
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string CurrentControllerActionName(this HttpContext httpContext)
        {
            return httpContext
                .GetRouteData()
                ?.Values["action"]
                ?.ToString();
        }
        /// <summary>
        /// Gets the current host base url
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string HostBaseUrl(this HttpContext httpContext)
        {
            var request = httpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }
        /// <summary>
        /// Gets the first lang code from accept-language header
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetLangCode(this HttpContext httpContext)
        {
            return httpContext.Request.Headers.TryGetValue("accept-language", out StringValues value) ?
                value.ToString().Split(',')[0] :
                null;
        }
        /// <summary>
        /// Gets all lang codes from accept-language header
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetLangCodes(this HttpContext httpContext)
        {
            return httpContext.Request.Headers.TryGetValue("accept-language", out StringValues value) ?
                value.ToString().Split(',').ToList() :
                null;
        }
    }
}

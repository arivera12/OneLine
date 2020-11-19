using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace OneLine.Extensions
{
    public static class HttpContextExtensions
    {
        public static string CurrentControllerName(this HttpContext httpContext)
        {
            return httpContext
                .GetRouteData()
                ?.Values["controller"]
                ?.ToString();
        }
        public static string CurrentControllerActionName(this HttpContext httpContext)
        {
            return httpContext
                .GetRouteData()
                ?.Values["action"]
                ?.ToString();
        }
        public static string HostBaseUrl(this HttpContext httpContext)
        {
            var request = httpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }
        public static string GetLangCode(this HttpContext httpContext)
        {
            return httpContext.Request.Headers.TryGetValue("accept-language", out StringValues value) ?
                value.ToString().Split(',')[0] :
                null;
        }
        public static IEnumerable<string> GetLangCodes(this HttpContext httpContext)
        {
            return httpContext.Request.Headers.TryGetValue("accept-language", out StringValues value) ?
                value.ToString().Split(',').ToList() :
                null;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace OneLine.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string CurrentControllerName(this IHttpContextAccessor HttpContextAccessor)
        {
            return HttpContextAccessor
                .HttpContext
                .GetRouteData()
                ?.Values["controller"]
                ?.ToString();
        }
        public static string CurrentControllerActionName(this IHttpContextAccessor HttpContextAccessor)
        {
            return HttpContextAccessor
                .HttpContext
                .GetRouteData()
                ?.Values["action"]
                ?.ToString();
        }
        public static string HostBaseUrl(this IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }
        public static string GetLangCode(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext.Request.Headers.TryGetValue("accept-language", out StringValues value) ?
                value.ToString().Split(',')[0] :
                null;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace OneLine.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetLangCode(this ControllerBase controller)
        {
            return controller.HttpContext.Request.Headers.TryGetValue("accept-language", out StringValues value) ?
                value.ToString().Split(',')[0] :
                null;
        }

        public static IEnumerable<string> GetLangCodes(this ControllerBase controller)
        {
            return controller.HttpContext.Request.Headers.TryGetValue("accept-language", out StringValues value) ?
                value.ToString().Split(',').ToList() :
                null;
        }
    }
}

using System;

namespace OneLine.Extensions.Shared
{
    public static class ExceptionExtensions
    {
        public static bool IsInvalidJsonString(this Exception exception)
        {
            return exception.Message == "Invalid JSON string";
        }

        public static bool IsUnauthorized(this Exception exception)
        {
            return exception.Message == "401 (Unauthorized)";
        }

        public static bool IsInternalServerError(this Exception exception)
        {
            return exception.Message == "500 (Internal Server Error)";
        }
    }
}

using System;

namespace OneLine.Extensions
{
    public static class GuidExtensions
    {
        public static string GenerateGuid(this Guid guid)
        {
            return Guid.NewGuid().ToString();
        }
    }
}


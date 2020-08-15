using OneLine.Bases;
using System;

namespace OneLine.Extensions
{
    public static class HttpServiceExtensions
    {
        public static void SetHttpClientServiceBaseAddress(this IHttpService httpService, string baseAddress)
        {
            httpService.HttpClient.BaseAddress = new Uri(baseAddress);
        }
        public static void SetHttpClientServiceBaseAddress(this IHttpService httpService, Uri baseAddress)
        {
            httpService.HttpClient.BaseAddress = baseAddress;
        }
    }
}

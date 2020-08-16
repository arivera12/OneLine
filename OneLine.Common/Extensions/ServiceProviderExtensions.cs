using Microsoft.Extensions.DependencyInjection;
using OneLine.Bases;
using System;

namespace OneLine.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void SetHttpClientServiceBaseAddress<THttpService>(this IServiceProvider serviceProvider, string baseAddress)
            where THttpService : IHttpService
        {
            serviceProvider.GetRequiredService<THttpService>().HttpClient.BaseAddress = new Uri(baseAddress);
        }
        public static void SetHttpClientServiceBaseAddress<THttpService>(this IServiceProvider serviceProvider, Uri baseAddress)
            where THttpService : IHttpService
        {
            serviceProvider.GetRequiredService<THttpService>().HttpClient.BaseAddress = baseAddress;
        }
    }
}

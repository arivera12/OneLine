using Microsoft.Extensions.DependencyInjection;
using OneLine.Bases;
using System;
using System.Linq;
using System.Reflection;

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
        public static void SetHttpClientServiceBaseAddressFromAssembly<THttpService>(this IServiceProvider serviceProvider, Assembly assembly, string baseAddress)
            where THttpService : IHttpService
        {
            foreach(var type in assembly.GetTypes().Where(w => w.IsAssignableFrom(typeof(THttpService))))
            {
                ((THttpService)serviceProvider.GetRequiredService(type)).HttpClient.BaseAddress = new Uri(baseAddress);
            }
        }
        public static void SetHttpClientServiceBaseAddressFromAssembly<THttpService>(this IServiceProvider serviceProvider, Assembly assembly, Uri baseAddress)
            where THttpService : IHttpService
        {
            foreach (var type in assembly.GetTypes().Where(w => w.IsAssignableFrom(typeof(THttpService))))
            {
                ((THttpService)serviceProvider.GetRequiredService(type)).HttpClient.BaseAddress = baseAddress;
            }
        }
        public static void SetHttpClientServiceBaseAddressFromAssemblyContaining<T>(this IServiceProvider serviceProvider, string baseAddress)
        {
            foreach (var type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(w => w.IsAssignableFrom(typeof(IHttpService))))
            {
                ((IHttpService)serviceProvider.GetRequiredService(type)).HttpClient.BaseAddress = new Uri(baseAddress);
            }
        }
        public static void SetHttpClientServiceBaseAddressFromAssemblyContaining<T>(this IServiceProvider serviceProvider, Uri baseAddress)
        {
            foreach (var type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(w => w.IsAssignableFrom(typeof(IHttpService))))
            {
                ((IHttpService)serviceProvider.GetRequiredService(type)).HttpClient.BaseAddress = baseAddress;
            }
        }
    }
}

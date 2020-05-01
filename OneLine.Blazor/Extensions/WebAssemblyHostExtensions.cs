using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneLine.Blazor.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace OneLine.Blazor.Extensions
{
    public static class WebAssemblyHostExtensions
    {
        public static void AddBaseAddressHttpClient(this WebAssemblyHost webAssemblyHost, string baseAddress)
        {
            var httpClient = webAssemblyHost.Services.GetRequiredService<HttpClient>();
            httpClient.BaseAddress = new Uri(baseAddress);
        }
        public static void AddEnvironmentConfiguration<TResource>(this WebAssemblyHostBuilder webAssemblyHostBuilder, string baseUri, Func<EnvironmentChooser> environmentChooserFactory)
        {
            var environementChooser = environmentChooserFactory();
            var uri = new Uri(baseUri);
            Assembly assembly = typeof(TResource).Assembly;
            string environment = environementChooser.GetCurrent(uri);
            var ressourceNames = new[]
            {
                assembly.GetName().Name + ".Configuration.appsettings.json",
                assembly.GetName().Name + ".Configuration.appsettings." + environment + ".json"
            };
            webAssemblyHostBuilder.Configuration.AddInMemoryCollection(new Dictionary<string, string>()
            {
                    { "Environment", environment }
            });
            foreach (var resource in ressourceNames)
            {
                if (assembly.GetManifestResourceNames().Contains(resource))
                {
                    webAssemblyHostBuilder.Configuration.AddJsonFile(new InMemoryFileProvider(assembly.GetManifestResourceStream(resource)), resource, false, false);
                }
            }
        }
    }
}
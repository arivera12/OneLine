using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazor.FileReader;
using BlazorDownloadFile;
using CurrieTechnologies.Razor.SweetAlert2;

namespace OneLine.Blazor.Demo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazorDownloadFile();

            builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);

            builder.Services.AddSweetAlert2();

            await builder.Build().RunAsync();
        }
    }
}

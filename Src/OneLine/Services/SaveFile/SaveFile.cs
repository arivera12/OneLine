using BlazorDownloadFile;
using Microsoft.Extensions.DependencyInjection;
using OneLine.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OneLine.Services
{
    public class SaveFile : ISaveFile
    {
        public IDevice Device { get; set; }
        public IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
        public SaveFile()
        { }
        public SaveFile(IDevice device)
        {
            Device = device;
        }
        public SaveFile(IDevice device, IBlazorDownloadFileService blazorDownloadFileService)
        {
            Device = device;
            BlazorDownloadFileService = blazorDownloadFileService;
        }
        public async Task SaveFileAsync(Stream stream, string path, int bufferSize)
        {
            if (Device.IsWebPlatform)
            {
                await BlazorDownloadFileService.DownloadFile(path, stream, bufferSize);
            }
            else if (Device.IsXamarinPlatform)
            {
                await stream.WriteStreamToFileSystemAsync(path, bufferSize);
            }
            else
            {
                new PlatformNotSupportedException("Saving a file seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        public async Task SaveFileAsync(byte[] byteArray, string path)
        {
            if (Device.IsWebPlatform)
            {
                await BlazorDownloadFileService.DownloadFile(path, byteArray, "application/octet-stream");
            }
            else if (Device.IsXamarinPlatform)
            {
                await File.WriteAllBytesAsync(path, byteArray);
            }
            else
            {
                new PlatformNotSupportedException("Saving a file seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        public async Task SaveFileAsync(Stream stream, string path)
        {
            if (Device.IsWebPlatform)
            {
                await BlazorDownloadFileService.DownloadFile(path, stream, "application/octet-stream");
            }
            else if (Device.IsXamarinPlatform)
            {
                await File.WriteAllBytesAsync(path, await stream.ToByteArrayAsync());
            }
            else
            {
                new PlatformNotSupportedException("Saving a file seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSaveFile(this IServiceCollection services)
        {
            return services.AddScoped<ISaveFile, SaveFile>();
        }
    }
}

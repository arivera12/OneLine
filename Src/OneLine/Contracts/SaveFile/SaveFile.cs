using BlazorDownloadFile;
using OneLine.Extensions;

namespace OneLine.Contracts
{
    public class SaveFile : ISaveFile
    {
        private IDevice Device { get; set; }
        private IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
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
        public async Task SaveFileAsync(Stream stream, string pathOrDownloadFileName, int bufferSize)
        {
            if (Device.IsXamarinPlatform)
            {
                await stream.WriteStreamToFileSystemAsync(pathOrDownloadFileName, bufferSize);
            }
            else if (Device.IsWebPlatform)
            {
                await BlazorDownloadFileService.DownloadFile(pathOrDownloadFileName, stream, bufferSize);
            }
            else
            {
                _ = new PlatformNotSupportedException("Saving a file seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        public async Task SaveFileAsync(byte[] byteArray, string pathOrDownloadFileName)
        {
            if (Device.IsXamarinPlatform)
            {
                await File.WriteAllBytesAsync(pathOrDownloadFileName, byteArray);
            }
            else if (Device.IsWebPlatform)
            {
                await BlazorDownloadFileService.DownloadFile(pathOrDownloadFileName, byteArray, "application/octet-stream");
            }
            else
            {
                _ = new PlatformNotSupportedException("Saving a file seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        public async Task SaveFileAsync(Stream stream, string pathOrDownloadFileName)
        {
            if (Device.IsXamarinPlatform)
            {
                await File.WriteAllBytesAsync(pathOrDownloadFileName, await stream.ToByteArrayAsync());
            }
            else if (Device.IsWebPlatform)
            {
                await BlazorDownloadFileService.DownloadFile(pathOrDownloadFileName, stream, "application/octet-stream");
            }
            else
            {
                _ = new PlatformNotSupportedException("Saving a file seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
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

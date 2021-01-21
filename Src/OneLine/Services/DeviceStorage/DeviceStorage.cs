using BlazorBrowserStorage;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Services
{
    public class DeviceStorage : IDeviceStorage
    {
        private IDevice Device { get; set; }
        private ILocalStorage LocalStorage { get; set; }
        private ISessionStorage SessionStorage { get; set; }
        private IDictionary<string, string> SessionStorageDictionary { get; set; }
        public DeviceStorage(IDevice device)
        {
            Device = device;
            SessionStorageDictionary = new Dictionary<string, string>();
        }
        public DeviceStorage(IDevice device, ILocalStorage localStorage, ISessionStorage sessionStorage)
        {
            Device = device;
            SessionStorageDictionary = new Dictionary<string, string>();
            LocalStorage = localStorage;
            SessionStorage = sessionStorage;
        }
        /// <inheritdoc/>
        public async Task Clear(bool useDevicePersistentStorageProvider = false)
        {
            if (Device.IsXamarinPlatform)
            {
                if (useDevicePersistentStorageProvider)
                {
                    Xamarin.Essentials.SecureStorage.RemoveAll();
                }
                else
                {
                    SessionStorageDictionary.Clear();
                }
            }
            else if (Device.IsWebPlatform)
            {
                if (useDevicePersistentStorageProvider)
                {
                    await LocalStorage.Clear();
                }
                else
                {
                    await SessionStorage.Clear();
                }
            }
        }
        /// <inheritdoc/>
        public async Task<T> GetItem<T>(string key, bool useDevicePersistentStorageProvider = false)
        {
            if (Device.IsXamarinPlatform)
            {
                if (useDevicePersistentStorageProvider)
                {
                    var stringValue = await Xamarin.Essentials.SecureStorage.GetAsync(key);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringValue);
                }
                else
                {
                    var stringValue = SessionStorageDictionary[key];
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringValue);
                }
            }
            else if (Device.IsWebPlatform)
            {
                if (useDevicePersistentStorageProvider)
                {
                    return await LocalStorage.GetItem<T>(key);
                }
                else
                {
                    return await SessionStorage.GetItem<T>(key);
                }
            }
            return default;
        }
        /// <inheritdoc/>
        public async Task RemoveItem(string key, bool useDevicePersistentStorageProvider = false)
        {
            if (Device.IsXamarinPlatform)
            {
                if (useDevicePersistentStorageProvider)
                {
                    Xamarin.Essentials.SecureStorage.Remove(key);
                }
                else
                {
                    SessionStorageDictionary.Remove(key);
                }
            }
            else if (Device.IsWebPlatform)
            {
                if (useDevicePersistentStorageProvider)
                {
                    await LocalStorage.RemoveItem(key);
                }
                else
                {
                    await SessionStorage.RemoveItem(key);
                }
            }
        }
        /// <inheritdoc/>
        public async Task SetItem<T>(string key, T item, bool useDevicePersistentStorageProvider = false)
        {
            if (Device.IsXamarinPlatform)
            {
                if (useDevicePersistentStorageProvider)
                {
                    await Xamarin.Essentials.SecureStorage.SetAsync(key, Newtonsoft.Json.JsonConvert.SerializeObject(item));
                }
                else
                {
                    SessionStorageDictionary.Add(key, Newtonsoft.Json.JsonConvert.SerializeObject(item));
                }
            }
            else if (Device.IsWebPlatform)
            {
                if (useDevicePersistentStorageProvider)
                {
                    await LocalStorage.SetItem<T>(key, item);
                }
                else
                {
                    await SessionStorage.SetItem<T>(key, item);
                }
            }
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeviceStorage(this IServiceCollection services)
        {
            return services.AddScoped<IDeviceStorage, DeviceStorage>();
        }
    }
}

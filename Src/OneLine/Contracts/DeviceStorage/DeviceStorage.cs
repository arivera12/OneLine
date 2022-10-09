using BlazorBrowserStorage;

namespace OneLine.Contracts
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
                    SecureStorage.RemoveAll();
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
                    var stringValue = await SecureStorage.GetAsync(key);
                    if (!string.IsNullOrWhiteSpace(stringValue))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringValue);
                    }
                }
                else
                {
                    if (SessionStorageDictionary.ContainsKey(key))
                    {
                        var stringValue = SessionStorageDictionary[key];
                        if (!string.IsNullOrWhiteSpace(stringValue))
                        {
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringValue);
                        }
                    }
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
                    SecureStorage.Remove(key);
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
                    await SecureStorage.SetAsync(key, Newtonsoft.Json.JsonConvert.SerializeObject(item));
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
                    await LocalStorage.SetItem(key, item);
                }
                else
                {
                    await SessionStorage.SetItem(key, item);
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

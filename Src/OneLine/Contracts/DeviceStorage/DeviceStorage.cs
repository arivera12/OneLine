using BlazorBrowserStorage;

namespace OneLine.Contracts
{
    public class DeviceStorage : IDeviceStorage
    {
        private ILocalStorage LocalStorage { get; set; }
        private ISessionStorage SessionStorage { get; set; }
        private IDictionary<string, string> SessionStorageDictionary { get; set; }
        public DeviceStorage(ILocalStorage localStorage, ISessionStorage sessionStorage)
        {
            SessionStorageDictionary = new Dictionary<string, string>();
            LocalStorage = localStorage;
            SessionStorage = sessionStorage;
        }
        /// <inheritdoc/>
        public async Task Clear(bool useDevicePersistentStorageProvider = false)
        {
            if (useDevicePersistentStorageProvider)
            {
                
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                Directory.Delete(FileSystem.Current.AppDataDirectory, true);                  
#else
                await LocalStorage.Clear();
#endif
            }
            else
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                SessionStorageDictionary.Clear();                   
#else
                await SessionStorage.Clear();
#endif
            }
        }
        /// <inheritdoc/>
        public async Task<T> GetItem<T>(string key, bool useDevicePersistentStorageProvider = false)
        {
            if (useDevicePersistentStorageProvider)
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                if(File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, key)))
                {
                    var stringValue = await File.ReadAllTextAsync(Path.Combine(FileSystem.Current.AppDataDirectory, key));
                    if (!string.IsNullOrWhiteSpace(stringValue))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringValue);
                    }
                }
                return default(T);
#else
                return await LocalStorage.GetItem<T>(key);
#endif
            }
            else
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                if (SessionStorageDictionary.ContainsKey(key))
                {
                    var stringValue = SessionStorageDictionary[key];
                    if (!string.IsNullOrWhiteSpace(stringValue))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringValue);
                    }
                    else
                    {
                        return default(T);
                    }
                }
                return default(T);
#else
                return await SessionStorage.GetItem<T>(key);
#endif     
            }
        }
        /// <inheritdoc/>
        public async Task RemoveItem(string key, bool useDevicePersistentStorageProvider = false)
        {
            if (useDevicePersistentStorageProvider)
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                if(File.Exists(Path.Combine(FileSystem.Current.AppDataDirectory, key)))
                {
                    File.Delete(Path.Combine(FileSystem.Current.AppDataDirectory, key));
                }
#else
                await LocalStorage.RemoveItem(key);
#endif
            }
            else
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                SessionStorageDictionary.Remove(key);
#else
                await SessionStorage.RemoveItem(key);
#endif
            }
        }
        /// <inheritdoc/>
        public async Task SetItem<T>(string key, T item, bool useDevicePersistentStorageProvider = false)
        {
            if (useDevicePersistentStorageProvider)
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                await File.WriteAllTextAsync(Path.Combine(FileSystem.Current.AppDataDirectory, key), Newtonsoft.Json.JsonConvert.SerializeObject(item));
#else
                await LocalStorage.SetItem(key, item);
#endif
            }
            else
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                SessionStorageDictionary.Add(Path.Combine(FileSystem.Current.AppDataDirectory, key), Newtonsoft.Json.JsonConvert.SerializeObject(item));
#else
                await SessionStorage.SetItem(key, item);
#endif
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

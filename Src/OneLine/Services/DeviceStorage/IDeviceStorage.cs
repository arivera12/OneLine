using System.Threading.Tasks;

namespace OneLine.Services
{
    /// <summary>
    /// A service which manage the storage of a device
    /// </summary>
    public interface IDeviceStorage
    {
        /// <summary>
        /// Gets an item from the device storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key of the item to look up in the device storage</param>
        /// <param name="useDevicePersistentStorageProvider">Tells to use persistent storage provider of the device</param>
        /// <returns></returns>
        Task<T> GetItem<T>(string key, bool useDevicePersistentStorageProvider = false);
        /// <summary>
        /// Removes an item from the device storage
        /// </summary>
        /// <param name="key">The key of the item to look up in the device storage</param>
        /// <param name="useDevicePersistentStorageProvider">Tells to use persistent storage provider of the device</param>
        /// <returns></returns>
        Task RemoveItem(string key, bool useDevicePersistentStorageProvider = false);
        /// <summary>
        /// Sets an item into the device storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key of the item to look up in the device storage</param>
        /// <param name="item">The item to be stored</param>
        /// <param name="useDevicePersistentStorageProvider">Tells to use persistent storage provider of the device</param>
        /// <returns></returns>
        Task SetItem<T>(string key, T item, bool useDevicePersistentStorageProvider = false);
        /// <summary>
        /// Clears all items in the device storage
        /// </summary>
        /// <param name="useDevicePersistentStorageProvider">Tells to use persistent storage provider of the device</param>
        /// <returns></returns>
        Task Clear(bool useDevicePersistentStorageProvider = false);
    }
}

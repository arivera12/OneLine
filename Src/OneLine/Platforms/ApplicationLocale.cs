using OneLine.Contracts;

namespace OneLine
{
    public class ApplicationLocale : IApplicationLocale
    {
        private string ApplicationLocaleStorageKey { get; set; }
        private IDeviceStorage DeviceStorage { get; set; }
        public ApplicationLocale(IDeviceStorage deviceStorage, string applicationLocaleStorageKey)
        {
            DeviceStorage = deviceStorage;
            ApplicationLocaleStorageKey = applicationLocaleStorageKey;
        }
        /// <inheritdoc/>
        public async Task<string> GetApplicationLocale()
        {
            return await DeviceStorage.GetItem<string>(ApplicationLocaleStorageKey, true);
        }
        /// <inheritdoc/>
        public async Task SetApplicationLocale(string applicationLocale)
        {
            await DeviceStorage.SetItem(ApplicationLocaleStorageKey, applicationLocale, true);
        }
    }
}

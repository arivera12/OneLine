using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Extensions;
using System;
using System.Threading.Tasks;

namespace OneLine.Services
{
    public class ApplicationState : IApplicationState
    {
        private string UserStorageKey { get; set; }
        private string EncryptionKeyStorageKey { get; set; }
        private string ApplicationSessionStorageKey { get; set; }
        private IDeviceStorage DeviceStorage { get; set; }
        public ApplicationState(IDeviceStorage deviceStorage, string userStorageKey, string encryptionKeyStorageKey, string applicationSessionStorageKey)
        {
            DeviceStorage = deviceStorage;
            UserStorageKey = userStorageKey;
            EncryptionKeyStorageKey = encryptionKeyStorageKey;
            ApplicationSessionStorageKey = applicationSessionStorageKey;
        }
        /// <inheritdoc/>
        public Task<ApplicationSession> GetApplicationSession()
        {
            return DeviceStorage.GetItem<ApplicationSession>(ApplicationSessionStorageKey, true);
        }
        /// <inheritdoc/>
        public async Task SetApplicationSession(ApplicationSession applicationSession)
        {
            await DeviceStorage.SetItem(ApplicationSessionStorageKey, applicationSession, true);
        }
        /// <inheritdoc/>
        public async Task<string> GetApplicationEncryptionKey()
        {
            return await DeviceStorage.GetItem<string>(EncryptionKeyStorageKey, true);
        }
        /// <inheritdoc/>
        public async Task SetApplicationEncryptionKey(string encryptionKey)
        {
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                throw new ArgumentNullException(nameof(encryptionKey));
            }
            await DeviceStorage.SetItem(EncryptionKeyStorageKey, encryptionKey, true);
        }
        /// <inheritdoc/>
        public async Task<TUser> GetApplicationUserSecure<TUser>()
        {
            try
            {
                var applicationSession = await GetApplicationSession();
                var encryptedUser = await DeviceStorage.GetItem<string>(UserStorageKey, applicationSession.IsPersistent());
                var applicationEncryptionKey = await GetApplicationEncryptionKey();
                var jsonUser = encryptedUser.Decrypt(applicationEncryptionKey);
                return JsonConvert.DeserializeObject<TUser>(jsonUser);
            }
            catch (Exception)
            {
                return default;
            }
        }
        /// <inheritdoc/>
        public async Task SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession)
        {
            if (user.IsNull())
            {
                throw new ArgumentNullException(nameof(user));
            }
            var jsonUser = JsonConvert.SerializeObject(user);
            var applicationEncryptionKey = await GetApplicationEncryptionKey();
            await DeviceStorage.SetItem(UserStorageKey, jsonUser.Encrypt(applicationEncryptionKey), applicationSession.IsPersistent());
        }
        /// <inheritdoc/>
        public async Task Logout()
        {
            var applicationSession = await GetApplicationSession();
            await DeviceStorage.RemoveItem(UserStorageKey, applicationSession.IsPersistent());
            await DeviceStorage.RemoveItem(EncryptionKeyStorageKey, true);
            await DeviceStorage.RemoveItem(ApplicationSessionStorageKey, true);
        }
    }
}

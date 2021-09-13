using BlazorBrowserStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace OneLine.Services
{
    public class ApplicationState : IApplicationState
    {
        private string SessionSecureUser { get; set; }
        private string SessionKey { get; set; }
        private NavigationManager NavigationManager { get; set; }
        private ISessionStorage SessionStorage { get; set; }
        private ILocalStorage LocalStorage { get; set; }
        private IDevice Device { get; set; }
        private IJSRuntime JsRuntime { get; set; }
        public ApplicationState()
        {
        }
        public ApplicationState(IDevice device)
        {
            Device = device;
        }
        public ApplicationState(NavigationManager navigationManager, IDevice device)
        {
            NavigationManager = navigationManager;
            Device = device;
        }
        public ApplicationState(NavigationManager navigationManager, IDevice device, IJSRuntime jsRuntime)
        {
            NavigationManager = navigationManager;
            Device = device;
            JsRuntime = jsRuntime;
        }
        public ApplicationState(NavigationManager navigationManager, IDevice device, ISessionStorage sessionStorage, ILocalStorage localStorage, IJSRuntime jsRuntime)
        {
            NavigationManager = navigationManager;
            Device = device;
            SessionStorage = sessionStorage;
            LocalStorage = localStorage;
            JsRuntime = jsRuntime;
        }
        /// <inheritdoc/>
        public async ValueTask<TUser> GetApplicationUserSecure<TUser>()
        {
            try
            {
                string SUser, key, decryptedUser = null;
                var applicationSession = await GetApplicationSession();
                if (Device.IsXamarinPlatform &&
                    (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                    !Device.IsDesktop && 
                    applicationSession.Equals(ApplicationSession.LocalStorage))
                {
                    SUser = await SecureStorage.GetAsync("SUser");
                    return JsonConvert.DeserializeObject<TUser>(SUser);
                }
                else if (Device.IsXamarinPlatform &&
                    (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                    !Device.IsDesktop && 
                    applicationSession.Equals(ApplicationSession.SessionStorage))
                {
                    return JsonConvert.DeserializeObject<TUser>(SessionSecureUser.Decrypt(SessionKey));
                }
                else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.LocalStorage))
                {
                    var applicationDataUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SUser");
                    var applicationDataKeyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DUEK");
                    if (File.Exists(applicationDataUserPath) && File.Exists(applicationDataKeyPath))
                    {
                        SUser = File.ReadAllText(applicationDataUserPath);
                        key = File.ReadAllText(applicationDataKeyPath);
                        decryptedUser = SUser.Decrypt(key);
                        return JsonConvert.DeserializeObject<TUser>(SUser);
                    }
                }
                else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.SessionStorage))
                {
                    return JsonConvert.DeserializeObject<TUser>(SessionSecureUser.Decrypt(SessionKey));
                }
                else if (Device.IsWebPlatform)
                {
                    if (applicationSession == ApplicationSession.LocalStorage)
                    {
                        key = await LocalStorage.GetItem<string>("DUEK");
                        SUser = await LocalStorage.GetItem<string>("SUser");
                    }
                    else
                    {
                        key = await SessionStorage.GetItem<string>("DUEK");
                        SUser = await SessionStorage.GetItem<string>("SUser");
                    }
                    decryptedUser = SUser.Decrypt(key);
                    return JsonConvert.DeserializeObject<TUser>(decryptedUser);
                }
                else
                {
                    new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
                }
            }
            catch
            {
                return default;
            }
            return default;
        }
        /// <inheritdoc/>
        public async ValueTask SetApplicationUserSecure<TUser>(TUser user)
        {
            var jsonUser = JsonConvert.SerializeObject(user);
            var applicationSession = await GetApplicationSession();
            if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop && 
                applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                await SecureStorage.SetAsync("SUser", jsonUser);
            }
            else if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop && 
                applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionKey = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                SessionKey = SessionKey.Encrypt(SessionKey);
                SessionSecureUser = jsonUser.Encrypt(SessionKey);
            }
            else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                key = key.Encrypt(key);
                string jsonUserEncrypted = jsonUser.Encrypt(key);
                var applicationDataUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SUser");
                File.WriteAllText(applicationDataUserPath, jsonUserEncrypted);
                var applicationDataKeyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DUEK");
                File.WriteAllText(applicationDataKeyPath, key);
            }
            else if (Device.IsXamarinPlatform &&
                (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionKey = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                SessionKey = SessionKey.Encrypt(SessionKey);
                SessionSecureUser = jsonUser.Encrypt(SessionKey);
            }
            else if (Device.IsWebPlatform)
            {
                var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                key = key.Encrypt(key);
                string jsonUserEncrypted = jsonUser.Encrypt(key);
                if (applicationSession == ApplicationSession.LocalStorage)
                {
                    await LocalStorage.SetItem("DUEK", key);
                    await LocalStorage.SetItem("SUser", jsonUserEncrypted);
                }
                else
                {
                    await SessionStorage.SetItem("DUEK", key);
                    await SessionStorage.SetItem("SUser", jsonUserEncrypted);
                }
            }
            else
            {
                new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        /// <inheritdoc/>
        public async ValueTask SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession)
        {
            var jsonUser = JsonConvert.SerializeObject(user);
            if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                await SecureStorage.SetAsync("SUser", jsonUser);
            }
            else if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionKey = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                SessionKey = SessionKey.Encrypt(SessionKey);
                SessionSecureUser = jsonUser.Encrypt(SessionKey);
            }
            else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                key = key.Encrypt(key);
                string jsonUserEncrypted = jsonUser.Encrypt(key);
                var applicationDataUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SUser");
                File.WriteAllText(applicationDataUserPath, jsonUserEncrypted);
                var applicationDataKeyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DUEK");
                File.WriteAllText(applicationDataKeyPath, key);
            }
            else if (Device.IsXamarinPlatform &&
                (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionKey = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                SessionKey = SessionKey.Encrypt(SessionKey);
                SessionSecureUser = jsonUser.Encrypt(SessionKey);
            }
            else if (Device.IsWebPlatform)
            {
                var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                key = key.Encrypt(key);
                string jsonUserEncrypted = jsonUser.Encrypt(key);
                if (applicationSession == ApplicationSession.LocalStorage)
                {
                    await LocalStorage.SetItem("DUEK", key);
                    await LocalStorage.SetItem("SUser", jsonUserEncrypted);
                }
                else
                {
                    await SessionStorage.SetItem("DUEK", key);
                    await SessionStorage.SetItem("SUser", jsonUserEncrypted);
                }
            }
            else
            {
                new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        /// <inheritdoc/>
        public async ValueTask<TUser> GetApplicationUser<TUser>()
        {
            try
            {
                var applicationSession = await GetApplicationSession();
                if (Device.IsXamarinPlatform &&
                    (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                    !Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.LocalStorage))
                {
                    return JsonConvert.DeserializeObject<TUser>(await SecureStorage.GetAsync("User"));
                }
                else if (Device.IsXamarinPlatform &&
                    (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                    !Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.SessionStorage))
                {
                    return JsonConvert.DeserializeObject<TUser>(SessionSecureUser);
                }
                else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.LocalStorage))
                {
                    var applicationDataUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "User");
                    if (File.Exists(applicationDataUserPath))
                    {
                        return JsonConvert.DeserializeObject<TUser>(File.ReadAllText(applicationDataUserPath));
                    }
                }
                else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.SessionStorage))
                {
                    return JsonConvert.DeserializeObject<TUser>(SessionSecureUser);
                }
                else if (Device.IsWebPlatform)
                {
                    if (applicationSession == ApplicationSession.LocalStorage)
                    {
                        SessionSecureUser = await LocalStorage.GetItem<string>("User");
                    }
                    else
                    {
                        SessionSecureUser = await SessionStorage.GetItem<string>("User");
                    }
                    return JsonConvert.DeserializeObject<TUser>(SessionSecureUser);
                }
                else
                {
                    new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
                }
            }
            catch
            {
                return default;
            }
            return default;
        }
        /// <inheritdoc/>
        public async ValueTask SetApplicationUser<TUser>(TUser user)
        {
            var jsonUser = JsonConvert.SerializeObject(user);
            var applicationSession = await GetApplicationSession();
            if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                await SecureStorage.SetAsync("User", jsonUser);
            }
            else if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionSecureUser = jsonUser;
            }
            else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                var applicationDataUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "User");
                File.WriteAllText(applicationDataUserPath, jsonUser);
            }
            else if (Device.IsXamarinPlatform &&
                (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionSecureUser = jsonUser;
            }
            else if (Device.IsWebPlatform)
            {
                if (applicationSession == ApplicationSession.LocalStorage)
                {
                    await LocalStorage.SetItem("User", jsonUser);
                }
                else
                {
                    await SessionStorage.SetItem("User", jsonUser);
                }
            }
            else
            {
                new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        /// <inheritdoc/>
        public async ValueTask SetApplicationUser<TUser>(TUser user, ApplicationSession applicationSession)
        {
            var jsonUser = JsonConvert.SerializeObject(user);
            if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                await SecureStorage.SetAsync("User", jsonUser);
            }
            else if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionSecureUser = jsonUser;
            }
            else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop &&
                    applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                var applicationDataUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "User");
                File.WriteAllText(applicationDataUserPath, jsonUser);
            }
            else if (Device.IsXamarinPlatform &&
                (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                Device.IsDesktop &&
                applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionSecureUser = jsonUser.Encrypt(SessionKey);
            }
            else if (Device.IsWebPlatform)
            {
                if (applicationSession == ApplicationSession.LocalStorage)
                {
                    await LocalStorage.SetItem("User", jsonUser);
                }
                else
                {
                    await SessionStorage.SetItem("User", jsonUser);
                }
            }
            else
            {
                new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        /// <inheritdoc/>
        public async ValueTask Logout()
        {
            if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop)
            {
                SecureStorage.Remove("SUser");
                SecureStorage.Remove("DUEK");
                SessionKey = null;
                SessionSecureUser = null;
            }
            else if(Device.IsXamarinPlatform &&
                (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                Device.IsDesktop)
            {
                var applicationDataUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SUser");
                if(File.Exists(applicationDataUserPath))
                {
                    File.Delete(applicationDataUserPath);
                }
                var applicationDataKeyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DUEK");
                if (File.Exists(applicationDataKeyPath))
                {
                    File.Delete(applicationDataKeyPath);
                }
                SessionKey = null;
                SessionSecureUser = null;
            }
            else if (Device.IsWebPlatform)
            {
                await LocalStorage.RemoveItem("SUser");
                await SessionStorage.RemoveItem("SUser");
                await LocalStorage.RemoveItem("DUEK");
                await SessionStorage.RemoveItem("DUEK");
            }
            else
            {
                new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
        /// <inheritdoc/>
        public async ValueTask LogoutAndNavigateTo(string uri, bool forceLoad = false)
        {
            await Logout();
            NavigationManager.NavigateTo(uri, forceLoad);
        }
        /// <inheritdoc/>
        public async ValueTask<ApplicationSession> GetApplicationSession()
        {
            try
            {
                if (Device.IsXamarinPlatform &&
                    (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                    !Device.IsDesktop)
                {
                    return Enum.Parse<ApplicationSession>(await SecureStorage.GetAsync("ApplicationSession"));
                }
                else if(Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop)
                {
                    var applicationDataApplicationSessionPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ApplicationSession");
                    if(File.Exists(applicationDataApplicationSessionPath))
                    {
                        var applicationSessionString = File.ReadAllText(applicationDataApplicationSessionPath);
                        return Enum.Parse<ApplicationSession>(applicationSessionString);
                    }
                }
                else if (Device.IsWebPlatform)
                {
                    return await LocalStorage.GetItem<ApplicationSession>("ApplicationSession");
                }
                else
                {
                    new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
                }
            }
            catch
            {
                return ApplicationSession.LocalStorage;
            }
            return ApplicationSession.LocalStorage;
        }
        /// <inheritdoc/>
        public async ValueTask SetApplicationSession(ApplicationSession applicationSession)
        {
            if (Device.IsXamarinPlatform &&
                (Device.IsiOSDevice || Device.IsAndroidDevice || Device.IsWindowsOSPlatform) &&
                !Device.IsDesktop)
            {
                await SecureStorage.SetAsync("ApplicationSession", applicationSession.ToString());
            }
            else if (Device.IsXamarinPlatform &&
                    (Device.IsMacOSDevice || Device.IsWindowsOSPlatform || Device.IsLinuxOSPlatform) &&
                    Device.IsDesktop)
            {
                var applicationDataApplicationSessionPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ApplicationSession");
                File.WriteAllText(applicationDataApplicationSessionPath, applicationSession.ToString());
            }
            else if (Device.IsWebPlatform)
            {
                await LocalStorage.SetItem("ApplicationSession", applicationSession);
            }
            else
            {
                new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
    }
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationState(this IServiceCollection services)
        {
            return services.AddScoped<IApplicationState, ApplicationState>();
        }
    }
}

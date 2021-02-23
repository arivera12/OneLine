using BlazorBrowserStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Extensions;
using System;
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
                if (Device.IsXamarinPlatform && applicationSession.Equals(ApplicationSession.LocalStorage))
                {
                    SUser = await SecureStorage.GetAsync("SUser");
                    return JsonConvert.DeserializeObject<TUser>(SUser);
                }
                else if (Device.IsXamarinPlatform && applicationSession.Equals(ApplicationSession.SessionStorage))
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
                    if (Device.IsWebBlazorServerPlatform || Device.IsHybridPlatform)
                    {
                        decryptedUser = SUser.Decrypt(key);
                    }
                    else if (Device.IsWebBlazorWAsmPlatform)
                    {
                        decryptedUser = await JsRuntime.InvokeAsync<string>("eval", new[] { $@"CryptoJS.AES.decrypt('{SUser}', '{key}').toString(CryptoJS.enc.Utf8)" });
                    }
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
            if (Device.IsXamarinPlatform && applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                await SecureStorage.SetAsync("SUser", jsonUser);
            }
            else if (Device.IsXamarinPlatform && applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                SessionKey = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                SessionKey = SessionKey.Encrypt(SessionKey);
                SessionSecureUser = jsonUser.Encrypt(SessionKey);
            }
            else if (Device.IsWebPlatform)
            {
                var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                string jsonUserEncrypted = null;
                if (Device.IsWebBlazorServerPlatform || Device.IsHybridPlatform)
                {
                    key = key.Encrypt(key);
                    jsonUserEncrypted = jsonUser.Encrypt(key);
                }
                else if (Device.IsWebBlazorWAsmPlatform)
                {
                    key = await JsRuntime.InvokeAsync<string>("eval", new[] { $@"CryptoJS.AES.encrypt('{key}', '{key}').toString()" });
                    jsonUserEncrypted = await JsRuntime.InvokeAsync<string>("eval", new[] { $@"CryptoJS.AES.encrypt('{jsonUser}', '{key}').toString()" });
                }
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
            if (Device.IsXamarinPlatform && applicationSession.Equals(ApplicationSession.LocalStorage))
            {
                await SecureStorage.SetAsync("SUser", jsonUser);
            }
            else if (Device.IsXamarinPlatform && applicationSession.Equals(ApplicationSession.SessionStorage))
            {
                await SecureStorage.SetAsync("SUser", jsonUser);
                SessionKey = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                SessionKey = SessionKey.Encrypt(SessionKey);
                SessionSecureUser = jsonUser.Encrypt(SessionKey);
            }
            else if (Device.IsWebPlatform)
            {
                var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                string jsonUserEncrypted = null;
                if (Device.IsWebBlazorServerPlatform || Device.IsHybridPlatform)
                {
                    key = key.Encrypt(key);
                    jsonUserEncrypted = jsonUser.Encrypt(key);
                }
                else if (Device.IsWebBlazorWAsmPlatform)
                {
                    key = await JsRuntime.InvokeAsync<string>("eval", new[] { $@"CryptoJS.AES.encrypt('{key}', '{key}').toString()" });
                    jsonUserEncrypted = await JsRuntime.InvokeAsync<string>("eval", new[] { $@"CryptoJS.AES.encrypt('{jsonUser}', '{key}').toString()" });
                }
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
        public async ValueTask Logout()
        {
            if (Device.IsXamarinPlatform)
            {
                SecureStorage.Remove("SUser");
                SecureStorage.Remove("DUEK");
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
                if (Device.IsXamarinPlatform)
                {
                    return Enum.Parse<ApplicationSession>(await SecureStorage.GetAsync("ApplicationSession"));
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
            if (Device.IsXamarinPlatform)
            {
                await SecureStorage.SetAsync("ApplicationSession", applicationSession.ToString());
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

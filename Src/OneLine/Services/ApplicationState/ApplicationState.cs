using BlazorBrowserStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
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
        public NavigationManager NavigationManager { get; set; }
        public ISessionStorage SessionStorage { get; set; }
        public ILocalStorage LocalStorage { get; set; }
        public IDevice Device { get; set; } 
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
        public ApplicationState(NavigationManager navigationManager, IDevice device, ISessionStorage sessionStorage, ILocalStorage localStorage)
        {
            NavigationManager = navigationManager;
            Device = device;
            SessionStorage = sessionStorage;
            LocalStorage = localStorage;
        }
        public async ValueTask<TUser> GetApplicationUserSecure<TUser>()
        {
            try
            {
                string SUser, key;
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
                    return JsonConvert.DeserializeObject<TUser>(SUser.Decrypt(key));
                }
                else
                {
                    new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
                }
            }
            catch (Exception)
            {
                return default;
            }
            return default;
        }
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
                key = key.Encrypt(key);
                if (applicationSession == ApplicationSession.LocalStorage)
                {

                    await LocalStorage.SetItem("DUEK", key);
                    await LocalStorage.SetItem("SUser", jsonUser.Encrypt(key));
                }
                else
                {
                    await SessionStorage.SetItem("DUEK", key);
                    await SessionStorage.SetItem("SUser", jsonUser.Encrypt(key));
                }
            }
            else
            {
                new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
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
                key = key.Encrypt(key);
                if (applicationSession == ApplicationSession.LocalStorage)
                {
                    await LocalStorage.SetItem("DUEK", key);
                    await LocalStorage.SetItem("SUser", jsonUser.Encrypt(key));
                }
                else
                {
                    await SessionStorage.SetItem("DUEK", key);
                    await SessionStorage.SetItem("SUser", jsonUser.Encrypt(key));
                }
            }
            else
            {
                new PlatformNotSupportedException("Application state seems not to be supported by this platform. We could not recognize wether the platform is running on xamarin or blazor");
            }
        }
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
        public async ValueTask LogoutAndNavigateTo(string uri, bool forceReload = false)
        {
            await Logout();
            NavigationManager.NavigateTo(uri, forceReload);
        }
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
            catch (Exception)
            {
                return ApplicationSession.LocalStorage;
            }
            return ApplicationSession.LocalStorage;
        }
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
            return services.AddSingleton<IApplicationState, ApplicationState>();
        }
    }
}

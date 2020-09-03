using BlazorBrowserStorage;
using BlazorMobile.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Extensions;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;

namespace OneLine.Blazor.Services
{
    public class ApplicationState : IApplicationState
    {
        public NavigationManager NavigationManager { get; set; }
        public ISessionStorage SessionStorage { get; set; }
        public ILocalStorage LocalStorage { get; set; }
        public ISecureStorage SecureStorage { get; set; }
        public bool IsBlazorMobileDevice { get; set; }
        public ApplicationState(NavigationManager navigationManager, ISessionStorage sessionStorage, ILocalStorage localStorage)
        {
            NavigationManager = navigationManager;
            SessionStorage = sessionStorage;
            LocalStorage = localStorage;
            IsBlazorMobileDevice = BlazorDevice.RuntimePlatform == BlazorDevice.Android ||
                   BlazorDevice.RuntimePlatform == BlazorDevice.iOS ||
                   BlazorDevice.RuntimePlatform == BlazorDevice.UWP;
        }
        public ApplicationState(NavigationManager navigationManager, ISessionStorage sessionStorage, ILocalStorage localStorage, ISecureStorage secureStorage)
        {
            NavigationManager = navigationManager;
            SessionStorage = sessionStorage;
            LocalStorage = localStorage;
            SecureStorage = secureStorage;
            IsBlazorMobileDevice = BlazorDevice.RuntimePlatform == BlazorDevice.Android ||
                   BlazorDevice.RuntimePlatform == BlazorDevice.iOS ||
                   BlazorDevice.RuntimePlatform == BlazorDevice.UWP;
        }
        public async ValueTask<TUser> GetApplicationUserSecure<TUser>()
        {
            try
            {
                string SUser;
                var applicationSession = await GetApplicationSession();
                if (IsBlazorMobileDevice && applicationSession == ApplicationSession.LocalStorage)
                {
                    SUser = await SecureStorage.GetAsync("SUser");
                    return JsonConvert.DeserializeObject<TUser>(SUser);
                }
                else
                {
                    string key;
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
                    return JsonConvert.DeserializeObject<TUser>(SUser.DecryptData(key));
                }
            }
            catch (Exception)
            {
                return default;
            }
        }
        public async ValueTask SetApplicationUserSecure<TUser>(TUser user)
        {
            var jsonUser = JsonConvert.SerializeObject(user);
            var applicationSession = await GetApplicationSession();
            if (IsBlazorMobileDevice && applicationSession == ApplicationSession.LocalStorage)
            {
                await SecureStorage.SetAsync("SUser", jsonUser);
            }
            else
            {
                var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                key = key.EncryptData(key);
                if (applicationSession == ApplicationSession.LocalStorage)
                {

                    await LocalStorage.SetItem("DUEK", key);
                    await LocalStorage.SetItem("SUser", jsonUser.EncryptData(key));
                }
                else
                {
                    await SessionStorage.SetItem("DUEK", key);
                    await SessionStorage.SetItem("SUser", jsonUser.EncryptData(key));
                }
            }
        }
        public async ValueTask SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession)
        {
            var jsonUser = JsonConvert.SerializeObject(user);
            if (IsBlazorMobileDevice && applicationSession == ApplicationSession.LocalStorage)
            {
                await SecureStorage.SetAsync("SUser", jsonUser);
            }
            else
            {
                var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
                key = key.EncryptData(key);
                if (applicationSession == ApplicationSession.LocalStorage)
                {
                    await LocalStorage.SetItem("DUEK", key);
                    await LocalStorage.SetItem("SUser", jsonUser.EncryptData(key));
                }
                else
                {
                    await SessionStorage.SetItem("DUEK", key);
                    await SessionStorage.SetItem("SUser", jsonUser.EncryptData(key));
                }
            }
        }
        public async ValueTask Logout()
        {
            if (IsBlazorMobileDevice)
            {
                await SecureStorage.Remove("SUser");
            }
            else
            {
                await LocalStorage.RemoveItem("SUser");
                await SessionStorage.RemoveItem("SUser");
                await LocalStorage.RemoveItem("DUEK");
                await SessionStorage.RemoveItem("DUEK");
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
                if (IsBlazorMobileDevice)
                {
                    return Enum.Parse<ApplicationSession>(await SecureStorage.GetAsync("ApplicationSession"));
                }
                else
                {
                    return await LocalStorage.GetItem<ApplicationSession>("ApplicationSession");
                }
            }
            catch (Exception)
            {
                return ApplicationSession.LocalStorage;
            }
        }
        public async ValueTask SetApplicationSession(ApplicationSession applicationSession)
        {
            if (IsBlazorMobileDevice)
            {
                await SecureStorage.SetAsync("ApplicationSession", applicationSession.ToString());
            }
            else
            {
                await LocalStorage.SetItem("ApplicationSession", applicationSession);
            }
        }
        public async ValueTask<string> GetApplicationLocale()
        {
            try
            {
                if (IsBlazorMobileDevice)
                {
                    return await SecureStorage.GetAsync("ApplicationLocale");
                }
                else
                {
                    return await LocalStorage.GetItem<string>("ApplicationLocale");
                }
            }
            catch (Exception)
            {
                return default;
            }
        }
        public async ValueTask SetApplicationLocale(string locale)
        {
            if (IsBlazorMobileDevice)
            {
                await SecureStorage.SetAsync("ApplicationLocale", locale);
            }
            else
            {
                await LocalStorage.SetItem("ApplicationLocale", locale);
            }
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationState(this IServiceCollection services)
        {
            return services.AddScoped<IApplicationState, ApplicationState>();
        }
    }
}

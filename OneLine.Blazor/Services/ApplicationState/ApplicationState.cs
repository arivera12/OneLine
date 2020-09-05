using BlazorBrowserStorage;
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
        public ApplicationState(NavigationManager navigationManager, ISessionStorage sessionStorage, ILocalStorage localStorage)
        {
            NavigationManager = navigationManager;
            SessionStorage = sessionStorage;
            LocalStorage = localStorage;
        }
        public ApplicationState(NavigationManager navigationManager, ISessionStorage sessionStorage, ILocalStorage localStorage, ISecureStorage secureStorage)
        {
            NavigationManager = navigationManager;
            SessionStorage = sessionStorage;
            LocalStorage = localStorage;
            SecureStorage = secureStorage;
        }
        public async ValueTask<TUser> GetApplicationUserSecure<TUser>()
        {
            try
            {
                string SUser;
                var applicationSession = await GetApplicationSession();
                if (SecureStorage.IsNotNull() && applicationSession == ApplicationSession.LocalStorage)
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
            if (SecureStorage.IsNotNull() && applicationSession == ApplicationSession.LocalStorage)
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
            if (SecureStorage.IsNotNull() && applicationSession == ApplicationSession.LocalStorage)
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
            if (SecureStorage.IsNotNull())
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
                if (SecureStorage.IsNotNull())
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
            if (SecureStorage.IsNotNull())
            {
                await SecureStorage.SetAsync("ApplicationSession", applicationSession.ToString());
            }
            else
            {
                await LocalStorage.SetItem("ApplicationSession", applicationSession);
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

using BlazorBrowserStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Extensions;
using System;
using System.Threading.Tasks;

namespace OneLine.Blazor.Services
{
    public class ApplicationState : IApplicationState
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISessionStorage SessionStorage { get; set; }
        [Inject] public ILocalStorage LocalStorage { get; set; }
        public ApplicationState(NavigationManager navigationManager, ISessionStorage sessionStorage, ILocalStorage localStorage)
        {
            NavigationManager = navigationManager;
            SessionStorage = sessionStorage;
            LocalStorage = localStorage;
        }
        public async ValueTask<TUser> GetApplicationUserSecure<TUser>()
        {
            try
            {
                var applicationSession = await GetApplicationSession();
                string SUser;
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
            catch (Exception)
            {
                return default;
            }
        }
        public async ValueTask SetApplicationUserSecure<TUser>(TUser user)
        {
            var applicationSession = await GetApplicationSession();
            var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
            key = key.EncryptData(key);
            var jsonUser = JsonConvert.SerializeObject(user);
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
        public async ValueTask SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession)
        {
            var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
            key = key.EncryptData(key);
            var jsonUser = JsonConvert.SerializeObject(user);
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
        public async ValueTask Logout()
        {
            await LocalStorage.RemoveItem("User");
            await SessionStorage.RemoveItem("User");
            await LocalStorage.RemoveItem("SUser");
            await SessionStorage.RemoveItem("SUser");
            await LocalStorage.RemoveItem("DUEK");
            await SessionStorage.RemoveItem("DUEK");
        }
        public async ValueTask LogoutAndNavigateTo(string uri, bool forceReload = false)
        {
            await Logout();
            NavigationManager.NavigateTo(uri, forceReload);
        }
        public ValueTask<ApplicationSession> GetApplicationSession()
        {
            try
            {
                return LocalStorage.GetItem<ApplicationSession>("ApplicationSession");
            }
            catch (Exception)
            {
                return new ValueTask<ApplicationSession>(ApplicationSession.LocalStorage);
            }
        }
        public ValueTask SetApplicationSession(ApplicationSession applicationSession)
        {
            return LocalStorage.SetItem("ApplicationSession", applicationSession);
        }
        public ValueTask<string> GetApplicationLocale()
        {
            try
            {
                return LocalStorage.GetItem<string>("ApplicationLocale");
            }
            catch (Exception)
            {
                return default;
            }
        }
        public ValueTask SetApplicationLocale(string locale)
        {
            return LocalStorage.SetItem("ApplicationLocale", locale);
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

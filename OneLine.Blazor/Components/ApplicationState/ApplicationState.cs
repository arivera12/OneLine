using BlazorBrowserStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor
{
    public class ApplicationState : ComponentBase
    {
        [Inject] IJSRuntime jSRuntime { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        [Inject] ISessionStorage sessionStorage { get; set; }
        [Inject] ILocalStorage localStorage { get; set; }
        public static IJSRuntime JSRuntime { get; set; }
        public static NavigationManager NavigationManager { get; set; }
        public static HttpClient HttpClient { get; set; }
        public static ISessionStorage SessionStorage { get; set; }
        public static ILocalStorage LocalStorage { get; set; }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                JSRuntime = jSRuntime;
                NavigationManager = navigationManager;
                SessionStorage = sessionStorage;
                LocalStorage = localStorage;
            }
        }
        public static async ValueTask<TUser> GetApplicationUserSecure<TUser>()
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
        public static async ValueTask SetApplicationUserSecure<TUser>(TUser user)
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
        public static async ValueTask SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession)
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
        public static async ValueTask Logout()
        {
            await LocalStorage.RemoveItem("User");
            await SessionStorage.RemoveItem("User");
            await LocalStorage.RemoveItem("SUser");
            await SessionStorage.RemoveItem("SUser");
            await LocalStorage.RemoveItem("DUEK");
            await SessionStorage.RemoveItem("DUEK");
        }
        public static async ValueTask LogoutAndNavigateTo(string uri, bool forceReload = false)
        {
            await Logout();
            NavigationManager.NavigateTo(uri, forceReload);
        }
        public static ValueTask<ApplicationSession> GetApplicationSession()
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
        public static ValueTask SetApplicationSession(ApplicationSession applicationSession)
        {
            return LocalStorage.SetItem("ApplicationSession", applicationSession);
        }
        public static ValueTask<string> GetApplicationLocale()
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
        public static ValueTask SetApplicationLocale(string locale)
        {
            return LocalStorage.SetItem("ApplicationLocale", locale);
        }
    }
}

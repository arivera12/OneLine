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
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public ISessionStorage SessionStorage { get; set; }
        [Inject] public ILocalStorage LocalStorage { get; set; }
        public static IJSRuntime _JSRuntime { get; set; }
        public static NavigationManager _NavigationManager { get; set; }
        public static HttpClient _HttpClient { get; set; }
        public static ISessionStorage _SessionStorage { get; set; }
        public static ILocalStorage _LocalStorage { get; set; }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _JSRuntime = JSRuntime;
                _NavigationManager = NavigationManager;
                _SessionStorage = SessionStorage;
                _LocalStorage = LocalStorage;
            }
        }

        #region Unencrypted Methods

        public static async ValueTask<TUser> GetApplicationUser<TUser>()
        {
            try
            {
                var applicationSession = await GetApplicationSession();
                if (applicationSession == ApplicationSession.LocalStorage)
                {
                    return await _LocalStorage.GetItem<TUser>("User");
                }
                else
                {
                    return await _SessionStorage.GetItem<TUser>("User");
                }
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async ValueTask SetApplicationUser<TUser>(TUser user)
        {
            var applicationSession = await GetApplicationSession();
            if (applicationSession == ApplicationSession.LocalStorage)
            {
                await _LocalStorage.SetItem("User", user);
            }
            else
            {
                await _SessionStorage.SetItem("User", user);
            }
        }

        public static async ValueTask SetApplicationUser<TUser>(TUser user, ApplicationSession applicationSession)
        {
            if (applicationSession == ApplicationSession.LocalStorage)
            {
                await _LocalStorage.SetItem("User", user);
            }
            else
            {
                await _SessionStorage.SetItem("User", user);
            }
        }

        #endregion

        #region Encrypted Methods

        public static async ValueTask<TUser> GetApplicationUserSecure<TUser>()
        {
            try
            {
                var applicationSession = await GetApplicationSession();
                string SUser;
                string key;
                if (applicationSession == ApplicationSession.LocalStorage)
                {
                    key = await _LocalStorage.GetItem<string>("DUEK");
                    SUser = await _LocalStorage.GetItem<string>("SUser");
                }
                else
                {
                    key = await _SessionStorage.GetItem<string>("DUEK");
                    SUser = await _SessionStorage.GetItem<string>("SUser");
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
                await _LocalStorage.SetItem("DUEK", key);
                await _LocalStorage.SetItem("SUser", jsonUser.EncryptData(key));
            }
            else
            {
                await _SessionStorage.SetItem("DUEK", key);
                await _SessionStorage.SetItem("SUser", jsonUser.EncryptData(key));
            }
        }

        public static async ValueTask SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession)
        {
            var key = (Guid.NewGuid().ToString("N") + string.Empty.NewNumericIdentifier()).Replace("-", "");
            key = key.EncryptData(key);
            var jsonUser = JsonConvert.SerializeObject(user);
            if (applicationSession == ApplicationSession.LocalStorage)
            {
                await _LocalStorage.SetItem("DUEK", key);
                await _LocalStorage.SetItem("SUser", jsonUser.EncryptData(key));
            }
            else
            {
                await _SessionStorage.SetItem("DUEK", key);
                await _SessionStorage.SetItem("SUser", jsonUser.EncryptData(key));
            }
        }

        #endregion

        #region Common/Shared

        public static async ValueTask Logout()
        {
            await _LocalStorage.RemoveItem("User");
            await _SessionStorage.RemoveItem("User");
            await _LocalStorage.RemoveItem("SUser");
            await _SessionStorage.RemoveItem("SUser");
            await _LocalStorage.RemoveItem("DUEK");
            await _SessionStorage.RemoveItem("DUEK");
        }

        public static async ValueTask LogoutAndNavigateTo(string uri, bool forceReload = false)
        {
            await Logout();
            _NavigationManager.NavigateTo(uri, forceReload);
        }

        public static async ValueTask<ApplicationSession> GetApplicationSession()
        {
            try
            {
                return await _LocalStorage.GetItem<ApplicationSession>("ApplicationSession");
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static async ValueTask SetApplicationSession(ApplicationSession applicationSession)
        {
            await _LocalStorage.SetItem("ApplicationSession", applicationSession);
        }

        public static async ValueTask<string> GetApplicationLocale()
        {
            try
            {
                return await _LocalStorage.GetItem<string>("ApplicationLocale");
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async ValueTask SetApplicationLocale(string locale)
        {
            await _LocalStorage.SetItem("ApplicationLocale", locale);
        }

        public static void SetHttpClientAuthorizationToken(string AuthorizationToken, bool AddBearerScheme = false)
        {
            _HttpClient.AddJwtAuthorizationBearerHeader(AuthorizationToken, AddBearerScheme);
        }

        #endregion
    }
}

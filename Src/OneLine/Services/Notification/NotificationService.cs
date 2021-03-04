using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace OneLine.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly IJSRuntime JSRuntime;
        private readonly JsonSerializerSettings JsonSerializerSettings =
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                DefaultValueHandling = DefaultValueHandling.Ignore
            }; 
        public NotificationService(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }
        /// <inheritdoc/>
        public ValueTask<bool> IsSupportedByBrowserAsync()
        {
            return JSRuntime.InvokeAsync<bool>("eval", new[] { "'Notification' in window" });
        }
        /// <inheritdoc/>
        public async ValueTask<NotificationPermissionType> RequestPermissionAsync()
        {
            string permission = await JSRuntime.InvokeAsync<string>("eval", new[] { "Notification.requestPermission()" });
            if (permission.Equals("granted", StringComparison.InvariantCultureIgnoreCase))
            {
                return NotificationPermissionType.Granted;
            }
            else if (permission.Equals("denied", StringComparison.InvariantCultureIgnoreCase))
            {
                return NotificationPermissionType.Denied;
            }
            else
            {
                return NotificationPermissionType.Default;
            }
        }
        /// <inheritdoc/>
        public ValueTask CreateAsync(string title, NotificationOptions options) => JSRuntime.InvokeVoidAsync("eval", new[] { @$"new Notification('{title}', {JsonConvert.SerializeObject(options, JsonSerializerSettings)})" });
        /// <inheritdoc/>
        public ValueTask CreateAsync(string title, string body, string icon)
        {
            NotificationOptions options = new NotificationOptions
            {
                Body = body,
                Icon = icon,
            };
            return JSRuntime.InvokeVoidAsync("eval", new[] { @$"new Notification('{title}', {JsonConvert.SerializeObject(options, JsonSerializerSettings)})" });
        }
    }
}

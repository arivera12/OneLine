using Blazor.Extensions.Storage.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace OneLine.Blazor
{
    public class Resourcer : ComponentBase
    {
        [Inject] public ILocalStorage LocalStorage { get; set; }
        [Parameter] public string DefaultResourcerSource { get; set; }
        [Parameter] public Assembly DefaultResourcerAssembly { get; set; }
        [Parameter] public IReadOnlyDictionary<string, Tuple<string, string, Assembly>> Resourcers { get; set; }
        [Parameter]  public static IReadOnlyDictionary<string, Tuple<string, string, Assembly>> CurrentResourcers { get; set; }
        [Parameter] public Action OnLoadCompleted { get; set; }
        public static Action OnChangeAndLoadCompleted { get; set; }
        public static string CurrentResourcerSource { get; set; }
        public static Assembly CurrentResourcerAssembly { get; set; }
        public static bool IsResourceManagerLoaded { get; set; }
        private static string InternalRefResourcerSource { get; set; }
        private static ResourceManager CurrentResourceManager { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (Resourcers == null || Resourcers.Count == 0)
                {
                    throw new Exception("The Resourcers parameter is null or count is zero");
                }
                CurrentResourcers = Resourcers;
                var ApplicationLocale = await LocalStorage.GetItem<string>("ApplicationLocale");
                if (!string.IsNullOrWhiteSpace(ApplicationLocale))
                {
                    if (Resourcers.Any(w => w.Key == ApplicationLocale))
                    {
                        DefaultResourcerSource = CurrentResourcers.FirstOrDefault(w => w.Key == ApplicationLocale).Value.Item2;
                        DefaultResourcerAssembly = CurrentResourcers.FirstOrDefault(w => w.Key == ApplicationLocale).Value.Item3;
                    }
                    else
                    {
                        await LocalStorage.RemoveItem("ApplicationLocale");
                    }
                }
                else if (DefaultResourcerSource == null)
                {
                    throw new ArgumentNullException("The Resourcer parameter is null, empty or whitespace");
                }
                CurrentResourcerSource = DefaultResourcerSource;
                CurrentResourcerAssembly = DefaultResourcerAssembly;
                InternalRefResourcerSource = DefaultResourcerSource;
                CurrentResourceManager = new ResourceManager(DefaultResourcerSource, DefaultResourcerAssembly);
                OnLoadCompleted?.Invoke();
            }
        }
        private static ResourceManager ResourceManager
        {
            get
            {
                if (CurrentResourceManager != null &&
                    InternalRefResourcerSource != null &&
                    CurrentResourcerSource != InternalRefResourcerSource)
                {
                    CurrentResourceManager = new ResourceManager(CurrentResourcerSource, CurrentResourcerAssembly);
                    InternalRefResourcerSource = CurrentResourcerSource;
                    OnChangeAndLoadCompleted?.Invoke();
                }
                return CurrentResourceManager;
            }
        }
        public static object GetObject(string resourceName)
        {
            object objectResource = ResourceManager?.GetObject(resourceName);
            if (objectResource == null)
            {
                return resourceName;
            }
            else
            {
                return objectResource;
            }
        }
        public static string GetString(string resourceName)
        {
            string stringResource = ResourceManager?.GetString(resourceName);
            if (string.IsNullOrWhiteSpace(stringResource))
            {
                return resourceName;
            }
            else
            {
                return stringResource;
            }
        }
        public static UnmanagedMemoryStream GetStream(string resourceName)
        {
            return ResourceManager?.GetStream(resourceName);
        }
    }
}

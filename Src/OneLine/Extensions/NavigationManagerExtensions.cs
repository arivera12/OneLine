using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace OneLine.Extensions
{
    public static class NavigationManagerExtensions
    {
        /// <summary>
        /// Tries to get a value from the <see cref="NavigationManager.Uri"/> and convert it to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="navigationManager"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void TryGetQueryStringValue<T>(this NavigationManager navigationManager, string key, out T value)
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                try
                {
                    value = valueFromQueryString.ToType<T>();
                }
                catch
                {
                    value = default;
                }
            }
            else
            {
                value = default;
            }
        }
    }
}

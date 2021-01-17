using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace OneLine.Extensions
{
    public static class NavigationManagerExtensions
    {
        /// <summary>
        /// Tries to get a value from the <see cref="NavigationManager.Uri"/> parse it and convert it. 
        /// This method returns true is value was parseable and converted succesfully otherwise false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="navManager"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetQueryStringValue<T>(this NavigationManager navigationManager, string key, out T value)
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }
                if (typeof(T) == typeof(double) && double.TryParse(valueFromQueryString, out var valueAsDouble))
                {
                    value = (T)(object)valueAsDouble;
                    return true;
                }
                if (typeof(T) == typeof(bool) && bool.TryParse(valueFromQueryString, out var valueAsBool))
                {
                    value = (T)(object)valueAsBool;
                    return true;
                }
                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                    return true;
                }
                if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
                {
                    value = (T)(object)valueAsDecimal;
                    return true;
                }
                if (typeof(T) == typeof(float) && float.TryParse(valueFromQueryString, out var valueAsFloat))
                {
                    value = (T)(object)valueAsFloat;
                    return true;
                }
            }
            value = default;
            return false;
        }
    }
}

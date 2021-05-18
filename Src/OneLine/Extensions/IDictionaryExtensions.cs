using System.Collections.Generic;

namespace OneLine.Extensions
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Merges the current dictionary with the supplied dictionary into a new dictionary.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="current"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> current, IDictionary<TKey, TValue> keyValuePairs)
        {
            var dictionaryMerge = new Dictionary<TKey, TValue>(current);
            foreach (var item in keyValuePairs)
            {
                dictionaryMerge[item.Key] = item.Value;
            }
            return dictionaryMerge;
        }
    }
}

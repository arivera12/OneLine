namespace OneLine.Extensions
{
    public static class NullableExtensions
    {
        /// <summary>
        /// Checks where the value is null or the default value.
        /// </summary>
        /// <typeparam name="T">The nullable struct</typeparam>
        /// <param name="value">The value of the struct</param>
        /// <returns></returns>
        public static bool IsNullOrDefaultValue<T>(this T? value) where T : struct
        {
            return !value.HasValue || value.Value.IsDefaultValue();
        }
        /// <summary>
        /// Checks where the value is not null and not the default value.
        /// </summary>
        /// <typeparam name="T">The nullable struct</typeparam>
        /// <param name="value">The value of the struct</param>
        /// <returns></returns>
        public static bool IsNotNullAndNotDefaultValue<T>(this T? value) where T : struct
        {
            return value.HasValue && !value.Value.IsDefaultValue();
        }
    }
}
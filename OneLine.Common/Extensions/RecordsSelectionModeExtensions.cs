using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class RecordsSelectionModeExtensions
    {
        /// <summary>
        /// Checks whether the <see cref="RecordsSelectionMode"/> equals <seealso cref="RecordsSelectionMode.Single"/>
        /// </summary>
        /// <param name="recordsSelectionMode"></param>
        /// <returns></returns>
        public static bool IsSingle(this RecordsSelectionMode recordsSelectionMode)
        {
            return recordsSelectionMode.Equals(RecordsSelectionMode.Single);
        }
        /// <summary>
        /// Checks whether the <see cref="RecordsSelectionMode"/> equals <seealso cref="RecordsSelectionMode.Multiple"/>
        /// </summary>
        /// <param name="recordsSelectionMode"></param>
        /// <returns></returns>
        public static bool IsMultiple(this RecordsSelectionMode recordsSelectionMode)
        {
            return recordsSelectionMode.Equals(RecordsSelectionMode.Multiple);
        }
    }
}

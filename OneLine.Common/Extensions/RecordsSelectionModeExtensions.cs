using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class RecordsSelectionModeExtensions
    {
        public static bool IsSingle(this RecordsSelectionMode recordsSelectionMode)
        {
            return recordsSelectionMode == RecordsSelectionMode.Single;
        }
        public static bool IsMultiple(this RecordsSelectionMode recordsSelectionMode)
        {
            return recordsSelectionMode == RecordsSelectionMode.Multiple;
        }
    }
}

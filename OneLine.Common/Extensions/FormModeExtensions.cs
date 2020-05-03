using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class FormModeExtensions
    {
        public static bool IsSingle(this FormMode formMode)
        {
            return formMode == FormMode.Single;
        }
        public static bool IsMultiple(this FormMode formMode)
        {
            return formMode == FormMode.Multiple;
        }
    }
}

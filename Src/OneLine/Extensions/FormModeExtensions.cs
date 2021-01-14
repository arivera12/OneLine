using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class FormModeExtensions
    {
        /// <summary>
        /// Checks whether the <see cref="FormMode"/> equals <seealso cref="FormMode.Single"/>
        /// </summary>
        /// <param name="formMode"></param>
        /// <returns></returns>
        public static bool IsSingle(this FormMode formMode)
        {
            return formMode.Equals(FormMode.Single);
        }
        /// <summary>
        ///  Checks whether the <see cref="FormMode"/> equals <seealso cref="FormMode.Multiple"/>
        /// </summary>
        /// <param name="formMode"></param>
        /// <returns></returns>
        public static bool IsMultiple(this FormMode formMode)
        {
            return formMode.Equals(FormMode.Multiple);
        }
    }
}

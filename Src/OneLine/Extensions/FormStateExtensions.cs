using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class FormStateExtensions
    {
        /// <summary>
        /// Check wheter <see cref="FormState"/> equals <seealso cref="FormState.Copy"/>
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        public static bool IsCopy(this FormState formState)
        {
            return formState.Equals(FormState.Copy);
        }
        /// <summary>
        /// Check wheter <see cref="FormState"/> equals <seealso cref="FormState.Create"/>
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        public static bool IsCreate(this FormState formState)
        {
            return formState.Equals(FormState.Create);
        }
        /// <summary>
        /// Check wheter <see cref="FormState"/> equals <seealso cref="FormState.Delete"/>
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        public static bool IsDelete(this FormState formState)
        {
            return formState.Equals(FormState.Delete);
        }
        /// <summary>
        /// Check wheter <see cref="FormState"/> equals <seealso cref="FormState.Deleted"/>
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        public static bool IsDeleted(this FormState formState)
        {
            return formState.Equals(FormState.Deleted);
        }
        /// <summary>
        /// Check wheter <see cref="FormState"/> equals <seealso cref="FormState.Details"/>
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        public static bool IsDetails(this FormState formState)
        {
            return formState.Equals(FormState.Details);
        }
        /// <summary>
        /// Check wheter <see cref="FormState"/> equals <seealso cref="FormState.Edit"/>
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        public static bool IsEdit(this FormState formState)
        {
            return formState.Equals(FormState.Edit);
        }
        /// <summary>
        /// Check wheter <see cref="FormState"/> equals <seealso cref="FormState.ReadOnly"/>
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        public static bool IsReadOnly(this FormState formState)
        {
            return formState.Equals(FormState.ReadOnly);
        }
    }
}

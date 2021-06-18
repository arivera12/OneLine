using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class SaveOperationExtensions
    {
        /// <summary>
        /// Check whether the <see cref="SaveOperation"/> equals <seealso cref="SaveOperation.Add"/>
        /// </summary>
        /// <param name="saveOperation"></param>
        /// <returns></returns>
        public static bool IsAdd(this SaveOperation saveOperation)
        {
            return saveOperation == SaveOperation.Add;
        }
        /// <summary>
        /// Check whether the <see cref="SaveOperation"/> equals <seealso cref="SaveOperation.Update"/>
        /// </summary>
        /// <param name="saveOperation"></param>
        /// <returns></returns>
        public static bool IsUpdate(this SaveOperation saveOperation)
        {
            return saveOperation == SaveOperation.Update;
        }
        /// <summary>
        /// Check whether the <see cref="SaveOperation"/> equals <seealso cref="SaveOperation.Any"/>
        /// </summary>
        /// <param name="saveOperation"></param>
        /// <returns></returns>
        public static bool IsAny(this SaveOperation saveOperation)
        {
            return saveOperation == SaveOperation.Any;
        }
    }
}

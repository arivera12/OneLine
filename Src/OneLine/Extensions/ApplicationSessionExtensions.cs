using OneLine.Enums;

namespace OneLine.Extensions
{
    public static class ApplicationSessionExtensions
    {
        /// <summary>
        /// Checks if the application session is persistent
        /// </summary>
        /// <param name="applicationSession"></param>
        /// <returns></returns>
        public static bool IsPersistent(this ApplicationSession applicationSession)
        {
            return applicationSession.Equals(ApplicationSession.LocalStorage);
        }
        /// <summary>
        /// Checks if the application session is not persistent
        /// </summary>
        /// <param name="applicationSession"></param>
        /// <returns></returns>
        public static bool IsNotPersistent(this ApplicationSession applicationSession)
        {
            return applicationSession.Equals(ApplicationSession.SessionStorage);
        }
    }
}

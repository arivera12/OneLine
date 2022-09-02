using OneLine.Enums;
using System.Threading.Tasks;

namespace OneLine.Services
{
    /// <summary>
    /// A base application state to manage the user state.
    /// </summary>
    public interface IApplicationState
    {
        /// <summary>
        /// Gets the application current user securely
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <returns></returns>
        Task<TUser> GetApplicationUserSecure<TUser>();
        /// <summary>
        /// Sets the application current user securely in local or session storage based on the <paramref name="applicationSession"/>
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <param name="applicationSession"></param>
        /// <returns></returns>
        Task SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession);
        /// <summary>
        /// Logs out the current user from the application
        /// </summary>
        /// <returns></returns>
        Task Logout();
        /// <summary>
        /// Gets the <see cref="ApplicationSession"/>
        /// </summary>
        /// <returns></returns>
        Task<ApplicationSession> GetApplicationSession();
        /// <summary>
        /// Sets the <see cref="ApplicationSession"/>
        /// </summary>
        /// <param name="applicationSession"></param>
        /// <returns></returns>
        Task SetApplicationSession(ApplicationSession applicationSession);
        /// <summary>
        /// Gets the application encryption key
        /// </summary>
        /// <returns></returns>
        Task<string> GetApplicationEncryptionKey();
        /// <summary>
        /// Sets the the application encryption key
        /// </summary>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        Task SetApplicationEncryptionKey(string encryptionKey);
    }
}

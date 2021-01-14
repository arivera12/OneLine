using OneLine.Enums;
using System.Threading.Tasks;

namespace OneLine.Services
{
    /// <summary>
    /// A base application state
    /// </summary>
    public interface IApplicationState
    {
        /// <summary>
        /// Gets the application current user securely
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <returns></returns>
        ValueTask<TUser> GetApplicationUserSecure<TUser>();
        /// <summary>
        /// Sets the application current user securely
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        ValueTask SetApplicationUserSecure<TUser>(TUser user);
        /// <summary>
        /// Sets the application current user securely in local or session storage based on the <paramref name="applicationSession"/>
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        /// <param name="applicationSession"></param>
        /// <returns></returns>
        ValueTask SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession);
        /// <summary>
        /// Logs out the current user from the application
        /// </summary>
        /// <returns></returns>
        ValueTask Logout();
        /// <summary>
        /// Logs out the current user from the application and navigates to the specified uri
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="forceReload"></param>
        /// <returns></returns>
        ValueTask LogoutAndNavigateTo(string uri, bool forceReload = false);
        /// <summary>
        /// Gets the <see cref="ApplicationSession"/>
        /// </summary>
        /// <returns></returns>
        ValueTask<ApplicationSession> GetApplicationSession();
        /// <summary>
        /// Sets the <see cref="ApplicationSession"/>
        /// </summary>
        /// <param name="applicationSession"></param>
        /// <returns></returns>
        ValueTask SetApplicationSession(ApplicationSession applicationSession);
    }
}

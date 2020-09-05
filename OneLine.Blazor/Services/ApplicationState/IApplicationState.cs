using OneLine.Enums;
using System.Threading.Tasks;

namespace OneLine.Blazor.Services
{
    public interface IApplicationState
    {
        ValueTask<TUser> GetApplicationUserSecure<TUser>();
        ValueTask SetApplicationUserSecure<TUser>(TUser user);
        ValueTask SetApplicationUserSecure<TUser>(TUser user, ApplicationSession applicationSession);
        ValueTask Logout();
        ValueTask LogoutAndNavigateTo(string uri, bool forceReload = false);
        ValueTask<ApplicationSession> GetApplicationSession();
        ValueTask SetApplicationSession(ApplicationSession applicationSession);
    }
}

using OneLine.Bases;
using OneLine.Models.Users;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorCoreViewComponentAuthorized<T, TIdentifier, THttpService> :
        IBlazorCoreComponent,
        ICoreView<T, TIdentifier, THttpService>
    {
        IEnumerable<string> AuthorizedRoles { get; set; }
        AspNetUsersViewModel User { get; set; }
    }
}
using OneLine.Bases;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorCoreViewComponentAuthorized<T, TIdentifier, THttpService, TUser> :
        IBlazorCoreComponent,
        ICoreView<T, TIdentifier, THttpService>
    {
        IEnumerable<string> AuthorizedRoles { get; set; }
        TUser User { get; set; }
    }
}
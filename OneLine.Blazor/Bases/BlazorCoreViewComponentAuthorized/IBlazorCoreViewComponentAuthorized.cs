using OneLine.Bases;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorCoreViewComponentAuthorized<T, TIdentifier, THttpService, TUser> :
        IBlazorCoreComponent,
        ICoreView<T, TIdentifier, THttpService>
    {
        /// <summary>
        /// The context view authorized roles
        /// </summary>
        IEnumerable<string> AuthorizedRoles { get; set; }
        /// <summary>
        /// The context user
        /// </summary>
        TUser User { get; set; }
    }
}
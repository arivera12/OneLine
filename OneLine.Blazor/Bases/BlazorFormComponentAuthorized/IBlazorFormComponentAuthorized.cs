using OneLine.Bases;
using OneLine.Models.Users;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorFormComponentAuthorized<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService>
    {
        IEnumerable<string> AuthorizedRoles { get; set; }
        AspNetUsersViewModel User { get; set; }
    }
}

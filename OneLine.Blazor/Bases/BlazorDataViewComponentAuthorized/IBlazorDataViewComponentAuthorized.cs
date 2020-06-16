using OneLine.Bases;
using OneLine.Models.Users;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorDataViewComponentAuthorized<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IDataView<T, TIdentifier, THttpService>
    {
        IEnumerable<string> AuthorizedRoles { get; set; }
        AspNetUsersViewModel User { get; set; }
    }
}
using BlazorStrap;
using OneLine.Bases;
using OneLine.Models.Users;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapFormComponentAuthorized<T, TIdentifier, THttpService> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService>
    {
        bool IsFormOpen { get; set; }
        BSModal Modal { get; set; }
        IEnumerable<string> AuthorizedRoles { get; set; }
        AspNetUsersViewModel User { get; set; }
    }
}

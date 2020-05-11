using OneLine.Bases;
using OneLine.Models.Users;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapIndexComponentAuthorized<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
    {
        bool IsFormOpen { get; set; }
        bool ShowModal { get; set; }
        IEnumerable<string> AuthorizedRoles { get; set; }
        AspNetUsersViewModel User { get; set; }
    }
}

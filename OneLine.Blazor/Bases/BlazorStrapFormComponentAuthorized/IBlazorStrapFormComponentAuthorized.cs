﻿using BlazorStrap;
using OneLine.Bases;
using OneLine.Models.Users;
using System.Collections.Generic;

namespace OneLine.Blazor.Bases
{
    public interface IBlazorStrapFormComponentAuthorized<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        IBlazorComponent,
        IForm<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
    {
        bool IsFormOpen { get; set; }
        BSModal Modal { get; set; }
        IEnumerable<string> AuthorizedRoles { get; set; }
        AspNetUsersViewModel User { get; set; }
    }
}

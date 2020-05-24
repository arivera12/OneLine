using BlazorCurrentDevice;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract class BlazorStrapIndexComponentAuthorized<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs> :
        BlazorStrapIndexComponent<T, TIdentifier, TId, THttpService, TBlobData, TBlobValidator, TUserBlobs>,
        IBlazorStrapIndexComponentAuthorized<T, TIdentifier, THttpService, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>, new()
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        [Parameter] public virtual IEnumerable<string> AuthorizedRoles { get; set; }
        public AspNetUsersViewModel User { get; set; }
        public override async Task OnAfterFirstRenderAsync()
        {
            User = await ApplicationState<AspNetUsersViewModel>.GetApplicationUserSecure();
            if (User.IsNull() || (!AuthorizedRoles.IsNullOrEmpty() && !AuthorizedRoles.Any(w => User.Roles.Contains(w))))
            {
                await ApplicationState<AspNetUsersViewModel>.Logout();
                NavigationManager.NavigateTo($@"/login/{NavigationManager.Uri.Split().Last()}");
            }
            else
            {
                HttpClient.AddJwtAuthorizationBearerHeader(User.Token);
                HttpService.HttpClient = HttpClient;
                IsMobile = await BlazorCurrentDeviceService.Mobile();
                IsTablet = await BlazorCurrentDeviceService.Tablet();
                IsDesktop = await BlazorCurrentDeviceService.Desktop();
                if (FormMode.IsSingle())
                {
                    if (Record.IsNull())
                    {
                        if (Identifier.IsNotNull() && Identifier.Model.IsNotNull())
                        {
                            await Load();
                        }
                    }
                }
                else if (FormMode.IsMultiple())
                {
                    if (Records.IsNullOrEmpty())
                    {
                        if (Identifiers.IsNotNullAndNotEmpty())
                        {
                            await Load();
                        }
                    }
                }
            }
            StateHasChanged();
        }
    }
}
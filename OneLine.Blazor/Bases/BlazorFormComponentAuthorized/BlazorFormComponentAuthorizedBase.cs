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
    public abstract class BlazorFormComponentAuthorizedBase<T, TIdentifier, TId, THttpService> :
        BlazorFormComponentBase<T, TIdentifier, TId, THttpService>,
        IBlazorFormComponentAuthorized<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        [Parameter] public virtual IEnumerable<string> AuthorizedRoles { get; set; }
        public virtual AspNetUsersViewModel User { get; set; }
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
                if (AutoLoad)
                {
                    await Load();
                }
            }
            StateHasChanged();
        }
    }
}
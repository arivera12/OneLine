using FluentValidation;
using Microsoft.AspNetCore.Components;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract class BlazorStrapDataViewComponentAuthorizedBase<T, TIdentifier, TId, THttpService> :
        BlazorStrapDataViewComponentBase<T, TIdentifier, TId, THttpService>,
        IBlazorStrapDataViewComponentAuthorized<T, TIdentifier, THttpService>
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
                if (RecordsSelectionMode.IsSingle())
                {
                    if (Record.IsNull())
                    {
                        if (Identifier.IsNotNull() && Identifier.Model.IsNotNull())
                        {
                            await Load();
                        }
                    }
                }
                else if (RecordsSelectionMode.IsMultiple())
                {
                    if (Records.IsNullOrEmpty())
                    {
                        if (Identifiers.IsNotNullAndNotEmpty())
                        {
                            await Load();
                        }
                    }
                }
                //This null check allows to prevent override the listeners from parent if it's listening to any of this events
                OnBeforeSearch ??= new Action(async () => await BeforeSearch());
                OnAfterSearch ??= new Action(async () => await AfterSearch());
            }
            StateHasChanged();
        }
    }
}

﻿using DeviceDetectorNET;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using OneLine.Blazor.Contracts;
using OneLine.Contracts;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public class BlazorCoreViewComponentAuthorizedBase<T, TIdentifier, TId, THttpService, TUser> :
        BlazorCoreViewComponentBase<T, TIdentifier, TId, THttpService>,
        IBlazorCoreViewComponentAuthorized<T, TIdentifier, THttpService, TUser>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier>, new()
        where TUser : class, new()
    {
        public BlazorCoreViewComponentAuthorizedBase() : base()
        {   
        }
        /// <inheritdoc/>
        [Parameter] public virtual IEnumerable<string> AuthorizedRoles { get; set; }
        /// <inheritdoc/>
        public virtual TUser User { get; set; }
        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                FirstRenderOcurred = firstRender;
                await InitializeComponentAsync();
            }
        }
        /// <inheritdoc/>
        public override async Task InitializeComponentAsync()
        {
            User = await ApplicationState.GetApplicationUserSecure<TUser>();
            IEnumerable<string> roles = default;
            var rolesObject = User.GetType().GetProperty("Roles").GetValue(User);
            if (rolesObject.IsNotNull())
            {
                roles = (rolesObject as IList<string>).AsEnumerable();
            }
            if (User.IsNull() ||
                (AuthorizedRoles.IsNotNull() && AuthorizedRoles.Any() &&
                roles.IsNotNull() && roles.Any() &&
                !AuthorizedRoles.Any(w => roles.Contains(w))))
            {
                await ApplicationState.Logout();
                NavigationManager.NavigateTo($@"/login/{NavigationManager.Uri.Split().Last()}");
            }
            else
            {
                var token = User.GetType().GetProperty("Token").GetValue(User)?.ToString();
                HttpService.HttpClient.AddJwtAuthorizationBearerHeader(token, true);
                HttpClient.AddJwtAuthorizationBearerHeader(token, true);
                OnBeforeSearch ??= new Action(async () => await BeforeSearch());
                OnAfterSearch ??= new Action(async () => await AfterSearch());
                OnBeforeSave ??= new Action(async () => await BeforeSave());
                OnAfterSave ??= new Action(async () => await AfterSave());
                OnBeforeDelete ??= new Action(async () => await BeforeDelete());
                OnAfterDelete ??= new Action(async () => await AfterDelete());
                OnBeforeCancel ??= new Action(async () => await BeforeCancel());
                OnAfterCancel ??= new Action(async () => await AfterCancel());
                OnBeforeReset ??= new Action(async () => await BeforeReset());
                OnAfterReset ??= new Action(async () => await AfterReset());
                if (!string.IsNullOrWhiteSpace(RecordId))
                {
                    Identifier = new TIdentifier
                    {
                        Model = (TId)Convert.ChangeType(RecordId, typeof(TId))
                    };
                }
                if (AutoLoad)
                {
                    if (OnBeforeLoad.IsNotNull())
                    {
                        OnBeforeLoad.Invoke();
                    }
                    else
                    {
                        await Load();
                    }
                }
                if (InitialAutoSearch)
                {
                    if (OnBeforeSearch.IsNotNull())
                    {
                        OnBeforeSearch.Invoke();
                    }
                    else
                    {
                        await Search();
                    }
                }
            }
            StateHasChanged();
        }
    }
}
﻿using Microsoft.AspNetCore.Components;
using OneLine.Bases;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public abstract partial class BlazorStrapCoreViewComponentAuthorizedBase<T, TIdentifier, TId, THttpService, TUser> :
        BlazorCoreViewComponentAuthorizedBase<T, TIdentifier, TId, THttpService, TUser>,
        IBlazorCoreViewComponentAuthorized<T, TIdentifier, THttpService, TUser>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier>, new()
        where TUser : class, new()
    {
        public override async Task OnAfterFirstRenderAsync()
        {
            User = await ApplicationState.GetApplicationUserSecure<TUser>();
            IEnumerable<string> roles = default;
            var rolesObject = User.GetType().GetProperty("Roles").GetValue(User);
            if (rolesObject.IsNotNull())
            {
                roles = (rolesObject as IList<string>).AsEnumerable();
            }
            if (User.IsNull() || (!AuthorizedRoles.IsNullOrEmpty() && !AuthorizedRoles.Any(w => roles.Contains(w))))
            {
                await ApplicationState.Logout();
                NavigationManager.NavigateTo($@"/login/{NavigationManager.Uri.Split().Last()}");
            }
            else
            {
                var token = User.GetType().GetProperty("Token").GetValue(User)?.ToString();
                HttpService.HttpClient.AddJwtAuthorizationBearerHeader(token, true);
                IsMobile = await BlazorCurrentDeviceService.Mobile();
                IsTablet = await BlazorCurrentDeviceService.Tablet();
                IsDesktop = await BlazorCurrentDeviceService.Desktop();
                //This null check allows to prevent override the listeners from parent if it's listening to any of this events
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
        public virtual TColor HighlightItem<TColor>(T record, TColor selectedColor, TColor unSelectedColor)
        {
            if (RecordsSelectionMode.IsSingle())
            {
                if (SelectedRecord == record)
                {
                    return selectedColor;
                }
                else
                {
                    return unSelectedColor;
                }
            }
            else if (RecordsSelectionMode.IsMultiple())
            {
                if (SelectedRecords.Contains(record))
                {
                    return selectedColor;
                }
                else
                {
                    return unSelectedColor;
                }
            }
            return unSelectedColor;
        }
    }
}
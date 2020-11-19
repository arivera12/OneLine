using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OneLine.Blazor.Contracts;
using OneLine.Contracts;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    public class BlazorStrapCoreViewComponentBase<T, TIdentifier, TId, THttpService> :
        BlazorCoreViewComponentBase<T, TIdentifier, TId, THttpService>,
        IBlazorDataViewComponent<T, TIdentifier, THttpService>
        where T : class, new()
        where TIdentifier : IIdentifier<TId>, new()
        where THttpService : class, IHttpCrudExtendedService<T, TIdentifier>, new()
    {
        public BlazorStrapCoreViewComponentBase(
           IApplicationConfiguration applicationConfiguration,
           IJSRuntime jSRuntime,
           NavigationManager navigationManager,
           ISaveFile saveFile,
           IDevice device,
           IResourceManagerLocalizer resourceManagerLocalizer,
           SweetAlertService sweetAlertService,
           HttpClient httpClient,
           IApplicationState applicationState,
           THttpService httpService) :
           base(applicationConfiguration,
               jSRuntime,
               navigationManager,
               saveFile,
               device,
               resourceManagerLocalizer,
               sweetAlertService,
               httpClient,
               applicationState,
               httpService)
        {
        }
        public BlazorStrapCoreViewComponentBase(
            IApplicationConfiguration applicationConfiguration,
            ISaveFile saveFile,
            IDevice device,
            IResourceManagerLocalizer resourceManagerLocalizer,
            HttpClient httpClient,
            IApplicationState applicationState,
            THttpService httpService) :
            base(applicationConfiguration,
                saveFile,
                device,
                resourceManagerLocalizer,
                httpClient,
                applicationState,
                httpService)
        {
        }
        /// <inheritdoc/>
        public override async Task InitializeComponentAsync()
        {
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

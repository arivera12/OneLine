using CurrieTechnologies.Razor.SweetAlert2;
using DeviceDetectorNET;
using JsonLanguageLocalizerNet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneLine.Blazor.Services;
using OneLine.Enums;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Blazor.Bases
{
    /// <summary>
    /// Defines a base core component using <see cref="OneLine.Bases.CoreViewBase{T, TIdentifier, TId, THttpService}"/> and blazor specific services andsome third party services
    /// </summary>
    public interface IBlazorCoreComponent : IComponent, IHandleEvent, IHandleAfterRender
    {
        /// <summary>
        /// The javascript runtime
        /// </summary>
        IJSRuntime JSRuntime { get; set; }
        /// <summary>
        /// The navigation manager
        /// </summary>
        NavigationManager NavigationManager { get; set; }
        /// <summary>
        /// The blazor current device detector by using the browser user agent
        /// </summary>
        DeviceDetector DeviceDetector { get; set; }
        /// <summary>
        /// The save file service
        /// </summary>
        ISaveFile SaveFile { get; set; }
        /// <summary>
        /// The sweet alert service
        /// </summary>
        SweetAlertService SweetAlertService { get; set; }
        /// <summary>
        /// The http client of blazor context
        /// </summary>
        HttpClient HttpClient { get; set; }
        /// <summary>
        /// The application state
        /// </summary>
        IApplicationState ApplicationState { get; set; }
        /// <summary>
        /// The language localizer service
        /// </summary>
        IJsonLanguageLocalizerService LanguageLocalizer { get; set; }
        /// <summary>
        /// The language localizer supported cultures
        /// </summary>
        IJsonLanguageLocalizerSupportedCulturesService LanguageLocalizerSupportedCultures { get; set; }
        /// <summary>
        /// Indicator that let's know if the device is a desktop 
        /// </summary>
        bool IsDesktop { get; set; }
        /// <summary>
        /// Indicator that let's know if the device is a tablet
        /// </summary>
        bool IsTablet { get; set; }
        /// <summary>
        /// Indicator that let's know if the device is a mobile
        /// </summary>
        bool IsMobile { get; set; }
        /// <summary>
        /// Indicator that shows the form component and hides the list or table component
        /// </summary>
        bool ShowForm { get; set; }
        /// <summary>
        /// The show form changed action callback
        /// </summary>
        Action<bool> ShowFormChanged { get; set; }
        /// <summary>
        /// Indicator that hides the cancel or back button
        /// </summary>
        bool HideCancelOrBackButton { get; set; }
        /// <summary>
        /// Indicator that hides the reset button
        /// </summary>
        bool HideResetButton { get; set; }
        /// <summary>
        /// Indicator that hides the save button
        /// </summary>
        bool HideSaveButton { get; set; }
        /// <summary>
        /// Indicator that hides the delete button
        /// </summary>
        bool HideDeleteButton { get; set; }
        /// <summary>
        /// Indicator that hides de create or new button
        /// </summary>
        bool HideCreateOrNewButton { get; set; }
        /// <summary>
        /// Indicator that show the options dialog. Modal or pop up window
        /// </summary>
        bool ShowOptionsDialog { get; set; }
        /// <summary>
        /// Shows options dialog changed action callback
        /// </summary>
        Action<bool> ShowOptionsDialogChanged { get; set; }
        /// <summary>
        /// Hides the details button in the dialog option moda o pop up window
        /// </summary>
        bool HideDetailsDialogOption { get; set; }
        /// <summary>
        /// Hides the copy button in the dialog option moda o pop up window
        /// </summary>
        bool HideCopyDialogOption { get; set; }
        /// <summary>
        /// Hides the edit button in the dialog option moda o pop up window
        /// </summary>
        bool HideEditDialogOption { get; set; }
        /// <summary>
        /// Hides the delete button in the dialog option moda o pop up window
        /// </summary>
        bool HideDeleteDialogOption { get; set; }
        /// <summary>
        /// Hide the current view via razor syntax
        /// </summary>
        bool Hide { get; set; }
        /// <summary>
        /// Hide the current view via css
        /// </summary>
        bool Hidden { get; set; }
        /// <summary>
        /// Indicator that tell's when the view is read only
        /// </summary>
        bool ReadOnly { get; set; }
        /// <summary>
        /// Indicator that tell's when the view is disabled
        /// </summary>
        bool Disabled { get; set; }
        /// <summary>
        /// Enables a confirm dialog before save. By default this option is false
        /// </summary>
        bool EnableConfirmOnSave { get; set; }
        /// <summary>
        /// Enables a confirm dialog on reset. By default thi option is false
        /// </summary>
        bool EnableConfirmOnReset { get; set; }
        /// <summary>
        /// Enables a confirm dialog on delete. By default this option is true
        /// </summary>
        bool EnableConfirmOnDelete { get; set; }
        /// <summary>
        /// Enables a confirm dialog on cancel. By default this option is false
        /// </summary>
        bool EnableConfirmOnCancel { get; set; }
        /// <summary>
        /// Enables close or hide the form after save or delete. By default this option is true
        /// </summary>
        bool CloseFormAfterSaveOrDelete { get; set; }
        /// <summary>
        /// Indicator that triggers the search method after a form is closed.
        /// </summary>
        bool AutoSearchAfterFormClose { get; set; }
        /// <summary>
        /// Indicator that lets know when to trigger the search method
        /// </summary>
        bool TriggerSearchMethod { get; set; }
        /// <summary>
        /// This is the method that triggers the search method when you set this property to true
        /// </summary>
        bool TriggerSearch { get; set; }
        /// <summary>
        /// The trigger search method action callback
        /// </summary>
        Action<bool> TriggerSearchChanged { get; set; }
        /// <summary>
        /// The debounce interval to wait for the user input
        /// </summary>
        int DebounceInterval { get; set; }
        /// <summary>
        /// Method that gets fired only once and only after the first render as ocurred and everything is already initialized
        /// </summary>
        /// <returns></returns>
        Task OnAfterFirstRenderAsync();
        /// <summary>
        /// Show the form with the specified <see cref="Enums.FormState"/>
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        Task ShowFormChangeFormState(FormState formState);
        /// <summary>
        /// Show the form with the specified <see cref="Enums.FormState"/> and hides the option dialog
        /// </summary>
        /// <param name="formState"></param>
        /// <returns></returns>
        Task ShowFormChangeFormStateHideOptionsDialog(FormState formState);
        /// <summary>
        /// Hides the form, sets the <see cref="Enums.FormState"/> to <seealso cref="Enums.FormState.Create"/> and may <see cref="TriggerSearch"/> if <seealso cref="AutoSearchAfterFormClose"/> is true 
        /// </summary>
        void HideFormAfterFormCancel();
        /// <summary>
        /// Hides the options dialog
        /// </summary>
        /// <returns></returns>
        Task HideOptionsDialog();
    }
}

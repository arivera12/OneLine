using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation.Results;
using OneLine.Services;
using System.Threading.Tasks;

namespace OneLine.Blazor.Extensions
{
    public static class SweetAlertServiceExtension
    {
        /// <summary>
        /// Method that show a sweet alert dialog if there is any error on the <paramref name="FluentValidationResult"/> and localizes the message using the <paramref name="LanguageLocalizer"/>
        /// </summary>
        /// <param name="Swal"></param>
        /// <param name="FluentValidationResult"></param>
        /// <param name="localizer"></param>
        /// <param name="title"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static async Task ShowFluentValidationsAlertMessageAsync(this SweetAlertService Swal, ValidationResult FluentValidationResult, IResourceManagerLocalizer localizer, string title = null, string style = "color:red")
        {
            if (FluentValidationResult != null && !FluentValidationResult.IsValid && FluentValidationResult.Errors.Count > 0)
            {
                string validations = "";
                foreach (var item in FluentValidationResult.Errors)
                {
                    validations += $"<div>{localizer.ResourceManager.GetString(item.ErrorMessage)}</div>";
                }
                string validationMessage = $@"<div style=""{style}"">{validations}</div>";
                await Swal.FireAsync(title, validationMessage, SweetAlertIcon.Error);
            }
        }
        /// <summary>
        /// Shows a confirm alert dialog
        /// </summary>
        /// <param name="Swal"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="confirmButtonText"></param>
        /// <param name="cancelButtonText"></param>
        /// <returns></returns>
        public static async Task<bool> ShowConfirmAlertAsync(this SweetAlertService Swal, 
            string title, 
            string text,
            string confirmButtonText,
            string cancelButtonText)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = title,
                Text = text,
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = confirmButtonText,
                CancelButtonText = cancelButtonText,
                AllowEnterKey = true,
                AllowEscapeKey = true,
                AllowOutsideClick = false
            });
            return !string.IsNullOrWhiteSpace(result.Value);
        }
        /// <summary>
        /// Shows sweet alert loader dialog
        /// </summary>
        /// <param name="Swal"></param>
        /// <param name="sweetAlertCallback"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task ShowLoaderAsync(this SweetAlertService Swal, SweetAlertCallback sweetAlertCallback, string title = null, string text = null)
        {
            await Swal.FireAsync(new SweetAlertOptions()
            {
                AllowEnterKey = false,
                AllowEscapeKey = false,
                AllowOutsideClick = false,
                Title = title,
                Text = text,
                OnBeforeOpen = new SweetAlertCallback(async () => await Swal.ShowLoadingAsync()),
                OnOpen = sweetAlertCallback
            });
        }
        /// <summary>
        /// Hides the sweet alert loader dialog
        /// </summary>
        /// <param name="Swal"></param>
        /// <returns></returns>
        public static async Task HideLoaderAsync(this SweetAlertService Swal)
        {
            await Swal.CloseAsync();
        }
        /// <summary>
        /// Hides the sweet aler loader dialog and fires a chained callback
        /// </summary>
        /// <param name="Swal"></param>
        /// <param name="sweetAlertCallback"></param>
        /// <returns></returns>
        public static async Task HideLoaderAsync(this SweetAlertService Swal, SweetAlertCallback sweetAlertCallback)
        {
            await Swal.CloseAsync();
            sweetAlertCallback?.InvokeAsync();
        }
    }
}

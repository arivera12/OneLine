using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace OneLine.Blazor.Extensions
{
    public static class SweetAlertServiceExtension
    {
        public static async Task ShowFluentValidationsAlertMessageAsync(this SweetAlertService Swal, ValidationResult FluentValidationResult, string title = null, string style = "color:red")
        {
            if (FluentValidationResult != null && !FluentValidationResult.IsValid && FluentValidationResult.Errors.Count > 0)
            {
                string validations = "";
                foreach (var item in FluentValidationResult.Errors)
                {
                    validations += $"<div>{Resourcer.GetString(item.ErrorMessage)}</div>";
                }
                string validationMessage = $@"<div style=""{style}"">{validations}</div>";
                await Swal.FireAsync(title, validationMessage, SweetAlertIcon.Error);
            }
        }
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
        public static async Task HideLoaderAsync(this SweetAlertService Swal)
        {
            await Swal.CloseAsync();
        }
        public static async Task HideLoaderAsync(this SweetAlertService Swal, SweetAlertCallback sweetAlertCallback)
        {
            await Swal.CloseAsync();
            sweetAlertCallback?.InvokeAsync();
        }
    }
}

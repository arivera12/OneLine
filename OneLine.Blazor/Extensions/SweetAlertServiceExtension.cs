using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace OneLine.Blazor.Extensions
{
    public static class SweetAlertServiceExtension
    {
        public static async Task ShowFluentValidationsAlertMessageAsync(this SweetAlertService Swal, ValidationResult FluentValidationResult, string title = null)
        {
            if (FluentValidationResult != null && !FluentValidationResult.IsValid && FluentValidationResult.Errors.Count > 0)
            {
                string validations = "";
                foreach (var item in FluentValidationResult.Errors)
                {
                    validations += $"<li>{Resourcer.GetString(item.ErrorMessage)}</li>";
                }
                string validationMessage =
                    $@"<div class='alert alert-danger alert-dismissible fade show' role='alert'>
                            <ul>
                                {validations}
                            </ul>
                        </div>";
                await Swal.FireAsync(title, validationMessage, SweetAlertIcon.Error);
            }
        }
        public static async Task<bool> ShowGenericConfirmAlertAsync(this SweetAlertService Swal)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = Resourcer.GetString("Confirm"),
                Text = Resourcer.GetString("AreYouSureYouWantToPerformTheCurrentAction"),
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = Resourcer.GetString("Yes"),
                CancelButtonText = Resourcer.GetString("Cancel")
            });
            return !string.IsNullOrWhiteSpace(result.Value);
        }
        public static async Task ShowLoaderAsync(this SweetAlertService Swal, string title = null, string message = null)
        {
            await Swal.FireAsync(new SweetAlertOptions()
            {
                AllowEnterKey = false,
                AllowEscapeKey = false,
                AllowOutsideClick = false,
                Title = title,
                Text = message,
                OnBeforeOpen = new SweetAlertCallback(async () => await Swal.ShowLoadingAsync())
            });
        }
        public static async Task ShowLoaderAsync(this SweetAlertService Swal, SweetAlertCallback sweetAlertCallback, string title = null, string message = null)
        {
            await Swal.FireAsync(new SweetAlertOptions()
            {
                AllowEnterKey = false,
                AllowEscapeKey = false,
                AllowOutsideClick = false,
                Title = title,
                Text = message,
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

﻿using CurrieTechnologies.Razor.SweetAlert2;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace OneLine.Blazor.Extensions
{
    public static class SweetAlertServiceExtension
    {
        public static async Task ShowFluentValidationsAlertMessageAsync(this SweetAlertService Swal, ValidationResult FluentValidationResult, string title = null, string style = "list-style:none;color:red")
        {
            if (FluentValidationResult != null && !FluentValidationResult.IsValid && FluentValidationResult.Errors.Count > 0)
            {
                string validations = "";
                foreach (var item in FluentValidationResult.Errors)
                {
                    validations += $"<li>{Resourcer.GetString(item.ErrorMessage)}</li>";
                }
                string validationMessage = $@"<ul style=""{style}"">{validations}</ul>";
                await Swal.FireAsync(title, validationMessage, SweetAlertIcon.Error);
            }
        }
        public static async Task<bool> ShowConfirmAlertAsync(this SweetAlertService Swal, 
            string title = "Confirm", 
            string text = "AreYouSureYouWantToPerformTheCurrentAction",
            string confirmButtonText = "Yes",
            string cancelButtonText = "Cancel")
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = Resourcer.GetString(title),
                Text = Resourcer.GetString(text),
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = Resourcer.GetString(confirmButtonText),
                CancelButtonText = Resourcer.GetString(cancelButtonText),
            });
            return !string.IsNullOrWhiteSpace(result.Value);
        }
        public static async Task ShowLoaderAsync(this SweetAlertService Swal, string title = null, string text = null)
        {
            await Swal.FireAsync(new SweetAlertOptions()
            {
                AllowEnterKey = false,
                AllowEscapeKey = false,
                AllowOutsideClick = false,
                Title = title,
                Text = text,
                OnBeforeOpen = new SweetAlertCallback(async () => await Swal.ShowLoadingAsync())
            });
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
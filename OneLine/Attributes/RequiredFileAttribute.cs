using Microsoft.AspNetCore.Mvc.Filters;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System;
using System.IO;
using System.Linq;

namespace OneLine.Attributes
{
    /// <summary>
    /// Validates that a required binary data have been send on the request
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class RequiredBlobAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The form field name to lookup 
        /// </summary>
        public string FormFieldName { get; set; }
        /// <summary>
        /// The maximun allowed files. Default value: 1.
        /// </summary>
        public short AllowedMaximunFiles { get; set; } = 1;
        /// <summary>
        /// The minimun allowed files. Default value: 1.
        /// </summary>
        public short AllowedMinimunFiles { get; set; } = 1;
        /// <summary>
        /// The allowed max length in bytes. Default value: int.MaxValue = 2048 MegaBytes = 2 Gigabytes.
        /// </summary>
        public long AllowedBlobMaxLength { get; set; } = int.MaxValue;
        /// <summary>
        /// The allowed content types
        /// </summary>
        public string[] AllowedContentTypes { get; set; }
        /// <summary>
        /// The allowed extensions
        /// </summary>
        public string[] AllowedExtensions { get; set; }
        /// <summary>
        /// The allowed content dispositions
        /// </summary>
        public string[] AllowedContentDispositions { get; set; }
        /// <summary>
        /// Validates that a required binary data have been send on the request
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AllowedBlobMaxLength <= 0)
            {
                throw new ArgumentException("The AllowedBlobMaxLength can't be zero or less.");
            }
            if (AllowedMinimunFiles <= 0)
            {
                throw new ArgumentException("The AllowedMinimunFiles can't be zero or less.");
            }
            if (AllowedMaximunFiles <= 0)
            {
                throw new ArgumentException("The AllowedMaximunFiles can't be zero or less.");
            }
            var formFiles = filterContext.HttpContext.Request.Form.Files;
            var blobs = string.IsNullOrWhiteSpace(FormFieldName) ? formFiles : formFiles.Where(w => w.Name == FormFieldName);
            if (!blobs.Any())
            {
                filterContext.Result = new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "FileUploadRequired" }.ToJsonActionResult();
            }
            else
            {
                if (blobs.Count() < AllowedMinimunFiles)
                {
                    filterContext.Result = new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheCountOfFilesUploadedHasExceededTheMaximunAllowedFiles" }.ToJsonActionResult();
                }
                if (blobs.Count() > AllowedMaximunFiles)
                {
                    filterContext.Result = new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheCountOfFilesUploadedIsLessThanTheMinimunAllowedFiles" }.ToJsonActionResult();
                }
                foreach (var blob in blobs)
                {
                    if (blob == null || blob.Length == 0)
                    {
                        filterContext.Result = new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "FileUploadRequired", Data = blob.Name }.ToJsonActionResult();
                    }
                    else if (blob.Length > AllowedBlobMaxLength)
                    {
                        filterContext.Result = new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileHasExceededTheLargestSizeAllowed", Data = blob.Name }.ToJsonActionResult();
                    }
                    else if (AllowedContentTypes != null && AllowedContentTypes.Any() && !AllowedContentTypes.Contains(blob.ContentType))
                    {
                        filterContext.Result = new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedContentType", Data = blob.Name }.ToJsonActionResult();
                    }
                    else if (AllowedExtensions != null && AllowedExtensions.Any() && !AllowedExtensions.Contains(Path.GetExtension(blob.FileName)))
                    {
                        filterContext.Result = new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedExtension", Data = blob.Name }.ToJsonActionResult();
                    }
                    else if (AllowedContentDispositions != null && AllowedContentDispositions.Any() && !AllowedContentDispositions.Contains(blob.ContentDisposition))
                    {
                        filterContext.Result = new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedContentDisposition", Data = blob.Name }.ToJsonActionResult();
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
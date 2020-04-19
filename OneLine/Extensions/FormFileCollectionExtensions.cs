using Microsoft.AspNetCore.Http;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.IO;
using System.Linq;

namespace OneLine.Extensions
{
    public static class FormFileCollectionExtensions
    {
        public static bool FormFileExists(this IFormFileCollection formFiles, Func<IFormFile, bool> FormBlob)
        {
            return formFiles != null && formFiles.Any(FormBlob);
        }
        public static bool IsValidFormFile(this IFormFileCollection formFiles, FormFileRules formFileRules)
        {
            return IsValidFormFile(formFiles, null, formFileRules);
        }
        public static bool IsValidFormFile(this IFormFileCollection formFiles, Func<IFormFile, bool> FormBlob, FormFileRules formFileRules)
        {
            return IsValidFormFileApiResponse(formFiles, FormBlob, formFileRules).Status == ApiResponseStatus.Succeeded;
        }
        public static ApiResponse<string> IsValidFormFileApiResponse(this IFormFileCollection formFiles, FormFileRules formFileRules)
        {
            return IsValidFormFileApiResponse(formFiles, null, formFileRules);
        }
        public static ApiResponse<string> IsValidFormFileApiResponse(this IFormFileCollection formFiles, Func<IFormFile, bool> FormBlob, FormFileRules formFileRules)
        {
            if (formFileRules.AllowedBlobMaxLength <= 0)
            {
                throw new ArgumentException("The AllowedBlobMaxLength can't be zero or less.");
            }
            if (formFileRules.AllowedMinimunFiles <= 0)
            {
                throw new ArgumentException("The AllowedMinimunFiles can't be zero or less.");
            }
            if (formFileRules.AllowedMaximunFiles <= 0)
            {
                throw new ArgumentException("The AllowedMaximunFiles can't be zero or less.");
            }
            var blobs = FormBlob == null ? formFiles : formFiles.Where(FormBlob);
            if (!blobs.Any())
            {
                return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "FileUploadRequired" };
            }
            else
            {
                if (blobs.Count() < formFileRules.AllowedMinimunFiles)
                {
                    return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheCountOfFilesUploadedHasExceededTheMaximunAllowedFiles" };
                }
                if (blobs.Count() > formFileRules.AllowedMaximunFiles)
                {
                    return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheCountOfFilesUploadedIsLessThanTheMinimunAllowedFiles" };
                }
                foreach (var blob in blobs)
                {
                    if (blob == null || blob.Length == 0)
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "FileUploadRequired", Data = blob.Name };
                    }
                    else if (blob.Length > formFileRules.AllowedBlobMaxLength)
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileHasExceededTheLargestSizeAllowed", Data = blob.Name };
                    }
                    else if (formFileRules.AllowedContentTypes != null && formFileRules.AllowedContentTypes.Any() && !formFileRules.AllowedContentTypes.Contains(blob.ContentType))
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedContentType", Data = blob.Name };
                    }
                    else if (formFileRules.AllowedExtensions != null && formFileRules.AllowedExtensions.Any() && !formFileRules.AllowedExtensions.Contains(Path.GetExtension(blob.FileName)))
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedExtension", Data = blob.Name };
                    }
                    else if (formFileRules.AllowedContentDispositions != null && formFileRules.AllowedContentDispositions.Any() && !formFileRules.AllowedContentDispositions.Contains(blob.ContentDisposition))
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedContentDisposition", Data = blob.Name };
                    }
                }
            }
            return new ApiResponse<string>() { Status = ApiResponseStatus.Succeeded };
        }
    }
}

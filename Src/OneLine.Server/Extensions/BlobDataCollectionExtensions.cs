using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OneLine.Extensions
{
    public static class BlobDataCollectionExtensions
    {
        public static bool BlobDataExists(this IEnumerable<IBlobData> blobDatas, Func<IBlobData, bool> predicate)
        {
            return blobDatas.IsNotNull() && blobDatas.Any() && blobDatas.Any(predicate);
        }
        public static bool IsValidBlobData(this IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules)
        {
            return IsValidBlobData(blobDatas, null, formFileRules);
        }
        public static bool IsValidBlobData(this IEnumerable<IBlobData> blobDatas, Func<IBlobData, bool> predicate, IFormFileRules formFileRules)
        {
            return IsValidBlobDataApiResponse(blobDatas, predicate, formFileRules).Status == ApiResponseStatus.Succeeded;
        }
        public static ApiResponse<string> IsValidBlobDataApiResponse(this IEnumerable<IBlobData> blobDatas, IFormFileRules formFileRules)
        {
            return IsValidBlobDataApiResponse(blobDatas, null, formFileRules);
        }
        public static ApiResponse<string> IsValidBlobDataApiResponse(this IEnumerable<IBlobData> blobDatas, Func<IBlobData, bool> predicate, IFormFileRules formFileRules)
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
            var blobs = predicate == null ? blobDatas : blobDatas.Where(predicate);
            if ((blobs.IsNull() || !blobDatas.Any()) && formFileRules.IsRequired || formFileRules.ForceUpload)
            {
                return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "FileUploadRequired" };
            }
            else if (blobs.IsNotNull() && blobs.Any())
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
                    if (blob.IsNull() || blob.Size == 0)
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "FileUploadRequired", Data = blob.Name };
                    }
                    else if (blob.Size > formFileRules.AllowedBlobMaxLength)
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileHasExceededTheLargestSizeAllowed", Data = blob.Name };
                    }
                    else if (formFileRules.AllowedContentTypes != null && formFileRules.AllowedContentTypes.Any() && !formFileRules.AllowedContentTypes.Contains(blob.Type))
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedContentType", Data = blob.Name };
                    }
                    else if (formFileRules.AllowedExtensions != null && formFileRules.AllowedExtensions.Any() && !formFileRules.AllowedExtensions.Contains(Path.GetExtension(blob.Name)))
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedExtension", Data = blob.Name };
                    }
                    else if (formFileRules.AllowedContentDispositions != null && formFileRules.AllowedContentDispositions.Any() && !formFileRules.AllowedContentDispositions.Contains(blob.Type))
                    {
                        return new ApiResponse<string>() { Status = ApiResponseStatus.Failed, Message = "TheFileDoesNotHaveTheExpectedContentDisposition", Data = blob.Name };
                    }
                }
            }
            return new ApiResponse<string>() { Status = ApiResponseStatus.Succeeded };
        }
    }
}

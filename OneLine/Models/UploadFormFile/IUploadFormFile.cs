using Microsoft.AspNetCore.Http;
using System;

namespace OneLine.Models
{
    public interface IUploadFormFile
    {
        /// <summary>
        /// The form file filter
        /// </summary>
        Func<IFormFile, bool> Predicate { get; set; }
        /// <summary>
        /// The file input name. This field must and should match the property name where blob reference will be stored.
        /// </summary>
        string FileInputName { get; set; }
        /// <summary>
        /// The form file rules.
        /// </summary>
        IFormFileRules FormFileRules { get; set; }
        /// <summary>
        /// Tells when to upload only one or multiple files. Remember set the FormFileRules to specify minimun and maximun. 
        /// </summary>
        bool IsMultiple { get; set; }
        /// <summary>
        /// Forces a file upload on update operation.
        /// </summary>
        bool ForceUploadOnUpdate { get; set; }
    }
}

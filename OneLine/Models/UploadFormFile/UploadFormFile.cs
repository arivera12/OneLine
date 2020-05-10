using Microsoft.AspNetCore.Http;
using System;

namespace OneLine.Models
{
    public class UploadFormFile : IUploadFormFile
    {
        /// <summary>
        /// The form file filter
        /// </summary>
        public virtual Func<IFormFile, bool> Predicate { get; set; }
        /// <summary>
        /// The file input name. This field must and should match the property name where blob reference will be stored.
        /// </summary>
        public virtual string FileInputName { get; set; }
        /// <summary>
        /// The form file rules.
        /// </summary>
        public virtual IFormFileRules FormFileRules { get; set; }
        /// <summary>
        /// Tells when to upload only one or multiple files. Remember set the FormFileRules to specify minimun and maximun. 
        /// </summary>
        public virtual bool IsMultiple { get; set; }
        /// <summary>
        /// Forces a file upload on update operation.
        /// </summary>
        public virtual bool ForceUploadOnUpdate { get; set; }
        public UploadFormFile()
        { }
        public UploadFormFile(Func<IFormFile, bool> predicate, string fileInputName, IFormFileRules formFileRules)
        {
            Predicate = predicate;
            FileInputName = fileInputName;
            FormFileRules = formFileRules;
        }
        public UploadFormFile(Func<IFormFile, bool> predicate, string fileInputName, IFormFileRules formFileRules, bool isMultiple, bool forceUploadOnUpdate)
        {
            Predicate = predicate;
            FileInputName = fileInputName;
            FormFileRules = formFileRules;
            IsMultiple = isMultiple;
            ForceUploadOnUpdate = forceUploadOnUpdate;
        }
    }
}
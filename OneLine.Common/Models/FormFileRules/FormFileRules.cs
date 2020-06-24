using System.Collections.Generic;

namespace OneLine.Models
{
    public class FormFileRules : IFormFileRules
    {
        /// <summary>
        /// Specify what file types the user can pick from the device file system. This property may be used for browser application only.
        /// </summary>
        public virtual string Accept { get; set; }
        /// <summary>
        /// Validates if the file is required.
        /// </summary>
        public virtual bool IsRequired { get; set; }
        /// <summary>
        /// The property name which rules are being validating.
        /// </summary>
        public virtual string PropertyName { get; set; }
        /// <summary>
        /// The maximun allowed files.
        /// </summary>
        public virtual short AllowedMaximunFiles { get; set; }
        /// <summary>
        /// The minimun allowed files.
        /// </summary>
        public virtual short AllowedMinimunFiles { get; set; }
        /// <summary>
        /// The allowed max length in bytes
        /// </summary>
        public virtual long AllowedBlobMaxLength { get; set; }
        /// <summary>
        /// The allowed content types
        /// </summary>
        public virtual IEnumerable<string> AllowedContentTypes { get; set; }
        /// <summary>
        /// The allowed extensions
        /// </summary>
        public virtual IEnumerable<string> AllowedExtensions { get; set; }
        /// <summary>
        /// The allowed content dispositions
        /// </summary>
        public virtual IEnumerable<string> AllowedContentDispositions { get; set; }
        /// <summary>
        /// Forces a file upload. Overrides IsRequired property rule.
        /// </summary>
        public virtual bool ForceUpload { get; set; }
        public FormFileRules()
        { }
        public FormFileRules(bool isRequired, string propertyName, string accept)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
        }
        public FormFileRules(bool isRequired, string propertyName, string accept, long allowedBlobMaxLength)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedBlobMaxLength = allowedBlobMaxLength;
        }
        public FormFileRules(bool isRequired, string propertyName, string accept, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedExtensions = allowedExtensions;
        }
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
        }
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedExtensions = allowedExtensions;
        }
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions, IEnumerable<string> allowedContentTypes)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedExtensions = allowedExtensions;
            AllowedContentTypes = allowedContentTypes;
        }
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions, IEnumerable<string> allowedContentTypes, IEnumerable<string> allowedContentDispositions)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedContentTypes = allowedContentTypes;
            AllowedExtensions = allowedExtensions;
            AllowedContentDispositions = allowedContentDispositions;
        }
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions, IEnumerable<string> allowedContentTypes, IEnumerable<string> allowedContentDispositions, bool forceUpload)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedContentTypes = allowedContentTypes;
            AllowedExtensions = allowedExtensions;
            AllowedContentDispositions = allowedContentDispositions;
            ForceUpload = forceUpload;
        }
    }
}
using System.Collections.Generic;

namespace OneLine.Models
{
    public class FormFileRules : IFormFileRules
    {
        /// <summary>
        /// Validates if the file is required.
        /// </summary>
        public virtual bool IsRequired { get; set; }
        /// <summary>
        /// The maximun allowed files. Default value: 1.
        /// </summary>
        public virtual short AllowedMaximunFiles { get; set; } = 1;
        /// <summary>
        /// The minimun allowed files. Default value: 1.
        /// </summary>
        public virtual short AllowedMinimunFiles { get; set; } = 1;
        /// <summary>
        /// The allowed max length in bytes. Default value: int.MaxValue = 2048 MegaBytes = 2 Gigabytes.
        /// </summary>
        public virtual long AllowedBlobMaxLength { get; set; } = int.MaxValue;
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
        public FormFileRules()
        { }
        public FormFileRules(bool isRequired)
        {
            IsRequired = isRequired;
        }
        public FormFileRules(bool isRequired, long allowedBlobMaxLength)
        {
            IsRequired = isRequired;
            AllowedBlobMaxLength = allowedBlobMaxLength;
        }
        public FormFileRules(bool isRequired, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions)
        {
            IsRequired = isRequired;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedExtensions = allowedExtensions;
        }
        public FormFileRules(bool isRequired, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength)
        {
            IsRequired = isRequired;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
        }
        public FormFileRules(bool isRequired, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions)
        {
            IsRequired = isRequired;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedExtensions = allowedExtensions;
        }
        public FormFileRules(bool isRequired, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions, IEnumerable<string> allowedContentTypes)
        {
            IsRequired = isRequired;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedExtensions = allowedExtensions;
            AllowedContentTypes = allowedContentTypes;
        }
        public FormFileRules(bool isRequired, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions, IEnumerable<string> allowedContentTypes, IEnumerable<string> allowedContentDispositions)
        {
            IsRequired = isRequired;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedContentTypes = allowedContentTypes;
            AllowedExtensions = allowedExtensions;
            AllowedContentDispositions = allowedContentDispositions;
        }
    }
}
using System.Collections.Generic;

namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure to set rules to a form file
    /// </summary>
    public class FormFileRules : IFormFileRules
    {
        /// <inheritdoc/>
        public virtual string Accept { get; set; }
        /// <inheritdoc/>
        public virtual bool IsRequired { get; set; }
        /// <inheritdoc/>
        public virtual string PropertyName { get; set; }
        /// <inheritdoc/>
        public virtual short AllowedMaximunFiles { get; set; }
        /// <inheritdoc/>
        public virtual short AllowedMinimunFiles { get; set; }
        /// <inheritdoc/>
        public virtual long AllowedBlobMaxLength { get; set; }
        /// <inheritdoc/>
        public virtual IEnumerable<string> AllowedContentTypes { get; set; }
        /// <inheritdoc/>
        public virtual IEnumerable<string> AllowedExtensions { get; set; }
        /// <inheritdoc/>
        public virtual IEnumerable<string> AllowedContentDispositions { get; set; }
        /// <inheritdoc/>
        public virtual bool ForceUpload { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public FormFileRules()
        { }
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        public FormFileRules(bool isRequired, string propertyName, string accept)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
        }
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        public FormFileRules(bool isRequired, string propertyName, string accept, long allowedBlobMaxLength)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedBlobMaxLength = allowedBlobMaxLength;
        }
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
        public FormFileRules(bool isRequired, string propertyName, string accept, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedExtensions = allowedExtensions;
        }
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedMaximunFiles">The maximun allowed files</param>
        /// <param name="allowedMinimunFiles">The minimun required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximunFiles, short allowedMinimunFiles, long allowedBlobMaxLength)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximunFiles = allowedMaximunFiles;
            AllowedMinimunFiles = allowedMinimunFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
        }
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedMaximunFiles">The maximun allowed files</param>
        /// <param name="allowedMinimunFiles">The minimun required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
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
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedMaximunFiles">The maximun allowed files</param>
        /// <param name="allowedMinimunFiles">The minimun required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
        /// <param name="allowedContentTypes">The allowed content types</param>
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
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedMaximunFiles">The maximun allowed files</param>
        /// <param name="allowedMinimunFiles">The minimun required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
        /// <param name="allowedContentTypes">The allowed content types</param>
        /// <param name="allowedContentDispositions">The allowed content dispositions</param>
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
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedMaximunFiles">The maximun allowed files</param>
        /// <param name="allowedMinimunFiles">The minimun required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
        /// <param name="allowedContentTypes">The allowed content types</param>
        /// <param name="allowedContentDispositions">The allowed content dispositions</param>
        /// <param name="forceUpload">Forces a file/s to be uploaded</param>
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
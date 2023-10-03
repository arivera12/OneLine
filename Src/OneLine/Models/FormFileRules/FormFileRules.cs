namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure to set rules to a form file
    /// </summary>
    public class FormFileRules : IFormFileRules
    {
        /// <inheritdoc/>
        public string Accept { get; set; }
        /// <inheritdoc/>
        public bool IsRequired { get; set; }
        /// <inheritdoc/>
        public string PropertyName { get; set; }
        /// <inheritdoc/>
        public short AllowedMaximumFiles { get; set; }
        /// <inheritdoc/>
        public short AllowedMinimumFiles { get; set; }
        /// <inheritdoc/>
        public long AllowedBlobMaxLength { get; set; }
        /// <inheritdoc/>
        public IEnumerable<string> AllowedContentTypes { get; set; }
        /// <inheritdoc/>
        public IEnumerable<string> AllowedExtensions { get; set; }
        /// <inheritdoc/>
        public IEnumerable<string> AllowedContentDispositions { get; set; }
        /// <inheritdoc/>
        public bool ForceUpload { get; set; }
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
        /// <param name="allowedMaximumFiles">The maximum allowed files</param>
        /// <param name="allowedMinimumFiles">The minimum required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximumFiles, short allowedMinimumFiles, long allowedBlobMaxLength)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximumFiles = allowedMaximumFiles;
            AllowedMinimumFiles = allowedMinimumFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
        }
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedMaximumFiles">The maximum allowed files</param>
        /// <param name="allowedMinimumFiles">The minimum required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximumFiles, short allowedMinimumFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximumFiles = allowedMaximumFiles;
            AllowedMinimumFiles = allowedMinimumFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedExtensions = allowedExtensions;
        }
        /// <summary>
        /// Form file Rules
        /// </summary>
        /// <param name="isRequired">Is the form file required?</param>
        /// <param name="propertyName">The property name which is being validated</param>
        /// <param name="accept">Tells the input file dialog which file types accept</param>
        /// <param name="allowedMaximumFiles">The maximum allowed files</param>
        /// <param name="allowedMinimumFiles">The minimum required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
        /// <param name="allowedContentTypes">The allowed content types</param>
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximumFiles, short allowedMinimumFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions, IEnumerable<string> allowedContentTypes)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximumFiles = allowedMaximumFiles;
            AllowedMinimumFiles = allowedMinimumFiles;
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
        /// <param name="allowedMaximumFiles">The maximum allowed files</param>
        /// <param name="allowedMinimumFiles">The minimum required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
        /// <param name="allowedContentTypes">The allowed content types</param>
        /// <param name="allowedContentDispositions">The allowed content dispositions</param>
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximumFiles, short allowedMinimumFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions, IEnumerable<string> allowedContentTypes, IEnumerable<string> allowedContentDispositions)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximumFiles = allowedMaximumFiles;
            AllowedMinimumFiles = allowedMinimumFiles;
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
        /// <param name="allowedMaximumFiles">The maximum allowed files</param>
        /// <param name="allowedMinimumFiles">The minimum required files</param>
        /// <param name="allowedBlobMaxLength">The maximum file size in measured in bytes</param>
        /// <param name="allowedExtensions">The allowed file extensions</param>
        /// <param name="allowedContentTypes">The allowed content types</param>
        /// <param name="allowedContentDispositions">The allowed content dispositions</param>
        /// <param name="forceUpload">Forces a file/s to be uploaded</param>
        public FormFileRules(bool isRequired, string propertyName, string accept, short allowedMaximumFiles, short allowedMinimumFiles, long allowedBlobMaxLength, IEnumerable<string> allowedExtensions, IEnumerable<string> allowedContentTypes, IEnumerable<string> allowedContentDispositions, bool forceUpload)
        {
            IsRequired = isRequired;
            PropertyName = propertyName;
            Accept = accept;
            AllowedMaximumFiles = allowedMaximumFiles;
            AllowedMinimumFiles = allowedMinimumFiles;
            AllowedBlobMaxLength = allowedBlobMaxLength;
            AllowedContentTypes = allowedContentTypes;
            AllowedExtensions = allowedExtensions;
            AllowedContentDispositions = allowedContentDispositions;
            ForceUpload = forceUpload;
        }
    }
}
using System.Collections.Generic;

namespace OneLine.Models
{
    public interface IFormFileRules
    {
        /// <summary>
        /// Specify what file types the user can pick from the device file system. This property may be used for browser application only.
        /// </summary>
        string Accept { get; set; }
        /// <summary>
        /// Validates if the file is required.
        /// </summary>
        bool IsRequired { get; set; }
        /// <summary>
        /// The property name which rules are being validating.
        /// </summary>
        string PropertyName { get; set; }
        /// <summary>
        /// The maximun allowed files.
        /// </summary>
        short AllowedMaximunFiles { get; set; }
        /// <summary>
        /// The minimun allowed files.
        /// </summary>
        short AllowedMinimunFiles { get; set; }
        /// <summary>
        /// The allowed max length in bytes.
        /// </summary>
        long AllowedBlobMaxLength { get; set; }
        /// <summary>
        /// The allowed content types
        /// </summary>
        IEnumerable<string> AllowedContentTypes { get; set; }
        /// <summary>
        /// The allowed extensions
        /// </summary>
        IEnumerable<string> AllowedExtensions { get; set; }
        /// <summary>
        /// The allowed content dispositions
        /// </summary>
        IEnumerable<string> AllowedContentDispositions { get; set; }
        /// <summary>
        /// Forces a file upload. Overrides IsRequired property rule.
        /// </summary>
        bool ForceUpload { get; set; }
    }
}

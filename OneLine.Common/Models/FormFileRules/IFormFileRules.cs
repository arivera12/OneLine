using System.Collections.Generic;

namespace OneLine.Models
{
    public interface IFormFileRules
    {
        /// <summary>
        /// Validates if the file is required.
        /// </summary>
        bool IsRequired { get; set; }
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
    }
}

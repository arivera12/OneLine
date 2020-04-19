using System.Collections.Generic;

namespace OneLine.Models
{
    public interface IFormFileRules
    {
        /// <summary>
        /// The maximun allowed files. Default value: 1.
        /// </summary>
        short AllowedMaximunFiles { get; set; }
        /// <summary>
        /// The minimun allowed files. Default value: 1.
        /// </summary>
        short AllowedMinimunFiles { get; set; }
        /// <summary>
        /// The allowed max length in bytes. Default value: int.MaxValue = 2048 MegaBytes = 2 Gigabytes.
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

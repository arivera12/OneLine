
using System.Collections.Generic;

namespace OneLine.Models
{
    public class FormFileRules : IFormFileRules
    {
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
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneLine.Models
{
    [NotMapped]
    /// <summary>
    /// Implements a structure to hold binary data
    /// </summary>
    public class BlobData : IBlobData
    {
        /// <inheritdoc/>
        public DateTime LastModified { get; set; }
        /// <inheritdoc/>
        public string Name { get; set; }
        /// <inheritdoc/>
        public string InputName { get; set; }
        /// <inheritdoc/>
        public long Size { get; set; }
        /// <inheritdoc/>
        public string Type { get; set; }
        /// <inheritdoc/>
        public byte[] Data { get; set; }
    }
}

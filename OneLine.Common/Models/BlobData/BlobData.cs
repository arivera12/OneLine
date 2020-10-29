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
        public virtual DateTime LastModified { get; set; }
        /// <inheritdoc/>
        public virtual string Name { get; set; }
        /// <inheritdoc/>
        public virtual string InputName { get; set; }
        /// <inheritdoc/>
        public virtual long Size { get; set; }
        /// <inheritdoc/>
        public virtual string Type { get; set; }
        /// <inheritdoc/>
        public virtual byte[] Data { get; set; }
    }
}


using System;
using System.IO;

namespace OneLine.Models
{
    public class BlobData : IBlobData
    {
        /// <summary>
        /// Last modified
        /// </summary>
        public virtual DateTime LastModified { get; set; }
        /// <summary>
        /// The File Name
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// The File Input Name
        /// </summary>
        public virtual string InputName { get; set; }
        /// <summary>
        /// The file size
        /// </summary>
        public virtual long Size { get; set; }
        /// <summary>
        /// The file type
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// The file stream data
        /// </summary>
        public virtual byte[] Data { get; set; }
    }
}

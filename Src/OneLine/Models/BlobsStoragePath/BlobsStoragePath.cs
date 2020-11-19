namespace OneLine.Models
{
    /// <summary>
    /// This class implements a basic blob storage path definition
    /// </summary>
    public class BlobsStoragePath : IBlobsStoragePath
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string PublicUploadFilePath { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual string PrivateUploadFilePath { get; set; }
    }
}

namespace OneLine.Models
{
    public class BlobsStoragePath : IBlobsStoragePath
    {
        /// <summary>
        /// Public web server path
        /// </summary>
        public virtual string PublicUploadFilePath { get; set; }
        /// <summary>
        /// Private web server path
        /// </summary>
        public virtual string PrivateUploadFilePath { get; set; }
    }
}

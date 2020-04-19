

namespace OneLine.Models
{
    public class BlobsStoragePath : IBlobsStoragePath
    {
        /// <summary>
        /// Public web server path
        /// </summary>
        public virtual string PublicWebServerBaseUploadFilePath { get; set; }
        /// <summary>
        /// Private web server path
        /// </summary>
        public virtual string PrivateWebServerBaseUploadFilePath { get; set; }
    }
}

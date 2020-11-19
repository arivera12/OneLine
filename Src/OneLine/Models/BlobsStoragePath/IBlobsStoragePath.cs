namespace OneLine.Models
{
    /// <summary>
    /// Defines a basic blob storage path
    /// </summary>
    public interface IBlobsStoragePath
    {
        /// <summary>
        /// Public web server path
        /// </summary>
        string PublicUploadFilePath { get; set; }
        /// <summary>
        /// Private web server path
        /// </summary>
        string PrivateUploadFilePath { get; set; }
    }
}

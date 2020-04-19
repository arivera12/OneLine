namespace OneLine.Models
{
    public interface IBlobsStoragePath
    {
        /// <summary>
        /// Public web server path
        /// </summary>
        string PublicWebServerBaseUploadFilePath { get; set; }
        /// <summary>
        /// Private web server path
        /// </summary>
        string PrivateWebServerBaseUploadFilePath { get; set; }
    }
}

namespace OneLine.Contracts
{
    /// <summary>
    /// A save file service
    /// </summary>
    public interface ISaveFile
    {
        /// <summary>
        /// Saves or downloads a file
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="pathOrDownloadFileName"></param>
        /// <returns></returns>
        Task SaveFileAsync(Stream stream, string pathOrDownloadFileName);
        /// <summary>
        /// Saves or downloads a file
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="pathOrDownloadFileName"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        Task SaveFileAsync(Stream stream, string pathOrDownloadFileName, int bufferSize);
        /// <summary>
        /// Saves or downloads a file
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="pathOrDownloadFileName"></param>
        /// <returns></returns>
        Task SaveFileAsync(byte[] byteArray, string pathOrDownloadFileName);
    }
}

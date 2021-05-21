using System.IO;
using System.Threading.Tasks;

namespace OneLine.Services
{
    /// <summary>
    /// A save file service
    /// </summary>
    public interface ISaveFile
    {
        Task SaveFileAsync(Stream stream, string pathOrDownloadFileName);
        Task SaveFileAsync(Stream stream, string pathOrDownloadFileName, int bufferSize);
        Task SaveFileAsync(byte[] byteArray, string pathOrDownloadFileName);
    }
}

using System.IO;
using System.Threading.Tasks;

namespace OneLine.Blazor.Services
{
    public interface ISaveFile
    {
        Task SaveFileAsync(Stream stream, string path);
        Task SaveFileAsync(Stream stream, string path, int bufferSize);
        Task SaveFileAsync(byte[] byteArray, string path);
    }
}

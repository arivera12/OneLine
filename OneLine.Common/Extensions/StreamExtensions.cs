using System.IO;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            var streamLength = (int)stream.Length;
            var data = new byte[streamLength];
            stream.Read(data, 0, streamLength);
            return data;
        }
        public static async Task<byte[]> ToByteArrayAsync(this Stream stream)
        {
            var streamLength = (int)stream.Length;
            var data = new byte[streamLength];
            await stream.ReadAsync(data, 0, streamLength);
            return data;
        }
    }
}


using System.IO;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads and converts a <see cref="Stream"/> into a <see cref="byte[]"/>. Take note that you may not use this method for huge files.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Stream stream)
        {
            var streamLength = (int)stream.Length;
            var data = new byte[streamLength];
            stream.Read(data, 0, streamLength);
            return data;
        }
        /// <summary>
        /// Asynchronously reads and converts a <see cref="Stream"/> into a <see cref="byte[]"/>. Take note that you may not use this method for huge files.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<byte[]> ToByteArrayAsync(this Stream stream)
        {
            var streamLength = (int)stream.Length;
            var data = new byte[streamLength];
            await stream.ReadAsync(data, 0, streamLength);
            return data;
        }
    }
}


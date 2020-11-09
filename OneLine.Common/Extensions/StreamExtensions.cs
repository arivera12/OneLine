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
        /// <summary>
        /// Copies the contents of input to output. Doesn't close either stream.
        /// </summary>
        public static async Task CopyStreamAsync(this Stream input, Stream output, int bufferSize)
        {
            if (bufferSize <= 0)
                throw new System.Exception("The bufferSize is zero or less");
            byte[] buffer = new byte[bufferSize];
            int len;
            while ((len = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await output.WriteAsync(buffer, 0, len);
            }
        }
        /// <summary>
        /// Copies the contents of the stream to the file system. 
        /// Doesn't close the input stream but closes the output stream after write is completed.
        /// The recommended buffer size is 32 KB but can be bigger for end devices.
        /// </summary>
        public static async Task WriteStreamToFileSystemAsync(this Stream input, string path, int bufferSize)
        {
            using Stream file = File.Create(path);
            await CopyStreamAsync(input, file, bufferSize);
        }
    }
}


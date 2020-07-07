using System.IO;

namespace OneLine.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            using MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
        public static byte[] ToByteArray(this Stream stream, int bufferSize)
        {
            using MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms, bufferSize);
            return ms.ToArray();
        }
    }
}


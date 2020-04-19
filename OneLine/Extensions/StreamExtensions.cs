using System.IO;

namespace OneLine.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            var streamLength = (int)stream.Length;
            var data = new byte[streamLength + 1];

            stream.Read(data, 0, streamLength);
            stream.Close();
            stream.Dispose();

            return data;
        }
    }
}


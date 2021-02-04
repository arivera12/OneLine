﻿using System.IO;
using System.Threading.Tasks;

namespace OneLine.Services
{
    /// <summary>
    /// A save file service
    /// </summary>
    public interface ISaveFile
    {
        Task SaveFileAsync(Stream stream, string path);
        Task SaveFileAsync(Stream stream, string path, int bufferSize);
        Task SaveFileAsync(byte[] byteArray, string path);
    }
}
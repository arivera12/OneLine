using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static class CsvHelperExtensions
    {
        /// <summary>
        /// Reads a csv from <see cref="Stream"/> and converts it to an <see cref="IEnumerable{T}"/>. This method specs that the stream is a real csv <see cref="Stream"/>.
        /// </summary>
        /// <typeparam name="T">The type to return</typeparam>
        /// <param name="stream">The stream</param>
        /// <returns></returns>
        public static IEnumerable<T> ReadCsv<T>(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<T>();
                }
            }
        }
        /// <summary>
        /// Reads a csv from <see cref="byte[]"/> and converts it to an <see cref="IEnumerable{T}"/>. This method specs that the stream is a real csv <see cref="byte[]"/>.
        /// </summary>
        /// <typeparam name="T">The type to retun</typeparam>
        /// <param name="byteArray">The byte array</param>
        /// <returns></returns>
        public static IEnumerable<T> ReadCsv<T>(this byte[] byteArray)
        {
            MemoryStream memoryStream = new MemoryStream(byteArray);
            using (var reader = new StreamReader(memoryStream))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<T>();
                }
            }
        }
        /// <summary>
        /// Reads a csv file from device and converts it to an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">The type to return</typeparam>
        /// <param name="path">The souce path to the csv file</param>
        /// <returns></returns>
        public static IEnumerable<T> ReadCsv<T>(string path)
        {
            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<T>();
                }
            }
        }
        /// <summary>
        /// Writes an <see cref="IEnumerable{T}"/> to a csv file to the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="enumerable">The collection</param>
        /// <param name="path">The source path to write the csv file</param>
        public static void WriteToCsv<T>(this IEnumerable<T> enumerable, string path)
        {
            if (enumerable.IsNull() || !enumerable.Any())
            {
                throw new ArgumentNullException("enumerable is null or empty");
            }
            using (var writer = new StreamWriter(path))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(enumerable);
                }
            }
        }
        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> to a csv byte array.
        /// </summary>
        /// <typeparam name="T">The collection type</typeparam>
        /// <param name="enumerable">The collection</param>
        /// <returns></returns>
        public static byte[] ToCsvByteArray<T>(this IEnumerable<T> enumerable)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(enumerable);
                    }
                }
                return memoryStream.ToArray();
            }
        }
        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> to a csv byte array asynchronously.
        /// </summary>
        /// <typeparam name="T">The collection type</typeparam>
        /// <param name="enumerable">The collection</param>
        /// <returns></returns>
        public static async Task<byte[]> ToCsvByteArrayAsync<T>(this IEnumerable<T> enumerable)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(enumerable);
                    }
                }
                return await memoryStream.ToByteArrayAsync();
            }
        }
        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> to a csv <see cref="MemoryStream"/>.
        /// </summary>
        /// <typeparam name="T">The collection type</typeparam>
        /// <param name="enumerable">The collection</param>
        /// <returns></returns>
        public static MemoryStream ToCsvMemoryStream<T>(this IEnumerable<T> enumerable)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(enumerable);
                    }
                }
                return memoryStream;
            }
        }
        /// <summary>
        /// Converts an <see cref="IEnumerable{T}"/> to a csv <see cref="Stream"/>.
        /// </summary>
        /// <typeparam name="T">The collection type</typeparam>
        /// <param name="enumerable">The collection</param>
        /// <returns></returns>
        public static Stream ToCsvStream<T>(this IEnumerable<T> enumerable)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(enumerable);
                    }
                }
                return memoryStream;
            }
        }
    }
}

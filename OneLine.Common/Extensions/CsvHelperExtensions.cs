using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace OneLine.Extensions
{
    public static class CsvHelperExtensions
    {
        public static IEnumerable<T> ReadCsv<T>(this IEnumerable<T> enumerable, string path) where T : class
        {
            if (enumerable == null || !enumerable.Any())
            {
                throw new ArgumentNullException("enumerable is null or empty");
            }
            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<T>();
                }
            }
        }

        public static void WriteToCsv<T>(this IEnumerable<T> enumerable, string path) where T : class
        {
            if (enumerable == null || !enumerable.Any())
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

        public static byte[] ToCsvByteArray<T>(this IEnumerable<T> enumerable) where T : class
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords<T>(enumerable);
                    }
                }
                return memoryStream.ToArray();
            }
        }
    }
}

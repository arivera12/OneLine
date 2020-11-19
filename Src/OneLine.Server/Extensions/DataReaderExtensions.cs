using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OneLine.Extensions
{
    public static class DataReaderExtensions
    {
        /// <summary>
        /// This method reader data reader and yield record to strong type IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static IEnumerable<T> DataReaderMapToEnumerable<T>(this IDataReader dr)
        {
            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                yield return obj;
            }
            dr.Close();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;

namespace OneLine.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Executes a stored command and yield records
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteCommandReader<T>(this DbContext dbContext, string sqlCommandText)
        {
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlCommandText;
                dbContext.Database.OpenConnection();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    foreach (T record in reader.DataReaderMapToEnumerable<T>())
                    {
                        yield return record;
                    }
                }
                dbContext.Database.CloseConnection();
            }
        }

        /// <summary>
        /// Executes a command and yield records
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> ExecuteCommandReader<T>(this DbContext dbContext, string sqlCommandText, params object[] parameters)
        {
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlCommandText;
                command.Parameters.AddRange(parameters);
                dbContext.Database.OpenConnection();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    foreach (T record in reader.DataReaderMapToEnumerable<T>())
                    {
                        yield return record;
                    }
                }
                dbContext.Database.CloseConnection();
            }
        }
    }
}


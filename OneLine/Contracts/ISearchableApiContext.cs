using OneLine.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface ISearchableApiContext<T>
    {
        /// <summary>
        /// Gets one record by it's primary key identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="NoTracking"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> GetOneAsync(IIdentifier<string> identifier, bool NoTracking = true);
        /// <summary>
        /// Gets one record by it's primary key identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="NoTracking"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> GetOneAuditedAsync(IIdentifier<string> identifier, bool NoTracking = true);
        /// <summary>
        /// Gets one record by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="NoTracking"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> GetOneAsync(Expression<Func<T, bool>> predicate, bool NoTracking = true);
        /// <summary>
        /// Gets one record by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="NoTracking"></param>
        /// <returns></returns>
        Task<IApiResponse<T>> GetOneAuditedAsync(Expression<Func<T, bool>> predicate, bool NoTracking = true);
        /// <summary>
        /// Gets a range of record's by it's primary key
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        IApiResponse<IEnumerable<T>> GetRange(IEnumerable<IIdentifier<string>> identifiers);
        /// <summary>
        /// Gets a range of record's by it's primary key
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> GetRangeAuditedAsync(IEnumerable<IIdentifier<string>> identifiers);
        /// <summary>
        /// Gets a range of record's by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="NoTracking"></param>
        /// <returns></returns>
        IApiResponse<IEnumerable<T>> GetRange(Expression<Func<T, bool>> predicate, bool NoTracking = true);
        /// <summary>
        /// Gets a range of record's by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="NoTracking"></param>
        /// <returns></returns>
        Task<IApiResponse<IEnumerable<T>>> GetRangeAuditedAsync(Expression<Func<T, bool>> predicate, bool NoTracking = true);
    }
}

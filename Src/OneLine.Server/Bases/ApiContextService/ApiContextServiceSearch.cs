using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneLine.Contracts;
using OneLine.Extensions;
using OneLine.Messaging;
using OneLine.Models;
using Storage.Net.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub> :
        IApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub>
        where TDbContext : DbContext
        where T : class, new()
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorage, new()
        where TSmtp : class, ISmtp, new()
        where TMessageHub : class, ISendMessageHub, new()
    {
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> GetOneAsync(IIdentifier<string> identifier, bool NoTracking = true)
        {
            if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
            {
                return new T().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var record = await DbContext.Set<T>().FindAsync(identifier.Model);
            if (record.IsNull())
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            if (NoTracking)
            {
                DbContext.Entry(record).State = EntityState.Detached;
            }
            return record.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> GetOneAuditedAsync(IIdentifier<string> identifier, bool NoTracking = true)
        {
            if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
            {
                //await CreateAuditrailsAsync(identifier, $"Record indentifier or model was null on method {MethodBase.GetCurrentMethod().Name}");
                return new T().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            var record = await DbContext.Set<T>().FindAsync(identifier.Model);
            if (record.IsNull())
            {
                //await CreateAuditrailsAsync(record, $"Record not found on method {MethodBase.GetCurrentMethod().Name}");
                return record.ToApiResponseFailed("RecordNotFound");
            }
            if (NoTracking)
            {
                DbContext.Entry(record).State = EntityState.Detached;
            }
            await CreateAuditrailsAsync(record, $"Record selected on method {MethodBase.GetCurrentMethod().Name}");
            return record.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> GetOneAsync(Expression<Func<T, bool>> predicate, bool NoTracking = true)
        {
            if (predicate.IsNull())
            {
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            T record = await DbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
            if (record.IsNull())
            {
                return record.ToApiResponseFailed("RecordNotFound");
            }
            if (NoTracking)
            {
                DbContext.Entry(record).State = EntityState.Detached;
            }
            return record.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<T>> GetOneAuditedAsync(Expression<Func<T, bool>> predicate, bool NoTracking = true)
        {
            if (predicate.IsNull())
            {
                //await CreateAuditrailsAsync(predicate, $"Predicate was null on method {MethodBase.GetCurrentMethod().Name}");
                return new T().ToApiResponseFailed("PredicateIsNull");
            }
            T record = await DbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
            if (record.IsNull())
            {
                //await CreateAuditrailsAsync(record, $"Record not found on method {MethodBase.GetCurrentMethod().Name}");
                return record.ToApiResponseFailed("RecordNotFound");
            }
            if (NoTracking)
            {
                DbContext.Entry(record).State = EntityState.Detached;
            }
            await CreateAuditrailsAsync(record, $"Record selected on method {MethodBase.GetCurrentMethod().Name}");
            return record.ToApiResponse();
        }
        /// <inheritdoc/>
        public IApiResponse<IEnumerable<T>> GetRange(IEnumerable<IIdentifier<string>> identifiers)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
                }
            }
            var ids = identifiers.Select(s => s.Model);
            var tablePrimaryKey = GetTablePrimaryKeyFieldName();
            IEnumerable<T> records = System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(DbContext.Set<T>(), $"{tablePrimaryKey} in @0", ids);
            if (records.IsNull() || !records.Any())
            {
                return records.ToApiResponseFailed("RecordNotFound");
            }
            return records.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> GetRangeAuditedAsync(IEnumerable<IIdentifier<string>> identifiers)
        {
            if (identifiers.IsNull() || !identifiers.Any())
            {
                //await CreateRangeAuditrailsAsync(identifiers, $"Records indentifiers or model was null on method {MethodBase.GetCurrentMethod().Name}");
                return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
            }
            foreach (var identifier in identifiers)
            {
                if (identifier.IsNull() || string.IsNullOrWhiteSpace(identifier.Model))
                {
                    //await CreateAuditrailsAsync(identifier, $"Record indentifier or model was null on method {MethodBase.GetCurrentMethod().Name}");
                    return Enumerable.Empty<T>().ToApiResponseFailed("IdentifierIsNullOrEmpty");
                }
            }
            var ids = identifiers.Select(s => s.Model);
            var tablePrimaryKey = GetTablePrimaryKeyFieldName();
            IEnumerable<T> records = System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(DbContext.Set<T>(), $"{tablePrimaryKey} in @0", ids);
            if (records.IsNull() || !records.Any())
            {
                //await CreateRangeAuditrailsAsync(records, $"Records not found on method {MethodBase.GetCurrentMethod().Name}");
                return records.ToApiResponseFailed("RecordNotFound");
            }
            await CreateRangeAuditrailsAsync(records, $"Records found and selected on method {MethodBase.GetCurrentMethod().Name}");
            return records.ToApiResponse();
        }
        /// <inheritdoc/>
        public IApiResponse<IEnumerable<T>> GetRange(Expression<Func<T, bool>> predicate, bool NoTracking = true)
        {
            if (predicate.IsNull())
            {
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            IEnumerable<T> records;
            if (NoTracking)
            {
                records = DbContext.Set<T>().AsNoTracking().Where(predicate);
            }
            else
            {
                records = DbContext.Set<T>().Where(predicate);
            }
            if (records.IsNull() || !records.Any())
            {
                return records.ToApiResponseFailed("RecordNotFound");
            }
            return records.ToApiResponse();
        }
        /// <inheritdoc/>
        public async Task<IApiResponse<IEnumerable<T>>> GetRangeAuditedAsync(Expression<Func<T, bool>> predicate, bool NoTracking = true)
        {
            if (predicate.IsNull())
            {
                //await CreateAuditrailsAsync(predicate, $"Predicate was null or empty on method {MethodBase.GetCurrentMethod().Name}");
                return Enumerable.Empty<T>().ToApiResponseFailed("PredicateIsNull");
            }
            IEnumerable<T> records;
            if (NoTracking)
            {
                records = DbContext.Set<T>().AsNoTracking().Where(predicate);
            }
            else
            {
                records = DbContext.Set<T>().Where(predicate);
            }
            if (records.IsNull() || !records.Any())
            {
                //await CreateRangeAuditrailsAsync(records, $"Records not found on method {MethodBase.GetCurrentMethod().Name}");
                return records.ToApiResponseFailed("RecordNotFound");
            }
            await CreateRangeAuditrailsAsync(records, $"Records selected on method {MethodBase.GetCurrentMethod().Name}");
            return records.ToApiResponse();
        }
    }
}

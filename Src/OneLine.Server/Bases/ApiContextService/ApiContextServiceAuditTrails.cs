using Microsoft.EntityFrameworkCore;
using OneLine.Contracts;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Messaging;
using OneLine.Models;
using Storage.Net.Blobs;
using System.Collections.Generic;
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
        public void AddAuditrails<TAudit>(TAudit entity, TransactionType transactionType) where TAudit : class
        {
            DbContext.Add(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void AddAuditrails<TAudit>(TAudit entity, string transactionMessage) where TAudit : class
        {
            DbContext.Add(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task AddAuditrailsAsync<TAudit>(TAudit entity, TransactionType transactionType) where TAudit : class
        {
            await DbContext.AddAsync(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task AddAuditrailsAsync<TAudit>(TAudit entity, string transactionMessage) where TAudit : class
        {
            await DbContext.AddAsync(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void AddRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType) where TAudit : class
        {
            DbContext.AddRange(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task AddRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, string transactionMessage) where TAudit : class
        {
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void CreateAuditrails<TAudit>(TAudit entity, TransactionType transactionType) where TAudit : class
        {
            DbContext.Add(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
            DbContext.SaveChanges();
        }
        /// <inheritdoc/>
        public async Task CreateAuditrailsAsync<TAudit>(TAudit entity, TransactionType transactionType) where TAudit : class
        {
            await DbContext.AddAsync(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
            await DbContext.SaveChangesAsync();
        }
        /// <inheritdoc/>
        public void CreateAuditrails<TAudit>(TAudit entity, string transactionMessage) where TAudit : class
        {
            DbContext.Add(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
            DbContext.SaveChanges();
        }
        /// <inheritdoc/>
        public async Task CreateAuditrailsAsync<TAudit>(TAudit entity, string transactionMessage) where TAudit : class
        {
            await DbContext.AddAsync(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
            await DbContext.SaveChangesAsync();
        }
        /// <inheritdoc/>
        public void CreateRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType) where TAudit : class
        {
            DbContext.AddRange(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
            DbContext.SaveChanges();
        }
        /// <inheritdoc/>
        public async Task CreateRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType) where TAudit : class
        {
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
            await DbContext.SaveChangesAsync();
        }
        /// <inheritdoc/>
        public void CreateRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, string transactionMessage) where TAudit : class
        {
            DbContext.AddRange(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
            DbContext.SaveChanges();
        }
        /// <inheritdoc/>
        public async Task CreateRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, string transactionMessage) where TAudit : class
        {
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
            await DbContext.SaveChangesAsync();
        }
        /// <inheritdoc/>
        public void AddAudited(T entity)
        {
            DbContext.Add(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Add, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task AddAuditedAsync(T entity)
        {
            await DbContext.AddAsync(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Add, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void AddAudited(T entity, string transactionMessage)
        {
            DbContext.Add(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task AddAuditedAsync(T entity, string transactionMessage)
        {
            await DbContext.AddAsync(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void AddRangeAudited(IEnumerable<T> entities)
        {
            DbContext.AddRange(entities);
            DbContext.AddRange(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Add, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task AddRangeAuditedAsync(IEnumerable<T> entities)
        {
            await DbContext.AddRangeAsync(entities);
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Add, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void AddRangeAudited(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.AddRange(entities);
            DbContext.AddRange(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task AddRangeAuditedAsync(IEnumerable<T> entities, string transactionMessage)
        {
            await DbContext.AddRangeAsync(entities);
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void UpdateAudited(T entity)
        {
            DbContext.Update(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Update, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task UpdateAuditedAsync(T entity)
        {
            DbContext.Update(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Update, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void UpdateAudited(T entity, string transactionMessage)
        {
            DbContext.Update(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task UpdateAuditedAsync(T entity, string transactionMessage)
        {
            DbContext.Update(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void UpdateRangeAudited(IEnumerable<T> entities)
        {
            DbContext.UpdateRange(entities);
            DbContext.AddRange(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Update, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task UpdateRangeAuditedAsync(IEnumerable<T> entities)
        {
            DbContext.UpdateRange(entities);
            await DbContext.AddAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Update, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void UpdateRangeAudited(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.UpdateRange(entities);
            DbContext.Add(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task UpdateRangeAuditedAsync(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.UpdateRange(entities);
            await DbContext.AddAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void RemoveAudited(T entity)
        {
            DbContext.Remove(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task RemoveAuditedAsync(T entity)
        {
            DbContext.Remove(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void RemoveAudited(T entity, string transactionMessage)
        {
            DbContext.Remove(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task RemoveAuditedAsync(T entity, string transactionMessage)
        {
            DbContext.Remove(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void RemoveRangeAudited(IEnumerable<T> entities)
        {
            DbContext.RemoveRange(entities);
            DbContext.Add(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task RemoveRangeAuditedAsync(IEnumerable<T> entities)
        {
            DbContext.RemoveRange(entities);
            await DbContext.AddAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public void RemoveRangeAudited(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.RemoveRange(entities);
            DbContext.Add(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <inheritdoc/>
        public async Task RemoveRangeAuditedAsync(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.RemoveRange(entities);
            await DbContext.AddAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
    }
}

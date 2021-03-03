using Microsoft.EntityFrameworkCore;
using OneLine.Enums;
using OneLine.Extensions;
using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, TAuditTrails, TUserBlobs, TBlobStorage>
        where TDbContext : DbContext
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
    {
        /// <summary>
        /// Audits a record adding it to the DbContext tracking 
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        public void AddAuditrails<TAudit>(TAudit entity, TransactionType transactionType)
        {
            DbContext.Add(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
        }
        /// <summary>
        /// Audits a record adding it to the DbContext tracking
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        public void AddAuditrails<TAudit>(TAudit entity, string transactionMessage)
        {
            DbContext.Add(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Audits a record adding it to the DbContext tracking
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        /// <returns></returns>
        public async Task AddAuditrailsAsync<TAudit>(TAudit entity, TransactionType transactionType)
        {
            await DbContext.AddAsync(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
        }
        /// <summary>
        /// Audits a record adding it to the DbContext tracking
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task AddAuditrailsAsync<TAudit>(TAudit entity, string transactionMessage)
        {
            await DbContext.AddAsync(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Audits a range of records adding it to the DbContext tracking
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionType"></param>
        public void AddRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType)
        {
            DbContext.AddRange(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
        }
        /// <summary>
        /// Audits a range of records adding it to the DbContext tracking
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task AddRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, string transactionMessage)
        {
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Creates and audits a range of records adding it to the DbContext tracking and saving the operation
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        public void CreateAuditrails<TAudit>(TAudit entity, TransactionType transactionType)
        {
            DbContext.Add(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
            DbContext.SaveChanges();
        }
        /// <summary>
        /// Creates and audits a records adding it to the DbContext tracking and saving the operation
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        /// <returns></returns>
        public async Task CreateAuditrailsAsync<TAudit>(TAudit entity, TransactionType transactionType)
        {
            await DbContext.AddAsync(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
            await DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Creates and audits a records adding it to the DbContext tracking and saving the operation
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        public void CreateAuditrails<TAudit>(TAudit entity, string transactionMessage)
        {
            DbContext.Add(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
            DbContext.SaveChanges();
        }
        /// <summary>
        /// Creates and audits a records adding it to the DbContext tracking and saving the operation
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task CreateAuditrailsAsync<TAudit>(TAudit entity, string transactionMessage)
        {
            await DbContext.AddAsync(entity.CreateAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
            await DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Creates and audits a range of records adding it to the DbContext tracking and saving the operation
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionType"></param>
        public void CreateRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType)
        {
            DbContext.AddRange(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
            DbContext.SaveChanges();
        }
        /// <summary>
        /// Creates and audits a range of records adding it to the DbContext tracking and saving the operation
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionType"></param>
        /// <returns></returns>
        public async Task CreateRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType)
        {
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionType, HttpContextAccessor));
            await DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Creates and audits a range of records adding it to the DbContext tracking and saving the operation
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        public void CreateRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, string transactionMessage)
        {
            DbContext.AddRange(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
            DbContext.SaveChanges();
        }
        /// <summary>
        /// Creates and audits a range of records adding it to the DbContext tracking and saving the operation
        /// </summary>
        /// <typeparam name="TAudit"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task CreateRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, string transactionMessage)
        {
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<TAudit, TAuditTrails>(transactionMessage, HttpContextAccessor));
            await DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Adds an entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void AddAudited<T>(T entity)
        {
            DbContext.Add(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Add, HttpContextAccessor));
        }
        /// <summary>
        /// Adds an entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAuditedAsync<T>(T entity)
        {
            await DbContext.AddAsync(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Add, HttpContextAccessor));
        }
        /// <summary>
        /// Adds an entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        public void AddAudited<T>(T entity, string transactionMessage)
        {
            DbContext.Add(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Adds an entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task AddAuditedAsync<T>(T entity, string transactionMessage)
        {
            await DbContext.AddAsync(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Adds a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public void AddRangeAudited<T>(IEnumerable<T> entities)
        {
            DbContext.AddRange(entities);
            DbContext.AddRange(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Add, HttpContextAccessor));
        }
        /// <summary>
        /// Adds a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAuditedAsync<T>(IEnumerable<T> entities)
        {
            await DbContext.AddRangeAsync(entities);
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Add, HttpContextAccessor));
        }
        /// <summary>
        /// Adds a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        public void AddRangeAudited<T>(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.AddRange(entities);
            DbContext.AddRange(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Adds a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task AddRangeAuditedAsync<T>(IEnumerable<T> entities, string transactionMessage)
        {
            await DbContext.AddRangeAsync(entities);
            await DbContext.AddRangeAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Updates a entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void UpdateAudited<T>(T entity)
        {
            DbContext.Update(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Update, HttpContextAccessor));
        }
        /// <summary>
        /// Updates a entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAuditedAsync<T>(T entity)
        {
            DbContext.Update(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Update, HttpContextAccessor));
        }
        /// <summary>
        /// Updates a entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        public void UpdateAudited<T>(T entity, string transactionMessage)
        {
            DbContext.Update(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Updates a entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task UpdateAuditedAsync<T>(T entity, string transactionMessage)
        {
            DbContext.Update(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Updates a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public void UpdateRangeAudited<T>(IEnumerable<T> entities)
        {
            DbContext.UpdateRange(entities);
            DbContext.AddRange(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Update, HttpContextAccessor));
        }
        /// <summary>
        /// Updates a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task UpdateRangeAuditedAsync<T>(IEnumerable<T> entities)
        {
            DbContext.UpdateRange(entities);
            await DbContext.AddAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Update, HttpContextAccessor));
        }
        /// <summary>
        /// Updates a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        public void UpdateRangeAudited<T>(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.UpdateRange(entities);
            DbContext.Add(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Updates a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task UpdateRangeAuditedAsync<T>(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.UpdateRange(entities);
            await DbContext.AddAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Remove a entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void RemoveAudited<T>(T entity)
        {
            DbContext.Remove(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
        }
        /// <summary>
        /// Remove a entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task RemoveAuditedAsync<T>(T entity)
        {
            DbContext.Remove(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
        }
        /// <summary>
        /// Remove a entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        public void RemoveAudited<T>(T entity, string transactionMessage)
        {
            DbContext.Remove(entity);
            DbContext.Add(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Remove a entity to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task RemoveAuditedAsync<T>(T entity, string transactionMessage)
        {
            DbContext.Remove(entity);
            await DbContext.AddAsync(entity.CreateAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Remove a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public void RemoveRangeAudited<T>(IEnumerable<T> entities)
        {
            DbContext.RemoveRange(entities);
            DbContext.Add(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
        }
        /// <summary>
        /// Remove a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task RemoveRangeAuditedAsync<T>(IEnumerable<T> entities)
        {
            DbContext.RemoveRange(entities);
            await DbContext.AddAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(TransactionType.Delete, HttpContextAccessor));
        }
        /// <summary>
        /// Remove a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        public void RemoveRangeAudited<T>(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.RemoveRange(entities);
            DbContext.Add(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
        /// <summary>
        /// Remove a range of entities to the change tracker and audit it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        /// <returns></returns>
        public async Task RemoveRangeAuditedAsync<T>(IEnumerable<T> entities, string transactionMessage)
        {
            DbContext.RemoveRange(entities);
            await DbContext.AddAsync(entities.CreateRangeAuditTrails<T, TAuditTrails>(transactionMessage, HttpContextAccessor));
        }
    }
}
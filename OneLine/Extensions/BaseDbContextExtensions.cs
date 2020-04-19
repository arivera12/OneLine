using OneLine.Bases;
using OneLine.Enums;
using OneLine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Extensions
{
    public static class BaseDbContextExtensions
    {
        #region Add Audit Trails

        public static void AddAuditrails<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Add(entity.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditrailsAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void AddAuditrails<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.AddRange(entities.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditrailsAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            await dbContext.AddRangeAsync(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Create Audit Trails

        public static void CreateAuditrails<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Add(entity.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
            dbContext.SaveChanges();
        }
        public static async Task CreateAuditrailsAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
            await dbContext.SaveChangesAsync();
        }
        public static void CreateAuditrails<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.AddRange(entities.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
            dbContext.SaveChanges();
        }
        public static async Task CreateAuditrailsAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            await dbContext.AddRangeAsync(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
            await dbContext.SaveChangesAsync();
        }

        #endregion

        #region Add Audited

        public static void AddAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Add(entity);
            dbContext.Add(entity.CreateAuditTrails(TransactionType.Add, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            await dbContext.AddAsync(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(TransactionType.Add, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void AddAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Add(entity);
            dbContext.Add(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            await dbContext.AddAsync(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void AddAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.AddRange(entities);
            dbContext.AddRange(entities.CreateAuditTrails(TransactionType.Add, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.AddRangeAsync(entities.CreateAuditTrails(TransactionType.Add, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void AddAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.AddRange(entities);
            dbContext.AddRange(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task AddAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            await dbContext.AddRangeAsync(entities);
            await dbContext.AddRangeAsync(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Update Audited

        public static void UpdateAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Update(entity);
            dbContext.Add(entity.CreateAuditTrails(TransactionType.Update, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task UpdateAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Update(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(TransactionType.Update, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void UpdateAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Update(entity);
            dbContext.Add(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task UpdateAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Update(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void UpdateAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
           where TEntity : class
        {
            dbContext.UpdateRange(entities);
            dbContext.AddRange(entities.CreateAuditTrails(TransactionType.Update, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task UpdateAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.UpdateRange(entities);
            await dbContext.AddAsync(entities.CreateAuditTrails(TransactionType.Update, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void UpdateAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.UpdateRange(entities);
            dbContext.Add(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task UpdateAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.UpdateRange(entities);
            await dbContext.AddAsync(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Remove Audited

        public static void RemoveAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Remove(entity);
            dbContext.Add(entity.CreateAuditTrails(TransactionType.Delete, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task RemoveAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Remove(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(TransactionType.Delete, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void RemoveAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Remove(entity);
            dbContext.Add(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task RemoveAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.Remove(entity);
            await dbContext.AddAsync(entity.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void RemoveAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.RemoveRange(entities);
            dbContext.Add(entities.CreateAuditTrails(TransactionType.Delete, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task RemoveAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.RemoveRange(entities);
            await dbContext.AddAsync(entities.CreateAuditTrails(TransactionType.Delete, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static void RemoveAudited<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.RemoveRange(entities);
            dbContext.Add(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }
        public static async Task RemoveAuditedAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class
        {
            dbContext.RemoveRange(entities);
            await dbContext.AddAsync(entities.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
        }

        #endregion

        #region Get One Audited
        
        public static async Task<IApiResponse<TEntity>> GetOneAsync<TEntity>(this BaseDbContext<AuditTrails, ExceptionsLogs, UserBlobs> dbContext, IIdentifier<string> identifier, string userId, string controllerName = null, string actionName = null, string remoteIpAddress = null)
            where TEntity : class, new()
        {
            if(identifier == null || string.IsNullOrWhiteSpace(identifier.Model))
            {
                return new TEntity().ToApiResponseFailed("RecordNotFound");
            }
            var record = await dbContext.Set<TEntity>().FindAsync(identifier.Model); ;
            if(record == null)
            {
                await dbContext.CreateAuditrailsAsync(record, "Not found", userId, controllerName, actionName, remoteIpAddress);
                return record.ToApiResponseFailed("RecordNotFound");
            }
            await dbContext.CreateAuditrailsAsync(record, "Record was found", userId, controllerName, actionName, remoteIpAddress);   
            return record.ToApiResponse();
        }
        
        #endregion
    }
}
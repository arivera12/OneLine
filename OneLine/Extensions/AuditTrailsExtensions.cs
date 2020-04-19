using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;

namespace OneLine.Extensions
{
    public static class AuditTrailsExtensions
    {
        public static AuditTrails CreateAuditTrails<TEntity>(this TEntity entity, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            string Action = transactionType.TransactionTypeMessage<TEntity>();
            return new AuditTrails()
            {
                AuditTrailId = Guid.NewGuid().GenerateGuid(),
                Action = Action,
                ActionName = actionName,
                ControllerName = controllerName,
                TableName = typeof(TEntity).Name,
                Record = JsonConvert.SerializeObject(entity),
                Hostname = Environment.MachineName,
                RemoteIpAddress = remoteIpAddress,
                CreatedBy = createdBy,
                CreatedOn = DateTime.Now
            };
        }
        public static AuditTrails CreateAuditTrails<TEntity>(this TEntity entity, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            return new AuditTrails()
            {
                AuditTrailId = Guid.NewGuid().GenerateGuid(),
                Action = transactionMessage,
                ActionName = actionName,
                ControllerName = controllerName,
                TableName = typeof(TEntity).Name,
                Record = JsonConvert.SerializeObject(entity),
                Hostname = Environment.MachineName,
                RemoteIpAddress = remoteIpAddress,
                CreatedBy = createdBy,
                CreatedOn = DateTime.Now
            };
        }
        public static IEnumerable<AuditTrails> CreateAuditTrails<TEntity>(this IEnumerable<TEntity> entities, TransactionType transactionType, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var AudiTrails = new List<AuditTrails>();
            foreach (var item in entities)
            {
                AudiTrails.Add(item.CreateAuditTrails(transactionType, createdBy, controllerName, actionName, remoteIpAddress));
            }
            return AudiTrails;
        }
        public static IEnumerable<AuditTrails> CreateAuditTrails<TEntity>(this IEnumerable<TEntity> entities, string transactionMessage, string createdBy = null, string controllerName = null, string actionName = null, string remoteIpAddress = null)
        {
            var AudiTrails = new List<AuditTrails>();
            foreach (var item in entities)
            {
                AudiTrails.Add(item.CreateAuditTrails(transactionMessage, createdBy, controllerName, actionName, remoteIpAddress));
            }
            return AudiTrails;
        }
    }
}

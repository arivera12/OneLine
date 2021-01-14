using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OneLine.Enums;
using OneLine.Models;
using System;
using System.Collections.Generic;

namespace OneLine.Extensions
{
    public static class AuditTrailsExtensions
    {
        /// <summary>
        /// Creates a new <typeparamref name="TAuditTrails"/> object saving the <typeparamref name="T"/> state and returns it binding the <paramref name="httpContextAccessor"/> values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TAuditTrails"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static TAuditTrails CreateAuditTrails<T, TAuditTrails>(this T entity, TransactionType transactionType, IHttpContextAccessor httpContextAccessor)
            where T : class
            where TAuditTrails : class, IAuditTrails, new()
        {
            string Action = transactionType.TransactionTypeMessage<T>();
            return new TAuditTrails()
            {
                AuditTrailId = Guid.NewGuid().ToString(),
                Action = Action,
                ActionName = httpContextAccessor?.HttpContext?.CurrentControllerActionName(),
                ControllerName = httpContextAccessor?.HttpContext?.CurrentControllerName(),
                TableName = typeof(T).Name,
                Record = JsonConvert.SerializeObject(entity, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                Hostname = Environment.MachineName,
                RemoteIpAddress = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress.ToString(),
                CreatedBy = httpContextAccessor?.HttpContext?.User.UserId(),
                CreatedOn = DateTime.Now
            };
        }
        /// <summary>
        /// Creates a new <typeparamref name="TAuditTrails"/> object saving the <typeparamref name="T"/> state and returns it binding the <paramref name="httpContextAccessor"/> values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TAuditTrails"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static TAuditTrails CreateAuditTrails<T, TAuditTrails>(this T entity, string transactionMessage, IHttpContextAccessor httpContextAccessor)
            where T : class
            where TAuditTrails : class, IAuditTrails, new()
        {
            return new TAuditTrails()
            {
                AuditTrailId = Guid.NewGuid().ToString(),
                Action = transactionMessage,
                ActionName = httpContextAccessor?.HttpContext?.CurrentControllerActionName(),
                ControllerName = httpContextAccessor?.HttpContext?.CurrentControllerName(),
                TableName = typeof(T).Name,
                Record = JsonConvert.SerializeObject(entity, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                Hostname = Environment.MachineName,
                RemoteIpAddress = httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress.ToString(),
                CreatedBy = httpContextAccessor?.HttpContext?.User.UserId(),
                CreatedOn = DateTime.Now
            };
        }
        /// <summary>
        /// Creates a range of new <typeparamref name="TAuditTrails"/> object saving the <typeparamref name="T"/> state and returns it binding the <paramref name="httpContextAccessor"/> values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TAuditTrails"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static IEnumerable<TAuditTrails> CreateRangeAuditTrails<T, TAuditTrails>(this IEnumerable<T> entities, TransactionType transactionType, IHttpContextAccessor httpContextAccessor)
            where T : class
            where TAuditTrails : class, IAuditTrails, new()
        {
            var AudiTrails = new List<TAuditTrails>();
            foreach (var item in entities)
            {
                AudiTrails.Add(item.CreateAuditTrails<T, TAuditTrails>(transactionType, httpContextAccessor));
            }
            return AudiTrails;
        }
        /// <summary>
        /// Creates a range of new <typeparamref name="TAuditTrails"/> object saving the <typeparamref name="T"/> state and returns it binding the <paramref name="httpContextAccessor"/> values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TAuditTrails"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static IEnumerable<TAuditTrails> CreateRangeAuditTrails<T, TAuditTrails>(this IEnumerable<T> entities, string transactionMessage, IHttpContextAccessor httpContextAccessor)
            where T : class
            where TAuditTrails : class, IAuditTrails, new()
        {
            var AudiTrails = new List<TAuditTrails>();
            foreach (var item in entities)
            {
                AudiTrails.Add(item.CreateAuditTrails<T, TAuditTrails>(transactionMessage, httpContextAccessor));
            }
            return AudiTrails;
        }




    }
}

using OneLine.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneLine.Contracts
{
    public interface IAuditableApiContext<T>
    {
        /// <summary>
        /// Adds the <typeparamref name="T"/> with the specified <see cref="TransactionType"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        void AddAuditrails<TAudit>(TAudit entity, TransactionType transactionType) where TAudit : class;
        /// <summary>
        /// Adds the <typeparamref name="T"/> with the specified <paramref name="transactionMessage"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        void AddAuditrails<TAudit>(TAudit entity, string transactionMessage) where TAudit : class;
        /// <summary>
        /// Adds the <typeparamref name="T"/> with the specified <see cref="TransactionType"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        Task AddAuditrailsAsync<TAudit>(TAudit entity, TransactionType transactionType) where TAudit : class;
        /// <summary>
        /// Adds the <typeparamref name="T"/> with the specified <paramref name="transactionMessage"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        Task AddAuditrailsAsync<TAudit>(TAudit entity, string transactionMessage) where TAudit : class;
        /// <summary>
        /// Adds a range of <typeparamref name="T"/> with the specified <see cref="TransactionType"/>
        /// </summary>
        /// <param name="transactionType"></param>
        void AddRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType) where TAudit : class;
        /// <summary>
        /// Adds a range of <typeparamref name="T"/> with the specified <paramref name="transactionMessage"/>
        /// </summary>
        /// <param name="transactionMessage"></param>
        Task AddRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, string transactionMessage) where TAudit : class;
        /// <summary>
        /// Creates an audit trail with the <typeparamref name="T"/> with the specified <paramref name="transactionType"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        void CreateAuditrails<TAudit>(TAudit entity, TransactionType transactionType) where TAudit : class;
        /// <summary>
        /// Creates an audit trail with the <typeparamref name="T"/> with the specified <paramref name="transactionType"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        Task CreateAuditrailsAsync<TAudit>(TAudit entity, TransactionType transactionType) where TAudit : class;
        /// <summary>
        /// Creates an audit trail with the <typeparamref name="T"/> with the specified <paramref name="transactionMessage"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        void CreateAuditrails<TAudit>(TAudit entity, string transactionMessage) where TAudit : class;
        /// <summary>
        /// Creates an audit trail with the <typeparamref name="T"/> with the specified <paramref name="transactionMessage"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        Task CreateAuditrailsAsync<TAudit>(TAudit entity, string transactionMessage) where TAudit : class;
        /// <summary>
        /// Creates a range of audit trails of the <typeparamref name="T"/> with the specified <paramref name="transactionType"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        void CreateRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType) where TAudit : class;
        /// <summary>
        /// Creates a range of audit trails of the <typeparamref name="T"/> with the specified <paramref name="transactionType"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionType"></param>
        Task CreateRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, TransactionType transactionType) where TAudit : class;
        /// <summary>
        /// Creates a range of audit trails of the <typeparamref name="T"/> with the specified <paramref name="transactionMessage"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        void CreateRangeAuditrails<TAudit>(IEnumerable<TAudit> entities, string transactionMessage) where TAudit : class;
        /// <summary>
        /// Creates a range of audit trails of the <typeparamref name="T"/> with the specified <paramref name="transactionMessage"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transactionMessage"></param>
        Task CreateRangeAuditrailsAsync<TAudit>(IEnumerable<TAudit> entities, string transactionMessage) where TAudit : class;
        /// <summary>
        /// Adds the entity and adds an audit trail to the <see cref="DbContext"/>
        /// </summary>
        /// <param name="entity"></param>
        void AddAudited(T entity);
        /// <summary>
        /// Adds the entity and adds an audit trail to the <see cref="DbContext"/>
        /// </summary>
        /// <param name="entity"></param>
        Task AddAuditedAsync(T entity);
        /// <summary>
        /// Adds the entity and adds an audit trail to the <see cref="DbContext"/>
        /// </summary>
        /// <param name="entity"></param>
        void AddAudited(T entity, string transactionMessage);
        /// <summary>
        /// Adds the entity and adds an audit trail to the <see cref="DbContext"/>
        /// </summary>
        /// <param name="entity"></param>
        Task AddAuditedAsync(T entity, string transactionMessage);
        /// <summary>
        /// Adds a range of entity and adds a range of audit trail to the <see cref="DbContext"/>
        /// </summary>
        /// <param name="entities"></param>
        void AddRangeAudited(IEnumerable<T> entities);
        /// <summary>
        /// Adds a range of entity and adds a a range of audit trail to the <see cref="DbContext"/>
        /// </summary>
        /// <param name="entities"></param>
        Task AddRangeAuditedAsync(IEnumerable<T> entities);
        /// <summary>
        /// Adds a range of entity and adds a range of audit trail to the <see cref="DbContext"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        void AddRangeAudited(IEnumerable<T> entities, string transactionMessage);
        /// <summary>
        /// Adds a range of entity and adds a range of audit trail to the <see cref="DbContext"/>
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transactionMessage"></param>
        Task AddRangeAuditedAsync(IEnumerable<T> entities, string transactionMessage);
        /// <summary>
        /// Adds the entity into save mode and creates an audit trail
        /// </summary>
        /// <param name="entity"></param>
        void UpdateAudited(T entity);
        /// <summary>
        /// Adds the entity into save mode and creates an audit trail
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAuditedAsync(T entity);
        /// <summary>
        /// Adds the entity into save mode and creates an audit trail
        /// </summary>
        /// <param name="entity"></param>
        void UpdateAudited(T entity, string transactionMessage);
        /// <summary>
        /// Adds the entity into save mode and creates an audit trail
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAuditedAsync(T entity, string transactionMessage);
        /// <summary>
        /// Adds the entities into save mode and creates a range of audit trail
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRangeAudited(IEnumerable<T> entities);
        /// <summary>
        /// Adds the entities into save mode and creates a range of audit trail
        /// </summary>
        /// <param name="entities"></param>
        Task UpdateRangeAuditedAsync(IEnumerable<T> entities);
        /// <summary>
        /// Adds the entities into save mode and creates a range of audit trail
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRangeAudited(IEnumerable<T> entities, string transactionMessage);
        /// <summary>
        /// Adds the entities into save mode and creates a range of audit trail
        /// </summary>
        /// <param name="entities"></param>
        Task UpdateRangeAuditedAsync(IEnumerable<T> entities, string transactionMessage);
        /// <summary>
        /// Adds the entity into delete mode and creates an audit trail
        /// </summary>
        /// <param name="entity"></param>
        void RemoveAudited(T entity);
        /// <summary>
        /// Adds the entity into delete mode and creates an audit trail
        /// </summary>
        /// <param name="entity"></param>
        Task RemoveAuditedAsync(T entity);
        /// <summary>
        /// Adds the entity into delete mode and creates an audit trail
        /// </summary>
        /// <param name="entity"></param>
        void RemoveAudited(T entity, string transactionMessage);
        /// <summary>
        /// Adds the entity into delete mode and creates an audit trail
        /// </summary>
        /// <param name="entity"></param>
        Task RemoveAuditedAsync(T entity, string transactionMessage);
        /// <summary>
        /// Adds the entities into delete mode and creates a range of audit trails
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRangeAudited(IEnumerable<T> entities);
        /// <summary>
        /// Adds the entities into delete mode and creates a range of audit trails
        /// </summary>
        /// <param name="entities"></param>
        Task RemoveRangeAuditedAsync(IEnumerable<T> entities);
        /// <summary>
        /// Adds the entities into delete mode and creates a range of audit trails
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRangeAudited(IEnumerable<T> entities, string transactionMessage);
        /// <summary>
        /// Adds the entities into delete mode and creates a range of audit trails
        /// </summary>
        /// <param name="entities"></param>
        Task RemoveRangeAuditedAsync(IEnumerable<T> entities, string transactionMessage);
    }
}

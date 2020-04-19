using System;

namespace OneLine.Models
{
    public class AuditTrails : IAuditTrails
    {
        /// <summary>
        /// The audit trail identifier
        /// </summary>
        public virtual string AuditTrailId { get; set; }
        /// <summary>
        /// The action that was performed.
        /// </summary>
        public virtual string Action { get; set; }
        /// <summary>
        /// The name of the action. This is the method that was called from the api.
        /// </summary>
        public virtual string ActionName { get; set; }
        /// <summary>
        /// The controller name. This is the class name of the controller that was calles from the api.
        /// </summary>
        public virtual string ControllerName { get; set; }
        /// <summary>
        /// The remote Ip Address of the client that made the request to the api.
        /// </summary>
        public virtual string RemoteIpAddress { get; set; }
        /// <summary>
        /// The database table name that the transaction was performed.
        /// </summary>
        public virtual string TableName { get; set; }
        /// <summary>
        /// The record state before any change was made except insert operation.
        /// </summary>
        public virtual string Record { get; set; }
        /// <summary>
        /// The host name that performed that processed the request.
        /// </summary>
        public virtual string Hostname { get; set; }
        /// <summary>
        /// The user that created the transaction.
        /// </summary>
        public virtual string CreatedBy { get; set; }
        /// <summary>
        /// The date time stamp that this trasaction was performed.
        /// </summary>
        public virtual DateTime CreatedOn { get; set; }
    }
}

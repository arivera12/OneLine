using System;

namespace OneLine.Models
{
    /// <summary>
    /// Defines a structure to save audit trails
    /// </summary>
    public interface IAuditTrails
    {
        /// <summary>
        /// The audit trail identifier
        /// </summary>
        string AuditTrailId { get; set; }
        /// <summary>
        /// The action that was performed on the api.
        /// </summary>
        string Action { get; set; }
        /// <summary>
        /// The name of the action. This is the method that was called from the api.
        /// </summary>
        string ActionName { get; set; }
        /// <summary>
        /// The controller name. This is the class name of the controller that was calles from the api.
        /// </summary>
        string ControllerName { get; set; }
        /// <summary>
        /// The remote Ip Address of the client that made the request to the api.
        /// </summary>
        string RemoteIpAddress { get; set; }
        /// <summary>
        /// The database table name that the transaction was performed.
        /// </summary>
        string TableName { get; set; }
        /// <summary>
        /// The record state before any change was made except insert operation.
        /// </summary>
        string Record { get; set; }
        /// <summary>
        /// The host name that performed that processed the request.
        /// </summary>
        string Hostname { get; set; }
        /// <summary>
        /// The user that created the transaction.
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// The date time stamp that this trasaction was performed.
        /// </summary>
        DateTime CreatedOn { get; set; }
    }
}

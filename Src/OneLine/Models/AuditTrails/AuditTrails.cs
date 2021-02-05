using System;

namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure to save audit trails
    /// </summary>
    public class AuditTrails : IAuditTrails
    {
        /// <inheritdoc/>
        public string AuditTrailId { get; set; }
        /// <inheritdoc/>
        public string Action { get; set; }
        /// <inheritdoc/>
        public string ActionName { get; set; }
        /// <inheritdoc/>
        public string ControllerName { get; set; }
        /// <inheritdoc/>
        public string RemoteIpAddress { get; set; }
        /// <inheritdoc/>
        public string TableName { get; set; }
        /// <inheritdoc/>
        public string Record { get; set; }
        /// <inheritdoc/>
        public string Hostname { get; set; }
        /// <inheritdoc/>
        public string CreatedBy { get; set; }
        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }
    }
    /// <summary>
    /// Implements a structure to save audit trails
    /// </summary>
    public class AuditTrailsViewModel : IAuditTrails
    {
        /// <inheritdoc/>
        public string AuditTrailId { get; set; }
        /// <inheritdoc/>
        public string Action { get; set; }
        /// <inheritdoc/>
        public string ActionName { get; set; }
        /// <inheritdoc/>
        public string ControllerName { get; set; }
        /// <inheritdoc/>
        public string RemoteIpAddress { get; set; }
        /// <inheritdoc/>
        public string TableName { get; set; }
        /// <inheritdoc/>
        public string Record { get; set; }
        /// <inheritdoc/>
        public string Hostname { get; set; }
        /// <inheritdoc/>
        public string CreatedBy { get; set; }
        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }
    }
}

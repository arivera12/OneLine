using System;

namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure to save audit trails
    /// </summary>
    public class AuditTrails : IAuditTrails
    {
        /// <inheritdoc/>
        public virtual string AuditTrailId { get; set; }
        /// <inheritdoc/>
        public virtual string Action { get; set; }
        /// <inheritdoc/>
        public virtual string ActionName { get; set; }
        /// <inheritdoc/>
        public virtual string ControllerName { get; set; }
        /// <inheritdoc/>
        public virtual string RemoteIpAddress { get; set; }
        /// <inheritdoc/>
        public virtual string TableName { get; set; }
        /// <inheritdoc/>
        public virtual string Record { get; set; }
        /// <inheritdoc/>
        public virtual string Hostname { get; set; }
        /// <inheritdoc/>
        public virtual string CreatedBy { get; set; }
        /// <inheritdoc/>
        public virtual DateTime CreatedOn { get; set; }
    }
    /// <summary>
    /// Implements a structure to save audit trails
    /// </summary>
    public class AuditTrailsViewModel : IAuditTrails
    {
        /// <inheritdoc/>
        public virtual string AuditTrailId { get; set; }
        /// <inheritdoc/>
        public virtual string Action { get; set; }
        /// <inheritdoc/>
        public virtual string ActionName { get; set; }
        /// <inheritdoc/>
        public virtual string ControllerName { get; set; }
        /// <inheritdoc/>
        public virtual string RemoteIpAddress { get; set; }
        /// <inheritdoc/>
        public virtual string TableName { get; set; }
        /// <inheritdoc/>
        public virtual string Record { get; set; }
        /// <inheritdoc/>
        public virtual string Hostname { get; set; }
        /// <inheritdoc/>
        public virtual string CreatedBy { get; set; }
        /// <inheritdoc/>
        public virtual DateTime CreatedOn { get; set; }
    }
}

using System;
namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure for user blobs
    /// </summary>
    public class UserBlobs : IUserBlobs
    {
        /// <inheritdoc/>
        public virtual string UserBlobId { get; set; }
        /// <inheritdoc/>
        public virtual string UserIdentifier { get; set; }
        /// <inheritdoc/>
        public virtual string ContentDisposition { get; set; }
        /// <inheritdoc/>
        public virtual string ContentType { get; set; }
        /// <inheritdoc/>
        public virtual string FileName { get; set; }
        /// <inheritdoc/>
        public virtual string Name { get; set; }
        /// <inheritdoc/>
        public virtual long Length { get; set; }
        /// <inheritdoc/>
        public virtual string FilePath { get; set; }
        /// <inheritdoc/>
        public virtual string TableName { get; set; }
        /// <inheritdoc/>
        public virtual bool IsDeleted { get; set; }
        /// <inheritdoc/>
        public virtual DateTime? CreatedOn { get; set; }
        /// <inheritdoc/>
        public virtual string CreatedBy { get; set; }
    }
    /// <summary>
    /// Implements a structure for user blobs
    /// </summary>
    public class UserBlobsViewModel : IUserBlobs
    {
        /// <inheritdoc/>
        public virtual string UserBlobId { get; set; }
        /// <inheritdoc/>
        public virtual string UserIdentifier { get; set; }
        /// <inheritdoc/>
        public virtual string ContentDisposition { get; set; }
        /// <inheritdoc/>
        public virtual string ContentType { get; set; }
        /// <inheritdoc/>
        public virtual string FileName { get; set; }
        /// <inheritdoc/>
        public virtual string Name { get; set; }
        /// <inheritdoc/>
        public virtual long Length { get; set; }
        /// <inheritdoc/>
        public virtual string FilePath { get; set; }
        /// <inheritdoc/>
        public virtual string TableName { get; set; }
        /// <inheritdoc/>
        public virtual bool IsDeleted { get; set; }
        /// <inheritdoc/>
        public virtual DateTime? CreatedOn { get; set; }
        /// <inheritdoc/>
        public virtual string CreatedBy { get; set; }
    }
}

using System;
namespace OneLine.Models
{
    /// <summary>
    /// Implements a structure for user blobs
    /// </summary>
    public class UserBlobs : IUserBlobs
    {
        /// <inheritdoc/>
        public string UserBlobId { get; set; }
        /// <inheritdoc/>
        public string UserIdentifier { get; set; }
        /// <inheritdoc/>
        public string ContentDisposition { get; set; }
        /// <inheritdoc/>
        public string ContentType { get; set; }
        /// <inheritdoc/>
        public string FileName { get; set; }
        /// <inheritdoc/>
        public string Name { get; set; }
        /// <inheritdoc/>
        public long Length { get; set; }
        /// <inheritdoc/>
        public string FilePath { get; set; }
        /// <inheritdoc/>
        public string TableName { get; set; }
        /// <inheritdoc/>
        public bool IsDeleted { get; set; }
        /// <inheritdoc/>
        public DateTime? CreatedOn { get; set; }
        /// <inheritdoc/>
        public string CreatedBy { get; set; }
    }
    /// <summary>
    /// Implements a structure for user blobs
    /// </summary>
    public class UserBlobsViewModel : IUserBlobs
    {
        /// <inheritdoc/>
        public string UserBlobId { get; set; }
        /// <inheritdoc/>
        public string UserIdentifier { get; set; }
        /// <inheritdoc/>
        public string ContentDisposition { get; set; }
        /// <inheritdoc/>
        public string ContentType { get; set; }
        /// <inheritdoc/>
        public string FileName { get; set; }
        /// <inheritdoc/>
        public string Name { get; set; }
        /// <inheritdoc/>
        public long Length { get; set; }
        /// <inheritdoc/>
        public string FilePath { get; set; }
        /// <inheritdoc/>
        public string TableName { get; set; }
        /// <inheritdoc/>
        public bool IsDeleted { get; set; }
        /// <inheritdoc/>
        public DateTime? CreatedOn { get; set; }
        /// <inheritdoc/>
        public string CreatedBy { get; set; }
    }
}

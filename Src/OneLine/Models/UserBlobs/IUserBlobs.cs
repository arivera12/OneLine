using System;

namespace OneLine.Models
{
    /// <summary>
    /// Defines a structure for user blobs
    /// </summary>
    public interface IUserBlobs
    {
        /// <summary>
        /// The user blob id
        /// </summary>
        string UserBlobId { get; set; }
        /// <summary>
        /// The user identifier
        /// </summary>
        string UserIdentifier { get; set; }
        /// <summary>
        /// The content disposition
        /// </summary>
        string ContentDisposition { get; set; }
        /// <summary>
        /// The content type
        /// </summary>
        string ContentType { get; set; }
        /// <summary>
        /// The original file name
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// The name of the input file
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The length of the file measured in bytes
        /// </summary>
        long Length { get; set; }
        /// <summary>
        /// The file path where the file is physically stored
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// The table name where this user blob is being referenced
        /// </summary>
        string TableName { get; set; }
        /// <summary>
        /// The is deleted user blob is to allow soft delete
        /// </summary>
        bool IsDeleted { get; set; }
        /// <summary>
        /// Created on
        /// </summary>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// Created by
        /// </summary>
        string CreatedBy { get; set; }
    }
}

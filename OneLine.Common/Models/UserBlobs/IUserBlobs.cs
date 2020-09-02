using System;

namespace OneLine.Models
{
    public interface IUserBlobs
    {
        string UserBlobId { get; set; }
        string UserIdentifier { get; set; }
        string ContentDisposition { get; set; }
        string ContentType { get; set; }
        string FileName { get; set; }
        string Name { get; set; }
        long Length { get; set; }
        string FilePath { get; set; }
        string TableName { get; set; }
        bool IsDeleted { get; set; }
        DateTime? CreatedOn { get; set; }
        string CreatedBy { get; set; }
    }
}

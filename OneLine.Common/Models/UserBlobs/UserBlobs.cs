
using System;

namespace OneLine.Models
{
    public class UserBlobs : IUserBlobs
    {
        public virtual string UserBlobId { get; set; }
        public virtual string UserIdentifier { get; set; }
        public virtual string ContentDisposition { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Name { get; set; }
        public virtual long Length { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string TableName { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual string CreatedBy { get; set; }
        
    }
    public class UserBlobsViewModel : IUserBlobs
    {
        public virtual string UserBlobId { get; set; }
        public virtual string UserIdentifier { get; set; }
        public virtual string ContentDisposition { get; set; }
        public virtual string ContentType { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Name { get; set; }
        public virtual long Length { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string TableName { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual string CreatedBy { get; set; }

    }
}

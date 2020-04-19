using Microsoft.EntityFrameworkCore;
using OneLine.Models;

namespace OneLine.Bases
{
    public class BaseDbContext<TAuditTrails, TExceptionsLogs, TUserBlobs> : DbContext, 
        IDbContext<TAuditTrails, TExceptionsLogs, TUserBlobs>
        where TAuditTrails : AuditTrails, IAuditTrails
        where TExceptionsLogs : ExceptionsLogs, IExceptionsLogs
        where TUserBlobs : UserBlobs, IUserBlobs
    {
        public virtual DbSet<TAuditTrails> AuditTrails { get; set; }
        public virtual DbSet<TExceptionsLogs> ExceptionsLogs { get; set; }
        public virtual DbSet<TUserBlobs> UserBlobs { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using OneLine.Models;

namespace OneLine.Bases
{
    public class BaseDbContext<TAuditTrails, TExceptionLogs, TUserBlobs> : DbContext, 
        IDbContext<TAuditTrails, TExceptionLogs, TUserBlobs>
        where TAuditTrails : AuditTrails, IAuditTrails
        where TExceptionLogs : ExceptionLogs, IExceptionLogs
        where TUserBlobs : UserBlobs, IUserBlobs
    {
        public BaseDbContext()
        {
        }
        public BaseDbContext(DbContextOptions<BaseDbContext<AuditTrails, ExceptionLogs, UserBlobs>> options)
            : base(options)
        {
        }
        public virtual DbSet<TAuditTrails> AuditTrails { get; set; }
        public virtual DbSet<TExceptionLogs> ExceptionLogs { get; set; }
        public virtual DbSet<TUserBlobs> UserBlobs { get; set; }

        
    }
}

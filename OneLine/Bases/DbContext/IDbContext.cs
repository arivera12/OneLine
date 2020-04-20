using Microsoft.EntityFrameworkCore;
using OneLine.Models;

namespace OneLine.Bases
{
    public interface IDbContext<TAuditTrails, TExceptionLogs, TUserBlobs>
        where TAuditTrails : AuditTrails, IAuditTrails
        where TExceptionLogs : ExceptionLogs, IExceptionLogs
        where TUserBlobs : UserBlobs, IUserBlobs
    {
        DbSet<TAuditTrails> AuditTrails { get; set; }
        DbSet<TExceptionLogs> ExceptionLogs { get; set; }
        DbSet<TUserBlobs> UserBlobs { get; set; }
    }
}

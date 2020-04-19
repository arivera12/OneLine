using Microsoft.EntityFrameworkCore;
using OneLine.Models;

namespace OneLine.Bases
{
    public interface IDbContext<TAuditTrails, TExceptionsLogs, TUserBlobs>
        where TAuditTrails : AuditTrails, IAuditTrails
        where TExceptionsLogs : ExceptionsLogs, IExceptionsLogs
        where TUserBlobs : UserBlobs, IUserBlobs
    {
        DbSet<TAuditTrails> AuditTrails { get; set; }
        DbSet<TExceptionsLogs> ExceptionsLogs { get; set; }
        DbSet<TUserBlobs> UserBlobs { get; set; }
    }
}

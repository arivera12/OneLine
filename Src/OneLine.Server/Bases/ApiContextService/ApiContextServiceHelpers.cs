using Microsoft.EntityFrameworkCore;
using OneLine.Contracts;
using OneLine.Messaging;
using OneLine.Models;
using System.Linq;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub> :
        IApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub>
        where TDbContext : DbContext
        where T : class, new()
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
        where TSmtp : class, ISmtp, new()
        where TMessageHub : MessageHub, new()
    {
        /// <inheritdoc/>
        public string GetTablePrimaryKeyFieldName()
        {
            return DbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
        }
    }
}

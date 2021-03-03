using Microsoft.EntityFrameworkCore;
using OneLine.Models;
using System.Linq;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, TAuditTrails, TUserBlobs, TBlobStorage>
        where TDbContext : DbContext
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
    {
        /// <summary>
        /// Gets primary key for this entity type. Returns null if no primary key is defined.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The primary key, or null if none is defined.</returns>
        public string GetTablePrimaryKeyFieldName<T>()
        {
            return DbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey()?.Properties.Select(x => x.Name).Single();
        }
    }
}

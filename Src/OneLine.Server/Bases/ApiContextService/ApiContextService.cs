using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OneLine.Models;

namespace OneLine.Bases
{
    public partial class ApiContextService<TDbContext, TAuditTrails, TUserBlobs, TBlobStorage>
        where TDbContext : DbContext
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorageService, new()
    {
        /// <summary>
        /// The database conext
        /// </summary>
        public TDbContext DbContext { get; set; }
        /// <summary>
        /// The Http context accessor
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        /// <summary>
        /// The blob storage service provider
        /// </summary>
        public TBlobStorage BlobStorageService { get; set; }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="dbContext"></param>
        public ApiContextService(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        public ApiContextService(TDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            DbContext = dbContext;
            HttpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        /// <param name="blobStorage"></param>
        public ApiContextService(TDbContext dbContext, IHttpContextAccessor httpContextAccessor, TBlobStorage blobStorage)
        {
            DbContext = dbContext;
            HttpContextAccessor = httpContextAccessor;
            BlobStorageService = blobStorage;
        }
    }
}

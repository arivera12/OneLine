using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OneLine.Contracts;
using OneLine.Messaging;
using OneLine.Models;

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
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        /// <inheritdoc/>
        public TDbContext DbContext { get; set; }
        /// <inheritdoc/>
        public TBlobStorage BlobStorageService { get; set; }
        /// <inheritdoc/>
        public TSmtp Smtp { get; set; }
        /// <inheritdoc/>
        public IHubContext<TMessageHub> SendMessageHub { get; set; }
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
        public ApiContextService(IHttpContextAccessor httpContextAccessor, TDbContext dbContext)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        /// <param name="sendMessageHub"></param>
        public ApiContextService(IHttpContextAccessor httpContextAccessor, TDbContext dbContext, IHubContext<TMessageHub> sendMessageHub)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
            SendMessageHub = sendMessageHub;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        /// <param name="blobStorage"></param>
        public ApiContextService(IHttpContextAccessor httpContextAccessor, TDbContext dbContext, TBlobStorage blobStorage)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
            BlobStorageService = blobStorage;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        /// <param name="blobStorage"></param>
        /// <param name="sendMessageHub"></param>
        public ApiContextService(IHttpContextAccessor httpContextAccessor, TDbContext dbContext, TBlobStorage blobStorage, IHubContext<TMessageHub> sendMessageHub)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
            BlobStorageService = blobStorage;
            SendMessageHub = sendMessageHub;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        /// <param name="smtp"></param>
        public ApiContextService(IHttpContextAccessor httpContextAccessor, TDbContext dbContext, TSmtp smtp)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
            Smtp = smtp;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        /// <param name="smtp"></param>
        /// <param name="sendMessageHub"></param>
        public ApiContextService(IHttpContextAccessor httpContextAccessor, TDbContext dbContext, TSmtp smtp, IHubContext<TMessageHub> sendMessageHub)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
            Smtp = smtp;
            SendMessageHub = sendMessageHub;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        /// <param name="smtp"></param>
        /// <param name="blobStorage"></param>
        public ApiContextService(IHttpContextAccessor httpContextAccessor, TDbContext dbContext, TSmtp smtp, TBlobStorage blobStorage)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
            Smtp = smtp;
            BlobStorageService = blobStorage;
        }
        /// <summary>
        /// The api context service will create a context containing from the most minimalist api service to the most robust service provider
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dbContext"></param>
        /// <param name="smtp"></param>
        /// <param name="blobStorage"></param>
        /// <param name="sendMessageHub"></param>
        public ApiContextService(IHttpContextAccessor httpContextAccessor, TDbContext dbContext, TSmtp smtp, TBlobStorage blobStorage, IHubContext<TMessageHub> sendMessageHub)
        {
            HttpContextAccessor = httpContextAccessor;
            DbContext = dbContext;
            Smtp = smtp;
            BlobStorageService = blobStorage;
            SendMessageHub = sendMessageHub;
        }
    }
}

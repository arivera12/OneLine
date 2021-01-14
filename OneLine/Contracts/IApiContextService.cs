using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OneLine.Messaging;
using OneLine.Models;
using Storage.Net.Blobs;

namespace OneLine.Contracts
{
    /// <summary>
    /// The api context service will create a context containing from the most minimalist api service to the most robust services provider/s.
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAuditTrails"></typeparam>
    /// <typeparam name="TUserBlobs"></typeparam>
    /// <typeparam name="TBlobStorage"></typeparam>
    /// <typeparam name="TSmtp"></typeparam>
    /// <typeparam name="TMessageHub"></typeparam>
    public interface IApiContextService<TDbContext, T, TAuditTrails, TUserBlobs, TBlobStorage, TSmtp, TMessageHub> :
        ISaveableApiContext<T>,
        ISaveableImportableApiContext<T>,
        ISaveableReplaceableApiContext<T>,
        ISaveableWithBlobsApiContext<T>,
        ISearchableApiContext<T>,
        IApiContextUserBlobsService<T, TUserBlobs>,
        IDeletableApiContext<T>,
        IDeletableWithBlobsApiContext<T>,
        IAuditableApiContext<T>,
        IHelpableApiContext,
        IValidatableAuditableApiContext<T>
        where TDbContext : DbContext
        where T : class, new()
        where TAuditTrails : class, IAuditTrails, new()
        where TUserBlobs : class, IUserBlobs, new()
        where TBlobStorage : class, IBlobStorage, new()
        where TSmtp : class, ISmtp, new()
        where TMessageHub : class, ISendMessageHub, new()
    {
        /// <summary>
        /// The api http context
        /// </summary>
        IHttpContextAccessor HttpContextAccessor { get; set; }
        /// <summary>
        /// The api database context
        /// </summary>
        TDbContext DbContext { get; set; }
        /// <summary>
        /// The api blob storage context
        /// </summary>
        TBlobStorage BlobStorage { get; set; }
        /// <summary>
        /// The api simple mail transfer protocol client context
        /// </summary>
        TSmtp Smtp { get; set; }
        /// <summary>
        /// The api Signal R hub message sender context
        /// </summary>
        TMessageHub SendMessageHub { get; set; }
    }
}

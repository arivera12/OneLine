using Storage.Net.Blobs;

namespace OneLine.Bases
{
    public class DefaultBlobStorageService : IDefaultBlobStorageService
    {
        public IBlobStorage BlobStorage { get; }
        public DefaultBlobStorageService(IBlobStorage blobStorage)
        {
            BlobStorage = blobStorage;
        }
    }
    public interface IDefaultBlobStorageService
    {
        public IBlobStorage BlobStorage { get; }
    }
}

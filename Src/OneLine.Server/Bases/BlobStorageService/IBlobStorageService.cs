using Storage.Net.Blobs;

namespace OneLine.Bases
{
    public interface IBlobStorageService
    {
        public IBlobStorage BlobStorage { get; set; }
    }
}

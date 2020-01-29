using Microsoft.WindowsAzure.Storage.Blob;

namespace Orders.Storage.FileStorage.Providers.Interfaces
{
    public interface IBlobStorageClientProvider
    {
        CloudBlobClient CreateBlobStorageProvider();
    }
}

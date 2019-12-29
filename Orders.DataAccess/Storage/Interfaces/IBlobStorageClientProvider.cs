using Microsoft.WindowsAzure.Storage.Blob;

namespace Orders.DataAccess.Storage.Interfaces
{
    public interface IBlobStorageClientProvider
    {
        CloudBlobClient CreateBlobStorageProvider();
    }
}

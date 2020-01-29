using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Orders.Storage.FileStorage.Providers.Interfaces;

namespace Orders.Storage.FileStorage.Providers
{
    public class BlobStorageClientProvider : IBlobStorageClientProvider
    {
        public CloudBlobClient CreateBlobStorageProvider()
        {
            //var credential  = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

            var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            var client = storageAccount.CreateCloudBlobClient();

            return client;
        }
    }
}

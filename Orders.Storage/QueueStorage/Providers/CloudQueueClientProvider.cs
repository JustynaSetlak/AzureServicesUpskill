using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Orders.Storage.QueueStorage.Providers.Interfaces;

namespace Orders.Storage.QueueStorage.Providers
{
    public class CloudQueueClientProvider : ICloudQueueClientProvider
    {
        public CloudQueueClient CreateQueueClient(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            return queueClient;
        }
    }
}

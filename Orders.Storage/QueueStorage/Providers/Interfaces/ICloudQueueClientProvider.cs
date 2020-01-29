using Microsoft.WindowsAzure.Storage.Queue;

namespace Orders.Storage.QueueStorage.Providers.Interfaces
{
    public interface ICloudQueueClientProvider
    {
        CloudQueueClient CreateQueueClient(string connectionString);
    }
}

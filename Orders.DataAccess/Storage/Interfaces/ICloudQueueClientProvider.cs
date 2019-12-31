using Microsoft.WindowsAzure.Storage.Queue;

namespace Orders.DataAccess.Storage.Interfaces
{
    public interface ICloudQueueClientProvider
    {
        CloudQueueClient CreateQueueClient(string connectionString);
    }
}

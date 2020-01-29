using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Orders.DataAccess.Storage.Models;
using Orders.Storage.Options;
using Orders.Storage.QueueStorage.Providers.Interfaces;
using Orders.Storage.QueueStorage.Services.Interfaces;
using System.Threading.Tasks;

namespace Orders.DataAccess.Storage
{
    public class TagManagementQueueStorageService : ITagManagementQueueStorageService
    {
        private readonly CloudQueueClient _cloudQueueClient;
        private readonly StorageConfig _storageConfig;

        public TagManagementQueueStorageService(ICloudQueueClientProvider cloudQueueClientProvider, IOptions<StorageConfig> storageConfigOptions)
        {
            _storageConfig = storageConfigOptions.Value;
            _cloudQueueClient = cloudQueueClientProvider.CreateQueueClient(_storageConfig.ConnectionString);
        }

        public async Task<TagModification> RetrieveMessage()
        {
            var queue = _cloudQueueClient.GetQueueReference(_storageConfig.TagManagementQueueName);
            await queue.CreateIfNotExistsAsync();

            var retrievedMessage = await queue.GetMessageAsync();

            if (retrievedMessage == null)
            {
                return null;
            }

            var tagModificationRequest = JsonConvert.DeserializeObject<TagModification>(retrievedMessage.AsString);
            //await queue.DeleteMessageAsync(retrievedMessage);

            return tagModificationRequest;
        }
    }
}

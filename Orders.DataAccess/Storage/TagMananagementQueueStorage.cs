using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orders.Common.Config;
using Orders.DataAccess.Storage.Interfaces;
using Orders.DataAccess.Storage.Models;
using System;
using System.Threading.Tasks;

namespace Orders.DataAccess.Storage
{
    public class TagMananagementQueueStorage : ITagManagementQueueStorage
    {
        private readonly CloudQueueClient _cloudQueueClient;
        private readonly StorageConfig _storageConfig;

        public TagMananagementQueueStorage(ICloudQueueClientProvider cloudQueueClientProvider, IOptions<StorageConfig> storageConfigOptions)
        {
            _storageConfig = storageConfigOptions.Value;
            _cloudQueueClient = cloudQueueClientProvider.CreateQueueClient(_storageConfig.ConnectionString);
        }

        public async Task<TagModificationRequest> RetrieveMessage()
        {
            var queue = _cloudQueueClient.GetQueueReference(_storageConfig.TagManagementQueueName);
            await queue.CreateIfNotExistsAsync();

            var retrievedMessage = await queue.GetMessageAsync();

            if (retrievedMessage == null)
            {
                return null;
            }

            var tagModificationRequest = JsonConvert.DeserializeObject<TagModificationRequest>(retrievedMessage.AsString);
            //await queue.DeleteMessageAsync(retrievedMessage);

            return tagModificationRequest;
        }
    }
}

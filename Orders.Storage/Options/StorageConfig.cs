namespace Orders.Storage.Options
{
    public class StorageConfig
    {
        public string ConnectionString { get; set; }

        public string ImageContainer { get; set; }

        public string TagManagementQueueName { get; set; }

        public string ImageMiniatureNamePrefix { get; set; }
    }
}

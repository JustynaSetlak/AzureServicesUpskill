namespace Orders.TableStorage.Options
{
    public class TableStorageConfig
    {
        public string ConnectionString { get; set; }

        public string CategoryTableName { get; set; }

        public string TagTableName { get; set; }
    }
}

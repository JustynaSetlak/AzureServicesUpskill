using Microsoft.WindowsAzure.Storage.Table;

namespace Orders.TableStorage.Models
{
    public class Tag : TableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

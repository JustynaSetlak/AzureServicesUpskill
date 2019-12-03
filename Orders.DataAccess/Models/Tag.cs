using Microsoft.Azure.Cosmos.Table;

namespace Orders.Models
{
    public class Tag : TableEntity 
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

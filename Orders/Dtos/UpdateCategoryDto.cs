
namespace Orders.Dtos
{
    public class UpdateCategoryDto
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string Description { get; set; }
    }
}

using System.Collections.Generic;

namespace Orders.Search.Models
{
    public class OrderUploadModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }
    }
}

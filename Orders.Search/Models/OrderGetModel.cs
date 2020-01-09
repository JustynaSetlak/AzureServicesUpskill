using System.Collections.Generic;

namespace Orders.Search.Models
{
    public class OrderGetModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }

        public List<string> Tags { get; set; }
    }
}

using System.Collections.Generic;

namespace Orders.DocumentDataAccess.Dtos
{
    public class OrderDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }

        public List<string> TagIds { get; set; }
    }
}

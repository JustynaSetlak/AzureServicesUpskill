using System.Collections.Generic;

namespace Orders.BusinessLogic.Dtos.Order
{
    public class OrderDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public List<string> Tags { get; set; }
    }
}

using System.Collections.Generic;

namespace Orders.BusinessLogic.Dtos.Order
{
    public class CreateOrderDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryId { get; set; }

        public List<string> TagIds { get; set; }
    }
}

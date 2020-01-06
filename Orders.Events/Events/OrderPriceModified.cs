using System;

namespace Orders.EventHandler.Events
{
    public class OrderPriceModified : IEvent
    {
        public OrderPriceModified(string id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Date = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }
    }
}

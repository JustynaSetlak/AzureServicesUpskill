using Newtonsoft.Json;
using System.Collections.Generic;

namespace Orders.Models
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }

        public List<string> TagIds { get; set; }
    }
}

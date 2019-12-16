using Microsoft.Azure.Search;
using Newtonsoft.Json;
using Orders.Search.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Orders.Search.Models.SearchModels
{
    public class OrderSearchModel : ISearchable
    {
        [JsonProperty(PropertyName = "id")]
        [Key]
        public string Id { get; set; }

        [IsSearchable]
        public string Name { get; set; }

        public string Description { get; set; }

        [IsFilterable, IsSortable]
        public double Price { get; set; }

        public string ImageUrl { get; set; }

        [IsFilterable]
        public string Category { get; set; }

        [IsFilterable]
        public List<string> Tags { get; set; }
    }
}

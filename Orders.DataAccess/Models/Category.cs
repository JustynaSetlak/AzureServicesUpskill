using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Models
{
    public class Category : TableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

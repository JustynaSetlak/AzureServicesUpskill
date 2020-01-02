﻿using Microsoft.Azure.Cosmos.Table;

namespace Orders.DataAccess.TableRepositories.Models
{
    public class Category : TableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

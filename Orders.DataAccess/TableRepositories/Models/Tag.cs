﻿using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;

namespace Orders.DataAccess.TableRepositories.Models
{
    public class Tag : TableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

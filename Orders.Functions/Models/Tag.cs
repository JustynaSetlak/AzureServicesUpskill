﻿using Microsoft.WindowsAzure.Storage.Table;

namespace Orders.Functions.Models
{
    public class Tag : TableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

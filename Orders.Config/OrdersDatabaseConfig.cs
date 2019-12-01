using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Config
{
    public class OrdersDatabaseConfig
    {
        public string DatabaseName { get; set; }

        public string OrdersCollectionName { get; set; }

        public string Url { get; set; }

        public string Key { get; set; }
    }
}

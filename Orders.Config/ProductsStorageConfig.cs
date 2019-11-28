using System;

namespace Orders.Config
{
    public class ProductsStorageConfig
    {
        public string ConnectionString { get; set; }

        public string CategoryTable { get; set; }

        public string TagTable { get; set; }
    }
}

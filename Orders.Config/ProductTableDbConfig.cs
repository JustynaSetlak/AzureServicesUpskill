using System;

namespace Orders.Config
{
    public class ProductTableDbConfig
    {
        public string ConnectionString { get; set; }

        public string CategoryTableName { get; set; }

        public string TagTableName { get; set; }
    }
}

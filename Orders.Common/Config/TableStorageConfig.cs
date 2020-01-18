using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Common.Config
{
    public class TableStorageConfig
    {
        public string ConnectionString { get; set; }

        public string CategoryTableName { get; set; }

        public string TagTableName { get; set; }
    }
}

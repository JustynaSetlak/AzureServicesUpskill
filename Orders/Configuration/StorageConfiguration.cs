using Microsoft.Extensions.Options;
using Orders.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Configuration
{
    public class StorageConfigurationService
    {
        public StorageConfigurationService(IOptions<ProductsStorageConfig>)
        {

        }
    }
}

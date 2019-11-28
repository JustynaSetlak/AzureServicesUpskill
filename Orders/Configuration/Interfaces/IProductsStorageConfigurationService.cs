using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Configuration.Interfaces
{
    public interface IProductsStorageConfigurationService
    {
        Task CreateDatabaseIfNotExist();
    }
}

using System.Threading.Tasks;

namespace Orders.Configuration.Interfaces
{
    public interface IProductsStorageConfigurationService
    {
        Task CreateDatabaseIfNotExist();
    }
}

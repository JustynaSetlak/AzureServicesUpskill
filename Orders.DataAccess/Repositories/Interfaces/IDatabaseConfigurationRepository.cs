using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories.Interfaces
{
    public interface IDatabaseConfigurationRepository
    {
        Task CreateOrdersDocumentDatabaseIfNotExist();

        Task CreateProductsTableDatabaseIfNotExist();
    }
}

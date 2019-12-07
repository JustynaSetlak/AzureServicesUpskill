using System.Threading.Tasks;

namespace Orders.DataAccess.Interfaces
{
    public interface IDatabaseConfigurationRepository
    {
        Task CreateOrdersDocumentDatabaseIfNotExist();

        Task CreateProductsTableDatabaseIfNotExist();
    }
}

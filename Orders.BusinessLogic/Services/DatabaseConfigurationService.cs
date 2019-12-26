using Orders.Configuration.Interfaces;
using Orders.DataAccess.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Orders.Configuration
{
    public class DatabaseConfigurationService : IDatabaseConfigurationService
    {
        private readonly IDatabaseConfigurationRepository _databaseConfigurationRepository;

        public DatabaseConfigurationService(IDatabaseConfigurationRepository databaseConfigurationRepository)
        {
            _databaseConfigurationRepository = databaseConfigurationRepository;
        }

        public async Task CreateDatabaseIfNotExist()
        {
            await _databaseConfigurationRepository.CreateOrdersDocumentDatabaseIfNotExist();
            await _databaseConfigurationRepository.CreateProductsTableDatabaseIfNotExist();
        }
    }
}

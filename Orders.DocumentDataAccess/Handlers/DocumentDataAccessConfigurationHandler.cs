using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Orders.DocumentDataAccess.Options;
using Orders.Infrastructure.InitConfiguration.Interfaces;
using System.Threading.Tasks;

namespace Orders.DocumentDataAccess.Handlers
{
    public class DocumentDataAccessConfigurationHandler : IConfigurationHandler
    {
        private readonly OrdersDatabaseConfig _ordersDatabaseConfigOptions;
        private readonly IDocumentClient _documentClient;

        public DocumentDataAccessConfigurationHandler(IDocumentClient documentClient, IOptions<OrdersDatabaseConfig> ordersDatabaseConfigOptions)
        {
            _ordersDatabaseConfigOptions = ordersDatabaseConfigOptions.Value;
            _documentClient = documentClient;
        }

        public async Task Configure()
        {
            await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = _ordersDatabaseConfigOptions.DatabaseName }).ConfigureAwait(false);
            await _documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_ordersDatabaseConfigOptions.DatabaseName), new DocumentCollection { Id = _ordersDatabaseConfigOptions.OrdersCollectionName });
        }
    }
}

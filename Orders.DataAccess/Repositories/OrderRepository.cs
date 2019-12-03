using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.Models;
using Orders.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDocumentClient _documentClient;
        private readonly Uri _orderDocumentCollectionFactory;

        public OrderRepository(IDocumentClient documentClient, IOptions<OrdersDatabaseConfig> ordersDatabaseConfigOptions)
        {
            _documentClient = documentClient;
            _orderDocumentCollectionFactory = UriFactory.CreateDocumentCollectionUri(ordersDatabaseConfigOptions.Value.DatabaseName, ordersDatabaseConfigOptions.Value.OrdersCollectionName);
        }

        public async Task CreateOrder(Order order)
        {
            await _documentClient.CreateDocumentAsync(_orderDocumentCollectionFactory, order);
        }
    }
}

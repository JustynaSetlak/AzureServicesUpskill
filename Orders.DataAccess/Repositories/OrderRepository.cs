using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDocumentClient _documentClient;
        private readonly OrdersDatabaseConfig _ordersDatabaseConfig;
        private readonly Uri _orderDocumentCollectionFactory;

        public OrderRepository(IDocumentClient documentClient, IOptions<OrdersDatabaseConfig> ordersDatabaseConfigOptions)
        {
            _documentClient = documentClient;
            _ordersDatabaseConfig = ordersDatabaseConfigOptions.Value;
            _orderDocumentCollectionFactory = UriFactory.CreateDocumentCollectionUri(ordersDatabaseConfigOptions.Value.DatabaseName, ordersDatabaseConfigOptions.Value.OrdersCollectionName);
        }

        public async Task<DataResult<string>> CreateOrder(Order order)
        {
            var response = await _documentClient.CreateDocumentAsync(_orderDocumentCollectionFactory, order);

            //wydzielic - extension - is successfull (?)
            var isSuccessful = response.StatusCode == HttpStatusCode.Created;

            var result = new DataResult<string>(isSuccessful, response.Resource.Id);

            return result;
        }

        public async Task ReplaceDocument(Order order)
        {
            var documentUri = UriFactory.CreateDocumentUri(_ordersDatabaseConfig.DatabaseName, _ordersDatabaseConfig.OrdersCollectionName, order.Id);

            await _documentClient.ReplaceDocumentAsync(documentUri, order);
        }

        public async Task<Order> GetOrder(string documentId)
        {
            try
            {
                var result = await _documentClient.ReadDocumentAsync<Order>(UriFactory.CreateDocumentUri(_ordersDatabaseConfig.DatabaseName, _ordersDatabaseConfig.OrdersCollectionName, documentId));

                return result;
            }
            catch(DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }
    }
}

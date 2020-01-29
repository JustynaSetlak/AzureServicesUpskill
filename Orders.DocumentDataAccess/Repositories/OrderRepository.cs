using AutoMapper;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.DataAccess.Repositories.Models;
using Orders.DocumentDataAccess.Dtos;
using Orders.DocumentDataAccess.Options;
using Orders.Results;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Orders.DocumentDataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDocumentClient _documentClient;
        private readonly IMapper _mapper;
        private readonly OrdersDatabaseConfig _ordersDatabaseConfig;
        private readonly Uri _orderDocumentCollectionFactory;

        public OrderRepository(
            IDocumentClient documentClient, 
            IOptions<OrdersDatabaseConfig> ordersDatabaseConfigOptions,
            IMapper mapper)
        {
            _documentClient = documentClient;
            _mapper = mapper;
            _ordersDatabaseConfig = ordersDatabaseConfigOptions.Value;
            _orderDocumentCollectionFactory = UriFactory.CreateDocumentCollectionUri(ordersDatabaseConfigOptions.Value.DatabaseName, ordersDatabaseConfigOptions.Value.OrdersCollectionName);
        }

        public async Task<DataResult<string>> Create(OrderDto orderDto)
        {
            var orderToCreate = _mapper.Map<Order>(orderDto);
            var response = await _documentClient.CreateDocumentAsync(_orderDocumentCollectionFactory, orderToCreate);

            var isSuccessful = response.StatusCode == HttpStatusCode.Created;

            var result = new DataResult<string>(isSuccessful, response.Resource.Id);

            return result;
        }

        public async Task Replace(OrderDto order)
        {
            var documentUri = UriFactory.CreateDocumentUri(_ordersDatabaseConfig.DatabaseName, _ordersDatabaseConfig.OrdersCollectionName, order.Id);

            await _documentClient.ReplaceDocumentAsync(documentUri, order);
        }

        public async Task<OrderDto> Get(string documentId)
        {
            try
            {
                var order = await _documentClient.ReadDocumentAsync<Order>(UriFactory.CreateDocumentUri(_ordersDatabaseConfig.DatabaseName, _ordersDatabaseConfig.OrdersCollectionName, documentId));
                var result = _mapper.Map<OrderDto>(order);
                return result;
            }
            catch (DocumentClientException ex)
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

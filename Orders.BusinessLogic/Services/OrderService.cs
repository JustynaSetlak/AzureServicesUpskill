using Orders.Dtos.Order;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrder(CreateOrderDto createOrderDto)
        {
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = createOrderDto.CategoryId,
                Description = createOrderDto.Description,
                Name = createOrderDto.Name,
                Price = createOrderDto.Price,
                TagIds = createOrderDto.TagIds
            };

            await _orderRepository.CreateOrder(order);
        }
    }
}

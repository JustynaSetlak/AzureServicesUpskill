using Orders.DataAccess.Interfaces;
using Orders.Dtos.Order;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBaseTableDbGenericRepository<Tag> _tagRepository;
        private readonly IBaseTableDbGenericRepository<Category> _categoryRepository;

        public OrderService(IOrderRepository orderRepository, IBaseTableDbGenericRepository<Tag> tagRepository, IBaseTableDbGenericRepository<Category> categoryRepository)
        {
            _orderRepository = orderRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task CreateOrder(CreateOrderDto createOrderDto)
        {
            var order = new Order
            {
                CategoryId = createOrderDto.CategoryId,
                Description = createOrderDto.Description,
                Name = createOrderDto.Name,
                Price = createOrderDto.Price,
                TagIds = createOrderDto.TagIds
            };

            await _orderRepository.CreateOrder(order);
        }

        public async Task<OrderDto> Get(string id)
        {
            var order = await _orderRepository.GetOrder(id);

            var result = new OrderDto
            {
                Id = order.Id,
                Description = order.Description,
                Name = order.Name
            };

            var categoryRetrieveResult = await _categoryRepository.Get(nameof(Category), order.CategoryId);

            var tags = new List<Tag>();

            foreach (var tagId in order.TagIds)
            {
                var tagRetrieveResult = await _tagRepository.Get(nameof(Tag), tagId);

                if (tagRetrieveResult.IsSuccessfull)
                {
                    tags.Add(tagRetrieveResult.Value);
                }
            }

            result.CategoryName = categoryRetrieveResult.Value.Name;
            result.Tags = tags.Select(t => t.Name).ToList();

            return result;
        }
    }
}

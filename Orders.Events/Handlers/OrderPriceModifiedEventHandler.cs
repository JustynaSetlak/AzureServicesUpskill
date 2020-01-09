using AutoMapper;
using Orders.EventHandler.Events;
using Orders.Search.Models;
using Orders.Search.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Orders.EventHandler.Handlers
{
    public class OrderPriceModifiedEventHandler : IEventHandler<OrderPriceModified>
    {
        private readonly IOrderSearchService _orderSearchService;
        private readonly IMapper _mapper;

        public OrderPriceModifiedEventHandler(IOrderSearchService orderSearchService, IMapper mapper)
        {
            _orderSearchService = orderSearchService;
            _mapper = mapper;
        }

        public async Task Handle(OrderPriceModified eventData)
        {
            var existingOrder = await _orderSearchService.Get(eventData.Id);

            existingOrder.Price = eventData.Price;

            var orderToUpdate = _mapper.Map<OrderUploadModel>(existingOrder);
            await _orderSearchService.MergeOrUpload(orderToUpdate);
        }
    }
}

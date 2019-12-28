using AutoMapper;
using Orders.EventHandler.Events;
using Orders.Search.Models;
using Orders.Search.Services.Interfaces;
using System.Threading.Tasks;

namespace Orders.EventHandler.Handlers
{
    public class ImageAssignedToOrderEventHandler : IEventHandler<ImageAssignedToOrder>
    {
        private readonly IOrderSearchService _orderSearchService;
        private readonly IMapper _mapper;

        public ImageAssignedToOrderEventHandler(IOrderSearchService orderSearchService, IMapper mapper)
        {
            _orderSearchService = orderSearchService;
            _mapper = mapper;
        }

        public async Task Handle(ImageAssignedToOrder eventData)
        {
            var existingOrder = await _orderSearchService.Get(eventData.OrderId);

            existingOrder.ImageUrl = eventData.ImageUrl;

            var orderToUpdate = _mapper.Map<OrderUploadModel>(existingOrder);
            await _orderSearchService.MergeOrUpload(orderToUpdate);
        }
    }
}

using AutoMapper;
using Orders.EventHandler.Events;
using Orders.Search.Models;
using Orders.Search.Services.Interfaces;
using System.Threading.Tasks;

namespace Orders.EventHandler.Handlers
{
    public class ImageUnussignedFromOrderEventHandler : IEventHandler<ImageUnussignedFromOrder>
    {
        private readonly IOrderSearchService _orderSearchService;
        private readonly IMapper _mapper;

        public ImageUnussignedFromOrderEventHandler(IOrderSearchService orderSearchService, IMapper mapper)
        {
            _orderSearchService = orderSearchService;
            _mapper = mapper;
        }

        public async Task Handle(ImageUnussignedFromOrder eventData)
        {
            var existingOrder = await _orderSearchService.Get(eventData.OrderId);

            existingOrder.ImageUrl = string.Empty;

            var orderToUpdate = _mapper.Map<OrderUploadModel>(existingOrder);
            await _orderSearchService.MergeOrUpload(orderToUpdate);
        }
    }
}

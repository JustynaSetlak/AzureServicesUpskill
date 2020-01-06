using AutoMapper;
using Microsoft.AspNetCore.Http;
using Orders.BusinessLogic.Dtos.Order;
using Orders.BusinessLogic.Interfaces;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.DataAccess.Repositories.Models;
using Orders.EventHandler.Events;
using Orders.EventHandler.Interfaces;
using Orders.Results;
using Orders.Search.Models;
using Orders.Search.Services.Interfaces;
using Orders.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IImageUploadService _imageUploadService;
        private readonly IOrderSearchService _orderSearchService;
        private readonly IOrderEventsPublishService _orderEventsPublishService;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IImageUploadService imageUploadService, 
            IOrderSearchService orderSearchService,
            IOrderEventsPublishService orderEventsPublishService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _imageUploadService = imageUploadService;
            _orderSearchService = orderSearchService;
            _orderEventsPublishService = orderEventsPublishService;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Search(SearchOrderParamsDto searchOrderParams)
        {
            var searchParameters = _mapper.Map<OrderSearchParamsModel>(searchOrderParams);

            var result = await _orderSearchService.Search(searchParameters);

            var mappedResult = _mapper.Map<List<OrderDto>>(result);

            return mappedResult;
        }

        public async Task<DataResult<string>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var orderToCreate = _mapper.Map<Order>(createOrderDto);
            var creatingResult = await _orderRepository.CreateOrder(orderToCreate);

            if (!creatingResult.IsSuccessfull)
            {
                return new DataResult<string>(creatingResult.IsSuccessfull, string.Empty);
            }

            orderToCreate.Id = creatingResult.Value;
            var newOrderCreatedEvent = _mapper.Map<NewOrderCreated>(orderToCreate);

            await _orderEventsPublishService.PublishEvent(newOrderCreatedEvent);

            return creatingResult;
        }

        public async Task<bool> AssignOrderImage(string id, string uploadedFileName, IFormFile uploadedFile)
        {
            var order = await _orderRepository.GetOrder(id);

            if (order == null || !string.IsNullOrEmpty(order.ImageUrl))
            {
                return false;
            }

            var imageUri = await _imageUploadService.UploadFile(uploadedFileName, uploadedFile);

            if (string.IsNullOrEmpty(imageUri))
            {
                return false;
            }

            order.ImageUrl = _imageUploadService.GetImageMiniatureUrl(uploadedFileName);
            await _orderRepository.ReplaceDocument(order);

            await _orderEventsPublishService.PublishEvent(new ImageAssignedToOrder(order.Id, order.ImageUrl));

            return true;
        }

        public async Task DeleteImage(string orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);

            if (string.IsNullOrEmpty(order?.ImageUrl))
            {
                return;
            }

            var oldImageUrl = order.ImageUrl;
            order.ImageUrl = string.Empty;

            await _orderRepository.ReplaceDocument(order);
            await _imageUploadService.RemoveFile(oldImageUrl);

            await _orderEventsPublishService.PublishEvent(new ImageUnussignedFromOrder(order.Id));
        }

        public async Task<OrderDto> Get(string id)
        {
            var searchedOrder = await _orderSearchService.Get(id);

            var result = _mapper.Map<OrderDto>(searchedOrder);

            return result;
        }

        public async Task UpdatePrice(string id, double price)
        {
            var order = await _orderRepository.GetOrder(id);

            if (string.IsNullOrEmpty(order?.ImageUrl))
            {
                return;
            }

            order.Price = price;
            await _orderRepository.ReplaceDocument(order);

            await _orderEventsPublishService.PublishEvent(new OrderPriceModified(order.Id, order.Name, order.Price));
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Orders.BusinessLogic.Dtos.Order;
using Orders.BusinessLogic.Services.Interfaces;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.DocumentDataAccess.Dtos;
using Orders.EventHandler.Events;
using Orders.EventHandler.Interfaces;
using Orders.Results;
using Orders.Search.Models;
using Orders.Search.Services.Interfaces;
using Orders.Storage.FileStorage.Services.Interfaces;
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

        public async Task<List<OrderDetailsDto>> Search(SearchOrderParamsDto searchOrderParams)
        {
            var searchParameters = _mapper.Map<OrderSearchParamsModel>(searchOrderParams);

            var result = await _orderSearchService.Search(searchParameters);

            var mappedResult = _mapper.Map<List<OrderDetailsDto>>(result);

            return mappedResult;
        }

        public async Task<DataResult<string>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var orderToCreate = _mapper.Map<OrderDto>(createOrderDto);
            var creatingResult = await _orderRepository.Create(orderToCreate);

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
            var order = await _orderRepository.Get(id);

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
            await _orderRepository.Replace(order);

            await _orderEventsPublishService.PublishEvent(new ImageAssignedToOrder(order.Id, order.ImageUrl));

            return true;
        }

        public async Task DeleteImage(string orderId)
        {
            var order = await _orderRepository.Get(orderId);

            if (string.IsNullOrEmpty(order?.ImageUrl))
            {
                return;
            }

            var oldImageUrl = order.ImageUrl;
            order.ImageUrl = string.Empty;

            await _orderRepository.Replace(order);
            await _imageUploadService.RemoveFile(oldImageUrl);

            await _orderEventsPublishService.PublishEvent(new ImageUnussignedFromOrder(order.Id));
        }

        public async Task<OrderDetailsDto> Get(string id)
        {
            var searchedOrder = await _orderSearchService.Get(id);

            var result = _mapper.Map<OrderDetailsDto>(searchedOrder);

            return result;
        }

        public async Task UpdatePrice(string id, double price)
        {
            var order = await _orderRepository.Get(id);

            if (string.IsNullOrEmpty(order?.ImageUrl))
            {
                return;
            }

            order.Price = price;
            await _orderRepository.Replace(order);

            await _orderEventsPublishService.PublishEvent(new OrderPriceModified(order.Id, order.Name, order.Price));
        }
    }
}

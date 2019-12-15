using AutoMapper;
using Microsoft.AspNetCore.Http;
using Orders.BusinessLogic.Interfaces;
using Orders.Common.Results;
using Orders.Dtos.Order;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Results;
using Orders.Search.Interfaces;
using Orders.Search.Models;
using Orders.Services.Interfaces;
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
        private readonly IImageUploadService _imageUploadService;
        private readonly IOrderSearchService _orderSearchService;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository, 
            IBaseTableDbGenericRepository<Tag> tagRepository, 
            IBaseTableDbGenericRepository<Category> categoryRepository, 
            IImageUploadService imageUploadService, 
            IOrderSearchService orderSearchService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _imageUploadService = imageUploadService;
            _orderSearchService = orderSearchService;
            _mapper = mapper;
        }

        public async Task<DataResult<string>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var orderToCreate = _mapper.Map<Order>(createOrderDto);
            var creatingResult = await _orderRepository.CreateOrder(orderToCreate);

            if (!creatingResult.IsSuccessfull)
            {
                return new DataResult<string>(creatingResult.IsSuccessfull, string.Empty);
            }

            var orderUploadModel = _mapper.Map<OrderUploadModel>(orderToCreate);
            orderUploadModel.Id = creatingResult.Value;

            await _orderSearchService.MergeOrUpload(orderUploadModel);

            return creatingResult;
        }

        public async Task<bool> UploadOrderImage(string id, string uploadedFileName, IFormFile uploadedFile)
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

            order.ImageUrl = imageUri;
            await _orderRepository.ReplaceDocument(order);

            var orderUploadModel = _mapper.Map<OrderUploadModel>(order);
            await _orderSearchService.MergeOrUpload(orderUploadModel);

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

            var orderUploadSearchModel = _mapper.Map<OrderUploadModel>(order);
            await _orderSearchService.MergeOrUpload(orderUploadSearchModel);
        }

        public async Task<OrderDto> Get(string id)
        {
            var searchedOrder = await _orderSearchService.Get(id);

            var result = _mapper.Map<OrderDto>(searchedOrder);

            return result;
        }
    }
}

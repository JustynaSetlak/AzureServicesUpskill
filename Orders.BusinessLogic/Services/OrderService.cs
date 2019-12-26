using AutoMapper;
using Microsoft.AspNetCore.Http;
using Orders.BusinessLogic.Dtos.Order;
using Orders.BusinessLogic.Interfaces;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.Models;
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
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageUploadService _imageUploadService;
        private readonly IOrderSearchService _orderSearchService;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ITagRepository tagRepository, 
            ICategoryRepository categoryRepository, 
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

            var orderToUpload = await GetOrderDetails(orderToCreate);
            orderToUpload.Id = creatingResult.Value;

            await _orderSearchService.MergeOrUpload(orderToUpload);

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

            var orderToUpload = await GetOrderDetails(order);
            await _orderSearchService.MergeOrUpload(orderToUpload);

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

            var orderToUpload = await GetOrderDetails(order);

            await _orderSearchService.MergeOrUpload(orderToUpload);
        }

        public async Task<OrderDto> Get(string id)
        {
            var searchedOrder = await _orderSearchService.Get(id);

            var result = _mapper.Map<OrderDto>(searchedOrder);

            return result;
        }

        private async Task<OrderUploadModel> GetOrderDetails(Order orderToUpload)
        {
            var searchModel = _mapper.Map<OrderUploadModel>(orderToUpload);

            var categoryRetrieveResult = await _categoryRepository.Get(orderToUpload.CategoryId);

            if(categoryRetrieveResult.IsSuccessfull && categoryRetrieveResult.Value != null)
            {
                searchModel.Category = categoryRetrieveResult.Value.Name;
            }

            searchModel.Tags = new List<string>();

            foreach (var tagId in orderToUpload.TagIds)
            {
                var tagRetrieveResult = await _tagRepository.Get(tagId);

                if (tagRetrieveResult.IsSuccessfull && tagRetrieveResult.Value != null)
                {
                    searchModel.Tags.Add(tagRetrieveResult.Value.Name);
                }
            }

            return searchModel;
        }
    }
}

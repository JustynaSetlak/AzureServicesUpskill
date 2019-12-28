using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.EventHandler.Events;
using Orders.Search.Models;
using Orders.Search.Services.Interfaces;

namespace Orders.EventHandler.Handlers
{
    public class NewOrderCreatedEventHandler : IEventHandler<NewOrderCreated>
    {
        private readonly IOrderSearchService _orderSearchService;
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;

        public NewOrderCreatedEventHandler(
            IOrderSearchService orderSearchService, 
            IMapper mapper,
            ITagRepository tagRepository,
            ICategoryRepository categoryRepository)
        {
            _orderSearchService = orderSearchService;
            _mapper = mapper;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(NewOrderCreated eventData)
        {
            var newOrderToUpload = await GetOrderDetails(eventData);

            await _orderSearchService.MergeOrUpload(newOrderToUpload);
        }

        private async Task<OrderUploadModel> GetOrderDetails(NewOrderCreated newOrderCreated)
        {
            var searchModel = _mapper.Map<OrderUploadModel>(newOrderCreated);

            var categoryRetrieveResult = await _categoryRepository.Get(newOrderCreated.CategoryId);

            if (categoryRetrieveResult.IsSuccessfull && categoryRetrieveResult.Value != null)
            {
                searchModel.Category = categoryRetrieveResult.Value.Name;
            }

            searchModel.Tags = new List<string>();

            foreach (var tagId in newOrderCreated.TagIds)
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

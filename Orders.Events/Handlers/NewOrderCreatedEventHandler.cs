using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Orders.BusinessLogic.Interfaces.Infrastructure;
using Orders.EventHandler.Events;
using Orders.EventHandler.Models;
using Orders.Hubs;
using Orders.Search.Models;
using Orders.Search.Services.Interfaces;
using Orders.TableStorage.Repositories.Interfaces;

namespace Orders.EventHandler.Handlers
{
    public class NewOrderCreatedEventHandler : IEventHandler<NewOrderCreated>
    {
        private readonly IOrderSearchService _orderSearchService;
        private readonly IMapper _mapper;
        private readonly ITagTableRepository _tagRepository;
        private readonly ICategoryTableRepository _categoryRepository;
        private readonly IHubContext<OrderHub, IClientOrderHubActions> _hubContext;
        private readonly ICacheService _cacheService;

        public NewOrderCreatedEventHandler(
            IOrderSearchService orderSearchService, 
            IMapper mapper,
            ITagTableRepository tagRepository,
            ICategoryTableRepository categoryRepository,
            IHubContext<OrderHub, IClientOrderHubActions> hubContext,
            ICacheService cacheService)
        {
            _orderSearchService = orderSearchService;
            _mapper = mapper;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _hubContext = hubContext;
            _cacheService = cacheService;
        }

        public async Task Handle(NewOrderCreated eventData)
        {
            var newOrderToUpload = await GetOrderDetails(eventData);

            await _orderSearchService.MergeOrUpload(newOrderToUpload);

            await _hubContext.Clients.All.BroadcastMessage(nameof(NewOrderCreated), newOrderToUpload.Name);
        }

        private async Task<OrderUploadModel> GetOrderDetails(NewOrderCreated newOrderCreated)
        {
            var searchModel = _mapper.Map<OrderUploadModel>(newOrderCreated);

            var categoryRetrieveResult = await _categoryRepository.Get(newOrderCreated.CategoryId);

            if (categoryRetrieveResult.IsSuccessfull && categoryRetrieveResult.Value != null)
            {
                searchModel.Category = categoryRetrieveResult.Value.Name;
            }

            searchModel.Tags = await GetTagNames(newOrderCreated.TagIds);

            return searchModel;
        }

        private async Task<List<string>> GetTagNames(List<string> tagIds)
        {
            var tags = new List<string>();

            var cachedData = await _cacheService.GetValue<List<TagDto>>("tags");
            if (cachedData != null)
            {
                var orderTags = cachedData
                    .Where(t => tagIds.Contains(t.RowKey))
                    .Select(t => t.Name)
                    .ToList();

                tags = orderTags;
            }

            foreach (var tagId in tagIds)
            {
                var tagRetrieveResult = await _tagRepository.Get(tagId);

                if (tagRetrieveResult.IsSuccessfull && tagRetrieveResult.Value != null)
                {
                    tags.Add(tagRetrieveResult.Value.Name);
                }
            }

            return tags;
        }
    }
}

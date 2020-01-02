using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Orders.BusinessLogic.Hubs;
using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.EventHandler.Events;
using Orders.Search.Models;
using Orders.Search.Services.Interfaces;

namespace Orders.EventHandler.Handlers
{
    public class NewOrderCreatedEventHandler : IEventHandler<NewOrderCreated>
    {
        private readonly IOrderSearchService _orderSearchService;
        private readonly IMapper _mapper;
        private readonly ITagTableRepository _tagRepository;
        private readonly ICategoryTableRepository _categoryRepository;
        private readonly IHubContext<OrderHub, IClientOrderHubActions> _hubContext;

        public NewOrderCreatedEventHandler(
            IOrderSearchService orderSearchService, 
            IMapper mapper,
            ITagTableRepository tagRepository,
            ICategoryTableRepository categoryRepository,
            IHubContext<OrderHub, IClientOrderHubActions> hubContext)
        {
            _orderSearchService = orderSearchService;
            _mapper = mapper;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _hubContext = hubContext;
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

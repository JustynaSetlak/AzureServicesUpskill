using AutoMapper;
using Orders.DataAccess.Interfaces;
using Orders.Models;
using Orders.Search.Interfaces;
using Orders.Search.Models;
using Orders.Search.Models.SearchModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Search.Providers
{
    public class OrderSearchService : IOrderSearchService
    {
        private readonly ISearchProvider<OrderSearchModel> _searchProvider;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public OrderSearchService(
            ISearchProvider<OrderSearchModel> searchProvider, 
            ICategoryRepository categoryRepository, 
            ITagRepository tagRepository,
            IMapper mapper)
        {
            _searchProvider = searchProvider;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<OrderGetModel> Get(string id)
        {
            var order = await _searchProvider.Get(id);

            var result = _mapper.Map<OrderGetModel>(order);

            return result;
        }

        public async Task MergeOrUpload(OrderUploadModel orderUploadModel)
        {
            var orderDetails = await GetOrderDetails(orderUploadModel);

            await _searchProvider.MergeOrUploadItem(orderDetails);
        }

        private async Task<OrderSearchModel> GetOrderDetails(OrderUploadModel orderUploadModel)
        {
            var searchModel = _mapper.Map<OrderSearchModel>(orderUploadModel);
            
            var category = await _categoryRepository.Get(orderUploadModel.CategoryId);
            searchModel.Category = category.Value.Name;

            searchModel.Tags = new List<string>();

            foreach (var tagId in orderUploadModel.TagIds)
            {
                var tagRetrieveResult = await _tagRepository.Get(tagId);

                if (tagRetrieveResult.IsSuccessfull)
                {
                    searchModel.Tags.Add(tagRetrieveResult.Value.Name);
                }
            }

            return searchModel;
        }
    }
}

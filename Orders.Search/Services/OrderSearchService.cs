using AutoMapper;
using Microsoft.Azure.Search.Models;
using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.Search.Models;
using Orders.Search.Models.SearchModels;
using Orders.Search.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Search.Providers
{
    public class OrderSearchService : IOrderSearchService
    {
        private readonly ISearchService<OrderSearchModel> _searchService;
        private readonly ICategoryTableRepository _categoryRepository;
        private readonly ITagTableRepository _tagRepository;
        private readonly IMapper _mapper;

        public OrderSearchService(
            ISearchService<OrderSearchModel> searchService, 
            ICategoryTableRepository categoryRepository, 
            ITagTableRepository tagRepository,
            IMapper mapper)
        {
            _searchService = searchService;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<OrderGetModel> Get(string id)
        {
            var order = await _searchService.Get(id);

            var result = _mapper.Map<OrderGetModel>(order);

            return result;
        }

        public async Task MergeOrUpload(OrderUploadModel orderUploadModel)
        {
            var orderSearchModel = _mapper.Map<OrderSearchModel>(orderUploadModel);
            
            await _searchService.MergeOrUploadItem(orderSearchModel);
        }

        public async Task<List<OrderGetModel>> Search(OrderSearchParamsModel orderSearchModel)
        {
            var searchParameters = BuildSearchBarameters(orderSearchModel);

            var result = await _searchService.Search(orderSearchModel.ProductName, searchParameters);

            var mappedResult = _mapper.Map<List<OrderGetModel>>(result);

            return mappedResult;
        }

        private SearchParameters BuildSearchBarameters(OrderSearchParamsModel orderSearchModel)
        {
            var filter = $"{nameof(OrderSearchModel.Price)} ge {orderSearchModel.MinimumPrice}";

            if (orderSearchModel.MaximumPrice != null)
            {
                var maximumPriceFilter = $"{nameof(OrderSearchModel.Price)} le {orderSearchModel.MaximumPrice}";
                filter = $"{filter} and {maximumPriceFilter}";
            }

            if (!string.IsNullOrEmpty(orderSearchModel.CategoryName))
            {
                var categoryFilter = $"{nameof(OrderSearchModel.Category)} eq '{orderSearchModel.CategoryName}'";
                filter = $"{filter} and {categoryFilter}";
            }

            var orderExpression = new List<string> { $"{nameof(OrderSearchModel.Price)} {(orderSearchModel.IsPriceSortingAscending ? "asc" : "desc")}" };

            var parameters = new SearchParameters
            {
                OrderBy = orderExpression,
                Skip = (orderSearchModel.PageNumber - 1) * orderSearchModel.NumberOfElementsOnPage,
                Top = orderSearchModel.NumberOfElementsOnPage,
                Filter = filter,
            };

            return parameters;
        }

    }
}

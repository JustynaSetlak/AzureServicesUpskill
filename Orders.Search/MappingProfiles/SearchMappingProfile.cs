using AutoMapper;
using Orders.Search.Models;
using Orders.Search.Models.SearchModels;

namespace Orders.Search.MappingProfiles
{
    public class SearchMappingProfile : Profile
    {
        public SearchMappingProfile()
        {
            CreateMap<OrderUploadModel, OrderSearchModel>();

            CreateMap<OrderSearchModel, OrderGetModel>();

            CreateMap<OrderGetModel, OrderUploadModel>();
        }
    }
}

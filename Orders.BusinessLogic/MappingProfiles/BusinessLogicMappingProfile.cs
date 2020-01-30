using AutoMapper;
using Orders.BusinessLogic.Dtos.Order;
using Orders.BusinessLogic.Dtos.Tag;
using Orders.DataAccess.Storage.Models;
using Orders.EventHandler.Events;
using Orders.Search.Models;

namespace Orders.BusinessLogic.MappingProfiles
{
    public class BusinessLogicMappingProfile : Profile
    {
        public BusinessLogicMappingProfile()
        {
            CreateMap<OrderGetModel, OrderDetailsDto>()
                .ForMember(dst => dst.CategoryName, opts => opts.MapFrom(src => src.Category));

            CreateMap<SearchOrderParamsDto, OrderSearchParamsModel>();

            CreateMap<NewOrderCreated, OrderUploadModel>();

            CreateMap<TagModification, UpsertTagDto>();
        }
    }
}

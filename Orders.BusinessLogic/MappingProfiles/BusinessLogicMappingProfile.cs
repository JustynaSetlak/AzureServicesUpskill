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
            //CreateMap<CreateOrderDto, Order>();

            CreateMap<OrderGetModel, OrderDetailsDto>()
                .ForMember(dst => dst.CategoryName, opts => opts.MapFrom(src => src.Category));

            CreateMap<SearchOrderParamsDto, OrderSearchParamsModel>();

            //CreateMap<Order, NewOrderCreated>();

            CreateMap<NewOrderCreated, OrderUploadModel>();

            CreateMap<TagModification, UpsertTagDto>();

            //CreateMap<UpsertTagDto, Tag>()
            //    .ForMember(dst => dst.RowKey, opts => opts.MapFrom(src => src.Id ?? Guid.NewGuid().ToString()))
            //    .ForMember(dst => dst.PartitionKey, opts => opts.MapFrom(src => nameof(Tag)));
        }
    }
}

using AutoMapper;
using Orders.BusinessLogic.Dtos.Order;
using Orders.EventHandler.Events;
using Orders.Models;
using Orders.Search.Models;
using Orders.Search.Models.SearchModels;

namespace Orders.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDto, Order>();

            CreateMap<OrderUploadModel, OrderSearchModel>();

            CreateMap<OrderSearchModel, OrderGetModel>();

            CreateMap<OrderGetModel, OrderDto>()
                .ForMember(dst => dst.CategoryName, opts => opts.MapFrom(src => src.Category));

            CreateMap<SearchOrderParamsDto, OrderSearchParamsModel>();

            CreateMap<Order, NewOrderCreated>();

            CreateMap<NewOrderCreated, OrderUploadModel>();
        }
    }
}

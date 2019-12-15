using AutoMapper;
using Orders.Dtos.Order;
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

            CreateMap<Order, OrderUploadModel>();

            CreateMap<OrderUploadModel, OrderSearchModel>()
                .ForMember(dst => dst.Category, opts => opts.Ignore())
                .ForMember(dst => dst.Tags, opts => opts.Ignore());

            CreateMap<OrderSearchModel, OrderGetModel>();

            CreateMap<OrderGetModel, OrderDto>()
                .ForMember(dst => dst.CategoryName, opts => opts.MapFrom(src => src.Category));
        }
    }
}

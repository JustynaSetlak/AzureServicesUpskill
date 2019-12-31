using AutoMapper;
using Orders.BusinessLogic.Dtos.Order;
using Orders.BusinessLogic.Dtos.Tag;
using Orders.DataAccess.Repositories.Models;
using Orders.DataAccess.Storage.Models;
using Orders.DataAccess.TableRepositories.Models;
using Orders.EventHandler.Events;
using Orders.Search.Models;
using Orders.Search.Models.SearchModels;
using System;

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

            CreateMap<OrderGetModel, OrderUploadModel>();

            CreateMap<TagModificationRequest, UpsertTagDto>();

            CreateMap<UpsertTagDto, Tag>()
                .ForMember(dst => dst.RowKey, opts => opts.MapFrom(src => src.Id ?? Guid.NewGuid().ToString()))
                .ForMember(dst => dst.PartitionKey, opts => opts.MapFrom(src => nameof(Tag)));
        }
    }
}

using AutoMapper;
using Orders.DataAccess.Repositories.Models;
using Orders.DocumentDataAccess.Dtos;

namespace Orders.DocumentDataAccess.MappingProfiles
{
    public class DocumentDataAccessMappingProfile : Profile
    {
        public DocumentDataAccessMappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}

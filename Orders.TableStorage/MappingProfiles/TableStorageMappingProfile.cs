using AutoMapper;
using Orders.TableStorage.Dtos;
using Orders.TableStorage.Models;

namespace Orders.TableStorage.MappingProfiles
{
    public class TableStorageMappingProfile : Profile
    {
        public TableStorageMappingProfile()
        {
            CreateMap<Tag, TagDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.RowKey));

            CreateMap<TagDto, Tag>()
                .ForMember(dest => dest.RowKey, opts => opts.MapFrom(src => src.Id));
        }
    }
}

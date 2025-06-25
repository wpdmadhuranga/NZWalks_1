using AutoMapper;
using NZWalks.Models.domain1;
using NZWalks.Models.DTO;

namespace NZWalks.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Region, RegionDTO>().ReverseMap();
    }
}
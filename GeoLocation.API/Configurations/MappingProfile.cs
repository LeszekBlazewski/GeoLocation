using AutoMapper;
using GeoLocation.API.Dtos;
using Models;

namespace GeoLocation.API.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IpAddressDetails, IpAddressDetailsDto>();
            CreateMap<IpAddressDetailsDto, IpAddressDetails>();
        }
    }
}

using AutoMapper;
using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Dtos;
using GeolocationApi.Application.Models.GeolocationData;

namespace GeolocationApi.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Geolocation, GeolocationDto>().ReverseMap();
            CreateMap<IEnumerable<Geolocation>, IEnumerable<GeolocationDto>>().ReverseMap();
            CreateMap<GeolocationModel, Geolocation>()
                .ForMember(dest => dest.ContinentCode, opt => opt.MapFrom(src => src.continent_code))
                .ForMember(dest => dest.ContinentName, opt => opt.MapFrom(src => src.continent_name))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.country_code))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.country_name))
                .ForMember(dest => dest.RegionCode, opt => opt.MapFrom(src => src.region_code))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.region_name));
        }
    }
}

using Application.Helpers;
using AutoMapper;
using Domain.Location;
using Domain.Location.Dto;

namespace Presentation.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<LocationEntity, LocationDto>().ReverseMap();
        CreateMap<LocationEntity, LocationDto>()
            .ForMember(dest => dest.LastModified, opt =>
            opt.MapFrom(src => $"{src.DateModified.GetCurrentAge()} ago"));
        CreateMap<LocationEntity, LocationForCreationDto>().ReverseMap();
        CreateMap<LocationEntity, LocationForUpdateDto>().ReverseMap();
    }
}

using Application.Helpers;
using AutoMapper;
using Domain.Entities.ClientType;
using Domain.Entities.ClientType.Dto;

namespace Presentation.Profiles;

public class ClientTypeProfile : Profile
{
    public ClientTypeProfile()
    {
        // map both ways
        CreateMap<ClientTypeEntity, ClientTypeDto>().ReverseMap();
        CreateMap<ClientTypeEntity, ClientTypeDto>()
            .ForMember(dest => dest.LastModified, opt =>
            opt.MapFrom(src => $"{src.DateModified.GetCurrentAge()} ago"));
        CreateMap<ClientTypeEntity, ClientTypeForCreationDto>().ReverseMap();
        CreateMap<ClientTypeEntity, ClientTypeForUpdateDto>().ReverseMap();

    }
}

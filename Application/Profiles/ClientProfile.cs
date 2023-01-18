using Application.Helpers;
using AutoMapper;
using Domain.Entities.Client;
using Domain.Entities.Client.Dto;

namespace Presentation.Profiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        // map both ways
        CreateMap<ClientEntity, ClientDto>().ReverseMap();
        CreateMap<ClientEntity, ClientDto>()
            .ForMember(dest => dest.LastModified, opt =>
            opt.MapFrom(src => $"{src.DateModified.GetCurrentAge()} ago"));
        CreateMap<ClientEntity, ClientForCreationDto>().ReverseMap();
        CreateMap<ClientEntity, ClientForUpdateDto>().ReverseMap();

    }
}

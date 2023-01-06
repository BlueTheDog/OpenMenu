using Application.Helpers;
using AutoMapper;
using Domain.MenuType;
using Domain.MenuType.Dto;

namespace Presentation.Profiles;

public class MenuTypeProfile : Profile
{
    public MenuTypeProfile()
    {
        // map both ways
        CreateMap<MenuTypeEntity, MenuTypeDto>().ReverseMap();
        CreateMap<MenuTypeEntity, MenuTypeDto>()
            .ForMember(dest => dest.LastModified, opt =>
            opt.MapFrom(src => $"{src.DateModified.GetCurrentAge()} ago"));
        CreateMap<MenuTypeEntity, MenuTypeForCreationDto>().ReverseMap();
        CreateMap<MenuTypeEntity, MenuTypeForUpdateDto>().ReverseMap();

    }
}

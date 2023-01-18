using Application.Helpers;
using AutoMapper;
using Domain.Entities.MenuItemType;
using Domain.Entities.MenuItemType.Dto;

namespace Presentation.Profiles;

public class MenuItemTypeProfile : Profile
{
    public MenuItemTypeProfile()
    {
        // map both ways
        CreateMap<MenuItemTypeEntity, MenuItemTypeDto>().ReverseMap();
        CreateMap<MenuItemTypeEntity, MenuItemTypeDto>()
            .ForMember(dest => dest.LastModified, opt =>
            opt.MapFrom(src => $"{src.DateModified.GetCurrentAge()} ago"));
        CreateMap<MenuItemTypeEntity, MenuItemTypeForCreationDto>().ReverseMap();
        CreateMap<MenuItemTypeEntity, MenuItemTypeForUpdateDto>().ReverseMap();

    }
}

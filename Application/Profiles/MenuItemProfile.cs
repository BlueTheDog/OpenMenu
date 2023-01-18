using Application.Helpers;
using AutoMapper;
using Domain.Entities.MenuItem;
using Domain.Entities.MenuItem.Dto;

namespace Presentation.Profiles;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        // map both ways
        CreateMap<MenuItemEntity, MenuItemDto>().ReverseMap();
        CreateMap<MenuItemEntity, MenuItemDto>()
            .ForMember(dest => dest.LastModified, opt =>
            opt.MapFrom(src => $"{src.DateModified.GetCurrentAge()} ago"));
        CreateMap<MenuItemEntity, MenuItemForCreationDto>().ReverseMap();
        CreateMap<MenuItemEntity, MenuItemForUpdateDto>().ReverseMap();

    }
}

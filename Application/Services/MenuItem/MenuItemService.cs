using Domain.ResourceParameters;
using AutoMapper;
using Application.Helpers;
using Application.Services.Entity;
using Application.Interfaces.Property;
using Domain.Entities.MenuItem.Dto;
using Domain.Entities.MenuItem;

namespace Application.Services.MenuItem
{
    public class MenuItemService :
        EntityService<MenuItemEntity, MenuItemResourceParameters, MenuItemDto, MenuItemForCreationDto, MenuItemForUpdateDto>
    {
        public MenuItemService(
            IEntityRepository<MenuItemEntity, MenuItemResourceParameters> clientTypeRepository,
            IMapper mapper,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService,
            IHateoasHelper hateoasHelper)
            : base(clientTypeRepository, mapper, propertyCheckerService, propertyMappingService, hateoasHelper)
        { }
    }
}

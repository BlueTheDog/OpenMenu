using Domain.ResourceParameters;
using AutoMapper;
using Domain.MenuItem;
using Domain.MenuItem.Dto;
using Application.Services;

namespace Application.MenuItem
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

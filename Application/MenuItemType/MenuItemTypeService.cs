using Domain.ResourceParameters;
using AutoMapper;
using Domain.MenuItemType;
using Domain.MenuItemType.Dto;
using Application.Services;

namespace Application.MenuItemType
{
    public class MenuItemTypeService :
        EntityService<MenuItemTypeEntity, MenuItemTypeResourceParameters, MenuItemTypeDto, MenuItemTypeForCreationDto, MenuItemTypeForUpdateDto>
    {
        public MenuItemTypeService(
            IEntityRepository<MenuItemTypeEntity, MenuItemTypeResourceParameters> clientTypeRepository,
            IMapper mapper,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService,
            IHateoasHelper hateoasHelper)
            : base(clientTypeRepository, mapper, propertyCheckerService, propertyMappingService, hateoasHelper)
        { }
    }
}

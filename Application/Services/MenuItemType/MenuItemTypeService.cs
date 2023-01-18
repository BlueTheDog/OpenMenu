using Domain.ResourceParameters;
using AutoMapper;
using Application.Helpers;
using Application.Services.Entity;
using Application.Interfaces.Property;
using Domain.Entities.MenuItemType.Dto;
using Domain.Entities.MenuItemType;

namespace Application.Services.MenuItemType
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

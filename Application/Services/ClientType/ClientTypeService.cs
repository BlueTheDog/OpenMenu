using Domain.ResourceParameters;
using AutoMapper;
using Application.Helpers;
using Application.Services.Entity;
using Application.Interfaces.Property;
using Domain.Entities.ClientType.Dto;
using Domain.Entities.ClientType;

namespace Application.Services.ClientType
{
    public class ClientTypeService :
        EntityService<ClientTypeEntity, ClientTypeResourceParameters, ClientTypeDto, ClientTypeForCreationDto, ClientTypeForUpdateDto>
    {
        public ClientTypeService(
            IEntityRepository<ClientTypeEntity, ClientTypeResourceParameters> clientTypeRepository,
            IMapper mapper,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService,
            IHateoasHelper hateoasHelper)
            : base(clientTypeRepository, mapper, propertyCheckerService, propertyMappingService, hateoasHelper)
        { }
    }
}

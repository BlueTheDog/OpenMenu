using Domain.ResourceParameters;
using AutoMapper;
using Domain.ClientType;
using Domain.ClientType.Dto;
using Application.Services;

namespace Application.ClientType
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

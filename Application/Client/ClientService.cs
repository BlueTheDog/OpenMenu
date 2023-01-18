using Domain.ResourceParameters;
using AutoMapper;
using Domain.Client;
using Domain.Client.Dto;
using Application.Services;

namespace Application.Client
{
    public class ClientService :
        EntityService<ClientEntity, ClientResourceParameters, ClientDto, ClientForCreationDto, ClientForUpdateDto>
    {
        public ClientService(
            IEntityRepository<ClientEntity, ClientResourceParameters> ClientRepository,
            IMapper mapper,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService,
            IHateoasHelper hateoasHelper)
            : base(ClientRepository, mapper, propertyCheckerService, propertyMappingService, hateoasHelper)
        { }
    }
}

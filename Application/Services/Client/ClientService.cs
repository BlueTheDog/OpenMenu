using Domain.ResourceParameters;
using AutoMapper;
using Application.Helpers;
using Application.Services.Entity;
using Application.Interfaces.Property;
using Domain.Entities.Client.Dto;
using Domain.Entities.Client;

namespace Application.Services.Client
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

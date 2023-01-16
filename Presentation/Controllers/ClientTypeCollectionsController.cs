using Application.ClientType;
using AutoMapper;
using Domain.ClientType.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/clienttypecollections")]
    public class ClientTypeCollectionsController : ControllerBase
    {
        private readonly ClientTypeService _clientTypeService;
        private readonly IMapper _mapper;
        public ClientTypeCollectionsController(ClientTypeService clientTypeService, IMapper mapper)
        {
            _clientTypeService = clientTypeService ??
            throw new ArgumentNullException(nameof(clientTypeService));
            _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({clientTypeIds})", Name = "GetClientTypeCollection")]
        public async Task<ActionResult<IEnumerable<ClientTypeForCreationDto>>>
        GetClientTypeCollection([FromRoute] IEnumerable<int> clientTypeIds)
        {
            return Ok(await _clientTypeService.GetEntityCollection(clientTypeIds));
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ClientTypeDto>>> CreateClientTypeCollection(
           IEnumerable<ClientTypeForCreationDto> clientTypeCollection)
        {
            (var clientTypeCollectionToReturn, var clientTypeIdsAsString) =
                await _clientTypeService.CreateEntityCollection(clientTypeCollection);

            return CreatedAtRoute("GetClientTypeCollection",
              new { clientTypeIds = clientTypeIdsAsString },
              clientTypeCollectionToReturn);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.JsonPatch;
using Common.Exceptions;
using Domain.ResourceParameters;
using Application.Services.Entity;
using Domain.Entities.Client.Dto;
using Domain.Entities.Client;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IEntityService<ClientEntity, ClientResourceParameters, ClientDto,
            ClientForCreationDto, ClientForUpdateDto> _clientService;
        public ClientsController(IEntityService<ClientEntity, ClientResourceParameters, ClientDto,
            ClientForCreationDto, ClientForUpdateDto> clientService)
        {
            _clientService = clientService ??
                throw new ArgumentNullException(nameof(clientService));
        }

        /// <summary>
        /// Retrieves a collection of menu types based on the provided parameters and media type.
        /// </summary>
        /// <param name="clientResourceParameters">The parameters to filter the clients.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>A collection of menu types and pagination metadata.</returns>
        [HttpGet(Name = "GetClients")]
        public async Task<IActionResult> GetClients(
            [FromQuery] ClientResourceParameters clientResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            (var clientsFromService, var paginationMetadata) =
                await _clientService.GetEntitiesAsync(clientResourceParameters, parsedMediaType.MediaType!);

            // add pagination headers to the response
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            return Ok(clientsFromService);
        }

        /// <summary>
        /// Retrieves a single menu type based on the provided id and media type.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to retrieve.</param>
        /// <param name="clientResourceParameters">The parameters to filter the menu type.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>The menu type matching the provided id.</returns>
        [HttpGet("{resourceId}", Name = "GetClient")]
        public async Task<IActionResult> GetClient(
            int resourceId,
            [FromQuery] ClientResourceParameters clientResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            var clientFromService = await _clientService.GetEntityAsync(
                clientResourceParameters,
                resourceId,
                mediaType);
            if (clientFromService == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            return Ok(clientFromService);
        }
        /// <summary>
        /// Creates a new menu type with the provided information.
        /// </summary>
        /// <param name="clientForCreationDto">The information for the new menu type.</param>
        /// <returns>The newly created menu type.</returns>
        [HttpPost(Name = "CreateClient")]
        public async Task<ActionResult<ClientDto>> CreateClient(
            ClientForCreationDto clientForCreationDto)
        {
            var clientDtoToReturn = await _clientService.AddEntity(clientForCreationDto);
            return CreatedAtRoute("GetClient", new { resourceId = clientDtoToReturn.Id }, clientDtoToReturn);
        }

        /// <summary>
        /// Updates a menu type with the provided information.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to update.</param>
        /// <param name="clientForUpdateDto">The updated data for the menu type.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPut("{resourceId}", Name = "UpdateClient")]
        public async Task<ActionResult<ClientDto>> UpdateClient(
            int resourceId,
            ClientForUpdateDto clientForUpdateDto)
        {
            await _clientService.UpdateEntity(clientForUpdateDto, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Partially updates a menu type with the provided information.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to partially update.</param>
        /// <param name="clientPatch">The patch document containing the updates to apply to the menu type.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPatch("{resourceId}", Name = "PartiallyUpdateClient")]
        public async Task<ActionResult<ClientDto>> PartiallyUpdateClient(
            int resourceId,
            JsonPatchDocument<ClientForUpdateDto> clientPatch)
        {
            await _clientService.PartiallyUpdateEntity(clientPatch, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Deletes a menu type with the provided id.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to delete.</param>
        /// <returns>An HTTP status code 204 (No Content) if the deletion was successful.</returns>
        [HttpDelete("{resourceId}", Name = "DeleteClient")]
        public async Task<ActionResult<ClientDto>> DeleteClient(int resourceId)
        {
            await _clientService.DeleteEntity(resourceId);
            return NoContent();
        }
    }
}


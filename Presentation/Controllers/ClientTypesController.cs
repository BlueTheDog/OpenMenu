using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.JsonPatch;
using Common.Exceptions;
using Domain.ResourceParameters;
using Application.ClientType;
using Domain.ClientType.Dto;
using Domain.ClientType;
using Application.Location;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/clientTypes")]
    public class ClientTypesController : ControllerBase
    {
        private readonly IEntityService<ClientTypeEntity, ClientTypeResourceParameters, ClientTypeDto,
            ClientTypeForCreationDto, ClientTypeForUpdateDto> _clientTypeService;
        public ClientTypesController(IEntityService<ClientTypeEntity, ClientTypeResourceParameters, ClientTypeDto,
            ClientTypeForCreationDto, ClientTypeForUpdateDto> clientTypeService)
        {
            _clientTypeService = clientTypeService ??
                throw new ArgumentNullException(nameof(clientTypeService));
        }

        /// <summary>
        /// Retrieves a collection of menu types based on the provided parameters and media type.
        /// </summary>
        /// <param name="clientTypeResourceParameters">The parameters to filter the client types.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>A collection of menu types and pagination metadata.</returns>
        [HttpGet(Name = "GetClientTypes")]
        public async Task<IActionResult> GetClientTypes(
            [FromQuery] ClientTypeResourceParameters clientTypeResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            (var clientTypesFromService, var paginationMetadata) =
                await _clientTypeService.GetEntitiesAsync(clientTypeResourceParameters, parsedMediaType.MediaType!);

            // add pagination headers to the response
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            return Ok(clientTypesFromService);
        }

        /// <summary>
        /// Retrieves a single menu type based on the provided id and media type.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to retrieve.</param>
        /// <param name="clientTypeResourceParameters">The parameters to filter the menu type.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>The menu type matching the provided id.</returns>
        [HttpGet("{resourceId}", Name = "GetClientType")]
        public async Task<IActionResult> GetClientType(
            int resourceId,
            [FromQuery] ClientTypeResourceParameters clientTypeResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            var clientTypeFromService = await _clientTypeService.GetEntityAsync(
                clientTypeResourceParameters,
                resourceId,
                mediaType);
            if (clientTypeFromService == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            return Ok(clientTypeFromService);
        }
        /// <summary>
        /// Creates a new menu type with the provided information.
        /// </summary>
        /// <param name="clientTypeForCreationDto">The information for the new menu type.</param>
        /// <returns>The newly created menu type.</returns>
        [HttpPost(Name = "CreateClientType")]
        public async Task<ActionResult<ClientTypeDto>> CreateClientType(
            ClientTypeForCreationDto clientTypeForCreationDto)
        {
            var clientTypeDtoToReturn = await _clientTypeService.AddEntity(clientTypeForCreationDto);
            return CreatedAtRoute("GetClientType", new { resourceId = clientTypeDtoToReturn.Id }, clientTypeDtoToReturn);
        }

        /// <summary>
        /// Updates a menu type with the provided information.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to update.</param>
        /// <param name="clientTypeForUpdateDto">The updated data for the menu type.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPut("{resourceId}", Name = "UpdateClientType")]
        public async Task<ActionResult<ClientTypeDto>> UpdateClientType(
            int resourceId,
            ClientTypeForUpdateDto clientTypeForUpdateDto)
        {
            await _clientTypeService.UpdateEntity(clientTypeForUpdateDto, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Partially updates a menu type with the provided information.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to partially update.</param>
        /// <param name="clientTypePatch">The patch document containing the updates to apply to the menu type.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPatch("{resourceId}", Name = "PartiallyUpdateClientType")]
        public async Task<ActionResult<ClientTypeDto>> PartiallyUpdateClientType(
            int resourceId,
            JsonPatchDocument<ClientTypeForUpdateDto> clientTypePatch)
        {
            await _clientTypeService.PartiallyUpdateEntity(clientTypePatch, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Deletes a menu type with the provided id.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to delete.</param>
        /// <returns>An HTTP status code 204 (No Content) if the deletion was successful.</returns>
        [HttpDelete("{resourceId}", Name = "DeleteClientType")]
        public async Task<ActionResult<ClientTypeDto>> DeleteClientType(int resourceId)
        {
            await _clientTypeService.DeleteEntity(resourceId);
            return NoContent();
        }
    }
}


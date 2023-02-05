using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.JsonPatch;
using Common.Exceptions;
using Domain.ResourceParameters;
using Application.Services.Entity;
using Domain.Entities.Location.Dto;
using Domain.Entities.Location;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly IEntityService<LocationEntity, LocationResourceParameters, LocationDto, 
            LocationForCreationDto, LocationForUpdateDto> _locationService;

        public LocationsController(
            IEntityService<LocationEntity, LocationResourceParameters, LocationDto, 
                LocationForCreationDto, LocationForUpdateDto> locationService)
        {
            _locationService = locationService ??
                throw new ArgumentNullException(nameof(locationService));
        }

        /// <summary>
        /// Retrieves a collection of locations based on the provided parameters and media type.
        /// </summary>
        /// <param name="locationResourceParameters">The parameters to filter the locations.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>A collection of locations and pagination metadata.</returns>
            [HttpGet(Name = "GetLocations")]
            public async Task<IActionResult> GetLocations(
                [FromQuery] LocationResourceParameters locationResourceParameters,
                [FromHeader(Name = "Accept")] string? mediaType)
            {
                //Validate MediaType exists
                if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
                {
                    throw new MediaTypeCustomException();
                }
                (var locationsFromService, var paginationMetadata) =
                    await _locationService.GetEntitiesAsync(locationResourceParameters, parsedMediaType.MediaType!);
           
                // add pagination headers to the response
                Response.Headers.Add("X-Pagination",
                    JsonSerializer.Serialize(paginationMetadata));
                return Ok(locationsFromService);
            }

        /// <summary>
        /// Retrieves a single location based on the provided id and media type.
        /// </summary>
        /// <param name="resourceId">The id of the location to retrieve.</param>
        /// <param name="locationResourceParameters">The parameters to filter the location.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>The location matching the provided id.</returns>
        /// 
        [Authorize]
        [HttpGet("{resourceId}", Name = "GetLocation")]
        public async Task<IActionResult> GetLocation(
            int resourceId,
            [FromQuery] LocationResourceParameters locationResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            var locationFromService = await _locationService.GetEntityAsync(
                locationResourceParameters,
                resourceId,
                mediaType);
            if (locationFromService == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            return Ok(locationFromService);
        }

        /// <summary>
        /// Creates a new location with the provided information.
        /// </summary>
        /// <param name="locationForCreationDto">The information for the new location.</param>
        /// <returns>The newly created location.</returns>
        [HttpPost(Name = "CreateLocation")]
        public async Task<ActionResult<LocationDto>> CreateLocation(
            LocationForCreationDto locationForCreationDto)
        {
            var locationDtoToReturn = await _locationService.AddEntity(locationForCreationDto);
            return CreatedAtRoute("GetLocation", new { resourceId = locationDtoToReturn.Id }, locationDtoToReturn);
        }

        /// <summary>
        /// Updates a location based on the provided location update data and resource id.
        /// </summary>
        /// <param name="resourceId">The id of the location to be updated.</param>
        /// <param name="locationForUpdateDto">The update data for the location.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPut("{resourceId}", Name = "PutLocation")]
        public async Task<ActionResult<LocationEntity>> UpdateLocation(
            int resourceId,
            LocationForUpdateDto locationForUpdateDto)
        {
            await _locationService.UpdateEntity(locationForUpdateDto, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Partially updates the location with the specified id.
        /// </summary>
        /// <param name="resourceId">The id of the location to update.</param>
        /// <param name="locationPatch">The patch document containing the updates to apply to the location.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPatch("{resourceId}", Name = "PatchLocation")]
        public async Task<ActionResult<LocationEntity>> PartiallyUpdateLocation(
            int resourceId,
            JsonPatchDocument<LocationForUpdateDto> locationPatch)
        {
            await _locationService.PartiallyUpdateEntity(locationPatch, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Deletes the location with the specified id.
        /// </summary>
        /// <param name="resourceId">The id of the location to delete.</param>
        /// <returns>An HTTP status code 204 (No Content) if the deletion was successful.</returns>
        [HttpDelete("{resourceId}", Name = "DeleteLocation")]
        public async Task<ActionResult> DeleteLocation(int resourceId)
        {
            await _locationService.DeleteEntity(resourceId);
            return NoContent();
        }
    }
}

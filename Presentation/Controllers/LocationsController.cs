using Application.Location;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Domain.Location.Dto;
using Domain.Location;
using Microsoft.AspNetCore.JsonPatch;
using Common.Exceptions;
using Domain.ResourceParameters;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly LocationService _locationService;
        public LocationsController(
            LocationService locationService)
        {
            _locationService = locationService ??
                throw new ArgumentNullException(nameof(locationService));
        }

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
            var locationFromService = await _locationService.GetLocationAsync(
                locationResourceParameters,
                resourceId,
                mediaType);
            if (locationFromService == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            return Ok(locationFromService);
        }

        [HttpPost(Name = "CreateLocation")]
        public async Task<ActionResult<LocationDto>> CreateLocation(
            LocationForCreationDto locationForCreationDto)
        {
            var locationDtoToReturn = await _locationService.AddLocation(locationForCreationDto);
            return CreatedAtRoute("GetLocation", new { resourceId = locationDtoToReturn.Id }, locationDtoToReturn);
        }

        [HttpPut("{resourceId}", Name = "PutLocation")]
        public async Task<ActionResult<LocationEntity>> UpdateLocation(
            int resourceId,
            LocationForUpdateDto locationForUpdateDto)
        {
            await _locationService.UpdateLocation(locationForUpdateDto, resourceId);
            return NoContent();
        }

        [HttpPatch("{resourceId}", Name = "PatchLocation")]
        public async Task<ActionResult<LocationEntity>> PartiallyUpdateLocation(
            int resourceId,
            JsonPatchDocument<LocationForUpdateDto> locationPatch)
        {
            await _locationService.PartiallyUpdateLocation(locationPatch, resourceId);
            return NoContent();
        }

        [HttpDelete("{resourceId}", Name = "DeleteLocation")]
        public async Task<ActionResult> DeleteLocation(int resourceId)
        {
            await _locationService.DeleteLocation(resourceId);
            return NoContent();
        }
    }
}

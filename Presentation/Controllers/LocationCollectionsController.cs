using Application.Services;
using Domain.Location.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
public class LocationCollectionsController : ControllerBase
{
    private readonly LocationService _locationService;

    public LocationCollectionsController(LocationService locationService)
    {
        _locationService = locationService ??
                throw new ArgumentNullException(nameof(locationService));
    }

    [HttpGet("({locationsIds})", Name = "GetLocationCollection")]
    public async Task<ActionResult<IEnumerable<LocationForCreationDto>>>
        GetLocationCollection([FromRoute] IEnumerable<int> locationIds)
    {
        return Ok(await _locationService.GetEntityCollection(locationIds));
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<LocationDto>>> CreateLocationTypeCollection(
          IEnumerable<LocationForCreationDto> locationCollection)
    {
        (var locationCollectionToReturn, var locationIdsAsString) = 
            await _locationService.CreateEntityCollection(locationCollection);

        return CreatedAtRoute("GetLocationCollection",
          new { locationIds = locationIdsAsString },
          locationCollectionToReturn);
    }
}

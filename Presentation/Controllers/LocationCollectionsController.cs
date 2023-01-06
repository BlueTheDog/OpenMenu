using Application.Location;
using Domain.Location.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
public class LocationCollectionsController : ControllerBase
{

    private readonly ILocationService _locationService;

    public LocationCollectionsController(ILocationService locationService)
    {
        _locationService = locationService ??
                throw new ArgumentNullException(nameof(locationService));
    }

    [HttpGet("({locationsIds})", Name = "GetLocationCollection")]
    public async Task<ActionResult<IEnumerable<LocationForCreationDto>>>
        GetLocationCollection([FromRoute] IEnumerable<int> locationIds)
    {
        return Ok(await _locationService.GetLocationCollection(locationIds));
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<LocationDto>>> CreateMenuTypeCollection(
          IEnumerable<LocationForCreationDto> locationCollection)
    {
        (var locationCollectionToReturn, var locationIdsAsString) = 
            await _locationService.CreateMenuTypeCollection(locationCollection);

        return CreatedAtRoute("GetLocationCollection",
          new { locationIds = locationIdsAsString },
          locationCollectionToReturn);
    }
}

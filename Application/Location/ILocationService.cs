using Domain;
using Domain.Location;
using Domain.Location.Dto;
using Domain.ResourceParameters;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Location;

public interface ILocationService
{
    Task<(Object?, PaginationMetadataDto)> GetLocationsAsync(
        LocationResourceParameters locationResourceParameters,
        string mediaType);

    Task<IEnumerable<LocationEntity>> GetLocationsAsync(IEnumerable<int> locationIds);
    Task<Object> GetLocationAsync(
        LocationResourceParameters locationResourceParameters,
        int locationId,
        string mediaType);
    public Task<IEnumerable<LocationDto>> GetLocationCollection(IEnumerable<int> locationIds);
    public Task<(IEnumerable<LocationDto>, string)> CreateMenuTypeCollection(
        IEnumerable<LocationForCreationDto> locationCollection);
    void AddLocation(LocationEntity Location);

    Task<LocationDto> AddLocation(LocationForCreationDto locationForCreationDto);
    public Task UpdateLocation(
        LocationForUpdateDto locationForUpdateDto,
        int resourceId);
    public Task PartiallyUpdateLocation(
       JsonPatchDocument<LocationForUpdateDto> locationPatch,
       int resourceId);
    public Task DeleteLocation(int resourceId);
    Task<bool> LocationExists(int locationId);
    Task<bool> DeleteLocationAsync(LocationEntity locationEntity);
    Task<bool> SaveChangesAsync();

}
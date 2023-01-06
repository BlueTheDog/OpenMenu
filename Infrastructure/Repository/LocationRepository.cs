using Application.Helpers;
using Application.Location;
using Application.Services;
using Common.Helpers;
using Domain.Location.Dto;
using Domain.Location;
using Domain.ResourceParameters;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class LocationRepository : ILocationRepository
{
    private readonly IOpenMenuContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;
    public LocationRepository(IOpenMenuContext dbContext, IPropertyMappingService propertyMappingService)
    {
        _dbContext = dbContext ??
            throw new ArgumentNullException(nameof(dbContext));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
    }
    public async Task<PagedList<LocationEntity>> GetLocationsAsync(
            LocationResourceParameters locationResourceParameters)
    {
        if (locationResourceParameters == null)
        {
            throw new ArgumentNullException(nameof(locationResourceParameters));
        }
        var collection = _dbContext.Locations as IQueryable<LocationEntity>;

        if (!string.IsNullOrWhiteSpace(locationResourceParameters.SearchQuery))
        {
            locationResourceParameters.SearchQuery = locationResourceParameters.SearchQuery.Trim();
            collection = collection.Where(x => x.Name.ToLower().Contains(locationResourceParameters.SearchQuery.ToLower()) ||
                x.Description != null && x.Description.ToLower().Contains(locationResourceParameters.SearchQuery.ToLower()));
        }

        // get property mapping dictionary
        var locationsPropertyMappingDictionary = _propertyMappingService
            .GetPropertyMapping<LocationDto, LocationEntity>();
        // orderBy
        collection = collection.ApplySort(locationResourceParameters.OrderBy,
            locationsPropertyMappingDictionary);

        return await PagedList<LocationEntity>.CreateAsync(collection,
            locationResourceParameters.PageNumber,
            locationResourceParameters.PageSize);
    }

    public async Task<IEnumerable<LocationEntity>> GetLocationsAsync(
        IEnumerable<int> locationIds)
    {
        if (locationIds == null)
        {
            throw new ArgumentNullException(nameof(locationIds));
        }
        var collection = _dbContext.Locations as IQueryable<LocationEntity>;
        collection = collection
            .Where(a => locationIds.Contains(a.Id))
            .OrderBy(a => a.Name);
        return await collection
            .ToListAsync();
    }
    public async Task<LocationEntity> GetLocationAsync(int LocationId)
    {
#pragma warning disable CS8603
        // fix this maybe :)
        return await _dbContext.Locations.FindAsync(LocationId);
#pragma warning restore CS8603
    }

    public void AddLocation(LocationEntity locationEntity)
    {
        if (locationEntity == null)
        {
            throw new ArgumentNullException(nameof(locationEntity));
        }
        _dbContext.Locations.Add(locationEntity);
    }

    public void DeleteLocation(LocationEntity locationEntity)
    {
        if (locationEntity == null)
        {
            throw new ArgumentNullException(nameof(locationEntity));
        }
        _dbContext.Locations.Remove(locationEntity);
    }
    public async Task<bool> LocationExists(int LocationId)
    {
        return await _dbContext.Locations.AnyAsync(c => c.Id == LocationId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() >= 0;
    }

}

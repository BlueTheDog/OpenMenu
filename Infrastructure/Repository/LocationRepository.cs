using Application.Services;
using Common.Helpers;
using Domain.Location;
using Domain.ResourceParameters;
using Infrastructure.DbContexts;
using Application.Helpers;
using Domain.Location.Dto;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
internal class LocationRepository : IEntityRepository<LocationEntity, LocationResourceParameters>
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

    public async Task<IEnumerable<LocationEntity>> GetEntitiesAsync(IEnumerable<int> locationIds)
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
        return await _dbContext.Locations.FindAsync(LocationId);
#pragma warning restore CS8603
    }

    public async Task<PagedList<LocationEntity>> GetEntitiesAsync(LocationResourceParameters locationResourceParameters)
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

    public async Task<LocationEntity> GetEntityAsync(int locationId)
    {

#pragma warning disable CS8603
        return await _dbContext.Locations.FindAsync(locationId);
#pragma warning restore CS8603
    }

    public void AddEntity(LocationEntity location)
    {
        if (location == null)
        {
            throw new ArgumentNullException(nameof(location));
        }
        _dbContext.Locations.Add(location);
    }

    public void DeleteEntity(LocationEntity location)
    {
        if (location == null)
        {
            throw new ArgumentNullException(nameof(location));
        }
        _dbContext.Locations.Remove(location);
    }

    public async Task<bool> EntityExistsAsync(int locationId)
    {
        return await _dbContext.Locations.AnyAsync(c => c.Id == locationId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() >= 0;
    }
}
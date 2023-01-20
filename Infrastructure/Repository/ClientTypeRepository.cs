using Common.Helpers;
using Infrastructure.DbContexts;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Application.Services.Entity;
using Application.Interfaces.Property;
using Domain.Entities.ClientType;
using Domain.ResourceParameters;
using Domain.Entities.ClientType.Dto;

namespace Infrastructure.Repository;
internal class ClientTypeRepository : IEntityRepository<ClientTypeEntity, ClientTypeResourceParameters>
{
    private readonly IOpenMenuContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;

    public ClientTypeRepository(IOpenMenuContext dbContext, IPropertyMappingService propertyMappingService)
    {
        _dbContext = dbContext ??
            throw new ArgumentNullException(nameof(dbContext));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
    }

    public async Task<IEnumerable<ClientTypeEntity>> GetEntitiesAsync(IEnumerable<int> clientTypeIds)
    {
        if (clientTypeIds == null)
        {
            throw new ArgumentNullException(nameof(clientTypeIds));
        }
        var collection = _dbContext.ClientTypes as IQueryable<ClientTypeEntity>;
        collection = collection
            .Where(a => clientTypeIds.Contains(a.Id))
            .OrderBy(a => a.Name);
        return await collection
            .ToListAsync();
    }
    public async Task<ClientTypeEntity> GetClientTypeAsync(int ClientTypeId)
    {
#pragma warning disable CS8603
        return await _dbContext.ClientTypes.FindAsync(ClientTypeId);
#pragma warning restore CS8603
    }

    public async Task<PagedList<ClientTypeEntity>> GetEntitiesAsync(ClientTypeResourceParameters clientTypeResourceParameters)
    {
        if (clientTypeResourceParameters == null)
        {
            throw new ArgumentNullException(nameof(clientTypeResourceParameters));
        }
        var collection = _dbContext.ClientTypes as IQueryable<ClientTypeEntity>;

        if (!string.IsNullOrWhiteSpace(clientTypeResourceParameters.SearchQuery))
        {
            clientTypeResourceParameters.SearchQuery = clientTypeResourceParameters.SearchQuery.Trim();
            collection = collection.Where(x => x.Name.ToLower().Contains(clientTypeResourceParameters.SearchQuery.ToLower()));
        }

        // get property mapping dictionary
        var clientTypesPropertyMappingDictionary = _propertyMappingService
            .GetPropertyMapping<ClientTypeDto, ClientTypeEntity>();
        // orderBy
        collection = collection.ApplySort(clientTypeResourceParameters.OrderBy,
            clientTypesPropertyMappingDictionary);

        return await PagedList<ClientTypeEntity>.CreateAsync(collection,
            clientTypeResourceParameters.PageNumber,
            clientTypeResourceParameters.PageSize);
    }

    public async Task<ClientTypeEntity> GetEntityAsync(int clientTypeId)
    {

#pragma warning disable CS8603
        return await _dbContext.ClientTypes.FindAsync(clientTypeId);
#pragma warning restore CS8603
    }

    public void AddEntity(ClientTypeEntity clientType)
    {
        if (clientType == null)
        {
            throw new ArgumentNullException(nameof(clientType));
        }
        _dbContext.ClientTypes.Add(clientType);
    }

    public void DeleteEntity(ClientTypeEntity clientType)
    {
        if (clientType == null)
        {
            throw new ArgumentNullException(nameof(clientType));
        }
        _dbContext.ClientTypes.Remove(clientType);
    }

    public async Task<bool> EntityExistsAsync(int clientTypeId)
    {
        return await _dbContext.ClientTypes.AnyAsync(c => c.Id == clientTypeId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() >= 0;
    }
}
using Common.Helpers;
using Domain.ResourceParameters;
using Infrastructure.DbContexts;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Application.Services.Entity;
using Application.Interfaces.Property;
using Domain.Entities.MenuItemType.Dto;
using Domain.Entities.MenuItemType;

namespace Infrastructure.Repository;
internal class MenuItemTypeRepository : IEntityRepository<MenuItemTypeEntity, MenuItemTypeResourceParameters>
{
    private readonly IOpenMenuContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;

    public MenuItemTypeRepository(IOpenMenuContext dbContext, IPropertyMappingService propertyMappingService)
    {
        _dbContext = dbContext ??
            throw new ArgumentNullException(nameof(dbContext));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
    }

    public async Task<IEnumerable<MenuItemTypeEntity>> GetEntitiesAsync(IEnumerable<int> clientTypeIds)
    {
        if (clientTypeIds == null)
        {
            throw new ArgumentNullException(nameof(clientTypeIds));
        }
        var collection = _dbContext.MenuItemTypes as IQueryable<MenuItemTypeEntity>;
        collection = collection
            .Where(a => clientTypeIds.Contains(a.Id))
            .OrderBy(a => a.Name);
        return await collection
            .ToListAsync();
    }
    public async Task<MenuItemTypeEntity> GetMenuItemTypeAsync(int MenuItemTypeId)
    {
#pragma warning disable CS8603
        return await _dbContext.MenuItemTypes.FindAsync(MenuItemTypeId);
#pragma warning restore CS8603
    }

    public async Task<PagedList<MenuItemTypeEntity>> GetEntitiesAsync(MenuItemTypeResourceParameters clientTypeResourceParameters)
    {
        if (clientTypeResourceParameters == null)
        {
            throw new ArgumentNullException(nameof(clientTypeResourceParameters));
        }
        var collection = _dbContext.MenuItemTypes as IQueryable<MenuItemTypeEntity>;

        if (!string.IsNullOrWhiteSpace(clientTypeResourceParameters.SearchQuery))
        {
            clientTypeResourceParameters.SearchQuery = clientTypeResourceParameters.SearchQuery.Trim();
            collection = collection.Where(x => x.Name.ToLower().Contains(clientTypeResourceParameters.SearchQuery.ToLower()));
        }

        // get property mapping dictionary
        var clientTypesPropertyMappingDictionary = _propertyMappingService
            .GetPropertyMapping<MenuItemTypeDto, MenuItemTypeEntity>();
        // orderBy
        collection = collection.ApplySort(clientTypeResourceParameters.OrderBy,
            clientTypesPropertyMappingDictionary);

        return await PagedList<MenuItemTypeEntity>.CreateAsync(collection,
            clientTypeResourceParameters.PageNumber,
            clientTypeResourceParameters.PageSize);
    }

    public async Task<MenuItemTypeEntity> GetEntityAsync(int clientTypeId)
    {

#pragma warning disable CS8603
        return await _dbContext.MenuItemTypes.FindAsync(clientTypeId);
#pragma warning restore CS8603
    }

    public void AddEntity(MenuItemTypeEntity clientType)
    {
        if (clientType == null)
        {
            throw new ArgumentNullException(nameof(clientType));
        }
        _dbContext.MenuItemTypes.Add(clientType);
    }

    public void DeleteEntity(MenuItemTypeEntity clientType)
    {
        if (clientType == null)
        {
            throw new ArgumentNullException(nameof(clientType));
        }
        _dbContext.MenuItemTypes.Remove(clientType);
    }

    public async Task<bool> EntityExistsAsync(int clientTypeId)
    {
        return await _dbContext.MenuItemTypes.AnyAsync(c => c.Id == clientTypeId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() >= 0;
    }
}
using Application.Services;
using Common.Helpers;
using Domain.ResourceParameters;
using Infrastructure.DbContexts;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Domain.MenuItem;
using Domain.MenuItem.Dto;

namespace Infrastructure.Repository;
internal class MenuItemRepository : IEntityRepository<MenuItemEntity, MenuItemResourceParameters>
{
    private readonly IOpenMenuContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;

    public MenuItemRepository(IOpenMenuContext dbContext, IPropertyMappingService propertyMappingService)
    {
        _dbContext = dbContext ??
            throw new ArgumentNullException(nameof(dbContext));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
    }

    public async Task<IEnumerable<MenuItemEntity>> GetEntitiesAsync(IEnumerable<int> clientTypeIds)
    {
        if (clientTypeIds == null)
        {
            throw new ArgumentNullException(nameof(clientTypeIds));
        }
        var collection = _dbContext.MenuItems as IQueryable<MenuItemEntity>;
        collection = collection
            .Where(a => clientTypeIds.Contains(a.Id))
            .OrderBy(a => a.Name);
        return await collection
            .ToListAsync();
    }
    public async Task<MenuItemEntity> GetMenuItemAsync(int MenuItemId)
    {
#pragma warning disable CS8603
        return await _dbContext.MenuItems.FindAsync(MenuItemId);
#pragma warning restore CS8603
    }

    public async Task<PagedList<MenuItemEntity>> GetEntitiesAsync(MenuItemResourceParameters clientTypeResourceParameters)
    {
        if (clientTypeResourceParameters == null)
        {
            throw new ArgumentNullException(nameof(clientTypeResourceParameters));
        }
        var collection = _dbContext.MenuItems as IQueryable<MenuItemEntity>;

        if (!string.IsNullOrWhiteSpace(clientTypeResourceParameters.SearchQuery))
        {
            clientTypeResourceParameters.SearchQuery = clientTypeResourceParameters.SearchQuery.Trim();
            collection = collection.Where(x => x.Name.ToLower().Contains(clientTypeResourceParameters.SearchQuery.ToLower()));
        }

        // get property mapping dictionary
        var clientTypesPropertyMappingDictionary = _propertyMappingService
            .GetPropertyMapping<MenuItemDto, MenuItemEntity>();
        // orderBy
        collection = collection.ApplySort(clientTypeResourceParameters.OrderBy,
            clientTypesPropertyMappingDictionary);

        return await PagedList<MenuItemEntity>.CreateAsync(collection,
            clientTypeResourceParameters.PageNumber,
            clientTypeResourceParameters.PageSize);
    }

    public async Task<MenuItemEntity> GetEntityAsync(int clientTypeId)
    {

#pragma warning disable CS8603
        return await _dbContext.MenuItems.FindAsync(clientTypeId);
#pragma warning restore CS8603
    }

    public void AddEntity(MenuItemEntity clientType)
    {
        if (clientType == null)
        {
            throw new ArgumentNullException(nameof(clientType));
        }
        _dbContext.MenuItems.Add(clientType);
    }

    public void DeleteEntity(MenuItemEntity clientType)
    {
        if (clientType == null)
        {
            throw new ArgumentNullException(nameof(clientType));
        }
        _dbContext.MenuItems.Remove(clientType);
    }

    public async Task<bool> EntityExistsAsync(int clientTypeId)
    {
        return await _dbContext.MenuItems.AnyAsync(c => c.Id == clientTypeId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() >= 0;
    }
}
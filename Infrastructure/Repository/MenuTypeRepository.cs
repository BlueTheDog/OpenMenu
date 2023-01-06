using Application.Helpers;
using Application.MenuType;
using Application.Services;
using Common.Helpers;
using Domain.MenuType;
using Domain.ResourceParameters;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Domain.MenuType.Dto;

namespace Infrastructure.Repository;

public class MenuTypeRepository : IMenuTypeRepository
{
    private readonly OpenMenuContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;

    public MenuTypeRepository(OpenMenuContext dbContext, IPropertyMappingService propertyMappingService)
    {
        _dbContext = dbContext ??
            throw new ArgumentNullException(nameof(dbContext));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
    }

    public void AddMenuType(MenuTypeEntity menuTypeEntity)
    {
        if (menuTypeEntity == null)
        {
            throw new ArgumentNullException(nameof(menuTypeEntity));
        }
        _dbContext.MenuTypes.Add(menuTypeEntity);
    }

    public void DeleteMenuType(MenuTypeEntity menuTypeEntity)
    {
        if (menuTypeEntity == null)
        {
            throw new ArgumentNullException(nameof(menuTypeEntity));
        }
        _dbContext.MenuTypes.Remove(menuTypeEntity);
    }

    public async Task<MenuTypeEntity> GetMenuTypeAsync(int menuTypeId)
    {
#pragma warning disable CS8603
        // fix this maybe :)
        return await _dbContext.MenuTypes.FindAsync(menuTypeId);
#pragma warning restore CS8603
    }

    public async Task<PagedList<MenuTypeEntity>> GetMenuTypesAsync(
        MenuTypeResourceParameters menuTypeEntityResourceParameters)
    {
        if (menuTypeEntityResourceParameters == null)
        {
            throw new ArgumentNullException(nameof(menuTypeEntityResourceParameters));
        }
        var collection = _dbContext.MenuTypes as IQueryable<MenuTypeEntity>;

        if (!string.IsNullOrWhiteSpace(menuTypeEntityResourceParameters.SearchQuery))
        {
            menuTypeEntityResourceParameters.SearchQuery = menuTypeEntityResourceParameters.SearchQuery.Trim();
            collection = collection!.Where(x => x.Name.ToLower().Contains(menuTypeEntityResourceParameters.SearchQuery.ToLower()));
        }

        // get property mapping dictionary
        var menuTypesPropertyMappingDictionary = _propertyMappingService
            .GetPropertyMapping<MenuTypeDto, MenuTypeEntity>();
        // orderBy
        collection = collection.ApplySort(menuTypeEntityResourceParameters.OrderBy,
            menuTypesPropertyMappingDictionary);

        return await PagedList<MenuTypeEntity>.CreateAsync(collection,
            menuTypeEntityResourceParameters.PageNumber,
            menuTypeEntityResourceParameters.PageSize);
    }

    public async Task<IEnumerable<MenuTypeEntity>> GetMenuTypesAsync(IEnumerable<int> menuTypeIds)
    {
        if (menuTypeIds == null)
        {
            throw new ArgumentNullException(nameof(menuTypeIds));
        }
        var collection = _dbContext.MenuTypes as IQueryable<MenuTypeEntity>;
        collection = collection
            .Where(a => menuTypeIds.Contains(a.Id))
            .OrderBy(a => a.Name);
        return await collection
            .ToListAsync();
    }

    public async Task<bool> MenuTypeExists(int menuTypeId)
    {
        return await _dbContext.MenuTypes.AnyAsync(c => c.Id == menuTypeId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() >= 0;
    }
}

using Common.Helpers;
using Domain.MenuType;
using Domain.ResourceParameters;

namespace Application.MenuType;

public interface IMenuTypeRepository
{
    Task<MenuTypeEntity> GetMenuTypeAsync(int menuTypeId);
    Task<PagedList<MenuTypeEntity>> GetMenuTypesAsync(MenuTypeResourceParameters menuTypeResourceParameters);
    Task<IEnumerable<MenuTypeEntity>> GetMenuTypesAsync(IEnumerable<int> menuTypeIds);
    void AddMenuType(MenuTypeEntity menuType);
    void DeleteMenuType(MenuTypeEntity menuType);
    Task<bool> MenuTypeExists(int menuTypeId);
    Task<bool> SaveChangesAsync();
}
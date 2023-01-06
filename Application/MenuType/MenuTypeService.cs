//using Common.Helpers;
//using Domain;
//using Domain.MenuType;
//using Domain.ResourceParameters;

//namespace Application.MenuType;
//public class MenuTypeService : IMenuTypeService
//{
//    private readonly IMenuTypeRepository _menuTypeRepository;

//    public MenuTypeService(
//        IMenuTypeRepository menuTypeRepository)
//    {
//        _menuTypeRepository = menuTypeRepository ??
//            throw new ArgumentNullException(nameof(menuTypeRepository));
//    }
//    public async Task<(PagedList<MenuTypeEntity>?, PaginationMetadataDto)> GetMenuTypesAsync(
//        MenuTypeResourceParameters menuTypeResourceParameters)
//    {
//        var menuTypes = await _menuTypeRepository.GetMenuTypesAsync(menuTypeResourceParameters);
//        return (menuTypes, CreatePaginationMetadata(menuTypes));
//    }

//    public async Task<IEnumerable<MenuTypeEntity>> GetMenuTypesAsync(IEnumerable<int> menuTypeIds)
//    {
//        return await _menuTypeRepository.GetMenuTypesAsync(menuTypeIds);
//    }

//    public async Task<MenuTypeEntity> GetMenuTypeAsync(int menuTypeId)
//    {
//        return await _menuTypeRepository.GetMenuTypeAsync(menuTypeId);
//    }

//    public void AddMenuType(MenuTypeEntity menuTypeEntity)
//    {
//        _menuTypeRepository.AddMenuType(menuTypeEntity);
//    }

//    public async Task<bool> DeleteMenuTypeAsync(MenuTypeEntity menuTypeEntity)
//    {
//        _menuTypeRepository.DeleteMenuType(menuTypeEntity);
//        await _menuTypeRepository.SaveChangesAsync();
//        return true;
//    }

//    public async Task<bool> MenuTypeExists(int menuTypeId)
//    {
//        return await _menuTypeRepository.MenuTypeExists(menuTypeId);
//    }

//    public async Task<bool> SaveChangesAsync()
//    {
//        return await _menuTypeRepository.SaveChangesAsync();
//    }

//    private static PaginationMetadataDto CreatePaginationMetadata(PagedList<MenuTypeEntity> menuTypeEntities)
//    {
//        return new PaginationMetadataDto(
//            menuTypeEntities.TotalCount,
//            menuTypeEntities.PageSize,
//            menuTypeEntities.CurrentPage,
//            menuTypeEntities.TotalPages
//            );
//    }
//}

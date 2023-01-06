using Domain.ResourceParameters;
using Domain;
using Domain.MenuType;
using Microsoft.AspNetCore.JsonPatch;
using Domain.MenuType.Dto;

namespace Application.MenuType;
public interface IMenuTypeService
{
    Task<(Object?, PaginationMetadataDto)> GetMenuTypesAsync(
        MenuTypeResourceParameters menuTypeResourceParameters,
        string mediaType);

    Task<IEnumerable<MenuTypeEntity>> GetMenuTypesAsync(IEnumerable<int> menuTypeIds);
    Task<Object> GetMenuTypeAsync(
        MenuTypeResourceParameters menuTypeResourceParameters,
        int menuTypeId,
        string mediaType);
    public Task<IEnumerable<MenuTypeDto>> GetMenuTypeCollection(IEnumerable<int> menuTypeIds);
    public Task<(IEnumerable<MenuTypeDto>, string)> CreateMenuTypeCollection(
        IEnumerable<MenuTypeForCreationDto> menuTypeCollection);
    void AddMenuType(MenuTypeEntity MenuType);

    Task<MenuTypeDto> AddMenuType(MenuTypeForCreationDto menuTypeForCreationDto);
    public Task UpdateMenuType(
        MenuTypeForUpdateDto menuTypeForUpdateDto,
        int resourceId);
    public Task PartiallyUpdateMenuType(
       JsonPatchDocument<MenuTypeForUpdateDto> menuTypePatch,
       int resourceId);
    public Task DeleteMenuType(int resourceId);
    Task<bool> MenuTypeExists(int LocationId);
    Task<bool> DeleteMenuTypeAsync(MenuTypeEntity menuTypeEntity);
    Task<bool> SaveChangesAsync();

}

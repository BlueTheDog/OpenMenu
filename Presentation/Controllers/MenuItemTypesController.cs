using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.JsonPatch;
using Common.Exceptions;
using Domain.ResourceParameters;
using Application.MenuItemType;
using Application.Services.Entity;
using Domain.Entities.MenuItemType.Dto;
using Domain.Entities.MenuItemType;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/menuitemtypes")]
    public class MenuItemTypesController : ControllerBase
    {

        private readonly IEntityService<MenuItemTypeEntity, MenuItemTypeResourceParameters, MenuItemTypeDto,
            MenuItemTypeForCreationDto, MenuItemTypeForUpdateDto> _menuItemTypeService;
        public MenuItemTypesController(IEntityService<MenuItemTypeEntity, MenuItemTypeResourceParameters, MenuItemTypeDto,
            MenuItemTypeForCreationDto, MenuItemTypeForUpdateDto> menuItemTypeService)
        {
            _menuItemTypeService = menuItemTypeService ??
                throw new ArgumentNullException(nameof(menuItemTypeService));
        }

        /// <summary>
        /// Retrieves a collection of menu item types based on the provided parameters and media type.
        /// </summary>
        /// <param name="menuItemTypeResourceParameters">The parameters to filter the menuItem types.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>A collection of menu item types and pagination metadata.</returns>
        [HttpGet(Name = "GetMenuItemTypes")]
        public async Task<IActionResult> GetMenuItemTypes(
            [FromQuery] MenuItemTypeResourceParameters menuItemTypeResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            (var menuItemTypesFromService, var paginationMetadata) =
                await _menuItemTypeService.GetEntitiesAsync(menuItemTypeResourceParameters, parsedMediaType.MediaType!);

            // add pagination headers to the response
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            return Ok(menuItemTypesFromService);
        }

        /// <summary>
        /// Retrieves a single menu type based on the provided id and media type.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to retrieve.</param>
        /// <param name="menuItemTypeResourceParameters">The parameters to filter the menu type.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>The menu type matching the provided id.</returns>
        [HttpGet("{resourceId}", Name = "GetMenuItemType")]
        public async Task<IActionResult> GetMenuItemType(
            int resourceId,
            [FromQuery] MenuItemTypeResourceParameters menuItemTypeResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            var menuItemTypeFromService = await _menuItemTypeService.GetEntityAsync(
                menuItemTypeResourceParameters,
                resourceId,
                mediaType);
            if (menuItemTypeFromService == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            return Ok(menuItemTypeFromService);
        }
        /// <summary>
        /// Creates a new menu type with the provided information.
        /// </summary>
        /// <param name="menuItemTypeForCreationDto">The information for the new menu type.</param>
        /// <returns>The newly created menu type.</returns>
        [HttpPost(Name = "CreateMenuItemType")]
        public async Task<ActionResult<MenuItemTypeDto>> CreateMenuItemType(
            MenuItemTypeForCreationDto menuItemTypeForCreationDto)
        {
            var menuItemTypeDtoToReturn = await _menuItemTypeService.AddEntity(menuItemTypeForCreationDto);
            return CreatedAtRoute("GetMenuItemType", new { resourceId = menuItemTypeDtoToReturn.Id }, menuItemTypeDtoToReturn);
        }

        /// <summary>
        /// Updates a menu type with the provided information.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to update.</param>
        /// <param name="menuItemTypeForUpdateDto">The updated data for the menu type.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPut("{resourceId}", Name = "UpdateMenuItemType")]
        public async Task<ActionResult<MenuItemTypeDto>> UpdateMenuItemType(
            int resourceId,
            MenuItemTypeForUpdateDto menuItemTypeForUpdateDto)
        {
            await _menuItemTypeService.UpdateEntity(menuItemTypeForUpdateDto, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Partially updates a menu type with the provided information.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to partially update.</param>
        /// <param name="menuItemTypePatch">The patch document containing the updates to apply to the menu type.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPatch("{resourceId}", Name = "PartiallyUpdateMenuItemType")]
        public async Task<ActionResult<MenuItemTypeDto>> PartiallyUpdateMenuItemType(
            int resourceId,
            JsonPatchDocument<MenuItemTypeForUpdateDto> menuItemTypePatch)
        {
            await _menuItemTypeService.PartiallyUpdateEntity(menuItemTypePatch, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Deletes a menu type with the provided id.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to delete.</param>
        /// <returns>An HTTP status code 204 (No Content) if the deletion was successful.</returns>
        [HttpDelete("{resourceId}", Name = "DeleteMenuItemType")]
        public async Task<ActionResult<MenuItemTypeDto>> DeleteMenuItemType(int resourceId)
        {
            await _menuItemTypeService.DeleteEntity(resourceId);
            return NoContent();
        }
    }
}


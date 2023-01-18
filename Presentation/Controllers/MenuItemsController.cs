using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.JsonPatch;
using Common.Exceptions;
using Domain.ResourceParameters;
using Domain.MenuItem.Dto;
using Application.Location;
using Domain.MenuItem;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/menuitems")]
    public class MenuItemsController : ControllerBase
    {

        private readonly IEntityService<MenuItemEntity, MenuItemResourceParameters, MenuItemDto,
            MenuItemForCreationDto, MenuItemForUpdateDto> _menuItemService;
        public MenuItemsController(IEntityService<MenuItemEntity, MenuItemResourceParameters, MenuItemDto,
            MenuItemForCreationDto, MenuItemForUpdateDto> menuItemService)
        {
            _menuItemService = menuItemService ??
                throw new ArgumentNullException(nameof(menuItemService));
        }

        /// <summary>
        /// Retrieves a collection of menu items based on the provided parameters and media type.
        /// </summary>
        /// <param name="menuItemResourceParameters">The parameters to filter the menuItem types.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>A collection of menu items and pagination metadata.</returns>
        [HttpGet(Name = "GetMenuItems")]
        public async Task<IActionResult> GetMenuItems(
            [FromQuery] MenuItemResourceParameters menuItemResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            (var menuItemsFromService, var paginationMetadata) =
                await _menuItemService.GetEntitiesAsync(menuItemResourceParameters, parsedMediaType.MediaType!);

            // add pagination headers to the response
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            return Ok(menuItemsFromService);
        }

        /// <summary>
        /// Retrieves a single menu type based on the provided id and media type.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to retrieve.</param>
        /// <param name="menuItemResourceParameters">The parameters to filter the menu type.</param>
        /// <param name="mediaType">The media type of the response.</param>
        /// <returns>The menu type matching the provided id.</returns>
        [HttpGet("{resourceId}", Name = "GetMenuItem")]
        public async Task<IActionResult> GetMenuItem(
            int resourceId,
            [FromQuery] MenuItemResourceParameters menuItemResourceParameters,
            [FromHeader(Name = "Accept")] string? mediaType)
        {
            // Validate MediaType
            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                throw new MediaTypeCustomException();
            }
            var menuItemFromService = await _menuItemService.GetEntityAsync(
                menuItemResourceParameters,
                resourceId,
                mediaType);
            if (menuItemFromService == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            return Ok(menuItemFromService);
        }
        /// <summary>
        /// Creates a new menu type with the provided information.
        /// </summary>
        /// <param name="menuItemForCreationDto">The information for the new menu type.</param>
        /// <returns>The newly created menu type.</returns>
        [HttpPost(Name = "CreateMenuItem")]
        public async Task<ActionResult<MenuItemDto>> CreateMenuItem(
            MenuItemForCreationDto menuItemForCreationDto)
        {
            var menuItemDtoToReturn = await _menuItemService.AddEntity(menuItemForCreationDto);
            return CreatedAtRoute("GetMenuItem", new { resourceId = menuItemDtoToReturn.Id }, menuItemDtoToReturn);
        }

        /// <summary>
        /// Updates a menu type with the provided information.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to update.</param>
        /// <param name="menuItemForUpdateDto">The updated data for the menu type.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPut("{resourceId}", Name = "UpdateMenuItem")]
        public async Task<ActionResult<MenuItemDto>> UpdateMenuItem(
            int resourceId,
            MenuItemForUpdateDto menuItemForUpdateDto)
        {
            await _menuItemService.UpdateEntity(menuItemForUpdateDto, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Partially updates a menu type with the provided information.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to partially update.</param>
        /// <param name="menuItemPatch">The patch document containing the updates to apply to the menu type.</param>
        /// <returns>A status code 204 (No Content) indicating that the update was successful.</returns>
        [HttpPatch("{resourceId}", Name = "PartiallyUpdateMenuItem")]
        public async Task<ActionResult<MenuItemDto>> PartiallyUpdateMenuItem(
            int resourceId,
            JsonPatchDocument<MenuItemForUpdateDto> menuItemPatch)
        {
            await _menuItemService.PartiallyUpdateEntity(menuItemPatch, resourceId);
            return NoContent();
        }

        /// <summary>
        /// Deletes a menu type with the provided id.
        /// </summary>
        /// <param name="resourceId">The id of the menu type to delete.</param>
        /// <returns>An HTTP status code 204 (No Content) if the deletion was successful.</returns>
        [HttpDelete("{resourceId}", Name = "DeleteMenuItem")]
        public async Task<ActionResult<MenuItemDto>> DeleteMenuItem(int resourceId)
        {
            await _menuItemService.DeleteEntity(resourceId);
            return NoContent();
        }
    }
}


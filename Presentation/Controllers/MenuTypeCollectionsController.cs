using Application.MenuType;
using AutoMapper;
using Domain.MenuType;
using Domain.MenuType.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/menutypecollections")]
    public class MenuTypeCollectionsController : ControllerBase
    {
        private readonly IMenuTypeService _menuTypeService;
        private readonly IMapper _mapper;
        public MenuTypeCollectionsController(IMenuTypeService menuTypeService, IMapper mapper)
        {
            _menuTypeService = menuTypeService ??
            throw new ArgumentNullException(nameof(menuTypeService));
            _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({menuTypeIds})", Name = "GetMenuTypeCollection")]
        public async Task<ActionResult<IEnumerable<MenuTypeForCreationDto>>>
        GetMenuTypeCollection([FromRoute] IEnumerable<int> menuTypeIds)
        {
            var menuTypeEntities = await _menuTypeService
                .GetMenuTypesAsync(menuTypeIds);

            // do we have all requested menuTypes?
            if (menuTypeIds.Count() != menuTypeEntities.Count())
            {
                return NotFound();
            }

            var menuTypesToReturn = _mapper.Map<IEnumerable<MenuTypeDto>>(menuTypeEntities);
            return Ok(menuTypesToReturn);
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<MenuTypeDto>>> CreateMenuTypeCollection(
           IEnumerable<MenuTypeForCreationDto> menuTypeCollection)
        {
            var menuTypeEntities = _mapper.Map<IEnumerable<MenuTypeEntity>>(menuTypeCollection);
            foreach (var menuType in menuTypeEntities)
            {
                _menuTypeService.AddMenuType(menuType);
            }
            await _menuTypeService.SaveChangesAsync();

            var menuTypeCollectionToReturn = _mapper.Map<IEnumerable<MenuTypeDto>>(menuTypeEntities);
            var menuTypeIdsAsString = string.Join(",", menuTypeCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetMenuTypeCollection",
              new { menuTypeIds = menuTypeIdsAsString },
              menuTypeCollectionToReturn);
        }
    }
}

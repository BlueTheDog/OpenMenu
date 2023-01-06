//using Application.Helpers;
//using Application.MenuType;
//using Application.Services;
//using AutoMapper;
//using Common.Exceptions;
//using Domain.ResourceParameters;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Net.Http.Headers;
//using JsonSerializer = System.Text.Json.JsonSerializer;
//using Domain.MenuType.Dto;
//using Domain.MenuType;
//using Presentation;

//namespace Infrastructure.Controllers
//{
//    [ApiController]
//    [Route("api/menutypes")]
//    [ResponseCache(CacheProfileName = "120SecondsProfile")]
//    public class MenuTypesController : ControllerBase
//    {
//        private readonly string applicationHateoas = "application/hateoas+json";
//        private readonly IMenuTypeService _menuTypeService;
//        private readonly IPropertyMappingService _propertyMappingService;
//        private readonly IPropertyCheckerService _propertyCheckerService;
//        private readonly IMapper _mapper;
//        private readonly IHateoasHelper _hateoasHelper;
//        public MenuTypesController(
//            IMenuTypeService menuTypeService,
//            IPropertyMappingService propertyMappingService,
//            IPropertyCheckerService propertyCheckerService,
//            IMapper mapper,
//            IHateoasHelper hateoasHelper)
//        {
//            _menuTypeService = menuTypeService ??
//               throw new ArgumentNullException(nameof(menuTypeService));
//            _propertyMappingService = propertyMappingService ??
//                throw new ArgumentNullException(nameof(propertyMappingService));
//            _propertyCheckerService = propertyCheckerService ??
//                throw new ArgumentNullException(nameof(propertyCheckerService));
//            _mapper = mapper ??
//                throw new ArgumentNullException(nameof(mapper));
//            _hateoasHelper = hateoasHelper ??
//                throw new ArgumentNullException(nameof(hateoasHelper));
//        }

//        [HttpGet(Name = "GetMenuTypes")]
//        public async Task<IActionResult> GetMenuTypes(
//            [FromQuery] MenuTypeResourceParameters menuTypesResourceParameters,
//            [FromHeader(Name = "Accept")] string? mediaType)
//        {
//            // Validate MediaType
//            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
//            {
//                throw new MediaTypeCustomException();
//            }
//            ValidateOrderBy(menuTypesResourceParameters);
//            ValidateFields(menuTypesResourceParameters.Fields!);
//            (var menuTypesFromService, var paginationMetadata) = await _menuTypeService
//                .GetMenuTypesAsync(menuTypesResourceParameters);

//            // add pagination headers to the response
//            Response.Headers.Add("X-Pagination",
//                JsonSerializer.Serialize(paginationMetadata));
//            if (parsedMediaType.MediaType == applicationHateoas)
//            {
//                //create links
//                var links = _hateoasHelper.CreateLinksForResources(
//                    nameof(MenuTypesController),
//                    menuTypesResourceParameters,
//                    menuTypesFromService!.HasNext,
//                    menuTypesFromService.HasPrevious);

//                var shapedMenuTypes = _mapper.Map<IEnumerable<MenuTypeDto>>(menuTypesFromService)
//                    .ShapeData(menuTypesResourceParameters.Fields);
//                var shapedMenuTypesWithLinks = shapedMenuTypes.Select(menuType =>
//                {
//                    var menuTypeAsDictionary = menuType as IDictionary<string, object?>;
//                    var menuTypeLinks = _hateoasHelper.CreateLinkForResource(
//                        nameof(MenuTypesController), 
//                        (int)menuTypeAsDictionary["Id"]!, null);
//                    menuTypeAsDictionary.Add("links", menuTypeLinks);
//                    return menuTypeLinks;
//                });
//                //var c = shapedMenuTypesWithLinks;
//                var linkedCollectionResource = new
//                {
//                    value = shapedMenuTypes,
//                    links
//                };

//                return Ok(linkedCollectionResource);
//            }
//            return Ok(_mapper.Map<IEnumerable<MenuTypeDto>>(menuTypesFromService)
//                .ShapeData(menuTypesResourceParameters.Fields));
//        }

//        //[ResponseCache(Duration = 120)]
//        [HttpGet("{resourceId}", Name = "GetMenuType")]
//        public async Task<IActionResult> GetMenuTypeAsync(int resourceId, string? fields,
//            [FromHeader(Name = "Accept")] string? mediaType)
//        {
//            // Validate MediaType
//            if (!MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
//            {
//                throw new MediaTypeCustomException();
//            }
//            ValidateFields(fields!);
//            var menuTypeFromRepo = await _menuTypeService.GetMenuTypeAsync(resourceId);
//            if (menuTypeFromRepo == null)
//            {
//                return NotFound();
//            }
//            if (parsedMediaType.MediaType == "application/hateoas+json")
//            {
//                // create links
//                var links = _hateoasHelper.CreateLinkForResource(
//                        nameof(MenuTypesController),
//                        resourceId, 
//                        fields);
//                var linkedResourceToReturn = _mapper.Map<MenuTypeDto>(menuTypeFromRepo)
//                    .ShapeData(fields) as IDictionary<string, object>;
//                linkedResourceToReturn.Add("links", links);

//                return Ok(linkedResourceToReturn);
//            }

//            return Ok(_mapper.Map<MenuTypeDto>(menuTypeFromRepo));
//        }

//        [HttpPost(Name = "CreateMenuType")]
//        public async Task<ActionResult<MenuTypeDto>> CreateMenuType(
//            MenuTypeForCreationDto menuTypeForCreation)
//        {
//            var menuTypeEntityToPersist = _mapper.Map<MenuTypeEntity>(menuTypeForCreation);
//            _menuTypeService.AddMenuType(menuTypeEntityToPersist);
//            await _menuTypeService.SaveChangesAsync();

//            // map entity to a MenuTypeDto so we can return it to user
//            var menuTypeToReturn = _mapper.Map<MenuTypeDto>(menuTypeEntityToPersist);

//            var links = _hateoasHelper.CreateLinkForResource(
//                        nameof(MenuTypesController),
//                menuTypeToReturn.Id, 
//                null);
//            var linkedResourceToReturn = _mapper.Map<MenuTypeDto>(menuTypeToReturn)
//                .ShapeData(null) as IDictionary<string, object?>;
//            linkedResourceToReturn.Add("links", links);

//            return CreatedAtRoute("GetMenuType", new { resourceId = linkedResourceToReturn["Id"] },
//                linkedResourceToReturn);
//        }

//        [HttpPut("{resourceId}", Name = "PutMenuType")]
//        public async Task<ActionResult<MenuTypeEntity>> UpdateMenuType(
//            int resourceId,
//            MenuTypeForUpdateDto menuTypeDto)
//        {
//            var menuTypeEntity = await _menuTypeService.GetMenuTypeAsync(resourceId);
//            if (menuTypeEntity == null)
//            {
//                return NotFound(resourceId);
//            }

//            // like this AutoMapper overwrites val from dest obj with those from source obj
//            _mapper.Map(menuTypeDto, menuTypeEntity);

//            await _menuTypeService.SaveChangesAsync();
//            return NoContent();
//        }

//        [HttpPatch("{resourceId}", Name = "PatchMenuType")]
//        public async Task<ActionResult<MenuTypeEntity>> PartiallyUpdateMenuType(
//            int resourceId,
//            JsonPatchDocument<MenuTypeForUpdateDto> menuTypePatch)
//        {
//            var menuTypeEntity = await _menuTypeService.GetMenuTypeAsync(resourceId);
//            if (menuTypeEntity == null)
//            {
//                return NotFound(resourceId);
//            }
//            var menuTypeToPatch = _mapper.Map<MenuTypeForUpdateDto>(menuTypeEntity);
//            menuTypePatch.ApplyTo(menuTypeToPatch);
//            if (!TryValidateModel(menuTypeToPatch))
//            {
//                //return BadRequest(ModelState);
//                return ValidationProblem(ModelState);
//            }
//            _mapper.Map(menuTypeToPatch, menuTypeEntity);
//            await _menuTypeService.SaveChangesAsync();

//            return NoContent();
//        }

//        [HttpDelete("{resourceId}", Name = "DeleteMenuType")]
//        public async Task<ActionResult> DeleteMenuType(int resourceId)
//        {
//            var menuTypeEntity = await _menuTypeService.GetMenuTypeAsync(resourceId);
//            if (menuTypeEntity == null)
//            {
//                return NotFound(resourceId);
//            }
//            await _menuTypeService.DeleteMenuTypeAsync(menuTypeEntity);
//            await _menuTypeService.SaveChangesAsync();

//            return NoContent();
//        }
//        private void ValidateOrderBy(MenuTypeResourceParameters resourceParameters)
//        {
//            // Validate OrderBy string
//            if (!_propertyMappingService.ValidMappingExistsFor<MenuTypeDto, MenuTypeEntity>(
//                resourceParameters.OrderBy))
//            {
//                throw new OrderByCustomException();
//            }
//        }
//        private void ValidateFields(string fields)
//        {
//            // Validate Fields
//            if (!_propertyCheckerService.TypeHasProperties<MenuTypeDto>(fields))
//            {
//                throw new DataShapingCustomException();
//            }
//        }
//    }
//}

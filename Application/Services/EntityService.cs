using Domain.ResourceParameters;
using Domain;
using AutoMapper;
using Common.Exceptions;
using Application.Helpers;
using Application.Location;

namespace Application.Services
{
    public class EntityService<TEntity, TResourceParameters, TDto, TCreationDto, TUpdateDto> :
        IEntityService<TEntity, TResourceParameters, TDto, TCreationDto, TUpdateDto>
        where TEntity : class
        where TResourceParameters : ResourceParameters
        where TDto : class
        where TCreationDto : class
        where TUpdateDto : class

    {
        private readonly string applicationHateoas = "application/hateoas+json";
        private readonly IEntityRepository<TEntity, TResourceParameters> _entityRepository;
        private readonly IPropertyCheckerService _propertyCheckerService;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        private readonly IHateoasHelper _hateoasHelper;
        public EntityService(
            IEntityRepository<TEntity, TResourceParameters> entityRepository,
            IMapper mapper,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService,
            IHateoasHelper hateoasHelper)
        {
            _entityRepository = entityRepository ??
                throw new ArgumentNullException(nameof(entityRepository));
            _propertyMappingService = propertyMappingService ??
                throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _hateoasHelper = hateoasHelper ??
                throw new ArgumentNullException(nameof(hateoasHelper));
        }
        public async Task<(Object?, PaginationMetadataDto)> GetEntitiesAsync(
            TResourceParameters entityResourceParameters,
            string mediaType)
        {
            ValidateOrderBy(entityResourceParameters);
            ValidateFields(entityResourceParameters.Fields!);

            var entitiesFromRepository = await _entityRepository.GetEntitiesAsync(entityResourceParameters);
            var entities = entitiesFromRepository;
            if (mediaType == applicationHateoas)
            {
                var links = _hateoasHelper.CreateLinksForResources(
                    "EntityController",
                    entityResourceParameters,
                    entitiesFromRepository!.HasNext,
                    entitiesFromRepository.HasPrevious);

                var shapedEntities = _mapper.Map<IEnumerable<TDto>>(entitiesFromRepository)
                    .ShapeData(entityResourceParameters.Fields);

                var shapedEntitiesWithLinks = shapedEntities.Select(entity =>
                {
                    var entityAsDictionary = entity as IDictionary<string, object>;
                    var entityLinks = _hateoasHelper.CreateLinkForResource(
                        "LocationsController",
                        (int)entityAsDictionary["Id"]!,
                        entityResourceParameters.Fields);
                    entityAsDictionary.Add("links", entityLinks);
                    return entityAsDictionary;
                });
                var linkedCollectionResource = new
                {
                    value = shapedEntitiesWithLinks,
                    links
                };
                return (linkedCollectionResource, PaginationHelper.CreatePaginationMetadata(entities));
            }
            var locationsToReturn = _mapper.Map<IEnumerable<TDto>>(entities)
            .ShapeData(entityResourceParameters.Fields);
            return (locationsToReturn, PaginationHelper.CreatePaginationMetadata(entities));
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesAsync(IEnumerable<int> entityIds)
        {
            return await _entityRepository.GetEntitiesAsync(entityIds);
        }

        public async Task<Object> GetEntityAsync(
            TResourceParameters resourceParameters,
            int entityId,
            string mediaType)
        {

            ValidateOrderBy(resourceParameters);
            ValidateFields(resourceParameters.Fields!);
            var entityFromRepo = await _entityRepository.GetEntityAsync(entityId);
            if (entityFromRepo == null)
            {
                throw new ResourceNotFoundCustomException($"Entity with id {entityId} was not found");
            }
            if (mediaType == applicationHateoas)
            {
                var links = _hateoasHelper.CreateLinkForResource(
                    "EntityController",
                    entityId,
                    resourceParameters.Fields);

                var shapedEntity = _mapper.Map<TDto>(entityFromRepo).ShapeData(resourceParameters.Fields);

                var shapedEntityAsDictionary = shapedEntity as IDictionary<string, object>;
                shapedEntityAsDictionary.Add("links", links);
                return shapedEntity;
            }
            else
            {
                return _mapper.Map<TDto>(entityFromRepo).ShapeData(resourceParameters.Fields);
            }
        }
        private void ValidateOrderBy(TResourceParameters resourceParameters)
        {
            // Validate OrderBy string
            if (!_propertyMappingService.ValidMappingExistsFor<TDto, TEntity>(
                resourceParameters.OrderBy))
            {
                throw new OrderByCustomException();

            }
        }
        private void ValidateFields(string fields)
        {
            // Validate Fields
            if (!_propertyCheckerService.TypeHasProperties<TDto>(fields))
            {
                throw new DataShapingCustomException();
            }
        }
    }
}
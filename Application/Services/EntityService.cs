using Domain.ResourceParameters;
using Domain;
using AutoMapper;
using Common.Exceptions;
using Application.Helpers;
using Application.Location;
using Microsoft.AspNetCore.JsonPatch;

// The EntitiesService is a generic service that handles CRUD operations
// for various types of entities and can provide HATEOAS links in the response. 
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
        // The applicationHateoas string is used to identify a media type for HATEOAS responses.
        private readonly string applicationHateoas = "application/hateoas+json";

        // These fields represent dependencies on other objects that the EntityService requires.
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

        // The GetEntitiesAsync method retrieves a collection of entities from the repository
        // based on the provided resource parameters.
        // If the media type is "application/hateoas+json", the response includes HATEOAS links.
        // Otherwise, the response only includes the requested fields for each entity.
        // The method returns a tuple containing the collection of entities and pagination metadata.
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
                    typeof(TEntity).Name,
                    entityResourceParameters,
                    entitiesFromRepository!.HasNext,
                    entitiesFromRepository.HasPrevious);

                var shapedEntities = _mapper.Map<IEnumerable<TDto>>(entitiesFromRepository)
                    .ShapeData(entityResourceParameters.Fields);

                var shapedEntitiesWithLinks = shapedEntities.Select(entity =>
                {
                    var entityAsDictionary = entity as IDictionary<string, object>;
                    var entityLinks = _hateoasHelper.CreateLinkForResource(
                        typeof(TEntity).Name,
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

        // The GetEntitiesAsync method retrieves a collection of entities from the repository based on the provided ids.
        public async Task<IEnumerable<TEntity>> GetEntitiesAsync(IEnumerable<int> entityIds)
        {
            return await _entityRepository.GetEntitiesAsync(entityIds);
        }

        // The GetEntityAsync method retrieves a single entity from the repository
        // based on the provided id and resource parameters.
        // If the entity is not found, a ResourceNotFoundCustomException is thrown.
        // If the media type is "application/hateoas+json", HATEOAS links are included in the response.
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
                    typeof(TEntity).Name,
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

        // The ValidateOrderBy method validates the "orderBy" parameter in the resource parameters.
        // If the "orderBy" parameter is not valid, an InvalidOperationException is thrown.
        private void ValidateOrderBy(TResourceParameters resourceParameters)
        {
            // Validate OrderBy string
            if (!_propertyMappingService.ValidMappingExistsFor<TDto, TEntity>(
                resourceParameters.OrderBy))
            {
                throw new OrderByCustomException();

            }
        }

        // The ValidateFields method validates the "fields" parameter in the resource parameters.
        // If the "fields" parameter is not valid, an InvalidOperationException is thrown.
        private void ValidateFields(string fields)
        {
            // Validate Fields
            if (!_propertyCheckerService.TypeHasProperties<TDto>(fields))
            {
                throw new DataShapingCustomException();
            }
        }

        // The GetEntityCollection method retrieves a collection of entities from the repository based on the provided ids.
        // If the number of requested ids does not match the number of retrieved entities,
        // a ResourceNotFoundCustomException is thrown.
        public async Task<IEnumerable<TDto>> GetEntityCollection(IEnumerable<int> entityIds)
        {
            var entities = await _entityRepository.GetEntitiesAsync(entityIds);
            if (entityIds.Count() != entities.Count())
            {
                throw new ResourceNotFoundCustomException();
            }
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        // The CreateEntityCollection method creates a collection of new entities in the repository
        // based on the provided creation DTOs.
        // The created entities are returned along with a string of their ids.
        public async Task<(IEnumerable<TDto>, string)> CreateEntityCollection(IEnumerable<TCreationDto> entityCollection)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(entityCollection);
            foreach (var entity in entities)
            {
                _entityRepository.AddEntity(entity);
            }
            await _entityRepository.SaveChangesAsync();

            var entityCollectionToReturn = _mapper.Map<IEnumerable<TDto>>(entities);
            ////TODO: FIX    //var entityIdsAsString = string.Join(",", entityCollectionToReturn.Select(x => x.Id));
            var entityIdsAsString = "1,2,3";
            return (entityCollectionToReturn, entityIdsAsString);
        }

        // The AddEntity method adds a new entity to the repository.
        public void AddEntity(TEntity entity)
        {
            _entityRepository.AddEntity(entity);
        }

        public async Task<TDto> AddEntity(TCreationDto creationDto)
        {

            var entityToPersist = _mapper.Map<TEntity>(creationDto);
            _entityRepository.AddEntity(entityToPersist);

            await _entityRepository.SaveChangesAsync();
            return _mapper.Map<TDto>(entityToPersist);
        }

        // The AddEntity method creates a new entity in the repository based on the provided creation DTO.
        // The created entity is returned as a DTO.
        public async Task UpdateEntity(TUpdateDto updateDto, int resourceId)
        {
            var entity = await _entityRepository.GetEntityAsync(resourceId);
            if (entity == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            // like this AutoMapper overwrites val from dest obj with those from source obj
            _mapper.Map(updateDto, entity);

            await _entityRepository.SaveChangesAsync();
        }

        // The PartiallyUpdateEntity method updates an existing entity in the repository based on the provided patch document and id.
        // If the entity is not found, a ResourceNotFoundCustomException is thrown.
        public async Task PartiallyUpdateEntity(JsonPatchDocument<TUpdateDto> entityPatch, int resourceId)
        {
            var entity = await _entityRepository.GetEntityAsync(resourceId);
            if (entity == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            var entityToPatch = _mapper.Map<TUpdateDto>(entity);
            entityPatch.ApplyTo(entityToPatch);
            //TODO: find a way to validate model here is possible
            //if (!TryValidateModel(locationToPatch))
            //{
            //    return BadRequest(ModelState);
            //}
            _mapper.Map(entityToPatch, entity);
            await _entityRepository.SaveChangesAsync();
        }

        // The DeleteEntity method deletes an existing entity from the repository based on the provided id.
        // If the entity is not found, a ResourceNotFoundCustomException is thrown.
        public async Task DeleteEntity(int resourceId)
        {
            var entity = await _entityRepository.GetEntityAsync(resourceId);
            if (entity == null)
            {
                throw new ResourceNotFoundCustomException();
            }
            _entityRepository.DeleteEntity(entity);
            await _entityRepository.SaveChangesAsync();
        }

        // The EntityExists method checks if an entity with the provided id exists in the repository.
        public async Task<bool> EntityExists(int entityId)
        {
            return await _entityRepository.EntityExistsAsync(entityId);
        }

        // The DeleteEntityAsync method deletes an existing entity from the repository.

        public async Task<bool> DeleteEntityAsync(TEntity entity)
        {
            _entityRepository.DeleteEntity(entity);
            await _entityRepository.SaveChangesAsync();
            return true;
        }

        // The SaveChangesAsync method saves an existing entity to the repository.

        public Task<bool> SaveChangesAsync()
        {
            return _entityRepository.SaveChangesAsync();
        }
    }
}
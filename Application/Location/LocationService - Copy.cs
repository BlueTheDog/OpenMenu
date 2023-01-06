//using Application.Helpers;
//using Application.Services;
//using AutoMapper;
//using Common.Exceptions;
//using Domain;
//using Domain.Location;
//using Domain.Location.Dto;
//using Domain.ResourceParameters;
//using Microsoft.AspNetCore.JsonPatch;

//namespace Application.Location;
//public class LocationService : IBaseEntityService<LocationEntity, LocationResourceParameters, LocationDto, LocationForCreationDto, LocationForUpdateDto>
//{
//    private readonly string applicationHateoas = "application/hateoas+json";
//    private readonly ILocationRepository _locationRepository;
//    private readonly IPropertyCheckerService _propertyCheckerService;
//    private readonly IPropertyMappingService _propertyMappingService;
//    private readonly IMapper _mapper;
//    private readonly IHateoasHelper _hateoasHelper;
//    public LocationService(
//        ILocationRepository locationRepository,
//        IMapper mapper,
//        IPropertyCheckerService propertyCheckerService,
//        IPropertyMappingService propertyMappingService,
//        IHateoasHelper hateoasHelper)
//    {
//        _locationRepository = locationRepository ??
//            throw new ArgumentNullException(nameof(locationRepository));
//        _propertyMappingService = propertyMappingService ??
//            throw new ArgumentNullException(nameof(propertyMappingService));
//        _propertyCheckerService = propertyCheckerService ??
//            throw new ArgumentNullException(nameof(propertyCheckerService));
//        _mapper = mapper ??
//            throw new ArgumentNullException(nameof(mapper));
//        _hateoasHelper = hateoasHelper ??
//            throw new ArgumentNullException(nameof(hateoasHelper));
//    }
//    // TODO Refactor this and use 2 methods with one concern
//    public async Task<(Object?, PaginationMetadataDto)> GetLocationsAsync(
//        LocationResourceParameters locationResourceParameters,
//        string mediaType)
//    {
//        ValidateOrderBy(locationResourceParameters);
//        ValidateFields(locationResourceParameters.Fields!);

//        var locationsFromService = await _locationRepository.GetLocationsAsync(locationResourceParameters);
//        var locations = locationsFromService;
//        if (mediaType == applicationHateoas)
//        {
//            var links = _hateoasHelper.CreateLinksForResources(
//                "LocationsController",
//                locationResourceParameters,
//                locationsFromService!.HasNext,
//                locationsFromService.HasPrevious);

//            var shapedLocations = _mapper.Map<IEnumerable<LocationDto>>(locationsFromService)
//                .ShapeData(locationResourceParameters.Fields);

//            var shapedLocationsWithLinks = shapedLocations.Select(location =>
//            {
//                var locationAsDictionary = location as IDictionary<string, object?>;
//                var locationLinks = _hateoasHelper.CreateLinkForResource(
//                    "LocationsController",
//                    (int)locationAsDictionary["Id"]!,
//                    locationResourceParameters.Fields);
//                locationAsDictionary.Add("links", locationLinks);
//                return locationAsDictionary;
//            });
//            var linkedCollectionResource = new
//            {
//                value = shapedLocationsWithLinks,
//                links
//            };
//            return (linkedCollectionResource, PaginationHelper.CreatePaginationMetadata(locations));
//        }
//        var locationsToReturn = _mapper.Map<IEnumerable<LocationDto>>(locations)
//            .ShapeData(locationResourceParameters.Fields);
//        return (locationsToReturn, PaginationHelper.CreatePaginationMetadata(locations));

//    }
//    public async Task<IEnumerable<LocationEntity>> GetLocationsAsync(IEnumerable<int> LocationIds)
//    {
//        var locations = await _locationRepository.GetLocationsAsync(LocationIds);
//        return locations;
//    }
//    public async Task<Object> GetLocationAsync(
//        LocationResourceParameters locationResourceParameters,
//        int resourceId,
//        string mediaType)
//    {

//        ValidateOrderBy(locationResourceParameters);
//        ValidateFields(locationResourceParameters.Fields!);
//        var locationFromRepo = await _locationRepository.GetLocationAsync(resourceId);

//        if (mediaType == applicationHateoas)
//        {
//            // create links
//            var links = _hateoasHelper.CreateLinkForResource("Location", resourceId, locationResourceParameters.Fields);
//            var linkedResourceToReturn = _mapper.Map<LocationDto>(locationFromRepo)
//                .ShapeData(locationResourceParameters.Fields) as IDictionary<string, object>;
//            linkedResourceToReturn.Add("links", links);

//            return linkedResourceToReturn;
//        }
//        return _mapper.Map<LocationDto>(locationFromRepo).ShapeData(locationResourceParameters.Fields);
//    }

//    public async Task<IEnumerable<LocationDto>> GetLocationCollection(
//        IEnumerable<int> locationIds)
//    {
//        var locationEntities = await _locationRepository.GetLocationsAsync(locationIds);

//        // do we have all requested menuTypes?
//        if (locationIds.Count() != locationEntities.Count())
//        {

//            throw new ResourceNotFoundCustomException();
//        }
//        return _mapper.Map<IEnumerable<LocationDto>>(locationEntities);
//    }

//    public async Task<(IEnumerable<LocationDto>, string)> CreateMenuTypeCollection(
//        IEnumerable<LocationForCreationDto> locationCollection)
//    {
//        var locationEntities = _mapper.Map<IEnumerable<LocationEntity>>(locationCollection);
//        foreach (var location in locationEntities)
//        {
//            _locationRepository.AddLocation(location);
//        }
//        await _locationRepository.SaveChangesAsync();

//        var locationCollectionToReturn = _mapper.Map<IEnumerable<LocationDto>>(locationEntities);
//        var locationIdsAsString = string.Join(",", locationCollectionToReturn.Select(a => a.Id));
//        return (locationCollectionToReturn, locationIdsAsString);
//    }
//    public async Task<bool> DeleteLocationAsync(LocationEntity locationEntity)
//    {
//        _locationRepository.DeleteLocation(locationEntity);
//        await _locationRepository.SaveChangesAsync();
//        return true;
//    }
//    public async Task<LocationDto> AddLocation(LocationForCreationDto locationForCreationDto)
//    {
//        var locationEntityToPersist = _mapper.Map<LocationEntity>(locationForCreationDto);
//        _locationRepository.AddLocation(locationEntityToPersist);

//        await _locationRepository.SaveChangesAsync();
//        return _mapper.Map<LocationDto>(locationEntityToPersist);
//    }
//    public void AddLocation(LocationEntity Location)
//    {
//        _locationRepository.AddLocation(Location);
//    }
//    public async Task UpdateLocation(
//        LocationForUpdateDto locationForUpdateDto,
//        int resourceId)
//    {
//        var locationEntity = await _locationRepository.GetLocationAsync(resourceId);
//        if (locationEntity == null)
//        {
//            throw new ResourceNotFoundCustomException();
//        }
//        // like this AutoMapper overwrites val from dest obj with those from source obj
//        _mapper.Map(locationForUpdateDto, locationEntity);

//        await _locationRepository.SaveChangesAsync();
//    }
//    public async Task PartiallyUpdateLocation(
//        JsonPatchDocument<LocationForUpdateDto> locationPatch,
//        int resourceId)
//    {
//        var locationEntity = await _locationRepository.GetLocationAsync(resourceId);
//        if (locationEntity == null)
//        {
//            throw new ResourceNotFoundCustomException();
//        }
//        var locationToPatch = _mapper.Map<LocationForUpdateDto>(locationEntity);
//        locationPatch.ApplyTo(locationToPatch);
//        //TODO: find a way to validate model here is possible
//        //if (!TryValidateModel(locationToPatch))
//        //{
//        //    return BadRequest(ModelState);
//        //}
//        _mapper.Map(locationToPatch, locationEntity);
//        await _locationRepository.SaveChangesAsync();
//    }
//    public async Task DeleteLocation(int resourceId)
//    {
//        var locationEntity = await _locationRepository.GetLocationAsync(resourceId);
//        if (locationEntity == null)
//        {
//            throw new ResourceNotFoundCustomException();
//        }
//        _locationRepository.DeleteLocation(locationEntity);
//        await _locationRepository.SaveChangesAsync();
//    }
//    public async Task<bool> SaveChangesAsync()
//    {
//        return await _locationRepository.SaveChangesAsync();
//    }
//    public async Task<bool> LocationExists(int LocationId)
//    {
//        return await _locationRepository.LocationExists(LocationId);
//    }

//    // Helper Methods
    
//    private void ValidateOrderBy(LocationResourceParameters resourceParameters)
//    {
//        // Validate OrderBy string
//        if (!_propertyMappingService.ValidMappingExistsFor<LocationDto, LocationEntity>(
//            resourceParameters.OrderBy))
//        {
//            throw new OrderByCustomException();

//        }
//    }
//    private void ValidateFields(string fields)
//    {
//        // Validate Fields
//        if (!_propertyCheckerService.TypeHasProperties<LocationDto>(fields))
//        {
//            throw new DataShapingCustomException();
//        }
//    }

//    public async Task<(object?, PaginationMetadataDto)> GetEntitiesAsync(
//        LocationResourceParameters locationResourceParameters, 
//        string mediaType)
//    {
//        ValidateOrderBy(locationResourceParameters);
//        ValidateFields(locationResourceParameters.Fields!);

//        var locationsFromService = await _locationRepository.GetLocationsAsync(locationResourceParameters);
//        var locations = locationsFromService;
//        if (mediaType == applicationHateoas)
//        {
//            var links = _hateoasHelper.CreateLinksForResources(
//                "LocationsController",
//                locationResourceParameters,
//                locationsFromService!.HasNext,
//                locationsFromService.HasPrevious);

//            var shapedLocations = _mapper.Map<IEnumerable<LocationDto>>(locationsFromService)
//                .ShapeData(locationResourceParameters.Fields);

//            var shapedLocationsWithLinks = shapedLocations.Select(location =>
//            {
//                var locationAsDictionary = location as IDictionary<string, object?>;
//                var locationLinks = _hateoasHelper.CreateLinkForResource(
//                    "LocationsController",
//                    (int)locationAsDictionary["Id"]!,
//                    locationResourceParameters.Fields);
//                locationAsDictionary.Add("links", locationLinks);
//                return locationAsDictionary;
//            });
//            var linkedCollectionResource = new
//            {
//                value = shapedLocationsWithLinks,
//                links
//            };
//            return (linkedCollectionResource, PaginationHelper.CreatePaginationMetadata(locations));
//        }
//        var locationsToReturn = _mapper.Map<IEnumerable<LocationDto>>(locations)
//            .ShapeData(locationResourceParameters.Fields);
//        return (locationsToReturn, PaginationHelper.CreatePaginationMetadata(locations));

//    }

//    public Task<IEnumerable<LocationEntity>> GetEntitiesAsync(IEnumerable<int> entityIds)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<object> GetEntityAsync(LocationResourceParameters resourceParameters, int entityId, string mediaType)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IEnumerable<LocationDto>> GetEntityCollection(IEnumerable<int> entityIds)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<(IEnumerable<LocationDto>, string)> CreateEntityCollection(IEnumerable<LocationForCreationDto> entityCollection)
//    {
//        throw new NotImplementedException();
//    }

//    public void AddEntity(LocationEntity entity)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<LocationDto> AddEntity(LocationForCreationDto creationDto)
//    {
//        throw new NotImplementedException();
//    }

//    public Task UpdateEntity(LocationForUpdateDto updateDto, int resourceId)
//    {
//        throw new NotImplementedException();
//    }

//    public Task PartiallyUpdateEntity(JsonPatchDocument<LocationForUpdateDto> entityToPatch, int resourceId)
//    {
//        throw new NotImplementedException();
//    }

//    public Task DeleteEntity(int resourceId)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> EntityExists(int entityId)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> DeleteEntityAsync(LocationEntity entity)
//    {
//        throw new NotImplementedException();
//    }
//}

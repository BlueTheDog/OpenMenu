using Domain.ResourceParameters;
using AutoMapper;
using Application.Helpers;
using Application.Services.Entity;
using Application.Interfaces.Property;
using Domain.Entities.Location.Dto;
using Domain.Entities.Location;

namespace Application.Services.Location
{
    public class LocationService :
        EntityService<LocationEntity, LocationResourceParameters, LocationDto, LocationForCreationDto, LocationForUpdateDto>
    {
        public LocationService(
            IEntityRepository<LocationEntity, LocationResourceParameters> locationRepository,
            IMapper mapper,
            IPropertyCheckerService propertyCheckerService,
            IPropertyMappingService propertyMappingService,
            IHateoasHelper hateoasHelper)
            : base(locationRepository, mapper, propertyCheckerService, propertyMappingService, hateoasHelper)
        { }
    }
}

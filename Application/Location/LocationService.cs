using Domain.ResourceParameters;
using AutoMapper;
using Domain.Location.Dto;
using Domain.Location;

namespace Application.Services
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

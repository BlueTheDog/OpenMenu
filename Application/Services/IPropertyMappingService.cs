using Common.Helpers;

namespace Application.Services;

public interface IPropertyMappingService
{
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    bool ValidMappingExistsFor<TSource, TDestination>(string fields);
}
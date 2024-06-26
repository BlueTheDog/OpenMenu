﻿using Common.Helpers;

namespace Application.Interfaces.Property;

public interface IPropertyMappingService
{
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    bool ValidMappingExistsFor<TSource, TDestination>(string fields);
}
using Application.Interfaces.Property;
using Common.Helpers;
using Domain.Entities.Client;
using Domain.Entities.Client.Dto;
using Domain.Entities.ClientType;
using Domain.Entities.ClientType.Dto;
using Domain.Entities.Location;
using Domain.Entities.Location.Dto;
using Domain.Entities.MenuItem;
using Domain.Entities.MenuItem.Dto;
using Domain.Entities.MenuItemType;
using Domain.Entities.MenuItemType.Dto;

namespace Infrastructure.Services;

public class PropertyMappingService : IPropertyMappingService
{
    private readonly Dictionary<string, PropertyMappingValue> _propertiesMapping =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", new(new[] { "id" }) },
            { "LastModified", new(new[] { "DateModified" }, true) },
            { "Description", new(new[] { "Description" }, true) },
            { "Name", new(new[] { "Name" }) }
        };

    private readonly IList<IPropertyMapping> _propertyMappings =
        new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<ClientDto, ClientEntity>(
            _propertiesMapping));
        _propertyMappings.Add(new PropertyMapping<ClientTypeDto, ClientTypeEntity>(
            _propertiesMapping));
        _propertyMappings.Add(new PropertyMapping<LocationDto, LocationEntity>(
            _propertiesMapping));
        _propertyMappings.Add(new PropertyMapping<MenuItemTypeDto, MenuItemTypeEntity>(
            _propertiesMapping));
        _propertyMappings.Add(new PropertyMapping<MenuItemDto, MenuItemEntity>(
            _propertiesMapping));
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping
          <TSource, TDestination>()
    {
        // get matching mapping
        var matchingMapping = _propertyMappings
            .OfType<PropertyMapping<TSource, TDestination>>();

        if (matchingMapping.Count() == 1)
        {
            return matchingMapping.First().MappingDictionary;
        }

        throw new Exception($"Cannot find exact property mapping instance " +
            $"for <{typeof(TSource)},{typeof(TDestination)}");
    }

    public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
    {
        var propertyMapping = GetPropertyMapping<TSource, TDestination>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            return true;
        }

        // the string is separated by ",", so we split it.
        var fieldsAfterSplit = fields.Split(',');

        // run through the fields clauses
        foreach (var field in fieldsAfterSplit)
        {
            // trim
            var trimmedField = field.Trim();

            // remove everything after the first " " - if the fields 
            // are coming from an orderBy string, this part must be 
            // ignored
            var indexOfFirstSpace = trimmedField.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1 ?
                trimmedField : trimmedField.Remove(indexOfFirstSpace);

            // find the matching property
            if (!propertyMapping.ContainsKey(propertyName))
            {
                return false;
            }
        }
        return true;
    }
}


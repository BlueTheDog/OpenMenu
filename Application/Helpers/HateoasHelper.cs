using Domain.Helpers;
using Domain.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace Application.Helpers;
public class HateoasHelper : IHateoasHelper
{
    private readonly IUrlHelper _urlHelper;

    public HateoasHelper(IUrlHelper urlHelper)
    {
        _urlHelper = urlHelper;
    }
    public string? CreateResourceUri(
        string resource,
        ResourceParameters resourceParameters,
        ResourceUriTypes types)
    {
        return types switch
        {
            ResourceUriTypes.PreviousPage => _urlHelper.Link($"Get{resource}",
                    new
                    {
                        fields = resourceParameters.Fields,
                        orderBy = resourceParameters.OrderBy,
                        pageNumber = resourceParameters.PageNumber - 1,
                        pageSize = resourceParameters.PageSize,
                        searchQuery = resourceParameters.SearchQuery
                    }),
            ResourceUriTypes.NextPage => _urlHelper.Link($"Get{resource}",
                    new
                    {
                        fields = resourceParameters.Fields,
                        orderBy = resourceParameters.OrderBy,
                        pageNumber = resourceParameters.PageNumber + 1,
                        pageSize = resourceParameters.PageSize,
                        searchQuery = resourceParameters.SearchQuery
                    }),
            ResourceUriTypes.Current => _urlHelper.Link($"Get{resource}",
                    new
                    {
                        fields = resourceParameters.Fields,
                        orderBy = resourceParameters.OrderBy,
                        pageNumber = resourceParameters.PageNumber,
                        pageSize = resourceParameters.PageSize,
                        searchQuery = resourceParameters.SearchQuery
                    }),
            _ => _urlHelper.Link($"Get{resource}",
                    new
                    {
                        fields = resourceParameters.Fields,
                        orderBy = resourceParameters.OrderBy,
                        pageNumber = resourceParameters.PageNumber,
                        pageSize = resourceParameters.PageSize,
                        searchQuery = resourceParameters.SearchQuery
                    }),
        };
    }
    public IEnumerable<LinkDto> CreateLinksForResources(
        string resource,
        ResourceParameters resourceResourceParameters,
        bool hasNext,
        bool hasPrevious)
    {
        var singleResource = resource.Replace("Entity", "").Trim();
        var resources = $"{singleResource}s".Trim();
        var links = new List<LinkDto>
            {
                // self
                new(CreateResourceUri(resources, resourceResourceParameters, ResourceUriTypes.Current),
                    "self",
                    "GET")
            };
        if (hasNext)
        {
            links.Add(
            new(CreateResourceUri(resources, resourceResourceParameters, ResourceUriTypes.NextPage),
                "nextPage",
                "GET"));
        }
        if (hasPrevious)
        {
            links.Add(
            new(CreateResourceUri(resources, resourceResourceParameters, ResourceUriTypes.PreviousPage),
                "previousPage",
                "GET"));
        }

        links.Add(
              new(_urlHelper.Link($"Create{singleResource}", null),
              $"create_{singleResource.ToLower()}",
              "POST"));
        return links;
    }

    public IEnumerable<LinkDto> CreateLinkForResource(
        string resource,
        int resourceId,
        string? fields)
    {
        resource = resource.Replace("Entity", "").Trim();
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            links.Add(
              new(_urlHelper.Link($"Get{resource}", new { resourceId, fields }),
              "self",
              "GET"));
        }
        else
        {
            links.Add(
              new(_urlHelper.Link($"Get{resource}", new { resourceId, fields }),
              "self",
              "GET"));
        }

        links.Add(
              new(_urlHelper.Link($"Put{resource}", new { resourceId }),
              $"update_{resource.ToLower()}",
              "PUT"));
        links.Add(
              new(_urlHelper.Link($"Patch{resource}", new { resourceId }),
              $"patch_{resource.ToLower()}",
              "PATCH"));

        links.Add(
              new(_urlHelper.Link($"Delete{resource}", new { resourceId }),
              $"delete_{resource.ToLower()}",
              "DELETE"));
        return links;
    }
}

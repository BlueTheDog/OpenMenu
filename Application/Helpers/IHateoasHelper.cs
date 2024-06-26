﻿using Domain.Helpers;
using Domain.ResourceParameters;

namespace Application.Helpers;
public interface IHateoasHelper
{
    IEnumerable<LinkDto> CreateLinkForResource(string resource, int resourceId, string? fields);
    IEnumerable<LinkDto> CreateLinksForResources(string resource, ResourceParameters resourceParameters, bool hasNext, bool hasPrevious);
    string? CreateResourceUri(string resource, ResourceParameters resourceParameters, ResourceUriTypes types);
}
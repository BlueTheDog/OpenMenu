using Common.Helpers;
using Domain;

namespace Application.Services;
public static class PaginationHelper
{
    public static PaginationMetadataDto CreatePaginationMetadata<TEntity>(PagedList<TEntity> entitiesToPage)
    {
        return new PaginationMetadataDto(
            entitiesToPage.TotalCount,
            entitiesToPage.PageSize,
            entitiesToPage.CurrentPage,
            entitiesToPage.TotalPages
            );
    }
}

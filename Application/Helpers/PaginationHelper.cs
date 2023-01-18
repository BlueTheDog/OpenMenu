using Common.Helpers;
using Domain.Helpers;

namespace Application.Helpers;
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

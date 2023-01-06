using Common.Helpers;
using Domain;
using Domain.ResourceParameters;

namespace Application.Services;

public interface IEntityRepository<TEntity, TResourceParameters>
    where TEntity : class
    where TResourceParameters : ResourceParameters
{
    public Task<IEnumerable<TEntity>> GetEntitiesAsync(IEnumerable<int> entityIds);
    public Task<PagedList<TEntity>> GetEntitiesAsync(TResourceParameters resourceParameters);
    public Task<(Object?, PaginationMetadataDto)> GetEntitiesAsync(
           TResourceParameters resourceParameters,
           string mediaType);
    public Task<Object> GetEntityAsync(int entityId);
    public Task<Object> GetEntityAsync(
            TResourceParameters resourceParameters,
            int entityId,
            string mediaType);
    public void AddEntity(TEntity entity);
    public void UpdateEntity(TEntity entity);
    public void DeleteEntity(TEntity entity);
    public Task<bool> EntityExistsAsync(int entityId);
    public Task<bool> SaveChangesAsync();
}
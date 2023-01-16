using Domain.ResourceParameters;
using Common.Helpers;

namespace Application.Services
{
    // The IEntityRepository interface defines a generic service that can work with various types of entities,
    // resource parameters, DTOs, create/update DTOs and persist entities.
    public interface IEntityRepository<TEntity, TResourceParameters>
        where TEntity : class
        where TResourceParameters : ResourceParameters
    {
        // The GetEntityAsync method retrieves a single entity from the repository based on the provided id.
        Task<TEntity> GetEntityAsync(int entityId);

        // The GetEntitiesAsync method retrieves a paged list of entities from the repository
        // based on the provided resource parameters.
        Task<PagedList<TEntity>> GetEntitiesAsync(TResourceParameters resourceParameters);

        // The GetEntitiesAsync method retrieves a collection of entities from the repository based on the provided ids.
        Task<IEnumerable<TEntity>> GetEntitiesAsync(IEnumerable<int> entityIds);

        // The AddEntity method adds a new entity to the repository.
        void AddEntity(TEntity entity);

        // The DeleteEntity method deletes an existing entity from the repository.
        void DeleteEntity(TEntity entity);

        // The EntityExistsAsync method checks if an entity with the provided id exists in the repository.
        Task<bool> EntityExistsAsync(int entityId);

        // The SaveChangesAsync method saves any changes made to the repository.
        Task<bool> SaveChangesAsync();
    }
}

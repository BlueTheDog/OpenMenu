using Domain.Helpers;
using Domain.ResourceParameters;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Services.Entity
{
    public interface IEntityService<TEntity, TResourceParameters, TDto, TCreationDto, TUpdateDto>
        where TEntity : class
        where TResourceParameters : ResourceParameters
        where TCreationDto : class
        where TUpdateDto : class
        where TDto : class
    {
        Task<(object?, PaginationMetadataDto)> GetEntitiesAsync(
            TResourceParameters resourceParameters,
            string mediaType);

        Task<IEnumerable<TEntity>> GetEntitiesAsync(IEnumerable<int> entityIds);
        Task<object> GetEntityAsync(
            TResourceParameters resourceParameters,
            int entityId,
            string mediaType);
        public Task<IEnumerable<TDto>> GetEntityCollection(IEnumerable<int> entityIds);
        public Task<(IEnumerable<TDto>, string)> CreateEntityCollection(
            IEnumerable<TCreationDto> entityCollection);
        void AddEntity(TEntity entity);

        Task<TDto> AddEntity(TCreationDto creationDto);
        public Task UpdateEntity(
            TUpdateDto updateDto,
            int resourceId);
        public Task PartiallyUpdateEntity(
           JsonPatchDocument<TUpdateDto> entityToPatch,
           int resourceId);
        public Task DeleteEntity(int resourceId);
        Task<bool> EntityExists(int entityId);
        Task<bool> DeleteEntityAsync(TEntity entity);
        Task<bool> SaveChangesAsync();
    }
}

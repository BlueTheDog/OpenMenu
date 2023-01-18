using Common.Helpers;
using Domain.ResourceParameters;
using Infrastructure.DbContexts;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Application.Services.Entity;
using Application.Interfaces.Property;
using Domain.Entities.Client.Dto;
using Domain.Entities.Client;

namespace Infrastructure.Repository;
internal class ClientRepository : IEntityRepository<ClientEntity, ClientResourceParameters>
{
    private readonly IOpenMenuContext _dbContext;
    private readonly IPropertyMappingService _propertyMappingService;

    public ClientRepository(IOpenMenuContext dbContext, IPropertyMappingService propertyMappingService)
    {
        _dbContext = dbContext ??
            throw new ArgumentNullException(nameof(dbContext));
        _propertyMappingService = propertyMappingService ??
            throw new ArgumentNullException(nameof(propertyMappingService));
    }

    public async Task<IEnumerable<ClientEntity>> GetEntitiesAsync(IEnumerable<int> ClientIds)
    {
        if (ClientIds == null)
        {
            throw new ArgumentNullException(nameof(ClientIds));
        }
        var collection = _dbContext.Clients as IQueryable<ClientEntity>;
        collection = collection
            .Where(a => ClientIds.Contains(a.Id))
            .OrderBy(a => a.Name);
        return await collection
            .ToListAsync();
    }
    public async Task<ClientEntity> GetClientAsync(int ClientId)
    {
#pragma warning disable CS8603
        return await _dbContext.Clients.FindAsync(ClientId);
#pragma warning restore CS8603
    }

    public async Task<PagedList<ClientEntity>> GetEntitiesAsync(ClientResourceParameters ClientResourceParameters)
    {
        if (ClientResourceParameters == null)
        {
            throw new ArgumentNullException(nameof(ClientResourceParameters));
        }
        var collection = _dbContext.Clients as IQueryable<ClientEntity>;

        if (!string.IsNullOrWhiteSpace(ClientResourceParameters.SearchQuery))
        {
            ClientResourceParameters.SearchQuery = ClientResourceParameters.SearchQuery.Trim();
            collection = collection.Where(x => x.Name.ToLower().Contains(ClientResourceParameters.SearchQuery.ToLower()));
        }

        // get property mapping dictionary
        var ClientsPropertyMappingDictionary = _propertyMappingService
            .GetPropertyMapping<ClientDto, ClientEntity>();
        // orderBy
        collection = collection.ApplySort(ClientResourceParameters.OrderBy,
            ClientsPropertyMappingDictionary);

        return await PagedList<ClientEntity>.CreateAsync(collection,
            ClientResourceParameters.PageNumber,
            ClientResourceParameters.PageSize);
    }

    public async Task<ClientEntity> GetEntityAsync(int ClientId)
    {

#pragma warning disable CS8603
        return await _dbContext.Clients.FindAsync(ClientId);
#pragma warning restore CS8603
    }

    public void AddEntity(ClientEntity Client)
    {
        if (Client == null)
        {
            throw new ArgumentNullException(nameof(Client));
        }
        _dbContext.Clients.Add(Client);
    }

    public void DeleteEntity(ClientEntity Client)
    {
        if (Client == null)
        {
            throw new ArgumentNullException(nameof(Client));
        }
        _dbContext.Clients.Remove(Client);
    }

    public async Task<bool> EntityExistsAsync(int ClientId)
    {
        return await _dbContext.Clients.AnyAsync(c => c.Id == ClientId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() >= 0;
    }
}
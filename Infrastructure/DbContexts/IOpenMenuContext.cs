using Domain.Entities.Client;
using Domain.Entities.ClientType;
using Domain.Entities.Location;
using Domain.Entities.MenuItem;
using Domain.Entities.MenuItemType;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts;
public interface IOpenMenuContext
{
    DbSet<ClientEntity> Clients { get; set; }
    DbSet<ClientTypeEntity> ClientTypes { get; set; }
    DbSet<LocationEntity> Locations { get; set; }
    DbSet<MenuItemEntity> MenuItems { get; set; }
    DbSet<MenuItemTypeEntity> MenuItemTypes { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
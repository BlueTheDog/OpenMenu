using Domain.Client;
using Domain.ClientType;
using Domain.Location;
using Domain.MenuItem;
using Domain.MenuItemType;
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
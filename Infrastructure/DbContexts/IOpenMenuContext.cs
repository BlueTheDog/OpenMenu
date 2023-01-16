using Domain.ClientType;
using Domain.Location;
using Domain.MenuItem;
using Domain.MenuItemType;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts;
public interface IOpenMenuContext
{
    DbSet<LocationEntity> Locations { get; set; }
    DbSet<ClientTypeEntity> ClientTypes { get; set; }
    DbSet<MenuItemTypeEntity> MenuItemTypes { get; set; }
    DbSet<MenuItemEntity> MenuItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
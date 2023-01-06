using Domain.Location;
using Domain.MenuType;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts;
public interface IOpenMenuContext
{
    DbSet<LocationEntity> Locations { get; set; }
    DbSet<MenuTypeEntity> MenuTypes { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
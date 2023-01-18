using Domain;
using Domain.Abstracts;
using Domain.Entities.Client;
using Domain.Entities.ClientType;
using Domain.Entities.Location;
using Domain.Entities.MenuItem;
using Domain.Entities.MenuItemType;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Infrastructure.DbContexts;

public class OpenMenuContext : DbContext, IOpenMenuContext
{
    public OpenMenuContext(DbContextOptions<OpenMenuContext> options)
        : base(options)
    {

    }
    public DbSet<ClientEntity> Clients { get; set; } = null!;
    public DbSet<ClientTypeEntity> ClientTypes { get; set; } = null!;
    public DbSet<LocationEntity> Locations { get; set; } = null!;
    public DbSet<MenuItemTypeEntity> MenuItemTypes { get; set; } = null!;
    public DbSet<MenuItemEntity> MenuItems { get; set; } = null!;
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var modificationHistory in this.ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity)
            //&& (e.State == EntityState.Added || e.State == EntityState.Modified))
            .Select(e => e.Entity as BaseEntity))
        {
            if (modificationHistory != null)
            {
                modificationHistory.DateModified = DateTime.Now;
                if (modificationHistory.DateCreated == DateTime.MinValue)
                {
                    modificationHistory.DateCreated = DateTime.Now;
                }
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItemTypeEntity>()
            .HasMany(item => item.MenuItems)
            .WithMany(item => item.MenuItemTypes)
            .UsingEntity(j => j.HasData(
                new { MenuItemTypesId = 1, MenuItemsId = 1 },
                new { MenuItemTypesId = 2, MenuItemsId = 2 },
                new { MenuItemTypesId = 1, MenuItemsId = 3 },
                new { MenuItemTypesId = 4, MenuItemsId = 4 },
                new { MenuItemTypesId = 4, MenuItemsId = 5 }
                ));
        modelBuilder.Entity<MenuItemTypeEntity>()
            .HasData(
            new MenuItemTypeEntity
            {
                Id = 1,
                Name = "Cafea",
                LocationId = 1,
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new MenuItemTypeEntity
            {
                Id = 2,
                Name = "Deserturi",
                LocationId = 1,
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new MenuItemTypeEntity
            {
                Id = 3,
                Name = "Cafea boabe",
                LocationId = 1,
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new MenuItemTypeEntity
            {
                Id = 4,
                Name = "Racoritoare",
                LocationId = 1,
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            });

        modelBuilder.Entity<MenuItemEntity>().HasData(
            new MenuItemEntity
            {
                Id = 1,
                Name = "Espresso",
                Description = "1 shot espresso",
                Price = Convert.ToDecimal(6),
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new MenuItemEntity
            {
                Id = 2,
                Name = "Flat White",
                Description = "2 shots and milk",
                Price = Convert.ToDecimal(9),
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new MenuItemEntity
            {
                Id = 3,
                Name = "Latte",
                Description = "one shot more milk",
                Price = Convert.ToDecimal(12),
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new MenuItemEntity
            {
                Id = 4,
                Name = "Coca cola",
                Description = "The CocaCola company",
                Price = Convert.ToDecimal(6),
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new MenuItemEntity
            {
                Id = 5,
                Name = "Pepsi",
                Description = "not the right one",
                Price = Convert.ToDecimal(5),
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            });
        modelBuilder.Entity<ClientTypeEntity>().HasData( // seed some date to work with
            new ClientTypeEntity("Restaurant")
            {
                Id = 1,
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new ClientTypeEntity("Cafenea")
            {
                Id = 2,
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            },
            new ClientTypeEntity("Atelier auto")
            {
                Id = 3,
                DateCreated = new DateTime(2022, 4, 20),
                DateModified = DateTime.UtcNow
            }
            );
        modelBuilder.Entity<ClientEntity>().HasData(
            new ClientEntity()
            {
                Id = 1,
                Name = "5ToGo",
                Description = "Cafenelele 5 to go",
                Email = "contact@5togo.ro",
                Phone = "0761.000.000",
                DateModified = DateTime.UtcNow,
                DateCreated = new DateTime(2022, 4, 20)
            },
            new ClientEntity()
            {
                Id = 2,
                Name = "Starbucks",
                Description = "Cafenelele Starbucks",
                Email = "contact@starbucks.ro",
                Phone = "0761.111.111",
                DateModified = DateTime.UtcNow,
                DateCreated = new DateTime(2022, 4, 20)
            }, new ClientEntity()
            {
                Id = 3,
                Name = "MartyRestaurants",
                Description = "Restaurantele Marty",
                Email = "contact@martycluj.ro",
                Phone = "0761.222.222",
                DateModified = DateTime.UtcNow,
                DateCreated = new DateTime(2022, 4, 20)
            },
            new ClientEntity()
            {
                Id = 4,
                Name = "Dacia Service",
                Description = "Dacia Service description",
                Email = "contact@dacia.ro",
                Phone = "0761.333.333",
                DateModified = DateTime.UtcNow,
                DateCreated = new DateTime(2022, 4, 20)
            },
            new ClientEntity()
            {
                Id = 5,
                Name = "Boulevard",
                Description = "Mancare cu suflet",
                Email = "contact@boulevard.ro",
                Phone = "0761.444.444",
                DateModified = DateTime.UtcNow,
                DateCreated = new DateTime(2022, 4, 20)
            },
            new ClientEntity()
            {
                Id = 6,
                Name = "TiriacSerices",
                Description = "Avioanele, ca am mai multe.",
                Email = "contact@tiriac.ro",
                Phone = "0761.666.666",
                DateModified = DateTime.UtcNow,
                DateCreated = new DateTime(2022, 4, 20)
            }
            );
        modelBuilder.Entity<LocationEntity>().HasData(
            new LocationEntity("5ToGo XL")
            {
                Id = 1,
                ClientId = 1,
                ClientTypeId = 2,
                Description = "La 5 to go folosim o cafea cu o aromă intensă, corpolentă , cremoasă ce se constituie într-un blend unic, creat special pentru lanţul de cafenele 5 to go.",
                DateCreated = new DateTime(2022, 01, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Starbucks")
            {
                Id = 2,
                ClientId = 2,
                ClientTypeId = 2,
                Description = "Starbucks Corporation is an American multinational chain of coffeehouses and roastery reserves headquartered in Seattle, Washington. ",
                DateCreated = new DateTime(2022, 02, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("5ToGo Hunedoara")
            {
                Id = 3,
                ClientId = 1,
                ClientTypeId = 2,
                Description = "Colaborăm şi susţinem ferme care respectă fructul de cafea, dar mai mult, respectă oamenii implicaţi în povestea cafelei.",
                DateCreated = new DateTime(2022, 03, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Marty Restaurants")
            {
                Id = 5,
                ClientId = 3,
                ClientTypeId = 1,
                Description = "Cele șase locații Marty Restaurants sunt asemănătoare în esență, dar fiecare dintre ele propune o atmosferă unică. ",
                DateCreated = new DateTime(2022, 05, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Boulevard")
            {
                Id = 6,
                ClientId = 5,
                ClientTypeId = 1,
                Description = "Mereu delicios",
                DateCreated = new DateTime(2022, 06, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Dacia Service")
            {
                Id = 9,
                ClientId = 4,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 09, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Renault Service")
            {
                Id = 10,
                ClientId = 4,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 10, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Mercedes Service")
            {
                Id = 11,
                ClientId = 6,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 11, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("BMW Service")
            {
                Id = 12,
                ClientId = 6,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 12, 01),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Tesla Service")
            {
                Id = 13,
                ClientId = 6,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 12, 02),
                DateModified = DateTime.UtcNow
            }
        );
    }
}

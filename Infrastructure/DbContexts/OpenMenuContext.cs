using Domain;
using Domain.Abstracts;
using Domain.ClientType;
using Domain.Location;
using Domain.MenuItem;
using Domain.MenuItemType;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts;

public class OpenMenuContext : DbContext, IOpenMenuContext
{
    public OpenMenuContext(DbContextOptions<OpenMenuContext> options)
        : base(options)
    {

    }
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
                new { MenuItemsId = 1, MenuItemTypesId = 1 },
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
        modelBuilder.Entity<LocationEntity>().HasData(
            new LocationEntity("5ToGo XL")
            {
                Id = 1,
                ClientTypeId = 2,
                Description = "La 5 to go folosim o cafea cu o aromă intensă, corpolentă , cremoasă ce se constituie într-un blend unic, creat special pentru lanţul de cafenele 5 to go.",
                DateCreated = new DateTime(2022, 01, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Starbucks")
            {
                Id = 2,
                ClientTypeId = 2,
                Description = "Starbucks Corporation is an American multinational chain of coffeehouses and roastery reserves headquartered in Seattle, Washington. ",
                DateCreated = new DateTime(2022, 02, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("NarCoffee")
            {
                Id = 3,
                ClientTypeId = 2,
                Description = "Colaborăm şi susţinem ferme care respectă fructul de cafea, dar mai mult, respectă oamenii implicaţi în povestea cafelei.",
                DateCreated = new DateTime(2022, 03, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Cafetarie")
            {
                Id = 4,
                ClientTypeId = 2,
                Description = "O cafea arabica, aromata, tare. Cam tot ce aștepți de la o cafea. Plus ca ii ajuți și pe micii fermieri cumpărând-o. ",
                DateCreated = new DateTime(2022, 04, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Marty Restaurants")
            {
                Id = 5,
                ClientTypeId = 1,
                Description = "Cele șase locații Marty Restaurants sunt asemănătoare în esență, dar fiecare dintre ele propune o atmosferă unică. ",
                DateCreated = new DateTime(2022, 05, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Boulevard")
            {
                Id = 6,
                ClientTypeId = 1,
                Description = "Mereu delicios",
                DateCreated = new DateTime(2022, 06, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Curtea Veche")
            {
                Id = 7,
                ClientTypeId = 1,
                Description = "Mâncarea e delicioasa, ca la mama acasă, proaspătă și servita fără întârziere.",
                DateCreated = new DateTime(2022, 07, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Casa Romaneasca")
            {
                Id = 8,
                ClientTypeId = 1,
                DateCreated = new DateTime(2022, 08, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Dacia Service")
            {
                Id = 9,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 09, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Renault Service")
            {
                Id = 10,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 10, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Mercedes Service")
            {
                Id = 11,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 11, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("BMW Service")
            {
                Id = 12,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 12, 01),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Tesla Service")
            {
                Id = 13,
                ClientTypeId = 3,
                DateCreated = new DateTime(2022, 12, 02),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Cafeneaua de la colt")
            {
                Id = 14,
                ClientTypeId = 2,
                DateCreated = new DateTime(2022, 04, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Happy Beans")
            {
                Id = 15,
                ClientTypeId = 2,
                DateCreated = new DateTime(2022, 04, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("All u can eat")
            {
                Id = 16,
                ClientTypeId = 1,
                DateCreated = new DateTime(2022, 04, 20),
                DateModified = DateTime.UtcNow
            },
            new LocationEntity("Moldovan")
            {
                Id = 17,
                ClientTypeId = 1,
                DateCreated = new DateTime(2022, 04, 20),
                DateModified = DateTime.UtcNow
            }
        );
    }
}

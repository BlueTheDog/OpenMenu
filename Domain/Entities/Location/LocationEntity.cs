using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Abstracts;
using Domain.Entities.MenuItemType;
using Domain.Entities.ClientType;
using Domain.Entities.Client;

namespace Domain.Entities.Location;

public class LocationEntity : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(1500)]
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    [ForeignKey("ClientId")]
    public ClientEntity? Client { get; set; }
    public int ClientId { get; set; }

    [ForeignKey("ClientTypeId")]
    public ClientTypeEntity? ClientType { get; set; }
    public int ClientTypeId { get; set; }

    public ICollection<MenuItemTypeEntity> MenuItemTypes { get; set; }
        = new List<MenuItemTypeEntity>();
    public LocationEntity()
    {

    }
    public LocationEntity(string name)
    {
        Name = name;
    }
}

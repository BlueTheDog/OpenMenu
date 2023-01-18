using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Abstracts;
using Domain.Location;
using Domain.MenuItem;

namespace Domain.MenuItemType;

public class MenuItemTypeEntity : BaseEntity
{

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [ForeignKey("LocationId")]
    public LocationEntity? Location { get; set; }
    public int LocationId { get; set; }

    public ICollection<MenuItemEntity> MenuItems { get; set; } = new List<MenuItemEntity>();

    public MenuItemTypeEntity()
    {

    }
}

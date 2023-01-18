using Domain.Abstracts;
using Domain.Entities.MenuItemType;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.MenuItem;
public class MenuItemEntity : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(1500)]
    public string? Description { get; set; }
    [Required]
    [MaxLength(10)]
    public decimal Price { get; set; }
    public ICollection<MenuItemTypeEntity> MenuItemTypes { get; set; } = new List<MenuItemTypeEntity>();
    public MenuItemEntity()
    {

    }
}

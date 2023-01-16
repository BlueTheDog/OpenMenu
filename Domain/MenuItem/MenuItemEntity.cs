using Domain.Abstracts;
using Domain.MenuItemType;
using System.ComponentModel.DataAnnotations;

namespace Domain.MenuItem;
public class MenuItemEntity : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(1500)]
    public string? Description { get; set; }

    public ICollection<MenuItemTypeEntity> MenuItemTypes { get; set; } = new List<MenuItemTypeEntity>();
    //public IList<MenuItemMenuItemType> MenuItemTypeMenuItem { get; set; } = null!;
    [Required]
    [MaxLength(10)]
    public decimal Price { get; set; }
    //public MenuItemEntity(string name, string? description, decimal productPrice)
    //{
    //    Name = name;
    //    Description = description;
    //    Price = productPrice;
    //}
    public MenuItemEntity()
    {

    }
}

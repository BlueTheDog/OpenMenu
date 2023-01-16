using Domain.Abstracts;
using Domain.MenuItem;
using Domain.MenuItemType;

namespace Domain;
public class MenuItemMenuItemType : BaseEntity
{
    public int MenuItemTypeId { get; set; }
    public MenuItemTypeEntity MenuItemType { get; set; } = null!;

    public int MenuItemId { get; set; }
    public MenuItemEntity MenuItem { get; set; } = null!;
}

using Domain.Abstracts;
using Domain.Entities.MenuItem;
using Domain.Entities.MenuItemType;

namespace Domain.Helpers;
public class MenuItemMenuItemType : BaseEntity
{
    public int MenuItemTypeId { get; set; }
    public MenuItemTypeEntity MenuItemType { get; set; } = null!;

    public int MenuItemId { get; set; }
    public MenuItemEntity MenuItem { get; set; } = null!;
}

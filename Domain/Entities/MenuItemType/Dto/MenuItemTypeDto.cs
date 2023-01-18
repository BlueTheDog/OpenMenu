namespace Domain.Entities.MenuItemType.Dto;

public class MenuItemTypeDto : MenuItemTypeBaseDto
{
    public int Id { get; set; }
    public string LastModified { get; set; } = null!;
    public string DateModified { get; set; } = null!;
}

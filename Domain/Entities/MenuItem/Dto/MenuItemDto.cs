namespace Domain.Entities.MenuItem.Dto;
public class MenuItemDto : MenuItemBaseDto
{
    public int Id { get; set; }
    public string LastModified { get; set; } = null!;
    public string DateModified { get; set; } = null!;
}

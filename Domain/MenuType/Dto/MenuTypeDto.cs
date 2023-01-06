namespace Domain.MenuType.Dto;

public class MenuTypeDto : MenuTypeBaseDto
{
    public int Id { get; set; }
    public string LastModified { get; set; } = null!;
    public string DateModified { get; set; } = null!;
}

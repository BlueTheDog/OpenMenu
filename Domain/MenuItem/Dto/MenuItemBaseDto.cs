using System.ComponentModel.DataAnnotations;

namespace Domain.MenuItem.Dto;
public class MenuItemBaseDto
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using Domain.Helpers;

namespace Domain.Entities.MenuItem.Dto;
public class MenuItemBaseDto
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}

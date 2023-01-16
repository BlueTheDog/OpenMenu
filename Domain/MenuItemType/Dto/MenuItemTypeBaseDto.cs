using System.ComponentModel.DataAnnotations;

namespace Domain.MenuItemType.Dto;

public abstract class MenuItemTypeBaseDto
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}

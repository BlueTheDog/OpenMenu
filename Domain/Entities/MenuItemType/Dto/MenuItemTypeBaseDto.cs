using System.ComponentModel.DataAnnotations;
using Domain.Helpers;

namespace Domain.Entities.MenuItemType.Dto;

public abstract class MenuItemTypeBaseDto
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}

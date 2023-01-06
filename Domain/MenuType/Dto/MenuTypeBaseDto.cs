using System.ComponentModel.DataAnnotations;

namespace Domain.MenuType.Dto;

public abstract class MenuTypeBaseDto
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}

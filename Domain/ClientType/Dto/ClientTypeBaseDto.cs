using System.ComponentModel.DataAnnotations;

namespace Domain.ClientType.Dto;

public abstract class ClientTypeBaseDto
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}

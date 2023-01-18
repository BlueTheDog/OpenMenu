using System.ComponentModel.DataAnnotations;
using Domain.Helpers;

namespace Domain.Entities.ClientType.Dto;

public abstract class ClientTypeBaseDto
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}

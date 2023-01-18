using System.ComponentModel.DataAnnotations;
using Domain.Helpers;

namespace Domain.Entities.Client.Dto;

public class ClientBaseDto : IValidatableObject
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    public string Name { get; set; } = null!;
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? Phone { get; set; }
    public virtual string Description { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (Name == Description)
        {
            yield return new ValidationResult(
                Labels.descriptionMustBeDifferentFromName,
                new[] { "Client" });
        }
    }
}

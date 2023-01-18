using System.ComponentModel.DataAnnotations;
using Domain.Helpers;

namespace Domain.Entities.Location.Dto;

public class LocationBaseDto : IValidatableObject
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    public string Name { get; set; } = null!;
    public virtual string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = Labels.provideANameValue)]
    public int ClientTypeId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Name == Description)
        {
            yield return new ValidationResult(
                Labels.descriptionMustBeDifferentFromName,
                new[] { "Location" });
        }
    }
}

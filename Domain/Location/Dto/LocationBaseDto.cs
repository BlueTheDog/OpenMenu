using System.ComponentModel.DataAnnotations;

namespace Domain.Location.Dto;

public class LocationBaseDto : IValidatableObject
{
    [Required(ErrorMessage = Labels.provideANameValue)]
    public string Name { get; set; } = null!;
    public virtual string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = Labels.provideANameValue)]
    public int MenuTypeId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Name == Description)
        {
            yield return new ValidationResult(
                Labels.descriptionMustBeDifferentFromName,
                new[] { "Location" }); //??
        }
    }
}

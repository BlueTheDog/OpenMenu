using System.ComponentModel.DataAnnotations;

namespace Domain.Location.Dto;

public class LocationForUpdateDto : LocationBaseDto
{
    [Required(ErrorMessage = Labels.youShouldFillInADescription)]
    public override string Description
    {
        get => base.Description;
        set => base.Description = value;
    }
}

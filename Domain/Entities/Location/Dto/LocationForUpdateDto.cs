using System.ComponentModel.DataAnnotations;
using Domain.Helpers;

namespace Domain.Entities.Location.Dto;

public class LocationForUpdateDto : LocationBaseDto
{
    [Required(ErrorMessage = Labels.youShouldFillInADescription)]
    public override string Description
    {
        get => base.Description;
        set => base.Description = value;
    }
}

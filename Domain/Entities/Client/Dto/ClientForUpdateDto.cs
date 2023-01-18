using System.ComponentModel.DataAnnotations;
using Domain.Helpers;

namespace Domain.Entities.Client.Dto;

public class ClientForUpdateDto : ClientBaseDto
{
    [Required(ErrorMessage = Labels.youShouldFillInADescription)]
    public override string Description
    {
        get => base.Description;
        set => base.Description = value;
    }
}

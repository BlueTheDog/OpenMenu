using System.ComponentModel.DataAnnotations;

namespace Domain.Client.Dto;

public class ClientForUpdateDto : ClientBaseDto
{
    [Required(ErrorMessage = Labels.youShouldFillInADescription)]
    public override string Description
    {
        get => base.Description;
        set => base.Description = value;
    }
}

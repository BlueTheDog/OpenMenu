namespace Domain.Entities.ClientType.Dto;

public class ClientTypeDto : ClientTypeBaseDto
{
    public int Id { get; set; }
    public string LastModified { get; set; } = null!;
    public string DateModified { get; set; } = null!;
}

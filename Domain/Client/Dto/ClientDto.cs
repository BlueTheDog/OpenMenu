namespace Domain.Client.Dto
{
    public class ClientDto : ClientBaseDto
    {
        public int Id { get; set; }
        public string DateCreated { get; set; } = null!;
        public string LastModified { get; set; } = null!;
        public string DateModified { get; set; } = null!;
    }
}
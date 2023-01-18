using Domain.Abstracts;
using Domain.Location;
using System.ComponentModel.DataAnnotations;

namespace Domain.Client;
public class ClientEntity : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(1500)]
    public string? Description { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? Phone { get; set; }
    public ICollection<LocationEntity> Locations { get; set; } 
        = new List<LocationEntity>();

}

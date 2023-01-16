using System.ComponentModel.DataAnnotations;
using Domain.Abstracts;
using Domain.Location;

namespace Domain.ClientType;

public class ClientTypeEntity : BaseEntity
{

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ICollection<LocationEntity> Locations { get; set; } = new List<LocationEntity>();

    public ClientTypeEntity(string name)
    {
        Name = name;
    }
    public ClientTypeEntity()
    {

    }
}

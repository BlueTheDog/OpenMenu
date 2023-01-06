using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Abstracts;
using Domain.Location;

namespace Domain.MenuType;

public class MenuTypeEntity : BaseEntity
{

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ICollection<LocationEntity> Locations { get; set; } = new List<LocationEntity>();

    public MenuTypeEntity(string name)
    {
        Name = name;
    }
    public MenuTypeEntity()
    {

    }
}

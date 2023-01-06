using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Abstracts;
using Domain.MenuType;

namespace Domain.Location;

public class LocationEntity : BaseEntity
{

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(1500)]
    public string? Description { get; set; }

    [ForeignKey("MenuTypeId")]
    public MenuTypeEntity? MenuType { get; set; }
    public int MenuTypeId { get; set; }

    public LocationEntity(string name)
    {
        Name = name;
    }
    public LocationEntity()
    {

    }
}

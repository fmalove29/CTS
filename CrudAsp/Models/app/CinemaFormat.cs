using CrudAsp.Models.app.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAsp.Models.app;

public class CinemaFormat : BaseEntity
{
    public ScreenType ScreenType { get; set; }
    public string ScreenTypeName {get; set;}
    public string Description {get; set;}

    [Column(TypeName = "decimal(18,2)")] 
    public decimal Price { get; set; }

    public virtual ICollection<Hall> Halls { get; set; }
} 

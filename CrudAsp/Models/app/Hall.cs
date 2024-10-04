using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
namespace CrudAsp.Models.app;

public class Hall : BaseEntity
{
    [ForeignKey("User")]
    public Guid CinemaId {get; set;}

    public string HallName {get; set;}

    public int SeatCapacity {get; set;}

    public virtual ICollection<Show> Shows{get; set;}
}
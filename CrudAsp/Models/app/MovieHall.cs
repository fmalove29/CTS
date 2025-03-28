using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CrudAsp.Models.app;

public class MovieHall
{
    [ForeignKey("Movie")]
    public Guid MovieId { get; set; }
    public virtual Movie Movie { get; set; }

    [ForeignKey("Hall")]
    public Guid HallId { get; set; }
    public virtual Hall Hall { get; set; }
}

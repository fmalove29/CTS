using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAsp.Models.app;

public class CinemaShow
{
    [ForeignKey("Cinema")]
    public Guid cinemaId {get; set;}
    public virtual Cinema Cinema {get; set;}

    [ForeignKey("Show")]
    public Guid ShowId {get; set;}
    public virtual Show Show {get; set;}
}

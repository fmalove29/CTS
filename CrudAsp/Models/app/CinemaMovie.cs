using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAsp.Models.app;

public class CinemaMovie
{
    [ForeignKey("Cinema")]
    public Guid cinemaId {get; set;}

    public virtual Cinema Cinema { get; set;}

    [ForeignKey("Movie")]
    public Guid MovieId {get; set;}
    public virtual Movie Movie {get; set;}

}

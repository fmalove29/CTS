using System.ComponentModel.DataAnnotations.Schema;


namespace CrudAsp.Models.app;

public class Movie : BaseEntity
{
    public string Title {get; set;}

    public string Description {get; set;}
    public DateTime ReleaseDate {get; set;}
    public DateTime EndDate { get; set;}
    
    public virtual ICollection<Genre> Genres {get; set;}

    public virtual ICollection<MovieImage> MovieImages {get; set;}
}
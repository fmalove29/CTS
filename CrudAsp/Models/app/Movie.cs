using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; 


namespace CrudAsp.Models.app;

public class Movie : BaseEntity
{
    public string Title {get; set;}

    public string Description {get; set;}
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime ReleaseDate { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime EndDate { get; set; }

    
    
    public virtual ICollection<Genre> Genres {get; set;} = new List<Genre>();

    public virtual ICollection<MovieHall> MovieHalls {get; set;} = new List<MovieHall>();

    public virtual ICollection<MovieImage> MovieImages {get; set;} = new List<MovieImage>();
    public virtual ICollection<MovieGenre> MovieGenres {get; set;} = new List<MovieGenre>();
    public virtual ICollection<CinemaMovie> CinemaMovies {get; set;}
}
namespace CrudAsp.Models.app;

public class MovieGenre : BaseEntity
{
    public Guid MoviesId { get; set; } // Foreign key to Movie
    public Movie Movies { get; set; }  // Navigation property

    public Guid GenresId { get; set; } // Foreign key to Genre
    public Genre Genres { get; set; }  // Navigation property
}
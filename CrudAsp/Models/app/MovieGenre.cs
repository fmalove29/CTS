namespace CrudAsp.Models.app;

public class MovieGenre : BaseEntity
{
    public Guid MoviesId { get; set; } // Foreign key to Movie
    public virtual Movie Movies { get; set; }  // Navigation property

    public Guid GenresId { get; set; } // Foreign key to Genre
    public virtual Genre Genres { get; set; }  // Navigation property
}
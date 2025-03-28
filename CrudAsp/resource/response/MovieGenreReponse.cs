namespace CrudAsp.resource.response;

public class MovieGenreReponse : BaseResponse
{
    public Guid MoviesId { get; set; } // Foreign key to Movie
    public virtual MovieResponse Movie { get; set; }  // Navigation property

    public Guid GenresId { get; set; } // Foreign key to Genre
    public virtual GenreResponse Genres { get; set; } 
}

namespace CrudAsp.Models.app;

public class MovieGenre : BaseEntity
{
    public Guid MoviesId { get; set; } 
    public virtual Movie Movies { get; set; }  

    public Guid GenresId { get; set; } 
    public virtual Genre Genres { get; set; }  
}
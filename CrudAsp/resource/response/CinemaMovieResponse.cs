namespace CrudAsp.resource.response;

public class CinemaMovieResponse
{
    public Guid CinemaId {get; set;}
    public virtual CinemaResponse  Cinema{get; set;}

    public Guid MovieId {get; set;}
    public virtual MovieResponse Movie {get; set;}
}

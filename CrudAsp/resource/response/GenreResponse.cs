namespace CrudAsp.resource.response;

public class GenreResponse : BaseResponse
{
    public string GenreName { get; set;}
    public virtual ICollection<MovieResponse>? Movies {get; set;}
}
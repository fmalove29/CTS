namespace CrudAsp.resource.response;

public class MovieResponse : BaseResponse
{
    public string Title {get; set; }

    public string Description {get; set;}

    public DateTime ReleaseDate {get; set;}
    public DateTime EndDate { get; set;}



    public virtual ICollection<GenreResponse> Genres {get; set;}
    public virtual ICollection<MovieImageResponse> MovieImages {get; set;} = new List<MovieImageResponse>();
}
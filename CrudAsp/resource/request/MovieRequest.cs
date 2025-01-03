
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace CrudAsp.resource.request;

public class MovieRequest : BaseRequest
{
    public string Title {get; set; }

    public string Description {get; set;}

    public DateTime ReleaseDate {get; set;}
    public DateTime EndDate { get; set;}

    

    public virtual ICollection<GenreRequest>? Genres {get; set;} = new List<GenreRequest>();

    public List<IFormFile> MovieImages { get; set; }
}
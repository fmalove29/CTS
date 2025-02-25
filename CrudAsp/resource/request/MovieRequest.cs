
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace CrudAsp.resource.request;

public class MovieRequest : BaseRequest
{
    public string Title {get; set; }

    public string Description {get; set;}

    public DateTime ReleaseDate {get; set;}
    public DateTime EndDate { get; set;}

    
    public string MovieGenresJson { get; set; } 
    public virtual ICollection<GenreRequest>? Genres {get; set;} = new List<GenreRequest>();
    [FromForm(Name = "MovieGenres[]")]
    public virtual ICollection<MovieGenreRequest> MovieGenres {get; set;} = new List<MovieGenreRequest>();

    public List<IFormFile> MovieImages { get; set; }
}
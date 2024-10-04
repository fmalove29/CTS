using System.Collections;
using System.ComponentModel.DataAnnotations;
namespace CrudAsp.resource.request;

public class GenreRequest : BaseRequest
{
    [Required]
    public string GenreName { get; set;}

    public virtual ICollection<MovieRequest>? Movies {get; set;}
}
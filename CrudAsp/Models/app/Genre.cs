using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CrudAsp.Models.app;

public class Genre : BaseEntity
{
    [Required]
    public string GenreName { get; set;}
    
    public virtual ICollection<Movie>? Movies { get; set; }

}
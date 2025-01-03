using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace CrudAsp.resource.request;

public class CinemaRequest : BaseRequest
{
    public string CinemaName {get; set;}
    public string Location {get; set;}

    public virtual ICollection<HallRequest> Halls { get; set; }
}
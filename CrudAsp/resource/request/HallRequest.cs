using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
namespace CrudAsp.resource.request;

public class HallRequest : BaseRequest
{
    [ForeignKey("User")]
    public Guid CinemaId {get; set;}

    public string HallName {get; set;}

    public int SeatCapacity {get; set;}

    public virtual ICollection<ShowRequest> Shows{get; set;}
}
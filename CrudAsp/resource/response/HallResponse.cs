namespace CrudAsp.resource.response;

public class HallResponse : BaseResponse
{
    public Guid CinemaId {get; set;}

    public string HallName {get; set;}

    public int SeatCapacity {get; set;}

    public virtual ICollection<ShowResponse>? Shows{get; set;}
}

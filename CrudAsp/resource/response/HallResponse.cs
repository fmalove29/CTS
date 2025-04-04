namespace CrudAsp.resource.response;

public class HallResponse : BaseResponse
{
    public Guid CinemaId {get; set;}
    public virtual CinemaResponse Cinema {get; set;}

    public string HallName {get; set;}

    public int SeatCapacity {get; set;}
    public Guid CinemaFormatId {get; set;}
    public virtual CinemaFormatDTO CinemaFormat {get; set;}
    public string ScreenTypeName {get; set;}

    public decimal TicketPrice {get; set;}
    public virtual ICollection<ShowResponse>? Shows{get; set;}
}

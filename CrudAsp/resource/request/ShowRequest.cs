namespace CrudAsp.resource.request;

public class ShowRequest : BaseRequest
{

    public Guid MovieId {get; set;}
    public Guid HallId {get; set;}

    public DateTime ShowDate {get; set;}

    public decimal TicketPrice {get; set;}
}
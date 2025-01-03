namespace CrudAsp.resource.response;

public class ShowResponse : BaseResponse
{
    
    public Guid MovieId {get; set;}
    public Guid HallId {get; set;}

    public DateTime ShowDate {get; set;}

    public decimal TicketPrice {get; set;}
}

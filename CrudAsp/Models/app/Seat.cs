namespace CrudAsp.Models.app;

public class Seat : BaseEntity
{
    public Guid HallId {get; set;}
    public string SeatNumber {get; set;}
}
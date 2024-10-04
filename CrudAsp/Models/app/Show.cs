using System.ComponentModel.DataAnnotations.Schema;
namespace CrudAsp.Models.app;

public class Show : BaseEntity
{
    [ForeignKey("Movie")]
    public Guid MovieId {get; set;}
    [ForeignKey("Hall")]
    public Guid HallId {get; set;}

    public DateTime ShowDate {get; set;}

    public decimal TicketPrice {get; set;}
}
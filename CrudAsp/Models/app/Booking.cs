using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAsp.Models.app;

public class Booking : BaseEntity
{
    public DateTime BookingDate {get; set;}
    public decimal TotalAmount {get; set;}

    [ForeignKey("User")]
    public Guid UserId {get; set;}
}
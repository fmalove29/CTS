using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CrudAsp.Models.app.Enum;

namespace CrudAsp.Models.app;

public class Booking : BaseEntity
{
    public int TransactionNumber { get; set; } = new Random().Next(100000, 999999);
    public DateTime BookingDate {get; set;} = DateTime.UtcNow;
    public decimal TotalAmount {get; set;}
    public Guid UserId {get; set;}

    [ForeignKey("Movie")]
    public Guid MovieId {get; set;}
    public virtual Movie Movie {get; set;}
    [ForeignKey("Cinema")]
    public Guid CinemaId {get; set;}
    public virtual Cinema Cinema {get; set;}

    [ForeignKey("Hall")]
    public Guid? HallId {get; set;}
    public virtual Hall Hall {get; set;}
    public DateTime ShowTime {get; set;}
    public PaymentMethod PaymentMethod {get; set;}
    public BookingStatus BookingStatus {get; set;}

}
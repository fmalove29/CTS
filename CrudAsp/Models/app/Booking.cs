using System.ComponentModel.DataAnnotations.Schema;
using CrudAsp.Models.app.Enum;

namespace CrudAsp.Models.app;

public class Booking : BaseEntity
{
    public int TransactionNumber { get; set; } = new Random().Next(100000, 999999);
    public DateTime BookingDate {get; set;}
    public decimal TotalAmount {get; set;}
    public Guid UserId {get; set;}
    public Guid MovieId {get; set;}
    public Guid CinemaId {get; set;}

    public Guid? HallId {get; set;}
    public DateTime ShowTime {get; set;}
    public PaymentMethod PaymentMethod {get; set;}
    public BookingStatus BookingStatus {get; set;}

}
using BookingPayments.API.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

public sealed class Booking : Entity<int>
{
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }
    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }
    // Navigational property for .Include()
    //public Service? Service { get; set; }
    public int UserId { get; set; }
}
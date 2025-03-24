using BookingPayments.API.Entities.Enums;
using BookingPayments.API.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

public sealed class Booking : Entity<int>
{
    public DateTime BookingDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int Price { get; set; }
    public BookingStatus Status { get; set; }


    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [NotMapped]
    public IUser<int> User { get; set; }

    // todo: davat sem room?
}
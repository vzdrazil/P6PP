using System.ComponentModel.DataAnnotations.Schema;
using BookingPayments.API.Entities.Interfaces;
namespace BookingPayments.API.Entities;

public class Bookings : Entity<int>
{
    DateTime BookingDate { get; set; }
    DateTime CheckInDate { get; set; }
    DateTime CheckOutDate { get; set; }
    int Price { get; set; }
    
    [ForeignKey(nameof(BookingStatus))]
    int StatusId { get; set; }
    
    [ForeignKey(nameof(Services))]
    int ServiceId { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public IUser<int> User { get; set; }
    
    // todo: davat sem room?
    
}
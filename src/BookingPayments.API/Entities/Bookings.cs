using System.ComponentModel.DataAnnotations.Schema;
using BookingPayments.API.Entities.Interfaces;
namespace BookingPayments.API.Entities;

public class Bookings : Entity<int>
{
    public DateTime BookingDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int Price { get; set; }
    
    [ForeignKey(nameof(BookingStatus))]
    public int StatusId { get; set; }
    
    [ForeignKey(nameof(Services))]
    public int ServiceId { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    
    [NotMapped]
    public IUser<int> User { get; set; }
    
    // todo: davat sem room?
    
}
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;


[Table(nameof(Service))]
public sealed class Service : Entity<int>
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int TrainerId { get; set; }
    public int Price { get; set; }
    public string? ServiceName { get; set; }
    public bool IsCancelled { get; set; }
    [ForeignKey(nameof(Room))]
    public int RoomId { get; set; }
    // Navigational property for .Include()
    public Room? Room { get; set; }


}
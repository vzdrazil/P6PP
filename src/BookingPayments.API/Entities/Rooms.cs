using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

[Table(nameof(Rooms))]
public class Rooms : Entity<int>
{
    public string? RoomName { get; set; }
    public int RoomCapacity { get; set; }

    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }

    public RoomStatus? Status { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

[Table(nameof(Rooms))]
public class Rooms : Entity<int>
{
    public string? RoomName { get; set; }
}
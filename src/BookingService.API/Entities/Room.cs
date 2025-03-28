using BookingPayments.API.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

[Table(nameof(Room))]
public sealed class Room : Entity<int>
{
    public string? Name { get; set; }
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; }
}
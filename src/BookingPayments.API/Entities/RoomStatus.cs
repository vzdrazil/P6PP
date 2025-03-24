using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities
{
    [Table(nameof(RoomStatus))]
    public class RoomStatus : Entity<int>
    {
        public string? Status { get; set; }
    }
}

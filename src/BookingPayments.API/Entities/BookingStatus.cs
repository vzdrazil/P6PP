using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

[Table(nameof(BookingStatus))]
public class BookingStatus :  Entity<int>
{
   [Required]
   public string? BookingStatusName { get; set; } 
}
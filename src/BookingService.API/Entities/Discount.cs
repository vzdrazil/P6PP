using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

[Table(nameof(Discount))]
public sealed class Discount : Entity<int>
{
    [Required]
    public int Percentage { get; set; }

    [Required]
    public DateTime ValidFrom { get; set; }

    [Required]
    public DateTime ValidTo { get; set; }

    public bool IsValid { get; set; }
}
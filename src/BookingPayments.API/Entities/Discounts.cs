using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

[Table(nameof(Discounts))]
public class Discounts : Entity<int>
{ 
    [Required]
    public int DiscountPercentage { get; set; }
    
    [Required]
    public DateTime ValidFrom { get; set; }
    
    [Required]
    public DateTime ValidTo { get; set; }
    
    public bool IsValid { get; set; }
}
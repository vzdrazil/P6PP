using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;

[Table(nameof(Discounts))]
public class Discounts : Entity<int>
{ 
    [Required]
    int DiscountPercentage { get; set; }
    
    [Required]
    DateTime ValidFrom { get; set; }
    
    [Required]
    DateTime ValidTo { get; set; }
    
    bool IsValid { get; set; }
}
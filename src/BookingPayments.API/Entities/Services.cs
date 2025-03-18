using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;


[Table(nameof(Services))]
public class Services : Entity<int>
{
    public int TrainerId { get; set; }
    public int Price { get; set; }
    public string? ServiceName { get; set; }
    public bool IsCancelled { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPayments.API.Entities;


[Table(nameof(Services))]
public class Services : Entity<int>
{
    int TrainerId { get; set; }
    int Price { get; set; }
    string? ServiceName { get; set; }
    bool IsCancelled { get; set; }
}
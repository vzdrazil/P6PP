using BookingPayments.API.Entities.Interfaces;

namespace BookingPayments.API.Entities;

public class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
    
}

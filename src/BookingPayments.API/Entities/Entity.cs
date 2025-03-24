using BookingPayments.API.Entities.Interfaces;

namespace BookingPayments.API.Entities;

public abstract class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; } = default!;
}

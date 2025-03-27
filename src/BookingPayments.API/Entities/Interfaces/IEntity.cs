namespace BookingPayments.API.Entities.Interfaces;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}
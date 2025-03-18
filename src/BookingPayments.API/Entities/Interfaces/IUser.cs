namespace BookingPayments.API.Entities.Interfaces;

public interface IUser<Tkey> : IEntity<int>
{
    string? Email { get; set; }
    // todo: jak handlovat usera
    
}
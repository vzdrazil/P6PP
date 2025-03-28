using BookingPayments.API.Entities;

namespace BookingPayments.API.Application.Abstraction;

public interface IRoomAppService
{
    Task<IList<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task CreateAsync(Room room);
    Task<bool> UpdateAsync(Room room);
    Task<bool> DeleteAsync(int id);
}
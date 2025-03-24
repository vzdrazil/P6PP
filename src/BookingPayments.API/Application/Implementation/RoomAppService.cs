using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.Data;
using BookingPayments.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API.Application.Implementation;

public sealed class RoomAppService : IRoomAppService
{
    private readonly BookPayDbContext _context;

    public RoomAppService(BookPayDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Room>> GetAllAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _context.Rooms.SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task CreateAsync(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Room room)
    {
        var existingRoom = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == room.Id);

        if (existingRoom is null)
            return false;

        existingRoom.Name = room.Name;
        existingRoom.Capacity = room.Capacity;
        existingRoom.Status = room.Status;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var room = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == id);

        if (room is null)
            return false;

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return true;
    }
}

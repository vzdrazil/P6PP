using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.Entities;
using BookingPayments.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API.Application.Implementation
{
    public class RoomAppService : IRoomAppService
    {
        private readonly BookPayDbContext _context;

        public RoomAppService(BookPayDbContext context)
        {
            _context = context;
        }

        public IList<Rooms> Select()
        {
            return _context.Rooms.Include(r => r.Status).ToList();
        }

        public void Create(Rooms room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Rooms? GetById(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public bool Edit(Rooms room)
        {
            var existingRoom = _context.Rooms.FirstOrDefault(r => r.Id == room.Id);
            if (existingRoom == null)
                return false;

            existingRoom.RoomName = room.RoomName;
            existingRoom.RoomCapacity = room.RoomCapacity;
            existingRoom.StatusId = room.StatusId;

            _context.SaveChanges();
            return true;
        }
    }
}

using BookingPayments.API.Domain.Enums;
using BookingPayments.API.Domain.Models;

namespace BookingPayments.API.Infrastructure.Seeding;

internal static class RoomsInit
{
    public static IEnumerable<Room> GetRooms()
    {
        return
        [
            new Room
            {
                Id = 1,
                Name = "Room A",
                Capacity = 20,
                Status = RoomStatus.Available
            },
            new Room
            {
                Id = 2,
                Name = "Room B",
                Capacity = 15,
                Status = RoomStatus.Occuppied
            },
            new Room
            {
                Id = 3,
                Name = "Room C",
                Capacity = 10,
                Status = RoomStatus.Maintnance
            },
            new Room
            {
                Id = 4,
                Name = "Room D",
                Capacity = 25,
                Status = RoomStatus.Reserved
            }
        ];
    }
}
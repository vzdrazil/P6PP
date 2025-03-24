namespace BookingPayments.API.Entities.Seeding;

internal class RoomsInit
{
    public IList<Rooms> GetRooms()
    {
        return new List<Rooms>
        {
            new Rooms { Id = 1, RoomName = "Room A", RoomCapacity = 20, StatusId = 1},
            new Rooms { Id = 2, RoomName = "Room B", RoomCapacity = 15, StatusId = 2},
            new Rooms { Id = 3, RoomName = "Room C", RoomCapacity = 10, StatusId = 3},
            new Rooms { Id = 4, RoomName = "Room D", RoomCapacity = 25, StatusId = 4}
        };
    }
}
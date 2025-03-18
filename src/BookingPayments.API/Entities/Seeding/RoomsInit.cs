namespace BookingPayments.API.Entities.Seeding;

internal class RoomsInit
{
    public IList<Rooms> GetRooms()
    {
        return new List<Rooms>
        {
            new Rooms { Id = 1, RoomName = "Room A" },
            new Rooms { Id = 2, RoomName = "Room B" }
        };
    }
}
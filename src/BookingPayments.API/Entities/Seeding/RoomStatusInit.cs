namespace BookingPayments.API.Entities.Seeding;

internal class RoomStatusInit
{
    public IList<RoomStatus> GetRoomStatuses()
    {
        return new List<RoomStatus>
        {
            new RoomStatus { Id = 1, Status = "Available" },
            new RoomStatus { Id = 2, Status = "Occupied" },
            new RoomStatus { Id = 3, Status = "Reserved" },
            new RoomStatus { Id = 4, Status = "Maintenance" }
        };
    }
}

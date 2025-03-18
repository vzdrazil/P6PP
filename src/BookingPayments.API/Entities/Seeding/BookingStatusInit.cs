namespace BookingPayments.API.Entities.Seeding;

internal class BookingStatusInit
{
    public IList<BookingStatus> GetBookingStatuses()
    {
        return new List<BookingStatus>
        {
            new BookingStatus { Id = 1, BookingStatusName = "Confirmed" },
            new BookingStatus { Id = 2, BookingStatusName = "Pending" },
            new BookingStatus { Id = 3, BookingStatusName = "Cancelled" }
        };
    }
}
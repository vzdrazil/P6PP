using BookingPayments.API.Entities;
using BookingPayments.API.Entities.Enums;

namespace BookingPayments.API.Data.Seeding;

internal class BookingsInit
{
    public static IEnumerable<Booking> GetBookings()
    {
        return
        [
            new Booking
            {
                Id = 1,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Confirmed,
                ServiceId = 1,
                UserId = 1
            },
            new Booking
            {
                Id = 2,
                BookingDate = DateTime.Now,
                Status = BookingStatus.Pending,
                ServiceId = 2,
                UserId = 2
            }
        ];
    }
}
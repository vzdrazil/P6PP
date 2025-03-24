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
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(2),
                Price = 150,
                Status = BookingStatus.Confirmed,
                ServiceId = 1,
                UserId = 1
            },
            new Booking
            {
                Id = 2,
                BookingDate = DateTime.Now,
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(4),
                Price = 250,
                Status = BookingStatus.Pending,
                ServiceId = 2,
                UserId = 2
            }
        ];
    }
}
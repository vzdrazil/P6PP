namespace BookingPayments.API.Entities.Seeding;

internal class BookingsInit
{
        public IList<Bookings> GetBookings()
        {
            return new List<Bookings>
            {
                new Bookings { Id = 1, BookingDate = DateTime.Now, CheckInDate = DateTime.Now.AddDays(1), CheckOutDate = DateTime.Now.AddDays(2), Price = 150, StatusId = 1, ServiceId = 1, UserId = 1 },
                new Bookings { Id = 2, BookingDate = DateTime.Now, CheckInDate = DateTime.Now.AddDays(3), CheckOutDate = DateTime.Now.AddDays(4), Price = 250, StatusId = 2, ServiceId = 2, UserId = 2 }
            };
        }
}
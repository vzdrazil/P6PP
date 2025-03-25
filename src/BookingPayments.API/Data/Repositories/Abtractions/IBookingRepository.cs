using BookingPayments.API.Entities;

namespace BookingPayments.API.Data.Repositories.Abtractions;

public interface IBookingRepository
{
    Task<List<Booking>> GetBookingsAsync(int userId, string? condition = default);
    Task<Booking?> GetBookingAsync(int bookingId);
    Task<Booking> CreateBookingAsync(Booking booking);
    Task<Booking> UpdateBookingAsync(Booking booking);
    Task<bool> DeleteBookingAsync(int bookingId);

}

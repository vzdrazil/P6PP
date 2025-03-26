using BookingPayments.API.Entities;

namespace BookingPayments.API.Application.Abstraction;

public interface IBookingAppService
{
    Task<List<Booking>> GetBookingsAsync(int userId, string? condition = default);
    Task<Booking?> GetBookingAsync(int bookingId);
    Task<Booking> CreateBookingAsync(Booking booking);
    Task<Booking?> UpdateBookingAsync(Booking booking);
    Task<bool> DeleteBookingAsync(int bookingId);
}

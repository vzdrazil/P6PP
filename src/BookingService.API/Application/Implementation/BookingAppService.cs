using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.Data;
using BookingPayments.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API.Application.Implementation;

public class BookingAppService : IBookingAppService
{
    private readonly BookPayDbContext _context;
    public BookingAppService(BookPayDbContext context)
    {
        _context = context;
    }
    public async Task<Booking> CreateBookingAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> DeleteBookingAsync(int bookingId)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);
        if (booking == null) return false;
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<Booking?> GetBookingAsync(int bookingId)
    {
        return _context.Bookings
            .AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == bookingId);
    }

    public Task<List<Booking>> GetBookingsAsync(int userId)
    {
        return _context.Bookings.Where(b => b.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
    }

    // How to do updates?
    public async Task<Booking?> UpdateBookingAsync(Booking booking)
    {
        var dbBooking = await _context.Bookings.FindAsync(booking.Id);
        if (dbBooking is null) return null; // Return result
        dbBooking.Status = booking.Status;
        _context.Bookings.Update(dbBooking);
        await _context.SaveChangesAsync();
        return dbBooking;
    }
}

using System.Linq.Expressions;
using BookingPayments.API.Data.Repositories.Abtractions;
using BookingPayments.API.Entities;
using BookingPayments.API.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API.Data.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly BookPayDbContext _context;
    public BookingRepository(BookPayDbContext context)
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
            .AsNoTracking().Include(b => b.Service)
            .SingleOrDefaultAsync(b => b.Id == bookingId);
    }

    public Task<List<Booking>> GetBookingsAsync(int userId, string? condition)
    {
        Expression<Func<Booking, bool>> predicate = condition switch
        {
            "upcoming-bookings" => b => b.CheckInDate > DateTime.Now,
            "past-bookings" => b => b.CheckOutDate < DateTime.Now,
            "cancelled-bookings" => b => b.Status == BookingStatus.Cancelled,
            _ => b => b.UserId == userId
        };
        return _context.Bookings.Include(b => b.Service).Where(predicate)
                .AsNoTracking()
                .ToListAsync();
    }

    // add exitence check
    public async Task<Booking> UpdateBookingAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return booking;
    }
}

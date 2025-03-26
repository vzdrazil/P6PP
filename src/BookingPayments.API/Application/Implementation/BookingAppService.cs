using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.Data;
using BookingPayments.API.Entities;
using BookingPayments.API.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public Task<List<Booking>> GetBookingsAsync(int userId, string? condition)
    {
        // for testing purposes
        Expression<Func<Booking, bool>> predicate = condition switch
        {
            "upcoming-bookings" => b => b.CheckInDate > DateTime.Now,
            "past-bookings" => b => b.CheckOutDate < DateTime.Now,
            "cancelled-bookings" => b => b.Status == BookingStatus.Cancelled,
            _ => b => b.UserId == userId
        };
        return _context.Bookings.Where(predicate)
                .AsNoTracking()
                .ToListAsync();
    }

    // How to do updates?
    public async Task<Booking?> UpdateBookingAsync(Booking booking)
    {
        var dbBooking = await _context.Bookings.FindAsync(booking.Id);
        if (dbBooking is null) return null; // Return result
        dbBooking.CheckInDate = booking.CheckInDate;
        dbBooking.CheckOutDate = booking.CheckOutDate;
        dbBooking.Status = booking.Status;
        _context.Bookings.Update(dbBooking);
        await _context.SaveChangesAsync();
        return dbBooking;
    }
}

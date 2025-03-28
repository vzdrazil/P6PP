using BookingPayments.API.Common.Exceptions;
using BookingPayments.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BookingPayments.API.Features.Bookings.Models;

namespace BookingPayments.API.Features.Bookings.Queries;

public sealed class GetBookingDetailQuery : IRequest<BookingResponse>
{
    public GetBookingDetailQuery(int bookingId)
    {
        BookingId = bookingId;
    }

    public int BookingId { get; set; }
}

public sealed class GetBookingDetailQueryHandler : IRequestHandler<GetBookingDetailQuery, BookingResponse>
{
    private readonly DataContext _context;

    public GetBookingDetailQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<BookingResponse> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Booking not found");

        return booking.Map();
    }
}
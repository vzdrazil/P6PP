using BookingPayments.API.Common.Exceptions;
using BookingPayments.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API.Features.Bookings.Commands;

public sealed class DeleteBookingCommand : IRequest
{
    public DeleteBookingCommand(int bookingId)
    {
        BookingId = bookingId;
    }

    public int BookingId { get; set; }
}

public sealed class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly DataContext _context;

    public DeleteBookingCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .SingleOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken)
            ?? throw new NotFoundException("Booking not found");

        // todo: check ownership?
        // todo: payments etc.

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

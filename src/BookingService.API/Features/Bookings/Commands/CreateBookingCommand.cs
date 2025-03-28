using BookingPayments.API.Common.MediatR.Interfaces;
using BookingPayments.API.Domain.Enums;
using BookingPayments.API.Features.Bookings.Models;
using BookingPayments.API.Infrastructure;
using MediatR;

namespace BookingPayments.API.Features.Bookings.Commands;

public sealed class CreateBookingCommand : IRequest<BookingResponse>, IRequestWithUserId
{
    public CreateBookingCommand(CreateBookingRequest booking)
    {
        Booking = booking;
    }

    public CreateBookingRequest Booking { get; set; }
    public int UserId { get; set; }
}

public sealed class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
{
    private readonly DataContext _context;

    public CreateBookingCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = request.Booking.Map();
        booking.UserId = request.UserId;
        booking.Status = BookingStatus.Pending;

        // todo: check service existence
        // todo: check if user already registered on that service
        // todo: check service room capacity

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

        return booking.Map();
    }
}

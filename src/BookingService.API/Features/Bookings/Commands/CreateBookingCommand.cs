using BookingService.API.Common.MediatR.Interfaces;
using BookingService.API.Domain.Enums;
using BookingService.API.Features.Bookings.Models;
using BookingService.API.Infrastructure;
using MediatR;

namespace BookingService.API.Features.Bookings.Commands;

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

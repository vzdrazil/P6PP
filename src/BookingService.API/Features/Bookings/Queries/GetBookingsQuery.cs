using BookingPayments.API.Common.MediatR.Interfaces;
using BookingPayments.API.Domain.Enums;
using BookingPayments.API.Features.Bookings.Models;
using BookingPayments.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API.Features.Bookings.Queries;

public sealed class GetBookingsQuery : IRequest<IList<BookingResponse>>, IRequestWithUserId
{
    public GetBookingsQuery(BookingStatus? status)
    {
        Status = status;
    }

    public BookingStatus? Status { get; set; }
    public int UserId { get; set; }
}

public sealed class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, IList<BookingResponse>>
{
    private readonly DataContext _context;

    public GetBookingsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<IList<BookingResponse>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Bookings
            .AsNoTracking()
            .Where(b => b.UserId == request.UserId);

        if (request.Status.HasValue)
            query = query.Where(b => b.Status == request.Status.Value);

        var bookings = await query.ToListAsync(cancellationToken);

        return bookings.Map();
    }
}

using BookingPayments.API.Common.Exceptions;
using BookingPayments.API.Features.Rooms.Models;
using BookingPayments.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingPayments.API.Features.Rooms.Queries;

public sealed class GetRoomDetailQuery : IRequest<RoomResponse>
{
    public GetRoomDetailQuery(int roomId)
    {
        RoomId = roomId;
    }

    public int RoomId { get; set; }
}

public sealed class GetRoomDetailQueryHandler : IRequestHandler<GetRoomDetailQuery, RoomResponse>
{
    private readonly DataContext _context;

    public GetRoomDetailQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<RoomResponse> Handle(GetRoomDetailQuery request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == request.RoomId, cancellationToken)
            ?? throw new NotFoundException("Room not found");

        return room.Map();
    }
}
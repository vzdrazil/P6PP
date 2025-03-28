using BookingPayments.API.Features.Rooms.Models;
using BookingPayments.API.Infrastructure;
using MediatR;

namespace BookingPayments.API.Features.Rooms.Commands;

public sealed class CreateRoomCommand : IRequest<RoomResponse>
{
    public CreateRoomCommand(RoomRequest room)
    {
        Room = room;
    }

    public RoomRequest Room { get; set; }
}

public sealed class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomResponse>
{
    private readonly DataContext _context;

    public CreateRoomCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<RoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = request.Room.Map();

        _context.Rooms.Add(room);
        await _context.SaveChangesAsync(cancellationToken);

        return room.Map();
    }
}

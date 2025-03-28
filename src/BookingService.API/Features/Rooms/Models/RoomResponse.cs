using BookingPayments.API.Domain.Enums;

namespace BookingPayments.API.Features.Rooms.Models;

public record RoomResponse(
    int Id,
    string Name,
    int Capacity,
    RoomStatus Status
);

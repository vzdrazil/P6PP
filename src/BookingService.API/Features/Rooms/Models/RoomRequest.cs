using BookingPayments.API.Domain.Enums;

namespace BookingPayments.API.Features.Rooms.Models;

public record RoomRequest(
    string Name,
    int Capacity,
    RoomStatus Status
);

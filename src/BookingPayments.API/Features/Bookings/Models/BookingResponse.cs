using BookingPayments.API.Domain.Enums;

namespace BookingPayments.API.Features.Bookings.Models;

public record BookingResponse(
    int Id,
    int ServiceId,
    BookingStatus Status);

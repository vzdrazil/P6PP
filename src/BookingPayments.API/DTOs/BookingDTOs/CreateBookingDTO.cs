using BookingPayments.API.Entities.Enums;

namespace BookingPayments.API.DTOs.BookingDTOs;

public record CreateBookingDTO(
    int UserId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int ServiceId);
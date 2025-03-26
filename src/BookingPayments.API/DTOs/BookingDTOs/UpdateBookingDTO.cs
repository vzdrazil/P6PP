using BookingPayments.API.Entities.Enums;

namespace BookingPayments.API.DTOs.BookingDTOs;

public record UpdateBookingDTO(
    int Id,
    DateTime CheckInDate,
    DateTime CheckOutDate);

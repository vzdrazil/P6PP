using BookingPayments.API.Entities.Enums;

namespace BookingPayments.API.DTOs.BookingDTOs;

public record ResponseBookingDTO(
    int Id,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int Price,
    int ServiceId,
    string Status);

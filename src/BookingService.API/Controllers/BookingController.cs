using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.DTOs.BookingDTOs;
using BookingPayments.API.Entities;
using BookingPayments.API.Entities.Enums;
using Microsoft.AspNetCore.Mvc;


namespace BookingPayments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingAppService _bookingService;
        public BookingController(IBookingAppService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetBookings(int userId)
        {
            // Check if user exists here or in the service
            var bookings = await _bookingService.GetBookingsAsync(userId);
            return Ok(bookings.Select(b => new ResponseBookingDTO
            (
                b.Id,
                b.ServiceId,
                b.Status.ToString(),
                b.UserId
            )));
        }


        [HttpGet("{bookingId:int}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            var booking = await _bookingService.GetBookingAsync(bookingId);
            if (booking == null) return NotFound();
            var response = new ResponseBookingDTO
            (
                booking.Id,
                booking.ServiceId,
                booking.Status.ToString(),
                booking.UserId
            );
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO bookingDTO)
        {
            // Check if the user is already registered on that service.
            var booking = new Booking
            {
                BookingDate = DateTime.Now,
                Status = BookingStatus.Pending,
                ServiceId = bookingDTO.ServiceId,
                UserId = bookingDTO.UserId
            };

            var b = await _bookingService.CreateBookingAsync(booking);
            var response = new ResponseBookingDTO
            (
                b.Id,
                b.ServiceId,
                b.Status.ToString(),
                b.UserId
            );
            return CreatedAtAction(nameof(GetBooking), new { bookingId = b.Id }, response);
        }

        // No use for update right now.
        //[HttpPut]
        //public async Task<IActionResult> UpdateBooking(UpdateBookingDTO bookingDTO)
        //{
        //    var booking = new Booking
        //    {
        //        Id = bookingDTO.Id,
        //        Status = BookingStatus.Pending,
        //    };
        //    var updatedBooking = await _bookingService.UpdateBookingAsync(booking);
        //    if (updatedBooking == null) return NotFound();
        //    var response = new ResponseBookingDTO
        //    (
        //        updatedBooking.Id,
        //        updatedBooking.ServiceId,
        //        updatedBooking.Status.ToString()
        //    );
        //    return Ok(response);
        //}

        // Deletion might not be necessary. Update the booking status to 'Cancelled'
        [HttpDelete("{bookingId:int}")]
        public async Task<IActionResult> DeleteBookingAsync(int bookingId)
        {
            return await _bookingService.DeleteBookingAsync(bookingId) ? Ok() : NotFound();
        }
    }
}

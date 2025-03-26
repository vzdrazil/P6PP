using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.DTOS;
using BookingPayments.API.Entities;
using BookingPayments.API.Entities.Enums;
using Microsoft.AspNetCore.Mvc;


// TODO DTOS
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
            var bookings = await _bookingService.GetBookingsAsync(userId);
            return Ok(bookings);
        }


        [HttpGet("{bookingId:int}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            var booking = await _bookingService.GetBookingAsync(bookingId);
            return booking is not null ? Ok(booking) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO bookingDTO)
        {
            var booking = new Booking
            {
                BookingDate = DateTime.Now,
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(5),
                Price = 150,
                Status = BookingStatus.Confirmed,
                ServiceId = 1,
            };

            var b = await _bookingService.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetBooking), new { bookingId = b.Id }, booking);
        }

        [HttpPut("{bookingId:int}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, CreateBookingDTO bookingDTO) // temporary DTO
        {
            var booking = new Booking
            {
                BookingDate = DateTime.Now,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate,
                Price = 150,
                Status = BookingStatus.Cancelled,
                ServiceId = 1,
            };
            var updatedBooking = await _bookingService.UpdateBookingAsync(booking);
            return Ok(updatedBooking);
        }

        [HttpDelete("{bookingId:int}")]
        public async Task<IActionResult> DeleteBookingAsync(int bookingId)
        {
            return await _bookingService.DeleteBookingAsync(bookingId) ? Ok() : NotFound();
        }

        // testing
        [HttpGet("user/{userId:int}/upcoming-bookings")]
        [HttpGet("user/{userId:int}/past-bookings")]
        [HttpGet("user/{userId:int}/cancelled-bookings")]
        public async Task<IActionResult> FilterBookings(int userId)
        {
            var path = HttpContext.Request.Path.Value;
            var bookings = await _bookingService.GetBookingsAsync(userId, path!.Split("/")[^1]);
            return Ok(bookings);
        }



    }
}

using BookingPayments.API.Application.Abstraction;
using BookingPayments.API.DTOs.BookingDTOs;
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
            return Ok(bookings.Select(b => new ResponseBookingDTO
            (
                b.Id,
                b.CheckInDate,
                b.CheckOutDate,
                b.Price,
                b.ServiceId,
                b.Status.ToString()
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
                booking.CheckInDate,
                booking.CheckOutDate,
                booking.Price,
                booking.ServiceId,
                booking.Status.ToString()
            );
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO bookingDTO)
        {
            var booking = new Booking
            {
                BookingDate = DateTime.Now,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate,
                Price = 150, // Get price from service table
                Status = BookingStatus.Pending,
                ServiceId = 1,
            };

            var b = await _bookingService.CreateBookingAsync(booking);
            var response = new ResponseBookingDTO
            (
                b.Id,
                b.CheckInDate,
                b.CheckOutDate,
                b.Price,
                b.ServiceId,
                b.Status.ToString()
            );
            return CreatedAtAction(nameof(GetBooking), new { bookingId = b.Id }, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDTO bookingDTO)
        {
            var booking = new Booking
            {
                Id = bookingDTO.Id,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate,
                Status = BookingStatus.Pending,
            };
            var updatedBooking = await _bookingService.UpdateBookingAsync(booking);
            if (updatedBooking == null) return NotFound();
            var response = new ResponseBookingDTO
            (
                updatedBooking.Id,
                updatedBooking.CheckInDate,
                updatedBooking.CheckOutDate,
                updatedBooking.Price,
                updatedBooking.ServiceId,
                updatedBooking.Status.ToString()
            );
            return Ok(response);
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
            return Ok(bookings.Select(b => 
                        new ResponseBookingDTO (
                            b.Id,
                            b.CheckInDate,
                            b.CheckOutDate,
                            b.Price,
                            b.ServiceId,
                            b.Status.ToString()
                        )));
        }



    }
}

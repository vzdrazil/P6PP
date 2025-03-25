using BookingPayments.API.Data.Repositories.Abtractions;
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
        private readonly IBookingRepository _bookingRepository;
        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetBookings(int userId)
        {
            var bookings = await _bookingRepository.GetBookingsAsync(userId);
            return Ok(bookings);
        }


        [HttpGet("{bookingId:int}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingAsync(bookingId);
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

            var b = await _bookingRepository.CreateBookingAsync(booking);
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
            var updatedBooking = await _bookingRepository.UpdateBookingAsync(booking);
            return Ok(updatedBooking);
        }

        [HttpDelete("{bookingId:int}")]
        public async Task<IActionResult> DeleteBookingAsync(int bookingId)
        {
            return await _bookingRepository.DeleteBookingAsync(bookingId) ? Ok() : NotFound();
        }


        [HttpGet("user/{userId:int}/upcoming-bookings")]
        [HttpGet("user/{userId:int}/past-bookings")]
        [HttpGet("user/{userId:int}/cancelled-bookings")]
        public async Task<IActionResult> FilterBookings(int userId)
        {
            var path = HttpContext.Request.Path.Value;
            var bookings = await _bookingRepository.GetBookingsAsync(userId, path!.Split("/")[^1]);
            return Ok(bookings);
        }



    }
}

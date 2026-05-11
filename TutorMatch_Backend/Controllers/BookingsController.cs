using Microsoft.AspNetCore.Mvc;
using TutorMatch_Backend.DTOs;
using TutorMatch_Backend.Models;
using TutorMatch_Backend.Services;
using TutorMatch_Backend.Validators;

namespace TutorMatch_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController(BookingService service) : ControllerBase
    {
      
        [HttpGet]
        public IActionResult GetBookings()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var booking = service.GetById(id);

            if (booking == null)
                return NotFound("Reserva no encontrada");

            return Ok(booking);
        }

        [HttpPost]
        public IActionResult CreateBooking(BookingDto dto)
        {
            if (!BookingValidator.Validate(dto))
                return BadRequest("Datos inválidos");

            var booking = new Booking
            {
                StudentName = dto.StudentName,
                Subject = dto.Subject,
                Date = dto.Date,
                TutorId = dto.TutorId
            };

            service.Add(booking);

            return Ok(booking);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            bool deleted = service.Delete(id);

            if (!deleted)
                return NotFound("Reserva no encontrada");

            return Ok("Reserva eliminada");
        }
    }
}
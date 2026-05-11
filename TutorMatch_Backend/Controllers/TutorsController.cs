using Microsoft.AspNetCore.Mvc;
using TutorMatch_Backend.DTOs;
using TutorMatch_Backend.Models;
using TutorMatch_Backend.Services;
using TutorMatch_Backend.Validators;

namespace TutorMatch_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorsController(TutorService service) : ControllerBase
    {
       
        [HttpGet]
        public IActionResult GetTutors()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetTutor(int id)
        {
            var tutor = service.GetById(id);

            if (tutor == null)
                return NotFound("Tutor no encontrado");

            return Ok(tutor);
        }

        [HttpPost]
        public IActionResult CreateTutor(TutorDto dto)
        {
            if (!TutorValidator.Validate(dto))
                return BadRequest("Datos inválidos");

            var tutor = new Tutor
            {
                Name = dto.Name,
                Subject = dto.Subject,
                Email = dto.Email
            };

            service.Add(tutor);

            return Ok(tutor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTutor(int id)
        {
            bool deleted = service.Delete(id);

            if (!deleted)
                return NotFound("Tutor no encontrado");

            return Ok("Tutor eliminado");
        }
    }
}
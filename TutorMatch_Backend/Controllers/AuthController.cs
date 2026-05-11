using Microsoft.AspNetCore.Mvc;
using TutorMatch_Backend.DTOs;
using TutorMatch_Backend.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await authService.RegisterAsync(dto);
        if (result == null) return BadRequest("Error al registrar");
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await authService.LoginAsync(dto);
        if (result == null) return Unauthorized("Credenciales inválidas");
        return Ok(result);
    }
}

using TutorMatch_Backend.DTOs;

namespace TutorMatch_Backend.Services.Interfaces

{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}

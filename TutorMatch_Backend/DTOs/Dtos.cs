namespace TutorMatch_Backend.DTOs;

public record RegisterDto(
    string FullName,
    string Email,
    string Password,
    bool IsTutor,
    bool IsStudent);

public record LoginDto(string Email, string Password);

public record AuthResponseDto(
    string Token,
    string UserId,
    string FullName,
    bool IsTutor,
    bool IsStudent);
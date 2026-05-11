using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TutorMatch_Backend.DTOs;
using TutorMatch_Backend.Models;
using TutorMatch_Backend.Services.Interfaces;
namespace TutorMatch_Backend.Services;

public class AuthService(
    UserManager<AppUser> userManager,
    IConfiguration config) : IAuthService
{
    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        if (!dto.IsTutor && !dto.IsStudent) return null;

        var user = new AppUser
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.Email,
            IsTutor = dto.IsTutor,
            IsStudent = dto.IsStudent,
            VisibilityScore = 1.0
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        if (dto.IsTutor) await userManager.AddToRoleAsync(user, "Tutor");
        if (dto.IsStudent) await userManager.AddToRoleAsync(user, "Student");

        return new AuthResponseDto(GenerateToken(user), user.Id, user.FullName, user.IsTutor, user.IsStudent);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
            return null;

        return new AuthResponseDto(GenerateToken(user), user.Id, user.FullName, user.IsTutor, user.IsStudent);
    }

    private string GenerateToken(AppUser user)
    {
        var roles = userManager.GetRolesAsync(user).Result;
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new("FullName", user.FullName)
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

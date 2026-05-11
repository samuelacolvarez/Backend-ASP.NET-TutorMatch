using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace TutorMatch_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController (IAuthenticationService authService) : ControllerBase
{
   
   
}

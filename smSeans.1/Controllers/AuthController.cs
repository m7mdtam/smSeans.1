using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var token = await _authService.AuthenticateAsync(loginDto.email, loginDto.password);
        if (token == null)
        {
            return Unauthorized("Invalid credentials");
        }

        return Ok(new { Token = token });
    }
}

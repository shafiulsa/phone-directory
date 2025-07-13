using Microsoft.AspNetCore.Mvc;
using PhoneDirectory.Api.Models;
using PhoneDirectory.Api.Services;

namespace PhoneDirectory.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await _authService.LoginAsync(loginDto);
        if (token == null) return Unauthorized();
        return Ok(new { Token = token });
    }
}
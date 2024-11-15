using LocalizationManager.BLL.Authentication;
using LocalizationManager.BLL.Model;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(
    IAuthService _authenticationService,
    ILogger<AuthenticationController> _logger) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto model)
    {
        await _authenticationService.RegisterAsync(model);

        return Ok();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginDto model)
    {
        string token = await _authenticationService.LoginAsync(model);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpPost("Logout")]
    public async Task<ActionResult> Logout()
    {
        await _authenticationService.LogoutAsync();
        return Ok();
    }
}

using LocalizationManager.BLL.Authentication;
using LocalizationManager.Transfer.AuthDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthenticationController(IAuthService _authenticationService) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto model)
    {
        await _authenticationService.RegisterAsync(model);

        return Ok();
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto model)
    {
        string token = await _authenticationService.LoginAsync(model);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpPost("Logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        await _authenticationService.LogoutAsync();
        return Ok();
    }
}

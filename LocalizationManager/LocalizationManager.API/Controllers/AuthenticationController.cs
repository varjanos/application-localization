using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(
    IAuthenticationService _authenticationService,
    ILogger<AuthenticationController> _logger) : ControllerBase
{

}

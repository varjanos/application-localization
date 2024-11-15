using LocalizationManager.BLL.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalizationController(
    ILocalizationService _localizationService,
    ILogger<AuthenticationController> _logger) : ControllerBase
{
}

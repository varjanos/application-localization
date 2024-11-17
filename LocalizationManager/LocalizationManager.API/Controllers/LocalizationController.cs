using LocalizationManager.BLL.Localization;
using LocalizationManager.Transfer.LocalizationDtos;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalizationController(
    ILocalizationService _localizationService,
    ILogger<LocalizationController> _logger) : ControllerBase
{
    [HttpGet]
    public async Task<List<LocalizationValueDto>> GetLocalizationValues(string clientId)
        => await _localizationService.GetLocalizationValuesAsync(clientId);

    [HttpPost]
    public async Task AddOrUpdateLocalizationValue([FromBody] LocalizationValueDto request)
        => await _localizationService.AddOrUpdateLocalizationValueAsync(request);
}

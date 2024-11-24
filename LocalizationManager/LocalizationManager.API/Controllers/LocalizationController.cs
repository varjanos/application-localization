using LocalizationManager.BLL.Localization;
using LocalizationManager.Transfer.LocalizationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LocalizationController(
    ILocalizationService _localizationService) : ControllerBase
{
    [HttpGet]
    public async Task<List<LocalizationValueDto>> GetLocalizationValues(string appId)
        => await _localizationService.GetLocalizationValuesAsync(appId);

    [HttpPost]
    public async Task AddLocalizationValue([FromBody] LocalizationValueDto request)
        => await _localizationService.AddLocalizationValueAsync(request);

    [HttpPut]
    public async Task UpdateLocalizationValue([FromBody] LocalizationValueDto request)
        => await _localizationService.UpdateLocalizationValueAsync(request);

    [HttpDelete]
    public async Task DeleteLocalizationValue([FromBody] LocalizationValueDto request)
        => await _localizationService.DeleteLocalizationValueAsync(request);
}
